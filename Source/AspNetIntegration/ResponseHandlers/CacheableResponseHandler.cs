﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

using Junior.Common;
using Junior.Route.Http;
using Junior.Route.Http.RequestHeaders;
using Junior.Route.Routing.Caching;
using Junior.Route.Routing.Responses;

namespace Junior.Route.AspNetIntegration.ResponseHandlers
{
	public class CacheableResponseHandler : IResponseHandler
	{
		private static readonly StatusAndSubStatusCode[] _cacheableStatusCodes = new[]
			{
				new StatusAndSubStatusCode(HttpStatusCode.OK),
				new StatusAndSubStatusCode(HttpStatusCode.NonAuthoritativeInformation),
				new StatusAndSubStatusCode(HttpStatusCode.MultipleChoices),
				new StatusAndSubStatusCode(HttpStatusCode.MovedPermanently),
				new StatusAndSubStatusCode(HttpStatusCode.Gone)
			};
		private readonly ISystemClock _systemClock;

		public CacheableResponseHandler(ISystemClock systemClock)
		{
			systemClock.ThrowIfNull("systemClock");

			_systemClock = systemClock;
		}

		public async Task<ResponseHandlerResult> HandleResponseAsync(HttpContextBase context, IResponse suggestedResponse, ICache cache, string cacheKey)
		{
			context.ThrowIfNull("context");
			suggestedResponse.ThrowIfNull("suggestedResponse");

			if (!suggestedResponse.CachePolicy.HasPolicy || cache == null || cacheKey == null)
			{
				return ResponseHandlerResult.ResponseNotHandled();
			}

			CacheItem cacheItem = await cache.GetAsync(cacheKey);
			string responseETag = suggestedResponse.CachePolicy.ETag;

			#region If-Match precondition header

			IfMatchHeader[] ifMatchHeaders = IfMatchHeader.ParseMany(context.Request.Headers["If-Match"]).ToArray();

			// Only consider If-Match headers if response status code is 2xx or 412
			if (ifMatchHeaders.Any() && ((suggestedResponse.StatusCode.StatusCode >= 200 && suggestedResponse.StatusCode.StatusCode <= 299) || suggestedResponse.StatusCode.StatusCode == 412))
			{
				// Return 412 if no If-Match header matches the response ETag
				// Return 412 if an "If-Match: *" header is present and the response has no ETag
				if (ifMatchHeaders.All(arg => arg.EntityTag.Value != responseETag) ||
				    (responseETag == null && ifMatchHeaders.Any(arg => arg.EntityTag.Value == "*")))
				{
					return await WriteResponseAsync(context.Response, new Response().PreconditionFailed());
				}
			}

			#endregion

			#region If-None-Match precondition header

			IfNoneMatchHeader[] ifNoneMatchHeaders = IfNoneMatchHeader.ParseMany(context.Request.Headers["If-None-Match"]).ToArray();

			if (ifNoneMatchHeaders.Any())
			{
				// Return 304 if an If-None-Match header matches the response ETag and the request method was GET or HEAD
				// Return 304 if an "If-None-Match: *" header is present, the response has an ETag and the request method was GET or HEAD
				// Return 412 if an "If-None-Match: *" header is present, the response has an ETag and the request method was not GET or HEAD
				if (ifNoneMatchHeaders.Any(arg => arg.EntityTag.Value == responseETag) ||
				    (ifNoneMatchHeaders.Any(arg => arg.EntityTag.Value == "*") && responseETag != null))
				{
					if (String.Equals(context.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase) || String.Equals(context.Request.HttpMethod, "HEAD", StringComparison.OrdinalIgnoreCase))
					{
						if (cacheItem != null)
						{
							cacheItem.Response.CachePolicy.Apply(context.Response.Cache);
						}
						else
						{
							suggestedResponse.CachePolicy.Apply(context.Response.Cache);
						}

						return await WriteResponseAsync(context.Response, new Response().NotModified());
					}

					return await WriteResponseAsync(context.Response, new Response().PreconditionFailed());
				}
			}

			#endregion

			#region If-Modified-Since precondition header

			IfModifiedSinceHeader ifModifiedSinceHeader = IfModifiedSinceHeader.Parse(context.Request.Headers["If-Modified-Since"]);
			bool validIfModifiedSinceHttpDate = ifModifiedSinceHeader != null && ifModifiedSinceHeader.HttpDate <= _systemClock.UtcDateTime;

			// Only consider an If-Modified-Since header if response status code is 200 and the HTTP-date is valid
			if (suggestedResponse.StatusCode.ParsedStatusCode == HttpStatusCode.OK && validIfModifiedSinceHttpDate)
			{
				// Return 304 if the response was cached before the HTTP-date
				if (cacheItem != null && cacheItem.CachedUtcTimestamp < ifModifiedSinceHeader.HttpDate)
				{
					return await WriteResponseAsync(context.Response, new Response().NotModified());
				}
			}

			#endregion

			#region If-Unmodified-Since precondition header

			IfUnmodifiedSinceHeader ifUnmodifiedSinceHeader = IfUnmodifiedSinceHeader.Parse(context.Request.Headers["If-Unmodified-Since"]);
			bool validIfUnmodifiedSinceHttpDate = ifUnmodifiedSinceHeader != null && ifUnmodifiedSinceHeader.HttpDate <= _systemClock.UtcDateTime;

			// Only consider an If-Unmodified-Since header if response status code is 2xx or 412 and the HTTP-date is valid
			if (((suggestedResponse.StatusCode.StatusCode >= 200 && suggestedResponse.StatusCode.StatusCode <= 299) || suggestedResponse.StatusCode.StatusCode == 412) && validIfUnmodifiedSinceHttpDate)
			{
				// Return 412 if the previous response was removed from the cache or was cached again at a later time
				if (cacheItem == null || cacheItem.CachedUtcTimestamp >= ifUnmodifiedSinceHeader.HttpDate)
				{
					return await WriteResponseAsync(context.Response, new Response().PreconditionFailed());
				}
			}

			#endregion

			#region No server caching

			// Do not cache the response when the response sends a non-cacheable status code, or when an Authorization header is present
			if (!_cacheableStatusCodes.Contains(suggestedResponse.StatusCode) || context.Request.Headers["Authorization"] != null)
			{
				return await WriteResponseAsync(context.Response, suggestedResponse);
			}

			CacheControlHeader cacheControlHeader = CacheControlHeader.Parse(context.Request.Headers["Cache-Control"]);

			// Do not cache the response if a "Cache-Control: no-cache" or "Cache-Control: no-store" header is present
			if (cacheControlHeader != null && (cacheControlHeader.NoCache || cacheControlHeader.NoStore))
			{
				return await WriteResponseAsync(context.Response, suggestedResponse);
			}

			IEnumerable<PragmaHeader> pragmaHeader = PragmaHeader.ParseMany(context.Request.Headers["Pragma"]);

			// Do not cache the response if a "Pragma: no-cache" header is present
			if (pragmaHeader.Any(arg => String.Equals(arg.Name, "no-cache", StringComparison.OrdinalIgnoreCase)))
			{
				return await WriteResponseAsync(context.Response, suggestedResponse);
			}

			#endregion

			// Return 504 if the response has not been cached but the client is requesting to receive only a cached response
			if (cacheItem == null && cacheControlHeader != null && cacheControlHeader.OnlyIfCached)
			{
				return await WriteResponseAsync(context.Response, new Response().GatewayTimeout());
			}

			if (cacheItem != null)
			{
				// Write the cached response if no Cache-Control header is present
				// Write the cached response if a "Cache-Control: max-age" header is validated
				// Write the cached response if a "Cache-Control: max-stale" header is validated
				// Write the cached response if a "Cache-Control: min-fresh" header is validated
				if (cacheControlHeader == null ||
				    _systemClock.UtcDateTime - cacheItem.CachedUtcTimestamp <= cacheControlHeader.MaxAge ||
				    cacheControlHeader.OnlyIfCached ||
				    cacheItem.ExpiresUtcTimestamp == null ||
				    _systemClock.UtcDateTime - cacheItem.ExpiresUtcTimestamp.Value <= cacheControlHeader.MaxStale ||
				    cacheItem.ExpiresUtcTimestamp.Value - _systemClock.UtcDateTime < cacheControlHeader.MinFresh)
				{
					return await WriteResponseInCacheAsync(context.Response, cacheItem);
				}
			}

			bool cacheOnServer = suggestedResponse.CachePolicy.AllowsServerCaching;
			var cacheResponse = new CacheResponse(suggestedResponse);

			if (cacheOnServer)
			{
				DateTime expirationUtcTimestamp = suggestedResponse.CachePolicy.ServerCacheExpirationUtcTimestamp != null
					                                  ? suggestedResponse.CachePolicy.ServerCacheExpirationUtcTimestamp.Value
					                                  : _systemClock.UtcDateTime + suggestedResponse.CachePolicy.ServerCacheMaxAge.Value;

				await cache.AddAsync(cacheKey, cacheResponse, expirationUtcTimestamp);
			}

			return await WriteResponseAsync(context.Response, cacheResponse);
		}

		private static Task<ResponseHandlerResult> WriteResponseAsync(HttpResponseBase httpResponse, IResponse response)
		{
			return WriteResponseAsync(httpResponse, new CacheResponse(response));
		}

		private static async Task<ResponseHandlerResult> WriteResponseAsync(HttpResponseBase httpResponse, CacheResponse cacheResponse)
		{
			await cacheResponse.WriteResponseAsync(httpResponse);

			return ResponseHandlerResult.ResponseWritten();
		}

		private static async Task<ResponseHandlerResult> WriteResponseInCacheAsync(HttpResponseBase httpResponse, CacheItem cacheItem)
		{
			await cacheItem.Response.WriteResponseAsync(httpResponse);

			httpResponse.Headers.Set("Last-Modified", cacheItem.CachedUtcTimestamp.ToHttpDate());

			return ResponseHandlerResult.ResponseWritten();
		}
	}
}
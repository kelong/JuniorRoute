---
layout: documentation_1_0
title: Response Handlers
root: ../../
documentationroot: ../../documentation/1.0/
---
ASP.net Integration - {{ page.title }}
=
Response handlers optionally write a [response]({{ page.documentationroot }}responses.html) to the ASP.net pipeline. Response handlers may also perform server [caching]({{ page.documentationroot }}caching.html). Response handlers are called in succession by [```AspNetHttpHandler```]({{ page.documentationroot }}aspnethttphandler.html) after a [response generator]({{ page.documentationroot }}response_generators.html) generates a response. Response handlers may choose to suggest a new response to the next response handler.

In the below example, a response handler is defined that writes the response's type as text to the ASP.net pipeline.

{% highlight csharp %}
public class ResponseTypeHandler : IResponseHandler
{
  public ResponseHandlerResult HandleResponse(HttpRequestBase httpRequest, HttpResponseBase httpResponse, IResponse suggestedResponse, ICache cache, string cacheKey)
  {
    httpResponse.ContentType = "text/plain";
    httpResponse.Write(suggestedResponse != null ? suggestedResponse.GetType().FullName : "");

    return ResponseHandlerResult.ResponseWritten();
  }
}
{% endhighlight %}

JuniorRoute provides four built-in implementations:
* ```CacheableResponseHandler```
* ```NonCacheableResponseHandler```
* ```DescriptiveHtmlStatusCodeHandler```
* ```DescriptiveTextStatusCodeHandler```

CacheableResponseHandler
-
This implementation attempts to closely follow the HTTP 1.1 specification for handling client and server caching of responses. While there is too much logic to fully document here, the following responses may be written in certain circumstances:
* The presence of an If-Match request header may write a 412 Precondition Failed
* The presence of an If-None-Match request header may write a 304 Not Modified, usually when the client is revalidating a cached response
* The presence of an If-None-Match request header may write a 412 Precondition Failed
* The presence of an If-Modified-Since request header may write a 304 Not Modified, usually when the client is revalidating a cached response
* The presence of an If-Unmodified-Since request header may write a 412 Precondition Failed

If the response does not have a server-cacheable status code, or if an Authorization request header is present, the response is written. In this case, any associated cache policy is ignored.

If the client is requesting only a cached response but the server cannot provide one, a 504 Gateway Timeout is written.

If none of the previous scenarios hold then a response is eligible for caching on the client and/or server. How that caching occurs is defined in detail by the HTTP 1.1 specification.

NonCacheableResponseHandler
-
This implementation simply writes the generated response to the ASP.net pipeline. Use caution when using this implementation because it ignores all client request headers (e.g., Accept).

DescriptiveHtmlStatusCodeHandler
-
If there are no Accept request headers present, or an Accept request header with the media type *text/html* is present, a simple text response containing the generated status codes and a description of the status code, if available, is written. IIS custom errors are also skipped, if possible.

DescriptiveTextStatusCodeHandler
-
If there are no Accept request headers present, or an Accept request header with the media type *text/plain* is present, a simple text response containing the generated status codes and a description of the status code, if available, is written. IIS custom errors are also skipped, if possible.

Extensibility
-
Developers may create their own response generators by implementing the ```IResponseHandler``` interface.
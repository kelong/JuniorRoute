---
layout: documentation_1_x
title: Caching
root: ../../
documentationversions: [1.0]
---
Core Concept - {{ page.title }}
=
Developers may optionally use JuniorRoute's built-in caching mechanism to cache [responses](responses.html). Caching is performed at the response-level rather than the [route](routes.html)-level for maximum flexibility. If a developer chooses, a single route may return many different responses, each with their own cache policy. JuniorRoute's ASP.net integration layer handles cache policies by following the HTTP 1.1 specification's requirements and recommendations. The underlying cache mechanism is ASP.net's ```HttpRuntime.Cache``` singleton.

By default, a ```Response``` instance has no cache policy attached, meaning the response is not cached on the server.

In the below example, a route is configured to return a response that defines a public client caching and server caching policy. The effect of this policy is to cache the response at the client until the current client time is ```DateTime.UtcNow``` plus five minutes. For clients that do not already have the response in their cache, the server cache is used with that same expiration timestamp. Although developers may configure client and server expiration timestamps or maximum ages independently, it is recommended to use the same expiration policy when using client and server caching.

{% highlight csharp %}
Response response = Response
  .OK()
  .Content(() => DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
DateTime expirationUtcTimestamp = DateTime.UtcNow.AddMinutes(5);

response.CachePolicy
  .PublicClientCaching(expirationUtcTimestamp)
  .ServerCaching(expirationUtcTimestamp);

new Route("route", Guid.NewGuid(), "route")
  .RespondWith(response);
{% endhighlight %}

In the below example, only the server is allowed to cache the response.

{% highlight csharp %}
Response response = Response
  .OK()
  .Content(() => DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
DateTime expirationUtcTimestamp = DateTime.UtcNow.AddMinutes(5);

response.CachePolicy
  .NoClientCaching()
  .ServerCaching(expirationUtcTimestamp);

new Route("route", Guid.NewGuid(), "route")
  .RespondWith(response);
{% endhighlight %}

In the below example, no caching of any kind is allowed.

{% highlight csharp %}
Response response = Response
  .OK()
  .Content(() => DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));

response.CachePolicy
  .NoClientCaching()
  .NoServerCaching();

new Route("route", Guid.NewGuid(), "route")
  .RespondWith(response);
{% endhighlight %}

In the below example, an eTag is associated with the response.

{% highlight csharp %}
Response response = Response
  .OK()
  .Content(() => DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));

response.CachePolicy
  .PublicClientCaching(TimeSpan.FromMinutes(10))
  .ETag("2b58a5bb-9f79-4930-ad56-039faca98fe0");

new Route("route", Guid.NewGuid(), "route")
  .RespondWith(response);
{% endhighlight %}

Extensibility
-
Developers may create their own caches by implementing the ```ICache``` interface.

Developers may create their own cache policy by implementing the ```ICachePolicy``` interface.
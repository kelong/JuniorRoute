---
layout: documentation_1_x
title: URL Resolvers
root: ../../
documentationversions: [1.0]
---
Core Concept - {{ page.title }}
=
URL resolvers have two purposes:
* Resolve a relative URL to an absolute URL
* Resolve a route to an absolute URL

The term *relative* means relative to the application's root URL. For example, if an IIS application using JuniorRoute lives at *http://localhost/MyApplication*, a relative URL is relative to that path. A relative URL of *about* resolves to an absolute URL of */MyApplication/about*.

JuniorRoute provides the built-in ```UrlResolver``` implementation.

It is recommended to use ```UrlResolver``` as the ```IUrlResolver``` implementation.

UrlResolver
-
```UrlResolver``` requires the following:
* a [route collection](route_collections.html)
* an ```IHttpRuntime``` implementation to determine the root URL of the application (e.g., /MyApplication if there is a MyApplication virtual directory in IIS)

Taking the application root URL from the ```IHttpRuntime``` implementation, ```UrlResolver``` then appends a relative URL to that path. The relative path can be either a plain string or it can be determined by [route]()routes.html) name or route ID. Absolute URLs resolved by ```UrlResolver``` do not have a trailing slash ('/').

In the below example, an absolute URL is resolved from a relative URL.

{% highlight csharp %}
var resolver = new UrlResolver(routeCollection, httpRuntime);

resolver.Absolute("relative");
{% endhighlight %}

In the below example, an absolute URL is resolved from a route name.

{% highlight csharp %}
var resolver = new UrlResolver(routeCollection, httpRuntime);

resolver.Route("Dashboard");
{% endhighlight %}

In the below example, an absolute URL is resolved from a route ID.

{% highlight csharp %}
var resolver = new UrlResolver(routeCollection, httpRuntime);

resolver.Route(Guid.Parse("f4987886-eb3f-4e0d-b5b4-724880d405ee"));
{% endhighlight %}

Extensibility
-
Developers may create their own URL resolvers by implementing the ```IUrlResolver``` interface.
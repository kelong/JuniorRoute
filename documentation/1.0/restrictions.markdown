---
layout: documentation_1_0
title: Restrictions
root: ../../
documentationroot: ../../documentation/1.0/
---
Core Concept - {{ page.title }}
=
Restrictions determine if a particular [route]({{ page.documentationroot }}routes.html) is allowed to handle a HTTP request message. JuniorRoute has many built-in implementations that handle most HTTP request message parameters such as request URL, referer URL, cookies and request headers.

Developers can use restrictions in different combinations to ensure that a HTTP request message is routed to the correct route. There is no limit to the number of restrictions that may be added to a route.

In the below example, a route is restricted to any relative path that contains the phrase *dog*, *cat* or *fish*. The [```CaseInsensitiveRegexComparer```]({{ page.documentationroot }}request_value_comparers.html) singleton treats the value as a regular expression pattern.

{% highlight csharp %}
new Route("Pet Info", Guid.NewGuid(), "pet_info")
  .RestrictByUrlRelativePath("dog|cat|fish", CaseInsensitiveRegexComparer.Instance, httpRuntime);
{% endhighlight %}

In the below example, a route is restricted to a relative path called *person*. It is also restricted to only when an *application/json* Accept header is present. Restriction overloads that do not take a comparer instance default to the case-insensitive plain comparer, if necessary.

{% highlight csharp %}
new Route("Person", Guid.NewGuid(), "person")
  .RestrictByUrlRelativePaths("person", httpRuntime)
  .RestrictByAcceptHeader(header => header.MediaTypeMatches("application/json"));
{% endhighlight %}

Extending the previous example, the route is further restricted to when the client requires a UTF-8 content response.

{% highlight csharp %}
new Route("Person", Guid.NewGuid(), "person")
  .RestrictByUrlRelativePaths("person", httpRuntime)
  .RestrictByAcceptHeader(header => header.MediaTypeMatches("application/json"))
  .RestrictByAcceptEncodingHeader(header => header.ContentCodingMatches("utf-8"));
{% endhighlight %}

In the below example, a route is restricted by using a custom restriction.

{% highlight csharp %}
new Route("Pet Info", Guid.NewGuid(), "pet_info")
  .AddRestrictions(new FirstCustomRestriction(), new SecondCustomRestriction())
{% endhighlight %}

JuniorRoute encapsulates all valid HTTP request headers, so any of them may be used as restrictions.

Extensibility
-
Developers may create their own restrictions by implementing the ```IRestriction``` interface.
---
layout: documentation
title: Responses
root: ../
documentationversions: [1.0]
---
Core Concept - {{ page.title }}
=
Responses encapsulate the information required to populate a HTTP response message. This information includes:
* Status and sub-status codes
* Content type
* Character set
* Content encoding
* Headers
* Header encoding
* Cookies
* Content
* Cache policy

JuniorRoute provides the built-in ```Response``` implementation.

Status and Sub-status Codes
-
These codes populate the Status-Line of the HTTP response message. JuniorRoute provides the built-in ```StatusAndSubStatusCode``` class that encapsulates this concept. Developers may specify either an integer code (even unofficial codes) or an ```HttpStatusCode``` enumeration value. The Response class requires this value. There is no default value for the primary status code and the sub-status code defaults to 0.

The below example demonstrates a [route](routes.html) returning a 404.1 status code in its response.

{% highlight csharp %}
new Route("route", Guid.NewGuid(), "route")
  .RespondWith(new Response(404, 1))
{% endhighlight %}

The below example demonstrates using the fluent interface to set a 404 status code in the route's response.

{% highlight csharp %}
new Route("route", Guid.NewGuid(), "route")
  .RespondWith(Response.NotFound());
{% endhighlight %}

Content type
-
This value populates the Content-Type header of the HTTP response message. Omitting the content type omits the Content-Type header from the response. There is no default value.

The below example demonstrates a route returning a 200.0 status code and a Content-Type header set to *application/json*.

{% highlight csharp %}
new Route("route", Guid.NewGuid(), "route")
  .RespondWith(Response.OK().ContentType("application/json"));
{% endhighlight %}

The below example demonstrates using the fluent interface to set a *text/plain* Content-Type header.

{% highlight csharp %}
new Route("route", Guid.NewGuid(), "route")
  .RespondWith(Response.OK().TextPlain());
{% endhighlight %}

Character set
-
This value populates the charset parameter of the Content-Type header in the HTTP response message. Omitting the character set omits the Content-Type header from the response. The default value is *utf-8*.

The below example demonstrates a route returning a 200.0 status code and a Content-Type header set to *application/json* with a charset parameter set to *utf-16*.

{% highlight csharp %}
new Route("route", Guid.NewGuid(), "route")
  .RespondWith(Response.OK().ContentType("application/json").Charset("utf-16"));
{% endhighlight %}

Content encoding
-
This value populates the Content-Encoding header of the HTTP response message. The ```Response``` class requires this value. The default value is *utf-8*.

The below example demonstrates a route returning a 200.0 status code and a Content-Encoding header set to *us-ascii*.

{% highlight csharp %}
new Route("route", Guid.NewGuid(), "route")
  .RespondWith(Response.OK().ContentEncoding(Encoding.ASCII));
{% endhighlight %}

Headers
-
Responses may contain additional headers that a developer wishes to provide. JuniorRoute provides the built-in ```Header``` class to represent response headers.

The below example demonstrates supplying a custom header in a response.

{% highlight csharp %}
new Route("route", Guid.NewGuid(), "route")
  .RespondWith(Response.OK().Header("X-Custom", "value"));
{% endhighlight %}

Header encoding
-
This value populates the Header-Encoding header of the HTTP response message. The ```Response``` class requires this value. The default value is *utf-8*.

The below example demonstrates a route returning a 200.0 status code and a Header-Encoding header set to *us-ascii*.

{% highlight csharp %}
new Route("route", Guid.NewGuid(), "route")
  .RespondWith(Response.OK().HeaderEncoding(Encoding.ASCII));
{% endhighlight %}

Cookies
-
Responses may contain cookies that a developer wishes to set. Supplying cookies populates the appropriate Set-Cookie response headers. JuniorRoute provides the built-in ```Cookie``` class to represent cookies.

The below example demonstrates a route returning a 200.0 status code and a cookie named *mycookie* with value *value*.

{% highlight csharp %}
new Route("route", Guid.NewGuid(), "route")
  .RespondWith(Response.OK().Cookie("mycookie", "value"));
{% endhighlight %}

Content
-
A method to retrieve the response body content is provided. However, this method is not called until the associated route is chosen as the matching route. Developers may provide binary or string content directly to the ```Response``` instance, or they may supply a delegate to be called once the content is needed. The delegate is useful for time-sensitive content or content that requires dependencies not available at the time the route is configured.

The below example demonstrates a route returning a 200.0 status code and a delegate-driven response.

{% highlight csharp %}
new Route("route", Guid.NewGuid(), "route")
  .RespondWith(Response.OK().Content(() => DateTime.Now.ToString(CultureInfo.InvariantCulture)));
{% endhighlight %}

Fluent Interface
-
The ```Response``` class provides fluent-interface methods to make configuration easier. In most cases, use of the fluent interface is additive, meaning it does not overwrite previous calls. Use of fluent-interface methods is recommended.

Extensibility
-
Developers may create their own responses by implementing the ```IResponse``` interface.
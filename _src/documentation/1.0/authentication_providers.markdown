---
layout: documentation_1_0
title: Authentication Providers
root: ../../
documentationroot: ../../documentation/1.0/
---
Core Concept - {{ page.title }}
=
Authentication providers serve two purposes:
* Determine if a [route]({{ page.documentationroot }}routes.html) authenticates given a HTTP request message
* Retrieve an authentication failure response if it doesn't

JuniorRoute provides the built-in ```FormsAuthenticationProvider``` implementation.

FormsAuthenticationProvider
-
This provider greatly simplifies the process of integrating forms authentication with JuniorRouteuses by encapsulating ASP.net's ```FormsAuthentication``` class to manage cookies and tickets. Developers may choose to append the URL to the redirect when authorization fails. Developers may also change the name of the forms cookie itself.

In the below example, a route authenticates an ASP.net forms authentication ticket sent with HTTP request messages. If the request contains a valid ticket then the route's regular response may be sent to the client. However, if the ticket is missing, invalid or expired, the client is redirected to a log-in page.

{% highlight csharp %}
new Route("Profile", Guid.NewGuid(), "profile")
  .FormsAuthenticationProviderWithRouteRedirectOnFailedAuthentication(urlResolver, "Log In");
{% endhighlight %}

Extensibility
-
Developers may create their own authentication providers by implementing the ```IAuthenticationProvider``` interface.
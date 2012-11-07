---
layout: documentation_1_0
title: Authentication Strategies
root: ../../
documentationroot: ../../documentation/1.0/
---
Auto-routing - {{ page.title }}
=
Authentication strategies determine if a particular endpoint method must be authenticated. If it must, its [route]({{ page.documentationroot }}routes.html)'s associated [authentication provider]({{ page.documentationroot }}authentication_providers.html), if any, is called. [```AutoRouteCollection```]({{ page.documentationroot }}autoroutecollection.html) does not require an authentication provider.

JuniorRoute provides the built-in ```AuthenticateAttributeStrategy``` implementation.

AuthenticateAttributeStrategy
-
This strategy checks an endpoint method for the presence of an ```[Authenticate]``` attribute. If any ```[Authenticate]``` attributes are found, that endpoint method must be authenticated before it can be invoked.

In the below example, an ```AuthenticateAttributeStrategy``` is instantiated.

{% highlight csharp %}
new AuthenticateAttributeStrategy();
{% endhighlight %}

Extensibility
-
Developers may create their own authentication strategies by implementing the ```IAuthenticationStrategy``` interface.
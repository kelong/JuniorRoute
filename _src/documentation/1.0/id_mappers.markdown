---
layout: documentation_1_0
title: ID Mappers
root: ../../
documentationroot: ../../documentation/1.0/
---
Auto-routing - {{ page.title }}
=
ID mappers map [route]({{ page.documentationroot }}routes.html) IDs during [```AutoRouteCollection```]({{ page.documentationroot }}autoroutecollection.html) route generation. ```AutoRouteCollection``` requires at least one ID mapper to be provided.

JuniorRoute provides two built-in implementations:
* ```RandomIdMapper```
* ```IdAttributeMapper```

RandomIdMapper
-
This mapper maps a route ID to a random GUID generated using an ```IGuidFactory``` implementation.

In the below example, a ```RandomIdMapper``` instance is configured with the built-in ```GuidFactory``` implementation.

{% highlight csharp %}
new RandomIdMapper(new GuidFactory());
{% endhighlight %}

IdAttributeMapper
-
This mapper maps a route ID to the value supplied in an ```[Id]``` attribute decorating an endpoint method.

In the below example, a route is assigned the ID *726c4040-6161-4c2e-9abf-f8f5685e3664*.

{% highlight csharp %}
public class Documentation
{
  [Id("726c4040-6161-4c2e-9abf-f8f5685e3664")]
  public HtmlResponse Get()
  {
    ...
  }
}
{% endhighlight %}

Extensibility
-
Developers may create their own ID mappers by implementing the ```IIdMapper``` interface.
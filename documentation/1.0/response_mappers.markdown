---
layout: documentation_1_0
title: Response Mappers
root: ../../
documentationroot: ../../documentation/1.0/
---
Auto-routing - {{ page.title }}
=
Response mappers determine map a [route]({{ page.documentationroot }}routes.html)'s [response]({{ page.documentationroot }}responses.html) for an endpoint method. Response mappers use the endpoint [container]({{ page.documentationroot }}containers.html) provided to an [```AutoRouteCollection```]({{ page.documentationroot }}autoroutecollection.html) instance to retrieve instances of endpoint classes. The container must be provided as a delegate because at the time response mappers are configured, the container reference may not yet be initialized.

Developers are encouraged to read the HTTP 1.1 specification to better understand how to respond to HTTP request messages.

JuniorRoute provides two built-in implementations:
* ```NoContentMapper```
* ```ResponseMethodReturnTypeMapper```

NoContentMapper
-
This mapper always maps a 204 No Content response.

In the below example, a ```NoContentMapper``` is instantiated:

{% highlight csharp %}
new NoContentMapper();
{% endhighlight %}

ResponseMethodReturnTypeMapper
-
This mapper returns the response of an invoked endpoint method. If the method returns ```void```, a 204 No Content response is mapped. If the method's return type does not implement ```IResponse```, an exception is thrown. The response is mapped as a delegate so that the endpoint type instantiation, invokation and return value processing only occurs when the route has been matched and processed.

```ResponseMethodReturnTypeMapper``` exposes several fluent-interface methods for easily configuring associated [parameter mappers]({{ page.documentationroot }}parameter_mappers.html). Parameter mappers should be provided, if necessary, so that endpoint method parameters may have their values mapped.

In the below example, a ```ResponseMethodReturnTypeMapper``` instance is configured with a JSON model mapper:

{% highlight csharp %}
new ResponseMethodReturnTypeMapper()
  .JsonModelMapper();
{% endhighlight %}

Extensibility
-
Developers may create their own response mappers by implementing the ```IResponseMapper``` interface.
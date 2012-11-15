---
layout: documentation
title: Method Filters
root: ../
documentationversions: [1.0]
---
Auto-routing - {{ page.title }}
=
Method filters control which endpoint methods are eligible for [auto-route](auto_routing.html) generation.

[```AutoRouteConfiguration```](autorouteconfiguration.html) does not require any method filters. In most cases, the only public instance methods in an endpoint class are methods designed to process a [route](routes.html); there is no need for method filters in this case. The method filter concept is provided to handle situations where there are non-route-handling public instance methods in an endpoint class.

There are two requirements for endpoint methods. Endpoint methods:
* must be public
* must be an instance method (i.e., non-static)

In the below example, a method filter is created that includes only methods whose names begin with *Route*.

{% highlight csharp %}
public class NameStartsWithRouteFilter : IMethodFilter
{
  public bool Matches(MethodInfo method)
  {
    return method.Name.StartsWith("Route");
  }
}
{% endhighlight %}

JuniorRoute provides a built-in implementation called ```DelegateFilter```.

DelegateFilter
-
This filter matches methods based on the specified delegate.

In the below example, a ```DelegateFilter``` is instantiated that matches methods whose names end with *Endpoint*.

{% highlight csharp %}
new DelegateFilter(methodInfo => methodInfo.Name.EndsWith("Endpoint"));
{% endhighlight %}

Extensibility
-
Developers may create their own method filters by implementing the ```IMethodFilter``` interface.
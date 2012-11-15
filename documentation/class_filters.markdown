---
layout: documentation
title: Class Filters
root: ../
documentationversions: [1.0]
---
Auto-routing - {{ page.title }}
=
Class filters control which classes are eligible for [auto-route](auto_routing.html) generation. Most Web projects contain classes that aren't intended to be endpoints (e.g., ```Global```). Class filters allows the developer to control which classes are endpoints and which aren't.

[```AutoRouteConfiguration```](autorouteconfiguration.html) does not require any class filters, but it is recommended to provide at least one so that routes are not accidentally generated for non-endpoints classes.

There are no base class, interface or attribute requirements for endpoints. There are two of general requirements, however. Endpoints:
* must not be abstract or static (i.e., they must be instantiatable)
* must be a reference type (i.e., a class)

In the below example, a class filter is created that includes only classes whose names end with *Endpoint*.

{% highlight csharp %}
public class NameEndsWithEndpointFilter : IClassFilter
{
  public bool Matches(Type type)
  {
    return type.Name.EndsWith("Endpoint");
  }
}
{% endhighlight %}

JuniorRoute provides eight built-in implementations:
* ```DelegateFilter```
* ```DerivesFilter```
* ```HasNamespaceAncestorFilter```
* ```ImplementsInterfaceFilter```
* ```InNamespaceFilter```
* ```NameEndsWithFilter```
* ```NameMatchesRegexPatternFilter```
* ```NameStartsWithFilter```

DelegateFilter
-
This filter matches types based on the specified delegate.

In the below example, a ```DelegateFilter``` is instantiated that matches types whose names end with *Endpoint*.

{% highlight csharp %}
new DelegateFilter(type => type.Name.EndsWith("Endpoint"));
{% endhighlight %}

DerivesFilter
-
This filter matches types that derive the specified base type.

In the below example, a ```DerivesFilter``` is instantiated that matches types whose base type is ```Endpoint```.

{% highlight csharp %}
new DerivesFilter<Endpoint>();
{% endhighlight %}

HasNamespaceAncestorFilter
-
This filter matches types that have a namespace ancestor matching the specified namespace. A namespace ancestor is any parent namespace of the type's namespace. For example, ```MyApplication.MyModule.Endpoints``` has the namespace ancestors ```MyApplication``` and ```MyApplication.MyModule```.

In the below example, a ```HasNamespaceAncestorFilter``` is instantiated that matches types with a namespace ancestor of ```WebApp```.

{% highlight csharp %}
new HasNamespaceAncestorFilter("WebApp");
{% endhighlight %}

ImplementsInterfaceFilter
-
This filter matches types that implement the specified interface.

In the below example, a ```ImplementsInterfaceFilter``` is instantiated that matches types implementing ```IEndpoint```.

{% highlight csharp %}
new ImplementsInterfaceFilter<IEndpoint>();
{% endhighlight %}

InNamespaceFilter
-
This filter matches types that are in the specified namespace.

In the below example, a ```InNamespaceFilter``` is instantiated that matches types in the namespace ```WebApp.Endpoints```.

{% highlight csharp %}
new InNamespaceFilter("WebApp.Endpoints");
{% endhighlight %}

NameEndsWithFilter
-
This filter matches types whose names end with the specified value.

In the below example, a ```NameEndsWithFilter``` is instantiated that matches types whose names end with ```Endpoint```.

{% highlight csharp %}
new NameEndsWithFilter("Endpoint");
{% endhighlight %}

NameMatchesRegexPatternFilter
-
This filter matches types whose names match the specified regular expression pattern.

In the below example, a ```NameMatchesRegexPatternFilter``` is instantiated that matches types whose names match the regular expression ```Endpoint```.

{% highlight csharp %}
new NameMatchesRegexPatternFilter("Endpoint");
{% endhighlight %}

NameStartsWithFilter
-
This filter matches types whose names start with the specified value.

In the below example, a ```NameStartsWithFilter``` is instantiated that matches types whose names start with ```Endpoint```.

{% highlight csharp %}
new NameStartsWithFilter("Endpoint");
{% endhighlight %}

Extensibility
-
Developers may create their own class filters by implementing the ```IClassFilter``` interface.
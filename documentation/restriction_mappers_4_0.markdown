---
layout: documentation
title: Restriction Mappers
root: ../
documentationversions: [1.0, 4.0]
---
Auto-routing - {{ page.title }}
=
Restriction mappers generate [restrictions](restrictions.html) for [routes](routes.html) during [```AutoRouteCollection```](autoroutecollection.html) route generation. ```AutoRouteCollection``` does not require any restrictions to be provided. However, in most cases developers should supply at least a ```MethodRestriction``` and a ```UrlRelativePathRestriction```.

JuniorRoute provides four built-in implementations:
* ```HttpMethodFromMethodsNamedAfterStandardHttpMethodsMapper```
* ```UrlRelativePathFromRelativeClassNamespaceAndClassNameMapper```
* ```RestrictionsFromAttributesMapper```
* ```RestrictionsFromAttributesMapper<T>```

HttpMethodFromMethodsNamedAfterStandardHttpMethodsMapper
-
This mapper adds a ```MethodRestriction``` to a route, but only if the method name is a standard HTTP method (GET, HEAD, POST, PUT, DELETE, TRACE or CONNECT). The method name comparison is case-insensitive.

In the below example, a ```HttpMethodFromMethodsNamedAfterStandardHttpMethodsMapper``` is instantiated.

{% highlight csharp %}
new HttpMethodFromMethodsNamedAfterStandardHttpMethodsMapper();
{% endhighlight %}

UrlRelativePathFromRelativeClassNamespaceAndClassNameMapper
-
This mapper adds a ```UrlRelativePathRestriction``` to a route using the endpoint's relative namespace and class name. The way in which these two parts are concatenated to form the path is configurable. The class takes five constructor parameters:
* *rootNamespace*
* *caseSensitive*
* *makeLowercase*
* *wordSeparator*
* *wordRegexPattern*

### rootNamespace

This parameter determines the part of the class' namespace that is trimmed during processing. For example, if an endpoint's namespace is *WebApp.Endpoints* and ```rootNamespace``` is *WebApp*, then only *Endpoints* is used in the path.

### caseSensitive

This parameter determines whether the restriction is case-sensitive.

### makeLowercase

This parameter determines whether the entire path is lowercased when generated.

### wordSeparator

A word separator may be used to separate words in the generated path. For example, a ```wordSeparator``` of ```_``` with a relative namespace of ```Help``` , lowercase generation and a class name of ```DocumentationIndex``` produces the path *help/documentation_index*.

### wordRegexPattern

The regular expression pattern used to split words from namespaces and class names may be changed to suit a developer's preference. The default pattern is ```\.|_|(?:(?<=[a-z])(?=[A-Z\d]))|(?:(?<=\d)(?=[A-Z]))|(?:(?<=[A-Z])(?=[A-Z][a-z]))```.

In the below example, a ```UrlRelativePathFromRelativeClassNamespaceAndClassNameMapper``` instance is configured to restrict paths to words separated by underscores.

{% highlight csharp %}
new UrlRelativePathFromRelativeClassNamespaceAndClassNameMapper("WebApp.Endpoints");
{% endhighlight %}

RestrictionsFromAttributesMapper and RestrictionsFromAttributesMapper&lt;T&gt;
-
These mappers check endpoint methods for attributes of a specific type that derives ```RestrictionAttribute```. If any are found, the attributes map the appopriate restrictions to the route.

In the below example, a ```RestrictionsFromAttributesMapper``` instance is configured to restrict by ```CookieAttribute```s.

{% highlight csharp %}
new RestrictionsFromAttributesMapper<CookieAttribute>();
{% endhighlight %}

JuniorRoute provides 22 built-in implementations of ```RestrictionAttribute```:
* ```CookieAttribute```
* ```HeaderAttribute```
* ```MethodAttribute```
* ```RefererUrlAbsolutePathAttribute```
* ```RefererUrlAuthorityAttribute```
* ```RefererUrlFragmentAttribute```
* ```RefererUrlHostAttribute```
* ```RefererUrlHostTypeAttribute```
* ```RefererUrlPathAndQueryAttribute```
* ```RefererUrlPortAttribute```
* ```RefererUrlQueryAttribute```
* ```RefererUrlQueryStringAttribute```
* ```RefererUrlSchemeAttribute```
* ```UrlAuthorityAttribute```
* ```UrlFragmentAttribute```
* ```UrlHostAttribute```
* ```UrlHostTypeAttribute```
* ```UrlPortAttribute```
* ```UrlQueryAttribute```
* ```UrlQueryStringAttribute```
* ```UrlRelativePathAttribute```
* ```UrlSchemeAttribute```

CookieAttribute
-
This attribute adds a ```CookieRestriction``` to a route.

In the below example, a ```[Cookie]``` attribute is configured to restrict a route by the presence of a cookie in the HTTP request message.

{% highlight csharp %}
public class Person
{
  [Cookie("api-key", "71910b0b-405d-47a4-8d8e-eb92ee6bdfa2")]
  public void Post()
  {
  }
}
{% endhighlight %}

HeaderAttribute
-
This attribute adds a ```HeaderRestriction``` to a route.

In the below example, a ```[Header]``` attribute is configured to restrict a route by the presence of a header in the HTTP request message.

{% highlight csharp %}
public class Person
{
  [Header("api-key", "71910b0b-405d-47a4-8d8e-eb92ee6bdfa2")]
  public void Post()
  {
  }
}
{% endhighlight %}

MethodAttribute
-
This attribute adds a ```MethodRestriction``` to a route.

In the below example, a ```[Method]``` attribute is configured to restrict a route to only GET requests.

{% highlight csharp %}
public class Person
{
  [Method(HttpMethod.Get)]
  public void Post()
  {
  }
}
{% endhighlight %}

RefererUrlAbsolutePathAttribute
-
This attribute adds a ```RefererUrlAbsolutePathRestriction``` to a route.

In the below example, a ```[RefererUrlAbsolutePath]``` attribute is configured to restrict a route to only referer URLs with an absolute path of */path*.

{% highlight csharp %}
public class Person
{
  [RefererUrlAbsolutePath("/path")]
  public void Post()
  {
  }
}
{% endhighlight %}

RefererUrlAuthorityAttribute
-
This attribute adds a ```RefererUrlAuthorityRestriction``` to a route.

In the below example, a ```[RefererUrlAuthority]``` attribute is configured to restrict a route to only referer URLs with an authority of *www.google.com:80*.

{% highlight csharp %}
public class Person
{
  [RefererUrlAuthority("www.google.com:80")]
  public void Post()
  {
  }
}
{% endhighlight %}

RefererUrlFragmentAttribute
-
This attribute adds a ```RefererUrlFragmentRestriction``` to a route.

In the below example, a ```[RefererUrlFragment]``` attribute is configured to restrict a route to only referer URLs with a fragment of *#fragment*.

{% highlight csharp %}
public class Person
{
  [RefererUrlFragment("#fragment")]
  public void Post()
  {
  }
}
{% endhighlight %}

RefererUrlHostAttribute
-
This attribute adds a ```RefererUrlHostRestriction``` to a route.

In the below example, a ```[RefererUrlHost]``` attribute is configured to restrict a route to only referer URLs with a host of *www.google.com*.

{% highlight csharp %}
public class Person
{
  [RefererUrlHost("www.google.com")]
  public void Post()
  {
  }
}
{% endhighlight %}

RefererUrlHostTypeAttribute
-
This attribute adds a ```RefererUrlHostTypeRestriction``` to a route.

In the below example, a ```[RefererUrlHostType]``` attribute is configured to restrict a route to only referer URLs with a host type of ```UriHostNameType.IPv4```.

{% highlight csharp %}
public class Person
{
  [RefererUrlHostType(UriHostNameType.IPv4)]
  public void Post()
  {
  }
}
{% endhighlight %}

RefererUrlPathAndQueryAttribute
-
This attribute adds a ```RefererUrlPathAndQueryRestriction``` to a route.

In the below example, a ```[RefererUrlPathAndQuery]``` attribute is configured to restrict a route to only referer URLs with a path and query of *path?id=1*.

{% highlight csharp %}
public class Person
{
  [RefererUrlPathAndQuery("path?id=1")]
  public void Post()
  {
  }
}
{% endhighlight %}

RefererUrlPortAttribute
-
This attribute adds a ```RefererUrlPortRestriction``` to a route.

In the below example, a ```[RefererUrlPort]``` attribute is configured to restrict a route to only referer URLs with a port of *8080*.

{% highlight csharp %}
public class Person
{
  [RefererUrlPort(8080)]
  public void Post()
  {
  }
}
{% endhighlight %}

RefererUrlQueryAttribute
-
This attribute adds a ```RefererUrlQueryRestriction``` to a route.

In the below example, a ```[RefererUrlQuery]``` attribute is configured to restrict a route to only referer URLs with a query of *?firstname=joe&lastname=public*.

{% highlight csharp %}
public class Person
{
  [RefererUrlQuery("?firstname=joe&lastname=public")]
  public void Post()
  {
  }
}
{% endhighlight %}

RefererUrlQueryStringAttribute
-
This attribute adds a ```RefererUrlQueryStringRestriction``` to a route.

In the below example, a ```[RefererUrlQueryString]``` attribute is configured to restrict a route to only referer URLs with a query string field of *id* and a value of *1*.

{% highlight csharp %}
public class Person
{
  [RefererUrlQueryString("id", "1")]
  public void Post()
  {
  }
}
{% endhighlight %}

RefererUrlSchemeAttribute
-
This attribute adds a ```RefererUrlSchemeRestriction``` to a route.

In the below example, a ```[RefererUrlScheme]``` attribute is configured to restrict a route to only referer URLs with a scheme of *https*.

{% highlight csharp %}
public class Person
{
  [RefererUrlScheme("https")]
  public void Post()
  {
  }
}
{% endhighlight %}

UrlAuthorityAttribute
-
This attribute adds a ```UrlAuthorityRestriction``` to a route.

In the below example, a ```[UrlAuthority]``` attribute is configured to restrict a route to only request URLs with an authority of *www.google.com:80*.

{% highlight csharp %}
public class Person
{
  [UrlAuthority("www.google.com:80")]
  public void Post()
  {
  }
}
{% endhighlight %}

UrlFragmentAttribute
-
This attribute adds a ```UrlFragmentRestriction``` to a route.

In the below example, a ```[UrlFragment]``` attribute is configured to restrict a route to only request URLs with a fragment of *#fragment*.

{% highlight csharp %}
public class Person
{
  [UrlFragment("#fragment")]
  public void Post()
  {
  }
}
{% endhighlight %}

UrlHostAttribute
-
This attribute adds a ```UrlHostRestriction``` to a route.

In the below example, a ```[UrlHost]``` attribute is configured to restrict a route to only request URLs with a host of *www.google.com*.

{% highlight csharp %}
public class Person
{
  [UrlHost("www.google.com")]
  public void Post()
  {
  }
}
{% endhighlight %}

UrlHostTypeAttribute
-
This attribute adds a ```UrlHostTypeRestriction``` to a route.

In the below example, a ```[UrlHostType]``` attribute is configured to restrict a route to only request URLs with a host type of ```UriHostNameType.IPv4```.

{% highlight csharp %}
public class Person
{
  [UrlHostType(UriHostNameType.IPv4)]
  public void Post()
  {
  }
}
{% endhighlight %}

UrlPortAttribute
-
This attribute adds a ```UrlPortRestriction``` to a route.

In the below example, a ```[UrlPort]``` attribute is configured to restrict a route to only request URLs with a port of *8080*.

{% highlight csharp %}
public class Person
{
  [UrlPort(8080)]
  public void Post()
  {
  }
}
{% endhighlight %}

UrlQueryAttribute
-
This attribute adds a ```UrlQueryRestriction``` to a route.

In the below example, a ```[UrlQuery]``` attribute is configured to restrict a route to only request URLs with a query of *?firstname=joe&lastname=public*.

{% highlight csharp %}
public class Person
{
  [UrlQuery("?firstname=joe&lastname=public")]
  public void Post()
  {
  }
}
{% endhighlight %}

UrlQueryStringAttribute
-
This attribute adds a ```UrlQueryStringRestriction``` to a route.

In the below example, a ```[UrlQueryString]``` attribute is configured to restrict a route to only request URLs with a query string field of *id* and a value of *1*.

{% highlight csharp %}
public class Person
{
  [UrlQueryString("id", "1")]
  public void Post()
  {
  }
}
{% endhighlight %}

UrlRelativePathAttribute
-
This attribute adds a ```UrlRelativePathRestriction``` to a route.

In the below example, a ```[UrlRelativePath]``` attribute is configured to restrict a route to only request URLs with a relative path of *person*.

{% highlight csharp %}
public class Person
{
  [UrlRelativePath("person")]
  public void Post()
  {
  }
}
{% endhighlight %}

UrlSchemeAttribute
-
This attribute adds a ```UrlSchemeRestriction``` to a route.

In the below example, a ```[UrlScheme]``` attribute is configured to restrict a route to only request URLs with a scheme of *https*.

{% highlight csharp %}
public class Person
{
  [UrlScheme("https")]
  public void Post()
  {
  }
}
{% endhighlight %}

Ignoring Mappers
-
The ```[IgnoreRestrictionMapperType]``` attribute may be applied to an endpoint method in order to ignore specific mappers during route generation. This proves useful when developers wish to map restrictions automatically most of the time while overriding the automatic behavior in specific cases.

In the below example, an ```[IgnoreRestrictionMapperType]``` instructs ```AutoRouteCollection``` to ignore ```UrlRelativePathFromRelativeClassNamespaceAndClassNameMapper``` when adding ```UrlRelativePathRestriction```s:

{% highlight csharp %}
public class Documentation
{
  [UrlRelativePath("docs")]
  [IgnoreRestrictionMapperType(typeof(UrlRelativePathFromRelativeClassNamespaceAndClassNameMapper))]
  public HtmlResponse Get()
  {
    ...
  }
}
{% endhighlight %}

Extensibility
-
Developers may create their own restriction mappers by implementing the ```IRestrictionMapper``` interface.
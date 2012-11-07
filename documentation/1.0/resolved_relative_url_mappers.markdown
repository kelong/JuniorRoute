---
layout: documentation_1_0
title: Resolved Relative URL Mappers
root: ../../
documentationroot: ../../documentation/1.0/
---
Auto-routing - {{ page.title }}
=
Resolved relative URL mappers map a [route]({{ page.documentationroot }}routes.html)'s resolved relative URLs during [```AutoRouteCollection```]({{ page.documentationroot }}autoroutecollection.html) route generation. ```AutoRouteCollection``` requires at least one resolved relative URL mapper to be provided.

JuniorRoute provides two built-in implementations:
* ```ResolvedRelativeUrlFromRelativeClassNamespaceAndClassNameMapper```
* ```ResolvedRelativeUrlAttributeMapper```

ResolvedRelativeUrlFromRelativeClassNamespaceAndClassNameMapper
-
This mapper maps a URL to the endpoint's relative class namespace and class name. The way in which these two parts are concatenated to form the URL is configurable. The class takes four constructor parameters:
* *rootNamespace*
* *makeLowercase*
* *wordSeparator*
* *wordRegexPattern*

### rootNamespace

This parameter determines the part of the class' namespace that is trimmed during processing. For example, if an endpoint's namespace is *WebApp.Endpoints* and ```rootNamespace``` is *WebApp*, then only *Endpoints* is used in the URL.

### makeLowercase

This parameter determines whether the entire URL is lowercased when generated.

### wordSeparator

A word separator may be used to separate words in the generated URL. For example, a ```wordSeparator``` of ```_``` with a relative namespace of ```Help``` , lowercase generation and a class name of ```DocumentationIndex``` produces the URL *help/documentation_index*.

### wordRegexPattern

The regular expression pattern used to split words from namespaces and class names may be changed to suit a developer's preference. The default pattern is ```\.|_|(?:(?<=[a-z])(?=[A-Z\d]))|(?:(?<=\d)(?=[A-Z]))|(?:(?<=[A-Z])(?=[A-Z][a-z]))```.

In the below example, a ```ResolvedRelativeUrlFromRelativeClassNamespaceAndClassNameMapper``` instance is configured to create lowercase resolved relative URLs whose words are separated by underscores.

{% highlight csharp %}
new ResolvedRelativeUrlFromRelativeClassNamespaceAndClassNameMapper("WebApp.Endpoints");
{% endhighlight %}

ResolvedRelativeUrlAttributeMapper
-
This mapper maps a URL to the value supplied in a ```[ResolvedRelativeUrl]``` attribute decorating an endpoint method.

In the below example, a URL of *help* is mapped.

{% highlight csharp %}
public class Documentation
{
  [ResolvedRelativeUrl("help")]
  public HtmlResponse Get()
  {
    ...
  }
}
{% endhighlight %}

Extensibility
-
Developers may create their own resolved relative URL mappers by implementing the ```IResolvedRelativeUrlMapper``` interface.
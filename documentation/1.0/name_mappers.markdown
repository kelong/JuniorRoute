---
layout: documentation_1_0
title: Name Mappers
root: ../../
documentationroot: ../../documentation/1.0/
---
Auto-routing - {{ page.title }}
=
Name mappers map [route]({{ page.documentationroot }}routes.html) names during [```AutoRouteCollection```]({{ page.documentationroot }}autoroutecollection.html) route generation. ```AutoRouteCollection``` requires at least one name mapper to be provided.

JuniorRoute provides three built-in implementations:
* ```NameAfterRelativeClassNamespaceAndClassNameAndMethodNameMapper```
* ```NameAfterRelativeClassNamespaceAndClassNameMapper```
* ```NameAttributeMapper```

NameAfterRelativeClassNamespaceAndClassNameAndMethodNameMapper
-
This mapper names a route after the endpoint's relative class namespace, class name and method name. The way in which these three parts are concatenated to form the name is configurable. The class takes four constructor parameters:
* *rootNamespace*
* *makeLowercase*
* *wordSeparator*
* *wordRegexPattern*

### rootNamespace

This parameter determines the part of the class' namespace that is trimmed during processing. For example, if an endpoint's namespace is *WebApp.Endpoints* and ```rootNamespace``` is *WebApp*, then only *Endpoints* is used in the route name.

### makeLowercase

This parameter determines whether the entire route name is lowercased when generated.

### wordSeparator

A word separator may be used to separate words in the generated route name. For example, a ```wordSeparator``` of ```_``` with a relative namespace of ```Endpoints.Help```, a class name of ```DocumentationIndex``` and a method name of ```Get``` produces the route name *Endpoints_Help_Documentation_Index_Get*.

### wordRegexPattern

The regular expression pattern used to split words from namespaces, class names and method names may be changed to suit a developer's preference. The default pattern is ```\.|_|(?:(?<=[a-z])(?=[A-Z\d]))|(?:(?<=\d)(?=[A-Z]))|(?:(?<=[A-Z])(?=[A-Z][a-z]))```.

In the below example, a ```NameAfterRelativeClassNamespaceAndClassNameAndMethodNameMapper``` instance is configured to create lowercase route names whose words are separated by spaces.

{% highlight csharp %}
new NameAfterRelativeClassNamespaceAndClassNameAndMethodNameMapper("WebApp.Endpoints", true);
{% endhighlight %}

NameAfterRelativeClassNamespaceAndClassNameMapper
-
This mapper names a route after the endpoint's relative class namespace and class name. The way in which these two parts are concatenated to form the name is configurable. The class takes four constructor parameters:
* *rootNamespace*
* *makeLowercase*
* *wordSeparator*
* *wordRegexPattern*

### rootNamespace

This parameter determines the part of the class' namespace that is trimmed during processing. For example, if an endpoint's namespace is *WebApp.Endpoints* and ```rootNamespace``` is *WebApp*, then only *Endpoints* is used in the route name.

### makeLowercase

This parameter determines whether the entire route name is lowercased when generated.

### wordSeparator

A word separator may be used to separate words in the generated route name. For example, a ```wordSeparator``` of ```_``` with a relative namespace of ```Endpoints.Help``` and a class name of ```DocumentationIndex``` produces the route name *Endpoints_Help_Documentation_Index*.

### wordRegexPattern

The regular expression pattern used to split words from namespaces, class names and method names may be changed to suit a developer's preference. The default pattern is ```\.|_|(?:(?<=[a-z])(?=[A-Z\d]))|(?:(?<=\d)(?=[A-Z]))|(?:(?<=[A-Z])(?=[A-Z][a-z]))```.

In the below example, a ```NameAfterRelativeClassNamespaceAndClassNameMapper``` instance is configured to create lowercase route names whose words are separated by spaces.

{% highlight csharp %}
new NameAfterRelativeClassNamespaceAndClassNameMapper("WebApp.Endpoints", true);
{% endhighlight %}

NameAttributeMapper
-
This mapper names a route after the value supplied in a ```[Name]``` attribute decorating an endpoint method.

In the below example, a route is named *Help*.

{% highlight csharp %}
public class Documentation
{
  [Name("help")]
  public HtmlResponse Get()
  {
    ...
  }
}
{% endhighlight %}

Extensibility
-
Developers may create their own name mappers by implementing the ```INameMapper``` interface.
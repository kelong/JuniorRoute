---
layout: documentation
title: Templates
root: ../../
documentationversions: [1.0,2.0]
---
Razor View Engine - {{ page.title }}
=
Razor uses template base classes to render and execute a particular template. Template base classes must expose a few methods to accomplish these goals. JuniorRoute's ```ITemplate``` interface enforces that these key methods are provided.

A template base class exposes its members to code within the template. For example, with ASP.net MVC the ```WebViewPage<>``` base class and its ancestors expose several properties to the template, a few of which are the ```Model```, ```Page``` and ```Server``` properties. JuniorRoute's ```ITemplate``` interface is much lighter-weight; it doesn't expose any methods beyond the few required by Razor and it only exposes a single property, ```Content```, that represents the rendered content.

For most applications, the only exposure developers have to template types is through the use of an [```ITemplateRepository```](template_repositories.html) implementation. ```ITemplateRepository``` has several ```Execute``` method overloads, many of which allow developers to specify the ```ITemplate``` implementation to use for the template base class. ```Execute``` overloads that don't have a template type generic type parameter should default to using the built-in ```Template``` or ```Template<>``` types; the built-in implementations do just that.

It is recommended to derive from ```Template``` or ```Template<>``` instead of implementing ```ITemplate``` or ```ITemplate<>``` directly, thus avoiding the need to rewrite the code that handles the actual template rendering.

In the below example, a template is created that renders a custom base class' Foo property.

{% highlight html %}
<!DOCTYPE html>
<html>
<head>
  <title></title>
</head>
<body>
  <h1>@Foo</h1>
</body>
</html>
{% endhighlight %}

Next, a custom template type is supplied to a template repository's ```Execute``` method.

{% highlight csharp %}
public class FooTemplate : Template
{
  public string Foo
  {
    get
    {
      return "Bar";
    }
  }
}
...
string content = templateRepository.Execute<FooTemplate>(@"Templates\Foo");
{% endhighlight %}
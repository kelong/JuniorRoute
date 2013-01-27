---
layout: documentation
title: Template Repositories
root: ../../
documentationversions: [1.0,2.0]
---
Razor View Engine - {{ page.title }}
=
A template repository's fundamental responsibility is to create and retrieve a [template](templates.html) given a key. The ```ITemplateRepository``` interface defines several ```Get``` method overloads to accomplish this task. Additionally, several helper ```Run``` overloads are provided that combine the functionality provided by ```Get``` with actual template execution.

JuniorRoute's only built-in implementation, ```FileSystemRepository```, orchestrates several activities to streamline template execution. ```FileSystemRepository```:
* retrieves templates from the [file system](../file_systems.html) using relative paths as keys
* compiles templates to in-memory assemblies and caches the compiled type
* runs compiled templates

Many of ```FileSystemRepository```'s reponsibilities are handled by specific components that are themselves exposed by interfaces. Developers using ```FileSystemRepository``` are encouraged to use an endpoint [container](../containers.html) combined with an IoC container to easily inject implementations into ```FileSystemRepository```'s constructor.

The various ```Get``` and ```Run``` methods allow developers to specify several things when retrieving or running a template:
* An optional template type may be specified as a generic type parameter. If a template type is not explicitly provided then ```Template``` or ```Template<>``` is used as appropriate.
* The relative path of the template is resolved to an absolute path using the provided [template path resolver](template_path_resolvers.html). The absolute path is then used to load the template from disk.
* An optional model instance can be any type and if one is specified then the template base class will expose a Model property to code within the template.
* Optional namespace imports provide the equivalent ```using``` keywords to the generated template code, sparing the developer from having to write ```@using``` code within the template. 

A single line of code from within an endpoint method is all it takes to load, compile, cache and run a template with a specific model. The below code is the HelloWorld endpoint from the [Visual Studio project templates](http://visualstudiogallery.msdn.microsoft.com/41f5e30a-d988-49a1-b86b-baa118fd832a).

{% highlight csharp %}
public class HelloWorld
{
  private readonly ITemplateRepository _templateRepository;

  public HelloWorld(ITemplateRepository templateRepository)
  {
    _templateRepository = templateRepository;
  }

  public HtmlResponse Get()
  {
    string content = _templateRepository.Run(@"Templates\HelloWorld", new Model { Message = "Hello, world." });

    return new HtmlResponse(content);
  }

  public class Model
  {
    public string Message
    {
      get;
      set;
    }
  }
}
{% endhighlight %}
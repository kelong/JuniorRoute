---
layout: documentation
title: Containers
root: ../
documentationversions: [1.0]
---
Auto-routing - {{ page.title }}
=
JuniorRoute uses containers to support [dependency injection](http://en.wikipedia.org/wiki/Dependency_injection), usually combined with an [inversion-of-control](http://en.wikipedia.org/wiki/Inversion_of_control) (IoC) container. However, JuniorRoute does not have a dependency on a specific container implementation; instead, JuniorRoute exposes an ```IContainer``` interface. ```IContainer```'s sole purpose is retrieving instances of objects given a type.

IContainer Implementations
-
JuniorRoute has no built-in third-party ```IContainer``` implementations because doing so would introduce an IoC container dependency; however, JuniorRoute does provide several implementations to handle basic cases. In most cases, developers are encouraged to implement ```IContainer``` themselves.

How AutoRouteCollection Uses Containers
-
Within [```AutoRouteCollection```](autoroutecollection.html), ```IContainer``` implementations:
* retrieve endpoint class instances
* retrieve [restriction](restrictions.html) instances
* retrieve [bundle](bundles.html) dependency instances

### Endpoint Containers

Endpoint containers retrieve instances of endpoint types. There are no types required to be created by endpoint containers. ```AutoRouteCollection``` defaults to using the ```NewInstancePerRouteEndpointContainer``` class for its endpoint container.

Most developers should register these three interfaces in their endpoint container implementations, as these interfaces are commonly used by endpoints:
* [```IRequestContext```](request_contexts.html) with the ```RequestContext``` implementation
* [```IUrlResolver```](url_resolvers.html) with the ```UrlResolver``` implementation
* ```ISystemClock``` with the ```SystemClock``` implementation

In the below example, an Autofac-based endpoint container is provided to an ```AutoRouteCollection``` instance.

{% highlight csharp %}
public class EndpointContainer : IContainer
{
  private readonly Autofac.IContainer _container;

  public EndpointContainer(Autofac.IContainer container)
  {
    _container = container;
  }

  public T GetInstance<T>()
  {
    return _container.Resolve<T>();
  }

  public object GetInstance(Type type)
  {
    return _container.Resolve(type);
  }
}
...
var container = new EndpointContainer(autofacContainer);

new AutoRouteCollection().EndpointContainer(container);
{% endhighlight %}

### Restriction Containers

Restriction containers are used by some [restriction mappers](restriction_mappers.html) to provide implementations of interfaces required by those restrictions. If there are no restriction mappers registered with an ```AutoRouteCollection``` instance then a restriction container is not required.

Currently only the ```UrlRelativePathRestriction``` class needs dependency injection, and its only dependency is ```IHttpRuntime```.

Developers are encouraged to use the built-in ```DefaultRestrictionContainer``` implementation and supply an ```HttpRuntimeWrapper``` instance to its constructor.

In the below example, a ```DefaultRestrictionContainer``` is instantiated. Then, the container is provided to an ```AutoRouteCollection``` instance.

{% highlight csharp %}
var container = new DefaultRestrictionContainer(new HttpRuntimeWrapper());

new AutoRouteCollection()
  .RestrictionContainer(container);
{% endhighlight %}

### Bundle Dependency Containers

Bundle dependency containers are used by ```AutoRouteCollection```'s bundle helper methods (e.g., ```CssBundle``` and ```JavaScriptBundle```). These helper methods create [```BundleWatcher```](bundle_watchers.html) instances and instances of classes derived from [```BundleWatcherRoute<T>```](bundle_watcher_routes.html). These classes have four dependencies:
* [```IFileSystem```](file_systems.html)
* ```IGuidFactory```
* ```IHttpRuntime```
* ```ISystemClock```

All bundle dependency contains must be able to create instances of classes implementing these four interfaces.

Developers are encouraged to use the built-in ```DefaultBundleDependencyContainer``` implementation and supply an ```HttpRuntimeWrapper``` instance and a ```FileSystem``` instance in its constructor.

In the below example, a ```DefaultBundleDependencyContainer``` is instantiated. Then, the container is provided to an ```AutoRouteCollection``` instance.

{% highlight csharp %}
var container = new DefaultBundleDependencyContainer(new HttpRuntimeWrapper(), new FileSystem());

new AutoRouteCollection()
  .BundleDependencyContainer(container);
{% endhighlight %}

Popular IoC Containers
-
It is recommended that developers use an IoC container with their JuniorRoute applications. Here are four popular containers:
* [Autofac](http://code.google.com/p/autofac/)
* [StructureMap](http://docs.structuremap.net/)
* [Unity] (http://unity.codeplex.com/)
* [Castle Windsor](http://docs.castleproject.org/Windsor.MainPage.ashx)

Extensibility
-
Developers may create their own containers by implementing the ```IContainer``` interface.
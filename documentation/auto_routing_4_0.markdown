---
layout: documentation
title: Auto-routing
root: ../
documentationversions: [1.0, 4.0]
---
{{ page.title }}
=
Creating [routes](routes.html) manually using the ```Route``` class is a time-consuming process. Properties like route name, ID and resolved relative URL must be supplied and [restrictions](restrictions.html) must be provided. Fortunately, JuniorRoute comes with built-in auto-routing capabilities that can greatly reduce the amount of configuration necessary to use JuniorRoute effectively.

Auto-routing creates routes by scanning assemblies for classes called *endpoints*, then scanning those endpoints for methods. Routes are created from endpoints and methods using various filters and mappers. JuniorRoute provides several built-in filters and mappers for most common endpoint and method conventions. This technique allows developers to focus on writing endpoint logic rather than spending a lot of time configuring JuniorRoute.

There are no base class, interface or attribute requirements for endpoints. There are three general requirements, however. Endpoints:
* must be public
* must not be abstract or static (i.e., it must be instantiatable)
* must be a reference type (i.e., a class)

There are two requirements for endpoint methods. Endpoint methods:
* must be public
* must be an instance method (i.e., non-static)

Auto-routing follows a [convention over configuration](http://en.wikipedia.org/wiki/Convention_over_configuration) approach: A developer supplies the patterns by which route attributes are determined, then JuniorRoute performs the route generation.

The class that drives the auto-routing mechanism is [```AutoRouteCollection```](autoroutecollection.html). ```AutoRouteCollection``` is usually used in the constructor of a class derived from [```JuniorRouteApplicationConfiguration```](juniorrouteapplicationconfiguration.html).

The below example demonstrates a simple auto-route configuration.

{% highlight csharp %}
const string rootEndpointNamespace = "WebApp.Endpoints";
var guidFactory = new GuidFactory();
var endpointContainer = new SingleInstancePerRouteEndpointContainer();
var restrictionContainer = new DefaultRestrictionContainer(new HttpRuntimeWrapper());
var parameterMappers = new IParameterMapper[] { new FormToIConvertibleMapper(), new QueryStringToIConvertibleMapper() };
AutoRouteCollection autoRouteCollection = new AutoRouteCollection()
  .Assemblies(Assembly.GetExecutingAssembly())
  .EndpointContainer(endpointContainer)
  .RestrictionContainer(restrictionContainer)
  .NameAfterRelativeClassNamespaceAndClassName(rootEndpointNamespace)
  .IdRandomly(guidFactory)
  .ResolvedRelativeUrlFromRelativeClassNamespacesAndClassNames(rootEndpointNamespace)
  .RestrictHttpMethodsToMethodsNamedAfterStandardHttpMethods()
  .RespondWithMethodReturnValuesThatImplementIResponse(parameterMappers);
{% endhighlight %}
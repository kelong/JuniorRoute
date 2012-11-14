---
layout: documentation_1_x
title: AutoRouteCollection
root: ../../
documentationversions: [1.0]
---
Auto-routing - {{ page.title }}
=
```AutoRouteCollection``` is the class that brings together JuniorRoute's auto-routing capabilities. Most of the time, developers will have created a class derived from [```JuniorRouteApplicationConfiguration```](juniorrouteapplicationconfiguration.html). In that class' constructor, ```AutoRouteCollection``` should be instantiated, configured and used to generate most of the application's [routes](routes.html).

```AutoRouteCollection``` provides a constructor parameter that determines whether duplicate route names are allowed.

Most configuration can be performed using fluent-interface methods. This includes configuration for the following:
* The assemblies that will be scanned for endpoint classes
* [Class filters](class_filters.html)
* [Method filters](method_filters.html)
* [Name mappers](name_mappers.html)
* [ID mappers](id_mappers.html)
* [Resolved relative URL mappers](resolved_relative_url_mappers.html)
* [Restriction mappers](restriction_mappers.html)
* [Response mappers](response_mappers.html)
* [Authentication providers](authentication_providers.html)
* [Bundles](bundles.html)
* Configuration-time-created routes, such as custom routes and [diagnostics](diagnostics.html) routes

Developers can call ```AutoRouteCollection```'s ```GenerateRouteCollection``` method to generate and returns routes once all configuration is complete. ```AutoRouteCollection``` has the following requirements that must be met before routes are generated:
* At least one assembly must be provided
* At least one name mapper must be provided
* At least one ID mapper must be provided
* At least one resolved relative URL mapper must be provided
* If any restriction mappers are provided, a restriction container must also be provided

Route generation will fail if, for any reason, a route name, ID or resolved relative URL cannot be mapped.

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
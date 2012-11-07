---
layout: documentation_1_0
title: AutoRouteCollection
root: ../../
documentationroot: ../../documentation/1.0/
---
Auto-routing - {{ page.title }}
=
```AutoRouteCollection``` is the class that brings together JuniorRoute's auto-routing capabilities. Most of the time, developers will have created a class derived from [```JuniorRouteApplicationConfiguration```]({{ page.documentationroot }}juniorrouteapplicationconfiguration.html). In that class' constructor, ```AutoRouteCollection``` should be instantiated, configured and used to generate most of the application's [routes]({{ page.documentationroot }}routes.html).

```AutoRouteCollection``` provides a constructor parameter that determines whether duplicate route names are allowed.

Most configuration can be performed using fluent-interface methods. This includes configuration for the following:
* The assemblies that will be scanned for endpoint classes
* [Class filters]({{ page.documentationroot }}class_filters.html)
* [Method filters]({{ page.documentationroot }}method_filters.html)
* [Name mappers]({{ page.documentationroot }}name_mappers.html)
* [ID mappers]({{ page.documentationroot }}id_mappers.html)
* [Resolved relative URL mappers]({{ page.documentationroot }}resolved_relative_url_mappers.html)
* [Restriction mappers]({{ page.documentationroot }}restriction_mappers.html)
* [Response mappers]({{ page.documentationroot }}response_mappers.html)
* [Authentication providers]({{ page.documentationroot }}authentication_providers.html)
* [Bundles]({{ page.documentationroot }}bundles.html)
* Configuration-time-created routes, such as custom routes and [diagnostics]({{ page.documentationroot }}diagnostics.html) routes

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
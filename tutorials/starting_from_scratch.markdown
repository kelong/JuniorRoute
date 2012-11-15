---
layout: tutorials
title: Starting From Scratch
root: ../
---
{{ page.title }}
=
Although it's easy to install the [JuniorRoute Project Templates extension](http://visualstudiogallery.msdn.microsoft.com/41f5e30a-d988-49a1-b86b-baa118fd832a), developers may find that bootstrapping a project by hand can help them better understand JuniorRoute. This tutorial describes the steps necessary to bootstrap a JuniorRoute Web application without using a project template.

Step 1 - Create a New ASP.NET Empty Web Application Solution
-
Create a new ASP.NET empty Web application solution. Be sure to target at least .NET Framework 4.

![Create a new ASP.NET empty Web application](images/new-aspnet-empty-web-application.png "Create a new ASP.NET empty Web application")

Step 2 - Remove References From the New Project
-
Remove all references except System and System.Web.

![Remove references](images/remove-references.png "Remove references")

Step 3 - Add NuGet Packages
-
JuniorRoute packages can be found in the Manage NuGet Packages dialog by searching for *JuniorRoute*.

Add all JuniorRoute packages to your project.

![Add NuGet packages](images/add-nuget-packages.png "Add NuGet packages")

![Updated references](images/updated-references.png "Updated references")

Step 4 - Add a New Global Application Class
-
JuniorRoute attaches to the ASP.net pipeline from within the global application class' constructor.

Add a new global application class called ```Global``` and change its code to the following:

{% highlight csharp %}
using System.Web;

using Junior.Route.AspNetIntegration;

namespace JuniorApplication
{
  public class Global : HttpApplication
  {
    public Global()
    {
      JuniorRouteApplication.AttachToHttpApplication(this);
    }
  }
}
{% endhighlight %}

Step 5 - Add a Configuration Class
-
Add a new class called ```JuniorRouteConfiguration``` and change its code to the following:

{% highlight csharp %}
using Junior.Route.AspNetIntegration;

namespace JuniorApplication
{
  public class JuniorRouteConfiguration : JuniorRouteApplicationConfiguration
  {
    public JuniorRouteConfiguration()
    {
    }
  }
}
{% endhighlight %}

We'll leave this class stubbed for now and come back to it in a few steps.

Step 6 - Add a Pre-application Start Method
-
Were we to register configuration in ```Global```, configuration would be registered every time the ASP.net runtime created an instance of ```Global```. Since registration involves scanning assemblies, it's possible that it may take some time to finish. Instead, we'll use ASP.net's *pre-application start method* mechanism to ensure configuration is only created once.

Add a new class called ```JuniorRoute``` to your project and change its code to the following:

{% highlight csharp %}
using Junior.Route.AspNetIntegration;

namespace JuniorApplication
{
  public static class JuniorRoute
  {
    public static void Start()
    {
      JuniorRouteApplication.RegisterConfiguration<JuniorRouteConfiguration>();
    }
  }
}
{% endhighlight %}

Step 7 - Add the PreApplicationStartMethod Attribute to AssemblyInfo.cs
-
Adding the ```[assembly:PreApplicationStartMethod]``` attribute instructs ASP.net to call the ```Start``` method before creating any global application class instances.

Replace the contents of AssemblyInfo.cs with the following code:

{% highlight csharp %}
using System.Reflection;
using System.Web;

using JuniorApplication;

[assembly:AssemblyTitle("JuniorApplication")]
[assembly:PreApplicationStartMethod(typeof(JuniorRoute), "Start")]
{% endhighlight %}

Step 8 - Configure Auto-routing
-
Now that bootstrapping is taken care of, it's time to configure [auto-routing]({{ page.root }}documentation/auto_routing.html). Auto-routing will automatically generate [routes]({{ page.root }}documentation/routes.html) given a set of conventions.

Open the ```JuniorRouteConfiguration``` class and change its code to the following:

{% highlight csharp %}
using System.Reflection;

using Junior.Common;
using Junior.Route.AspNetIntegration;
using Junior.Route.AspNetIntegration.AspNet;
using Junior.Route.AspNetIntegration.ResponseGenerators;
using Junior.Route.AspNetIntegration.ResponseHandlers;
using Junior.Route.AutoRouting;
using Junior.Route.AutoRouting.Containers;
using Junior.Route.AutoRouting.ParameterMappers;
using Junior.Route.Routing;
using Junior.Route.Routing.Caching;

namespace JuniorApplication
{
  public class JuniorRouteConfiguration : JuniorRouteApplicationConfiguration
  {
    public JuniorRouteConfiguration()
    {
      // Declare the root endpoint namespace
      const string endpointNamespace = "JuniorApplication.Endpoints";

      // Create dependencies
      var guidFactory = new GuidFactory();
      var httpRuntime = new HttpRuntimeWrapper();
      var restrictionContainer = new DefaultRestrictionContainer(httpRuntime);
      var responseGenerators = new IResponseGenerator[]
        {
          new MostMatchingRestrictionsGenerator(),
          new UnmatchedRestrictionsGenerator(),
          new NotFoundGenerator()
        };
      var responseHandlers = new IResponseHandler[] { new NonCacheableResponseHandler() };
      var parameterMappers = new IParameterMapper[] { new DefaultValueMapper() };
      var cache = new NoCache();

      // Provide conventions to a new AutoRouteCollection instance
      AutoRouteCollection autoRouteCollection = new AutoRouteCollection()
        .RestrictionContainer(restrictionContainer)
        .Assemblies(Assembly.GetExecutingAssembly())
        .NameAfterRelativeClassNamespaceAndClassName(endpointNamespace)
        .IdRandomly(guidFactory)
        .ResolvedRelativeUrlFromRelativeClassNamespacesAndClassNames(endpointNamespace)
        .RestrictHttpMethodsToMethodsNamedAfterStandardHttpMethods()
        .RestrictRelativeUrlsToRelativeClassNamespacesAndClassNames(endpointNamespace)
        .RespondWithMethodReturnValuesThatImplementIResponse(parameterMappers);

      // Generate routes
      IRouteCollection routeCollection = autoRouteCollection.GenerateRouteCollection();

      // Create an HTTP handler
      var httpHandler = new AspNetHttpHandler(routeCollection, cache, responseGenerators, responseHandlers);

      // Set the handler in the base class
      SetHandler(httpHandler);
    }
  }
}
{% endhighlight %}

Let's look at the auto-routing code in detail.

In the below code, we provide a [restriction]({{ page.root }}documentation/restrictions.html) [container]({{ page.root }}documentation/containers.html) to the ```AutoRouteCollection``` instance. The restriction container will be used to create instances of endpoint classes. If you plan on using an IoC container with your project then this line of code is where you'd provide an instance of your ```IContainer``` implementation.

{% highlight csharp %}
.RestrictionContainer(restrictionContainer)
{% endhighlight %}

In the below code, the Web application assembly is provided to the ```AutoRouteCollection``` instance. Only the Web application assembly will be searched for endpoint classes.

{% highlight csharp %}
.Assemblies(Assembly.GetExecutingAssembly())
{% endhighlight %}

```Route```'s constructor requires a name. In the below code, we choose to name routes after the endpoint class' namespace and class name. Internally, a new [name mapper]({{ page.root }}documentation/name_mappers.html) is added.

{% highlight csharp %}
.NameAfterRelativeClassNamespaceAndClassName(endpointNamespace)
{% endhighlight %}

```Route```'s constructor also requires an ID. The only time route IDs are normally used is when resolving URLs, and even then it's preferable to resolve routes by name for clarity. In the below code, we choose to generate route IDs randomly with the provided ```IGuidFactory``` implementation. Internally, a new [ID mapper]({{ page.root }}documentation/id_mappers.html) is added.

{% highlight csharp %}
.IdRandomly(guidFactory)
{% endhighlight %}

```Route```'s constructor also requires a resolved relative URL.  In the below code, we choose to assign resolved relative URLs based on the endpoint class' namespace and class name. Internally, a new [resolved relative URL mapper]({{ page.root }}documentation/resolved_relative_url_mappers.html) is added.

{% highlight csharp %}
.ResolvedRelativeUrlFromRelativeClassNamespacesAndClassNames(endpointNamespace)
{% endhighlight %}

We must assign certain restrictions to each route to ensure that a route matches only the HTTP request messages it's supposed to match. In the below code, we restrict each generated route's HTTP method to whatever the endpoint method is named. Internally, a new restriction is added.

{% highlight csharp %}
.RestrictHttpMethodsToMethodsNamedAfterStandardHttpMethods()
{% endhighlight %}

In the below code, we restrict each generated route's relative path to a path based on the endpoint class' namespace and class name. Internally, a new restriction is added.

{% highlight csharp %}
.RestrictRelativeUrlsToRelativeClassNamespacesAndClassNames(endpointNamespace)
{% endhighlight %}

In most cases, routes will be returning instances of [```IResponse```]({{ page.root }}documentation/responses.html). In the below code, we choose to invoke the endpoint methods and return whatever ```IResponse``` instance they return. Internally, a new [response mapper]({{ page.root }}documentation/response_mappers.html) is added.

{% highlight csharp %}
.RespondWithMethodReturnValuesThatImplementIResponse(parameterMappers);
{% endhighlight %}

Step 9 - Add an Endpoint Class
-
Add a new folder to the project called Endpoints. Notice that the namespace for the folder matches the endpoint namespace specified in the above configuration.

Then, add a new class called ```HelloWorld``` to the Endpoints folder and change its code to the following:

{% highlight csharp %}
using Junior.Route.Routing.Responses.Text;

namespace JuniorApplication.Endpoints
{
  public class HelloWorld
  {
    public HtmlResponse Get()
    {
      return new HtmlResponse(@"<html><body style=""font-size: 3em;"">Hello, world.</body></html>");
    }
  }
}
{% endhighlight %}

Step 10 - Test Your Route
-
Run the project in Visual Studio and navigate to the */hello_world* path. You'll see the following page, showing that your route was configured properly:

![Route test](images/route-test.png "Route test")

Step 11 - Add Diagnostic Routes, If Desired
-
Adding [diagnostic]({{ page.root }}documentation/diagnostics.html) routes causes significant changes to occur in your configuration class, so replace its code with the following:

{% highlight csharp %}
using System.Linq;
using System.Reflection;

using Junior.Common;
using Junior.Route.AspNetIntegration;
using Junior.Route.AspNetIntegration.AspNet;
using Junior.Route.AspNetIntegration.Diagnostics;
using Junior.Route.AspNetIntegration.ResponseGenerators;
using Junior.Route.AspNetIntegration.ResponseHandlers;
using Junior.Route.AutoRouting;
using Junior.Route.AutoRouting.Containers;
using Junior.Route.AutoRouting.ParameterMappers;
using Junior.Route.Diagnostics;
using Junior.Route.Routing;
using Junior.Route.Routing.Caching;
using Junior.Route.Routing.Diagnostics;

namespace JuniorApplication
{
  public class JuniorRouteConfiguration : JuniorRouteApplicationConfiguration
  {
    private readonly IRouteCollection _routeCollection;

    public JuniorRouteConfiguration()
    {
      // Declare the root endpoint namespace
      const string endpointNamespace = "JuniorApplication.Endpoints";

      // Create dependencies
      var guidFactory = new GuidFactory();
      var httpRuntime = new HttpRuntimeWrapper();
      var urlResolver = new UrlResolver(() => _routeCollection, httpRuntime);
      var restrictionContainer = new DefaultRestrictionContainer(httpRuntime);
      var responseGenerators = new IResponseGenerator[]
        {
          new MostMatchingRestrictionsGenerator(),
          new UnmatchedRestrictionsGenerator(),
          new NotFoundGenerator()
        };
      var responseHandlers = new IResponseHandler[] { new NonCacheableResponseHandler() };
      var parameterMappers = new IParameterMapper[] { new DefaultValueMapper() };
      var cache = new NoCache();

      // Provide conventions to a new AutoRouteCollection instance
      AutoRouteCollection autoRouteCollection = new AutoRouteCollection()
        .RestrictionContainer(restrictionContainer)
        .Assemblies(Assembly.GetExecutingAssembly())
		.ClassesInNamespace(endpointNamespace)
        .NameAfterRelativeClassNamespaceAndClassName(endpointNamespace)
        .IdRandomly(guidFactory)
        .ResolvedRelativeUrlFromRelativeClassNamespacesAndClassNames(endpointNamespace)
        .RestrictHttpMethodsToMethodsNamedAfterStandardHttpMethods()
        .RestrictRelativeUrlsToRelativeClassNamespacesAndClassNames(endpointNamespace)
        .RespondWithMethodReturnValuesThatImplementIResponse(parameterMappers)
        // Add diagnostics routes
        .AdditionalRoutes(
          DiagnosticConfigurationRoutes.Instance.GetRoutes(
            guidFactory,
            urlResolver,
            httpRuntime,
            "_diagnostics",
            new AspNetDiagnosticConfiguration(
              cache.GetType(),
              responseGenerators.Select(arg => arg.GetType()),
              responseHandlers.Select(arg => arg.GetType())),
            new RoutingDiagnosticConfiguration(() => _routeCollection)));

      // Generate routes
      _routeCollection = autoRouteCollection.GenerateRouteCollection();

      // Create an HTTP handler
      var httpHandler = new AspNetHttpHandler(_routeCollection, cache, responseGenerators, responseHandlers);

      // Set the handler in the base class
      SetHandler(httpHandler);
    }
  }
}
{% endhighlight %}

There are some important changes here. Notice how the [```IRouteCollection```]({{ page.root }}documentation/route_collections.html) instance is now a field instead of a variable. [```UrlResolver```]({{ page.root }}documentation/url_resolvers.html) requires a route collection so that it may search the collection for routes that match a particular name or ID. ```RoutingDiagnosticConfiguration``` renders a diagnostics page that displays all your configured routes. We need to pass the route collection to both of these objects. However, at the time we create instances of ```UrlResolver``` and ```RoutingDiagnosticConfiguration```, the route collection has not yet been generated. In the below code, we create an ```IRouteCollection``` field that allows us to provide the route collection as a delegate.

{% highlight csharp %}
private readonly IRouteCollection _routeCollection;
...
var urlResolver = new UrlResolver(() => _routeCollection, httpRuntime);
...
new RoutingDiagnosticConfiguration(() => _routeCollection)
...
_routeCollection = autoRouteCollection.GenerateRouteCollection();
{% endhighlight %}

Finally, run the project in Visual Studio and navigate to the */_diagnostics* path. You'll see the following page, showing that your diagnostics routes were configured properly:

![Diagnostics route test](images/diagnostics-route-test.png "Diagnostics route test")
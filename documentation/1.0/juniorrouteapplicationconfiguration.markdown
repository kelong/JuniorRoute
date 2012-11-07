---
layout: documentation_1_0
title: JuniorRouteApplicationConfiguration
root: ../../
documentationroot: ../../documentation/1.0/
---
ASP.net Integration - {{ page.title }}
=
The ```JuniorRouteApplicationConfiguration``` class is used to provide an ```IHttpHandler``` implementation to [```JuniorRouteApplication```]({{ page.documentationroot }}juniorrouteapplication.html).

In most cases, developers should derive this class, configure their desired routes in the derived class' constructor, create an instance of [```AspNetHttpHandler```]({{ page.documentationroot }}aspnethttphandler.html) and call ```SetHandler```.

In the below example, a configuration class is created that initializes an ```AspNetHttpHandler``` with a single route.

{% highlight csharp %}
public class JuniorRouteConfiguration : JuniorRouteApplicationConfiguration
{
  public JuniorRouteConfiguration()
  {
    var route = new Routing.Route("Home", Guid.NewGuid(), "home");
    var routeCollection = new RouteCollection { route };
    var responseGenerators = new[] { new MostMatchingRestrictionsGenerator() };
    var responseHandlers = new[] { new NonCacheableResponseHandler() };
    var handler = new AspNetHttpHandler(routeCollection, new NoCache(), responseGenerators, responseHandlers);

    SetHandler(handler);
  }
}
{% endhighlight %}
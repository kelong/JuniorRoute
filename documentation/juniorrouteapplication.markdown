---
layout: documentation
title: JuniorRouteApplication
root: ../
documentationversions: [1.0]
---
ASP.net Integration - {{ page.title }}
=
```JuniorRouteApplication``` is a static class responsible for integrating JuniorRoute with the ASP.net pipeline. The key to integration is remapping the default ```IHttpHandler``` instance to one designed with JuniorRoute in mind (normally, the [```AspNetHttpHandler```](aspnethttphandler.html) class) during every request. ```HttpApplication``` exposes an event called ```PostAuthenticateRequest```, which is the earliest time in the pipeline that this integration can occur.

This class is normally used in two places:
* a custom pre-application start method referenced by the ```[assembly:PreApplicationStartMethod]``` attribute in a Web application's AssemblyInfo.cs
* Global.asax.cs

There is a class called ```Global``` in just about every ASP.net application. ```Global``` derives HttpApplication. However, ASP.net may create multiple instances of this class during an application's life cycle. Because of this, it's important to avoid re-registering routes in ```Global```. Fortunately, there is a mechanism by which developers may execute code before any HttpApplication is instantiated: ```PreApplicationStartMethodAttribute```. The method referenced by the ```[assembly:PreApplicationStartMethod]``` attribute in a Web application's AssemblyInfo.cs is the correct place to initialize an application's routes.

In the below example, configuration is registered with JuniorRoute in a pre-application start method.

{% highlight csharp %}
[assembly:PreApplicationStartMethod(typeof(JuniorRoute), "Start")]
...
public static class JuniorRoute
{
  public static void Start()
  {
    JuniorRouteApplication.RegisterConfiguration<JuniorRouteConfiguration>();
  }
}
{% endhighlight %}

In the below example, JuniorRoute is integrated with ```Global``` class every time ASP.net instantiates it.

{% highlight csharp %}
public class Global : HttpApplication
{
  public Global()
  {
    JuniorRouteApplication.AttachToHttpApplication(this);
  }
}
{% endhighlight %}
---
layout: documentation_1_0
title: DiagnosticRouteHelper
root: ../../
documentationroot: ../../documentation/1.0/
---
Diagnostics - {{ page.title }}
=
This ```DiagnosticRouteHelper``` singleton provides helper methods to retrieve routes for three different types of content:
* Spark view templates
* Stylesheets
* JavaScript

Spark View Template Routes
-
JuniorRoute is responsible for defining the layout view template used by all diagnostics views. The layout view defines two named content sections. The *head* content section may be used to render content within the ```<head>``` tag. The *script* content section may be used to render content after all body content, just before the ```</body>``` tag. Content not defined within a ```<content>``` tag renders within the ```<body>``` tag.

Retrieving a view route requires the following:
* a route name
* an abstract view model class derived from the abstract ```View``` class
* an ```IGuidFactory``` implementation for assigning an ID to the generated route
* a resolved relative URL for the generated route
* a view template
* an optional set of namespaces to be used in by the view engine. For example, to use LINQ within a view template, provide the ```System.Linq``` namespace
* an ```IHttpRuntime``` implementation used by a route [restriction]({{ page.documentationroot }})

The abstract ```View``` class exposes the following properties that may be used in view templates:
* ```Title``` renders the contents of the ```title``` tag in the layout view template
* ```UrlResolver``` must be set to an implementation of ```IUrlResolver``` and is used to resolve URLs in view templates (e.g., URLs to other diagnostics routes)
* ```AssemblyVersion``` is rendered by the layout view template

In the below example, a view model is defined, a view template is defined and a route is created. It is recommended to prefix all diagnostics route names with *Diagnostics* for disambiguation. It is also recommended to use the same resolved relative URL root for all diagnostics routes (defaults to <em>_diagnostics</em>).

{% highlight csharp %}
public abstract class CurrentTimeView : View
{
  public string CurrentTime
  {
    get;
    private set;
  }

  public void Populate(IUrlResolver urlResolver)
  {
    UrlResolver = urlResolver;
    CurrentTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
  }
}
...
const string viewTemplate = @"Current time is: ${CurrentTime}</br><a href='${UrlResolver.Route(""Diagnostics Home View"")}'>Home</a>";

DiagnosticRouteHelper.Instance.GetViewRoute<CurrentTimeView>(
  "Diagnostics Current Time",
  guidFactory,
  "_diagnostics/current_time",
  viewTemplate,
  Enumerable.Empty<string>(),
  httpRuntime,
  view => view.Populate(urlResolver));
{% endhighlight %}

A good technique for maintaining diagnostics view templates is to create external files containing the view template, include the files in a Resources object, then reference the code-generated resource property when calling ```GetViewRoute```.

Stylesheet Routes
-
Retrieving a stylesheet route requires the following:
* a route name
* an ```IGuidFactory``` implementation for assigning an ID to the generated route
* a resolved relative URL for the generated route
* the stylesheet content
* an ```IHttpRuntime``` implementation used by a route [restriction]({{ page.documentationroot }}restrictions.html)

In the below example, a stylesheet route is created.

{% highlight csharp %}
DiagnosticRouteHelper.Instance.GetStylesheetRoute(
  "Diagnostics Custom CSS",
  guidFactory,
  "_diagnostics/css/custom",
  ". custom { text-align: center; }",
  httpRuntime);
{% endhighlight %}

JavaScript Routes
-
Retrieving a JavaScript route requires the following:
* a route name
* an ```IGuidFactory``` implementation for assigning an ID to the generated route
* a resolved relative URL for the generated route
* the script content
* an ```IHttpRuntime``` implementation used by a route [restriction]({{ page.documentationroot }})

In the below example, a stylesheet route is created.

{% highlight csharp %}
DiagnosticRouteHelper.Instance.GetJavaScriptRoute(
  "Diagnostics Custom JS",
  guidFactory,
  "_diagnostics/js/custom",
  @"$(function () { alert(""Hello, world.""); });",
  httpRuntime);
{% endhighlight %}
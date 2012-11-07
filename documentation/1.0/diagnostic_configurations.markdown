---
layout: documentation_1_0
title: Diagnostic Configurations
root: ../../
documentationroot: ../../documentation/1.0/
---
Diagnostics - {{ page.title }}
=
The diagnostics home view renders a list of links grouped by headings. Developers creating custom diagnostics views are encouraged to implement diagnostic configurations in order to interface with the home view. The built-in Route Table and ASP.net Integration Information views both use diagnostic configurations.

Diagnostic configurations require the following:
* A method to retrieve routes
* A method to retrieve home view link information

In the below example, a diagnostic configuration is implemented that adds a view route with a link under the heading *Custom*:

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
public class CustomDiagnosticConfiguration : IDiagnosticConfiguration
{
  public IEnumerable<Route> GetRoutes(IGuidFactory guidFactory, IUrlResolver urlResolver, IHttpRuntime httpRuntime, string diagnosticsRelativeUrl)
  {
    const string viewTemplate = @"Current time is: ${CurrentTime}</br><a href='${UrlResolver.Route(""Diagnostics Home View"")}'>Home</a>";

    yield return DiagnosticRouteHelper.Instance.GetViewRoute<CurrentTimeView>(
      "Diagnostics Current Time",
      guidFactory,
      "_diagnostics/current_time",
      viewTemplate,
      Enumerable.Empty<string>(),
      httpRuntime,
      view => view.Populate(urlResolver));
  }

  public IEnumerable<DiagnosticViewLink> GetLinks(string diagnosticsUrl)
  {
    yield return new DiagnosticViewLink("Custom", diagnosticsUrl + "/custom", "Displays the current time");
  }
}
{% endhighlight %}

The ```DiagnosticViewLink``` class takes any format URL. In the below example, a link is created with a full URL.

{% highlight csharp %}
new DiagnosticViewLink("Temp URI", "http://tempuri.org", "Don't click this");
{% endhighlight %}

Extensibility
-
Developers may create their own diagnostic configurations by implementing the ```IDiagnosticConfiguration``` class.
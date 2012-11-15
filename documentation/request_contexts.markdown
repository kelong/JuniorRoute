---
layout: documentation
title: Request Contexts
root: ../
documentationversions: [1.0]
---
Auto-routing - {{ page.title }}
=
Request contexts provide a way to inject the ASP.net pipeline's ```HttpRequestBase``` instance into an endpoint. The ```IRequestContext``` interface insulates endpoints against coupling to the ASP.net pipeline and provides for easier unit testing.

It is recommended to use the ```RequestContext``` implementation.

In the below example, an endpoint class is declared with an IRequestContext dependency.

{% highlight csharp %}
public class Documentation
{
  private IRequestContext _context;

  public Documentation(IRequestContext context)
  {
    _context = context;
  }
}
{% endhighlight %}

Extensibility
-
Developers may create their own authentication strategies by implementing the ```IRequestContext``` interface.
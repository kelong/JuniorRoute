---
layout: documentation_1_0
title: Request Value Comparers
root: ../../
documentationroot: ../../documentation/1.0/
---
Core Concept - {{ page.title }}
=
Request value comparers allow the developer to control how a [restriction]({{ page.documentationroot }}restrictions.html)'s string-based filter is compared against HTTP request message parameters. There are four built-in singleton implementations:
* ```CaseInsensitivePlainComparer```
* ```CaseInsensitiveRegexComparer```
* ```CaseSensitivePlainComparer```
* ```CaseSensitiveRegexComparer```

Plain comparers call ```String.Equals``` with the appropriate ```StringComparison``` enumeration value. Regular expression comparers call ```Regex.IsMatch``` with the appropriate case-sensitivity options.

In the below example, a route is restricted to hosts matching a regular expression pattern.

{% highlight csharp %}
new Route("Account", Guid.NewGuid(), "account")
  .RestrictByUrlHost(@"^(?:localhost|127\.0\.0\.1)$", CaseInsensitiveRegexComparer.Instance)
{% endhighlight %}

Extensibility
-
Developers may create their own request value comparers by implementing the ```IRequestValueComparer``` interface.
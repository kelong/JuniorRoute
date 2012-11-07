---
layout: documentation_1_0
title: Bundle Watcher Routes
root: ../../
documentationroot: ../../documentation/1.0/
---
Asset Management - {{ page.title }}
=
Bundle watcher routes are [routes]({{ page.documentationroot }}routes.html) that monitor a [bundle watcher]({{ page.documentationroot }}bundle_watchers.html) for changes. When a bundle watcher's contents change, the bundle watcher route's resolved relative URL, restrictions and response are regenerated using the new bundle contents and hash.

Usually, CSS and JavaScript files are cached by browsers. Sometimes a server may contain a newer copy of a CSS or JavaScript file than the cached copy, but the browser uses the cached copy anyway. This can lead to usability problems due to stale CSS and JavaScript being used with the latest markup files. To prevent this scenario, a version query string can be appended to a bundle URL. Changes to the bundle cause the URL's version query string to change, which in turn causes the browser to request the latest contents from the server.

JuniorRoute provides two built-in implementations:
* ```CssBundleWatcherRoute```
* ```JavaScriptBundleWatcherRoute```

These two implementations set the appropriate cache policies, eTags, expiration timestamps and content types for proper client caching. They also refresh their resolved relative URL, restrictions and contents when the bundle watcher indicates a change to the bundle.

In the below example, a new CSS bundle watcher route is added to a route collection:

{% highlight csharp %}
Bundle bundle = Bundle.FromDirectory("css", searchPattern:"*.css");
var watcher = new BundleWatcher(bundle, fileSystem);
var routes = new RouteCollection
  {
    new CssBundleWatcherRoute("CSS", Guid.NewGuid(), "css", watcher, httpRuntime, systemClock)
  };
{% endhighlight %}
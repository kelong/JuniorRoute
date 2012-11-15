---
layout: documentation
title: Bundle Watchers
root: ../
documentationversions: [1.0]
---
Asset Management - {{ page.title }}
=
Bundle watchers monitor the file system for changes to a [bundle](bundles.html)'s [file and directory assets](assets.html). Bundle watchers expose the contents of the bundle and an MD5 hash of the contents.

A common technique with bundled CSS and JavaScript files is to append a version query string onto the URL representing the bundle. This version query string changes if the bundle contents change, causing cached copies of the bundle to be re-retrieved from the server. This mechanism helps prevent clients from using stale bundles that have changed on the server but have not expired in the client cache. A bundle watcher's hash changes when the contents change, allowing a [bundle watcher route](bundle_watcher_routes.html) to change its URL.

When a bundle watcher detects a change, it starts a 500 millisecond timer. If another change happens before the timer elapses, the timer is restarted. Only when the timer elapses is the content of the bundle watcher refreshed. This mechanism prevents several simultaneous file system changes from regenerating the bundle contents several times unnecessarily.

Bundle watchers expose an event that fires when the bundle contents change.

In the below example, a bundle watcher is created and monitored for changes. When changes are detected, a console message is written.

{% highlight csharp %}
var watcher = new BundleWatcher(bundle, fileSystem);

watcher.BundleChanged += () => Console.WriteLine("New hash is " + watcher.Hash);
{% endhighlight %}
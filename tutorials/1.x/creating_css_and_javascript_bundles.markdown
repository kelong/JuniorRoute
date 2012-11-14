---
layout: tutorials_1_x
title: Creating CSS and JavaScript Bundles
root: ../../
---
{{ page.title }}
=
Two common techniques for improving Web site performance are [bundling]({{ page.root }}documentation/1.x/bundles.html) and [minification]({{ page.root }}documentation/1.x/asset_transformers.html). [Auto-routing]({{ page.root }}documentation/1.x/auto_routing.html) supports both of these techniques through its fluent interface.

Step 1 - Add Assets
-
Follow the [quick-start]({{ page.root }}quick_start.html) to get a bootstrapped JuniorRoute Web application project configured. Once that's done, we'll add a few [assets]({{ page.root }}documentation/1.x/assets.html) to your project. Mimic the screenshot by adding files to your project:

![Add bundle contents](images/add-bundle-contents.png "Add bundle contents")

Populate the five new files' contents with anything you'd like, but be sure it's valid CSS and JavaScript.

Step 2 - Add a Bundle Dependency Container
-
JuniorRoute's asset classes require injection of several interface implementations. We'll use the built-in [```DefaultBundleDependencyContainer```]({{ page.root }}documentation/1.x/containers.html) implementation to keep things simple. In ```JuniorRouteConfiguration```, add the following code after the ```RestrictionContainer``` method call:

{% highlight csharp %}
.BundleDependencyContainer(new DefaultBundleDependencyContainer(httpRuntime, new FileSystem(httpRuntime)))
{% endhighlight %}

Step 3 - Add Bundles to the AutoRouteCollection
-
Now we'll add the appropriate bundles to the ```AutoRouteCollection``` in the ```JuniorRouteConfiguration``` class. Add the following code after the ```RespondWithMethodReturnValuesThatImplementIResponse``` method call:

{% highlight csharp %}
.CssBundle(
  Bundle.FromFiles(@"css/reset.css").Directory("css/folder", searchPattern:"*.css"),
  "CSS Bundle",
  "css",
  new DelimiterConcatenator(),
  new YuiCssTransformer())
.JavaScriptBundle(
  Bundle.FromDirectory("js", searchPattern:"*.js"),
  "JS Bundle",
  "js",
  new DelimiterConcatenator(),
  new YuiJavaScriptTransformer())
{% endhighlight %}

Note that directory paths are in URL format, not file system format. That is, use ```/``` characters, not ```\``` characters.

Run your project and navigate to both */css* and */js*. You should see your bundled and minified content:

{% highlight css %}
.this-is-reset-css{font-size:16px}.this-is-file1-css{text-align:center}.this-is-file2-css{margin:10px}
{% endhighlight %}

{% highlight javascript %}
function thisIsJs1(){alert("Hello, world.")};function thisIsJs2(){alert("Foo bar!")};
{% endhighlight %}

You can use a couple of different ```IUrlResolver``` methods to retrieve your bundles' absolute URLs. The first method is ```IUrlResolver.Route```; pass the name of the bundle route (in the example above, the names are *CSS Bundle* and *JS Bundle*.) The second method is ```IUrlResolver.Absolute```; pass the relative path of the bundle route (in the example above, the relative paths are *css* and *js*.)
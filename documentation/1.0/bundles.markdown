---
layout: documentation_1_0
title: Bundles
root: ../../
documentationroot: ../../documentation/1.0/
---
Asset Management - {{ page.title }}
=
A bundle consists of the following concepts:
* A collection of assets, such as [file and directory assets]({{ page.documentationroot }}assets.html)
* A way to retrieve the assets as a single entity

After adding assets to a bundle, developers retrieve the contents of the bundle by providing the following:
* A [file system]({{ page.documentationroot }}file_systems.html) implementation
* An optional asset-ordering ```IComparer```
* An optional asset [concatenator]({{ page.documentationroot }}asset_concatenators.html)
* Optional asset [transformers]({{ page.documentationroot }}asset_transformers.html)

The bundle resolves all provided file and directory assets, ordering them using the provided comparer, if any. If no comparer is provided, the assets are ordered by the order in which they are resolved. Once resolved, the files' contents are retrieved from the file system, transformed individually, then concatenated together. If transformers are provided, transformers are called in the order provided with outputs being passed to the each successive transformer. If no concatenator is provided then the assets are concatenated successively with no delimiter between each assets' concents.

In the below example, a bundle is created with several assets. Then, the bundle contents are retrieved.

{% highlight csharp %}
new Bundle()
  .File("css/common/reset.css")
  .Files("css/common/common.css", "css/common/views.css")
  .Directory("css/account_view", searchPattern:"*.css")
  .GetContents(fileSystem, new CustomCssConcatenator(), new CustomCssTransformer());
{% endhighlight %}

There may be times when the bundle is unable to read the contents of a file due to permissions, file-in-use exceptions, etc. To support [bundle watchers]({{ page.documentationroot }}bundle_watchers.html), the ```Bundle``` class attempts to read the file several times until a specified timeout elapses. Each read attempt is separated by a delay of 250 milliseconds. The file read timeout defaults to 30 seconds.

In the below example, a file read timeout of 10 seconds is provided.

{% highlight csharp %}
new Bundle(TimeSpan.FromSeconds(10));
{% endhighlight %}
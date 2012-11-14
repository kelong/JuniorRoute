---
layout: documentation_1_x
title: Assets
root: ../../
documentationversions: [1.0]
---
Asset Management - {{ page.title }}
=
Web sites are typically deployed with so-called "asset" files such as cascading style sheets (CSS) and JavaScript (JS). For performance and bandwidth reasons, Web site developers use techniques known as bundling and minification on CSS and JS assets. However, before those techniques may be employed, the file system assets themselves must be represented.

JuniorRoute implements two kinds of assets:
* File
* Directory

File Assets
-
A file asset consists of these concepts:
* A file path relative to the root application directory
* An optional encoding

In the below example, a file asset is created that represents a CSS asset with a UTF-8 encoding.

{% highlight csharp %}
new FileAsset(@"stylesheets\common.css", Encoding.UTF8);
{% endhighlight %}

In the below example a file asset is created that represents a CSS asset with the default encoding.

{% highlight csharp %}
new FileAsset(@"stylesheets\common.css");
{% endhighlight %}

Directory Assets
-
A directory asset consists of these concepts:
* A directory relative to the root application directory
* A file search pattern
* An option to include subfolders as part of the asset
* An optional file filter

In the below example, a directory asset is created that represents a set of JavaScript assets with the default encoding and performs a recursive search.

{% highlight csharp %}
new DirectoryAsset("js", searchPattern:"*.js", option:SearchOption.AllDirectories)
{% endhighlight %}

File Filters
-
When directory assets are resolved to file assets, the file filter, if specified, is used to control which files are included in the output. By default, no file filter is specified, so all files matching the search pattern are included in the output.

In the below example, a custom file filter filters out files whose names contain *exclude*.

{% highlight csharp %}
public class ExcludeFileFilter : IFileFilter
{
  public FilterResult Filter(string path)
  {
    return path.IndexOf("exclude", StringComparison.OrdinalIgnoreCase) > -1 ? FilterResult.Exclude : FilterResult.Include;
  }
}
...
new DirectoryAsset("css", searchPattern:"*.css", filter:new ExcludeFileFilter());
{% endhighlight %}

JuniorRoute contains no built-in implementations of ```IFileFilter```.

Extensibility
-
Developers may create their own file-system-based asset classes by implementing the ```IAsset``` interface.

Developers may create their own file filters by implementing the ```IFileFilter``` interface.
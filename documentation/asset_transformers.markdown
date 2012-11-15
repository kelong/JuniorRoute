---
layout: documentation
title: Asset Transformers
root: ../
documentationversions: [1.0]
---
Asset Management - {{ page.title }}
=
Asset transformers take an asset's contents and return a transformed version of those contents. When retrieving [bundle](bundles.html) contents, asset transformers may optionally be provided. JuniorRoute's YuiCompressor assembly provides asset transformers that use the [YUI Compressor for .Net](http://yuicompressor.codeplex.com/) library.

In the below example, an asset transformer implementation inserts a block comment at the beginning of the contents.

{% highlight csharp %}
public class CommentAdditionTransformer : IAssetTransformer
{
  public string Transform(string assetContents)
  {
    return "/* This file is copyright 2012. */" + assetContents;
  }
}
{% endhighlight %}

Extensibility
-
Developers may create their own asset transformers by implementing the ```IAssetTransformer``` interface.
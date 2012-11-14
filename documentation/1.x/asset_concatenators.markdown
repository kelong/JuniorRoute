---
layout: documentation_1_x
title: Asset Concatenators
root: ../../
documentationversions: [1.0]
---
Asset Management - {{ page.title }}
=
When retrieving [bundle](bundles.html) contents, an asset concatenator may optionally be provided. Asset concatenators take a set of [transformed]((asset_transformers.html) asset contents and combine them into a single entity.

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

JuniorRoute provides the built-in ```DelimiterConcatenator``` implementation.

DelimiterConcatenator
-
This concatenator calls ```String.Join``` on the asset contents with the provided delimiter.

In the below example, a ```DelimiterConcatenator``` instance is configured with a semi-colon delimiter.

{% highlight csharp %}
new DelimiterConcatenator(";");
{% endhighlight %}

Extensibility
-
Developers may create their own asset concatenators by implementing the ```IAssetConcatenator``` interface.
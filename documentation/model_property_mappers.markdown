---
layout: documentation
title: Model Property Mappers
root: ../
documentationversions: [1.0]
---
Auto-routing - {{ page.title }}
=
Model property mappers map HTTP request message values to properties of custom model types. Model property mappers are used by [```ModelMapper```](response_mappers.html) to map model type properties after they are instantiated.

Model property mappers have two responsibilites:
* determining if a particular property type can be mapped by the model property mapper implementation
* mapping a HTTP request message value to a property

Only public instance properties are eligible for mapping.

Model property mappers are called until one of them maps the property. If no model property mappers map a property, an exception is thrown.

JuniorRoute provides five built-in implementations:
* ```DefaultValueMapper```
* ```FormToIConvertibleMapper```
* ```QueryStringToIConvertibleMapper```

DefaultValueMapper
-
This mapper maps a property value to its type's default value. For reference types, the value is ```null``` and for value types, the value is ```default(T)```.

In the below example, a ```DefaultValueMapper``` is instantiated.

{% highlight csharp %}
new DefaultValueMapper();
{% endhighlight %}

FormToIConvertibleMapper
-
This mapper maps form values supplied in the HTTP request message to properties with the same name as the form field and also with properties types implementing ```IConvertible```. The name matching is optionally case-sensitive. A constructor parameter provides a choice of throwing an exception or mapping the default value if a name match is found but the form value cannot be converted.

In the below example, a ```FormToIConvertibleMapper``` instance is configured to map form fields case-insensitively and throw an exception if a conversion fails.

{% highlight csharp %}
new FormToIConvertibleMapper(true, DataConversionErrorHandling.ThrowException);
{% endhighlight %}

QueryStringToIConvertibleMapper
-
This mapper maps query string fields supplied in the HTTP request message to properties with the same name as the query string field and also with properties types implementing ```IConvertible```. The name matching is optionally case-sensitive. A constructor parameter provides a choice of throwing an exception or mapping the default value if a name match is found but the query string value cannot be converted.

In the below example, a ```QueryStringToIConvertibleMapper``` instance is configured to map query string fields case-insensitively and throw an exception if a conversion fails.

{% highlight csharp %}
new QueryStringToIConvertibleMapper(true, DataConversionErrorHandling.ThrowException);
{% endhighlight %}

Extensibility
-
Developers may create their own model property mappers by implementing the ```IModelPropertyMapper``` interface.
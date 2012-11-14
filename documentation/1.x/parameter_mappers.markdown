---
layout: documentation_1_x
title: Parameter Mappers
root: ../../
documentationversions: [1.0]
---
Auto-routing - {{ page.title }}
=
Parameter mappers map HTTP request message values to endpoint method parameters. Parameter mappers are used by [```ResponseMethodReturnTypeMapper```](response_mappers.html) to pass parameter values to endpoint methods when they are invoked.

Parameter mappers have two responsibilites:
* determining if a particular parameter type can be mapped by the parameter mapper implementation
* mapping a HTTP request message value to a parameter

Parameter mappers are called until one of them maps the parameter. If no parameter mappers map a parameter, an exception is thrown.

JuniorRoute provides five built-in implementations:
* ```DefaultValueMapper```
* ```FormToIConvertibleMapper```
* ```JsonModelMapper```
* ```ModelMapper```
* ```QueryStringToIConvertibleMapper```

DefaultValueMapper
-
This mapper maps a parameter value to its type's default value. For reference types, the value is ```null``` and for value types, the value is ```default(T)```.

In the below example, a ```DefaultValueMapper``` is instantiated.

{% highlight csharp %}
new DefaultValueMapper();
{% endhighlight %}

FormToIConvertibleMapper
-
This mapper maps form values supplied in the HTTP request message to parameters with the same name as the form field and also with parameter types implementing ```IConvertible```. The name matching is optionally case-sensitive. A constructor parameter provides a choice of throwing an exception or mapping the default value if a name match is found but the form value cannot be converted.

In the below example, a ```FormToIConvertibleMapper``` instance is configured to map form fields case-insensitively and throw an exception if a conversion fails.

{% highlight csharp %}
new FormToIConvertibleMapper(true, DataConversionErrorHandling.ThrowException);
{% endhighlight %}

JsonModelMapper
-
This mapper maps the JSON body of a HTTP request message to a custom model type by using Json.NET. One of the constructor parameters is a delegate that determines which parameter types can be mapped by the ```JsonModelMapper``` instance. Developers may choose to use the constructor overloads that do not take this delegate; in that case, parameter types whose names end with *Model* are eligible for mapping. Developers may optionally provide a ```JsonSerializerSettings``` instance to control how Json.NET deserializes the request body. A constructor parameter provides a choice of throwing an exception or mapping the default value if a type match is found but the request body cannot be converted.

In the below example, a ```JsonModelMapper``` instance is configured to map request bodies for types whose names end with *JsonModel*.

{% highlight csharp %}
new JsonModelMapper(type => type.Name.EndsWith("JsonModel"));
{% endhighlight %}

ModelMapper
-
This mapper maps a HTTP request message to a custom mode type by using the specified [model property mappers](model_property_mappers.html). One of the constructor parameters is a delegate that determines which parameter types can be mapped by the ```ModelMapper``` instance. Developers may choose to use the constructor overloads that do not take this delegate; in that case, parameter types whose names end with *Model* are eligible for mapping. Developers may optionally provide an [```IContainer```](containers.html) implementation that will be used to get instances of the model types. If no container is provided, the types are created with ```Activator.CreateInstance```.

In the below example, a ```ModelMapper``` instance is configured to map requests for types whose naems end with *CustomModel*. Types are instantiated using a container. A ```FormToIConvertibleMapper``` is used as the only parameter mapper.

{% highlight csharp %}
new ModelMapper(container, type => type.Name.EndsWith("CustomModel"));
{% endhighlight %}

QueryStringToIConvertibleMapper
-
This mapper maps query string fields supplied in the HTTP request message to parameters with the same name as the query string field and also with parameter types implementing ```IConvertible```. The name matching is optionally case-sensitive. A constructor parameter provides a choice of throwing an exception or mapping the default value if a name match is found but the query string value cannot be converted.

In the below example, a ```QueryStringToIConvertibleMapper``` instance is configured to map query string fields case-insensitively and throw an exception if a conversion fails.

{% highlight csharp %}
new QueryStringToIConvertibleMapper(true, DataConversionErrorHandling.ThrowException);
{% endhighlight %}

Extensibility
-
Developers may create their own parameter mappers by implementing the ```IParameterMapper``` interface.
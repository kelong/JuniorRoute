---
layout: documentation_1_0
title: Route Collections
root: ../../
documentationroot: ../../documentation/1.0/
---
Core Concept - {{ page.title }}
=
Route collections encapsulate a grouping of [routes]({{ page.documentationroot }}routes.html).

Route collections are used by the [```AspNetHttpHandler```]({{ page.documentationroot }}aspnethttphandler.html) and [```AutoRouteCollection```]({{ page.documentationroot }}autoroutecollection.html) classes.

JuniorRoute provides the built-in ```RouteCollection``` implementation.

RouteCollection
-
This collection uses a constructor-provided rule that controls whether duplicate route names are allowed. This collection also enforces that no two routes have the same ID.

Extensibility
-
Developers may create their own route collections by implementing the ```IRouteCollection``` interface.
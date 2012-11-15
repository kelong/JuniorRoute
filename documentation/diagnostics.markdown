---
layout: documentation
title: Diagnostics
root: ../
documentationversions: [1.0]
---
{{ page.title }}
=
JuniorRoute provides a built-in diagnostics framework. JuniorRoute also provides two assemblies that use the framework to render a route table visualization and ASP.net integration information.

A principle of the diagnostics framework is that regular [routes](routes.html) are used for all diagnostics content. There are no special hacks required to add diagnostics support to a JuniorRoute Web application; simply add the diagnostics routes you wish to a [route collection](route_collections.html).

The diagnostics pages are rendered using [Spark View Engine](http://sparkviewengine.com/) with an in-memory view folder. As a result, there are no external files or dependencies necessary to render diagnostics views, except for the Spark assembly and DLLs on which it depends.

All diagnostics views are rendered with jQuery 1.8.2 support.
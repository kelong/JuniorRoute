---
layout: documentation
title: Features and Benefits
---
{{ page.title }}
=
<br/>
Helpful Project Templates
-
How easy is it to bootstrap a JuniorRoute project? How about just <span class="get-started">three simple steps</span>! See the [quick-start]({{ page.root }}quick_start.html) page to get started.

Simplified Architecture
-
.NET MVC frameworks can be bloated or cumbersome. Why not ditch the complexity for the simplicity of a framework that integrates with HTTP more closely? JuniorRoute's [front controller](http://en.wikipedia.org/wiki/Front_Controller_pattern) maps a HTTP request message to a code method without base class requirements or complex context objects.

Convention-driven Automatic Route Generation
-
[Auto-routing](documentation/auto_routing.html) skips tedious and error-prone [route](documentation/routes.html)-by-route configuration in favor of automatic route generation using conventions. Once conventions are established, simply add new endpoint classes and methods to create new routes. It couldn't be simpler!

Bundling and Minification
-
[Bundling](documentation/bundles.html) and [minification](documentation/asset_transformers.html) are requirements for today's high-performance and bandwidth-conserving Web applications. Auto-routing exposes a simple fluent interface for configuring your bundles and integrates with YUICompressor .NET for reliable minification of your CSS and JavaScript assets.

IoC-ready
-
Are you awesome with Autofac? Super with StructureMap? JuniorRoute is built from the ground up to support your favorite IoC container. Inject dependencies into an endpoint class constructor and JuniorRoute will provide them using your configured container.

Modular Design
-
A single NuGet package often forces you into corners with regards to unnecessary dependencies. JuniorRoute's many [NuGet packages](http://nuget.org/packages?q=JuniorRoute) enable you to use only the features you want while keeping dependencies to a minimum.

No ASP.NET MVC Dependencies
-
JuniorRoute does not use any part of the ASP.NET MVC framework.

Open-source
-
Learn exactly what makes JuniorRoute tick by browsing the [source code](https://github.com/NathanAlden/JuniorRoute) or [fork](https://github.com/NathanAlden/JuniorRoute/fork) the repository to add your own features! Collaboration and pull-requests are always welcome!

Robust Documentation
-
JuniorRoute's concepts and classes are well-documented--something that can't be said for some other Web frameworks.

Unit-tested and NUnit-approved
-
JuniorRoute's codebase contains over 1000 unit tests to ensure a level of quality you can count on.
---
layout: documentation
title: Compiled Template Factories
root: ../../
documentationversions: [1.0]
---
Razor View Engine - {{ page.title }}
=
Compiled template factories are responsible for creating instances of compiled [template](templates.html) types. JuniorRoute provides a single built-in implementation called ```ActivatorFactory```. ```ActivatorFactory``` creates instances of types by calling the ```Activator.CreateInstance``` method.
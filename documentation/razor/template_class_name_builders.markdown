---
layout: documentation
title: Template Class Name Builders
root: ../../
documentationversions: [1.0]
---
Razor View Engine - {{ page.title }}
=
Template class name builders generate class names for compiled [templates](templates.html). JuniorRoute provides a single built-in implementation called ```RandomGuidBuilder```. ```RandomGuidBuilder``` generated class names using random GUIDs prepended by underscores.
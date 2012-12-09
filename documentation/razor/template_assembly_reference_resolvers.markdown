---
layout: documentation
title: Template Assembly Reference Resolvers
root: ../../
documentationversions: [1.0]
---
Razor View Engine - {{ page.title }}
=
Template assembly reference resolvers provide assembly references to in-memory assemblies containing compiled templates. JuniorRoute provides a single built-in implementation called ```AppDomainResolver```. ```AppDomainResolver``` returns all non-dynamic assemblies from the current app-domain.
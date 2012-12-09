---
layout: documentation
title: Template Path Resolvers
root: ../../
documentationversions: [1.0]
---
Razor View Engine - {{ page.title }}
=
Template path resolvers resolve a template's relative path to an absolute path. JuniorRoute provides two built-in implementations:
* CSharpResolver
* VisualBasicResolver

CSharpResolver
-
This implementation uses an ```IHttpRuntime``` implementation to resolve the relative path to an absolute path. If the relative path does not have a file extension, an extension of *.cshtml* is appended.

VisualBasicResolver
-
This implementation uses an ```IHttpRuntime``` implementation to resolve the relative path to an absolute path. If the relative path does not have a file extension, an extension of *.vbhtml* is appended.
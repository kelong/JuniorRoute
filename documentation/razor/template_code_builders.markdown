---
layout: documentation
title: Template Code Builders
root: ../../
documentationversions: [1.0]
---
Razor View Engine - {{ page.title }}
=
Template code builders use Razor to generate a ```CodeCompileUnit``` object representing a template class to be compiled. JuniorRoute provides a built-in abstract implementation called ```TemplateCodeBuilder```.  Two built-in classes deriving ```TemplateCodeBuilder``` are provided:
* CSharpBuilder
* VisualBasicBuilder
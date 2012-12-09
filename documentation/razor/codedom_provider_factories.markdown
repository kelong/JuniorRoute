---
layout: documentation
title: CodeDOM Provider Factories
root: ../../
documentationversions: [1.0]
---
Razor View Engine - {{ page.title }}
=
CodeDOM provider factories return the appropriate ```CodeDomProvider``` implementation for a given file extension. JuniorRoute's only built in implementation, ```FileExtensionFactory```, returns a ```CSharpCodeProvider``` instance if the extension is *.cshtml* or a ```VBCodeProvider``` instance if the extension is *.vbhtml*.
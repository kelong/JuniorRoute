---
layout: documentation
title: Version History
root: ../
---
{{ page.title }}
=
<br/>
February 23, 2013
-
<br/>
### Razor View Engine 2.0.3
<ul><li>Added missing WriteAttribute implementating to Razor template base class</li></ul>

### Razor View Engine 2.0.2
<ul><li>Corrected exception message that did not contain generic type parameter</li></ul>
-
January 27, 2013
-
<br/>
### Razor View Engine 2.0.1
* Fixed bug where loaded assemblies with no location were causing exceptions when building Razor templates
* Fixed bug where globally-qualified namespaces were causing VB template compilation to fail

### Razor View Engine 2.0.0
* Added support for layouts, sections and includes
* Fixed a critical bug in ```TemplateCodeBuilder``` where only the first template type implementation would be built correctly

### Razor View Engine - Routing Integration 2.0.1
<ul><li><code>ITemplateRepository</code> and <code>FileSystemRepository</code> can now retrieve templates in addition to executing them</li></ul>

### Assets - YUICompressor .NET 2.1.1
<ul><li>Updated YUICompressor .NET to 2.2.0</li></ul>
-
December 2, 2012
-
<br/>
### General
* Existing Visual Studio project templates updated to use latest packages
* Two new Visual Studio project templates created to support Razor view engine integration
* Updated NUnit to 2.6.2
* Updated DotSettings file to use ReSharper 7.1 settings
* Removed old RazorEngine implementation

### ASP.net Integration 2.0.0
* Major version update due to breaking change in dependency
* Added missing paramName parameters to some uses of ```ArgumentException```

### ASP.net Integration Diagnostics 2.0.0
* Major version update due to breaking change in dependency
* Updated Spark to 1.7.5

### Assets 2.0.0
* Major version update due to breaking change in dependency

### Assets - YUICompressor .NET 2.0.0
* Major version update due to breaking change in dependency

### Auto-routing 2.0.0
* Major version update due to breaking change in dependency
* Updated Json.NET to 4.5.11

### Core 2.0.0
* HashSetExtensions is now SetExtensions; extends ```ISet<>``` instead of ```HashSet```

### Core Diagnostics 2.0.0
* Major version update due to breaking change in dependency
* Updated Spark to 1.7.5

### Diagnostics 2.0.0
* Major version update due to breaking change in dependency
* Updated Spark to 1.7.5

### Razor View Engine 1.0.1
* Initial release

### Razor View Engine - Routing Integration 1.0.1
* Initial release

November 8, 2012
-
<br/>
### General
* Visual Studio project templates completed

November 6, 2012
-
<br/>
### General
* Initial release
* NuGet packages completed
* Documentation completed
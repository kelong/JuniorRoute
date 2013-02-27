---
layout: documentation
title: Version History
root: ../
---
{{ page.title }}
=
<br/>
February 26, 2013
-
<br/>
### General
* All assemblies now target .NET Framework 4.5

### ASP.net Integration 3.0.0
* Major version update due to breaking change in dependency
* Updated JuniorCommon to 3.0.0

### ASP.net Integration Diagnostics 3.0.0
* Major version update due to breaking change in dependency
* Updated JuniorCommon to 3.0.0

### Assets 3.0.0
* Major version update due to breaking change in dependency
* Updated JuniorCommon to 3.0.0

### Assets - YUICompressor .NET 3.0.0
* Major version update due to breaking change in dependency
* Updated JuniorCommon to 3.0.0

### Auto-routing 3.0.0
* Major version update due to breaking change in dependency
* Updated JuniorCommon to 3.0.0

### Core 3.0.0
* Major version update due to breaking change in dependency
* Updated JuniorCommon to 3.0.0

### Core Diagnostics 3.0.0
* Major version update due to breaking change in dependency
* Updated JuniorCommon to 3.0.0
* Updated Spark to 1.7.5.1

### Diagnostics 3.0.0
* Major version update due to breaking change in dependency
* Updated JuniorCommon to 3.0.0
* Updated Spark to 1.7.5.1

### Razor View Engine 3.0.0
* Major version update due to breaking change in dependency
* Updated JuniorCommon to 3.0.0

### Razor View Engine - Routing Integration 4.0.0
* Major version update due to breaking change in dependency
* Updated JuniorCommon to 3.0.0

-
February 23, 2013
-
<br/>
### Razor View Engine 2.0.3
* Added missing WriteAttribute implementating to Razor template base class

### Razor View Engine 2.0.2
* Corrected exception message that did not contain generic type parameter

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
* <code>ITemplateRepository</code> and <code>FileSystemRepository</code> can now retrieve templates in addition to executing them

### Assets - YUICompressor .NET 2.1.1
* Updated YUICompressor .NET to 2.2.0

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

-
November 8, 2012
-
<br/>
### General
* Visual Studio project templates completed

-
November 6, 2012
-
<br/>
### General
* Initial release
* NuGet packages completed
* Documentation completed
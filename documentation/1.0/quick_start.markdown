---
layout: documentation_1_0
title: Quick Start
root: ../../
documentationroot: ../../documentation/1.0/
---
{{ page.title }}
=
Getting started with JuniorRoute is very easy. This page shows you the few simple steps required to get JuniorRoute integrated with an ASP.net Web application.

Notes:
* This tutorial was written using Visual Studio 2012 with C#, so some steps may vary depending on your Visual Studio version and language selection.
* JuniorRoute requires integrated pipeline mode. By default, Visual Studio 2010 Web projects use the Visual Studio Development Server, which does not support an integrated pipeline. Those using Visual Studio 2010 must change the JuniorRoute Web application to use IIS Express by editing the appropriate settings under the Web tab in the project's properties.

Step 1 - Install the Project Templates Extension
-
The Visual Studio Gallery hosts an extension that will install JuniorRoute project templates into Visual Studio. The extension is compatible with Visual Studio 2010 and Visual Studio 2012. Both C# and Visual Basic.NET project templates are provided.

Follow these steps:
1. Under the Tools menu, choose Extensions and Updates
2. Click the Online category in the tree
3. Press CTRL+E to highlight the search box
4. Type *JuniorRoute* and press ENTER
5. Click Download and accept the license agreement
6. Click Close to close the Extensions and Updates dialog

![Install extension]({{ page.documentationroot }}images/install-extension.png "Install extension")

Step 2 - Create a New JuniorRoute Application
-
1. Under the File menu, select New, then Project
2. Under the Templates category, select either Visual C# or Visual Basic (for the purposes of this tutorial, Visual C# will be used)
3. Select the Web category
4. Enter a project name and click OK

![New project]({{ page.documentationroot }}images/new-project.png "New project")

Step 3 - Run Your Application
-
Run your project. It's normal to see a 404 because initially there are no routes defined for the root path. Navigate to */_diagnostics* and you should see the diagnostics page.

![Diagnostics]({{ page.documentationroot }}images/diagnostics.png "Diagnostics")

**That's it! You're done!**
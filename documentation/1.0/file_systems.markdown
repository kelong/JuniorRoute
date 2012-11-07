---
layout: documentation_1_0
title: FileSystem
root: ../../
documentationroot: ../../documentation/1.0/
---
ASP.net Integration - {{ page.title }}
=
JuniorRoute provides the built-in ```FileSystem``` implementation. ```FileSystem``` requires an ```IHttpRuntime``` implementation to determine the root directory of the application (e.g., C:\Inetpub\wwwroot\MyApplication matching a MyApplication virtual directory in IIS).

```FileSystem``` encapsulates various calls to .NET IO APIs.

```IFileSystem``` exists to make unit testing easier. The ```FileSystem``` implementation should be used most of the time.
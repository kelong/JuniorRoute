---
layout: documentation_1_0
title: Routes
root: ../../
documentationroot: ../../documentation/1.0/
---
Core Concept - {{ page.title }}
=
A route is the means by which a HTTP request message is handled by code. Routes consist of the following concepts:
* A name
* An ID
* A resolved relative URL
* [Restrictions]({{ page.documentationroot }}restrictions.html)
* An [authentication provider]({{ page.documentationroot }}authentication_providers.html)
* A [response]({{ page.documentationroot }}responses.html)

Additionally, route matching is how a route determines if it should handle a HTTP message. A route compares the HTTP message with its list of restrictions. If all the restrictions are met, the route matches.

Name
-
Route names are intended to be human-readable and should usually be unique. They are useful when identifying, at-a-glance, a route in code.

ID
-
Route IDs are GUIDs that must be unique when added to a [route collection]({{ page.documentationroot }}route_collections.html). Whereas routes may share the same name within the collection, they may never share the same ID. This guarantees a way to reference a specific route within the collection.

Resolved Relative URL
-
A route's resolved relative URL is the URL that is returned when using a [URL resolver]({{ page.documentationroot }}url_resolvers.html) to resolve a route's relative URL to an absolute one. All routes must have a resolved relative URL.

The term *relative* means relative to the application's root URL. For example, if an IIS application using JuniorRoute lives at http://localhost/MyApplication, a relative URL is relative to that path. A route with a resolved relative URL of *about* resolves to an absolute URL of */MyApplication/about*. Resolved relative URLs make it easy to host an application with any root URL without having to change the routes themselves.

Restrictions
-
Restrictions determine if a particular route is allowed to handle a particular HTTP request message. JuniorRoute comes with many restrictions built-in.

Authentication Provider
-
A route may be provided with an authentication provider. When a route is chosen to handle an HTTP request message, JuniorRoute first allows the route to authenticate the request using its authentication provider, if it has one.

Response
-
Once a route has been chosen and authenticated, its associated response is sent to the client. Developers may either directly provide a response, or they may provide a delegate to be called only when the response is needed. The delegate mechanism is useful in scenarios where the time the response is generated is important, or there are dependencies needed by the response that are not available at the time the route is created.

Fluent Interface
-
The ```Route``` class provides fluent-interface methods to make configuration easier. In most cases, use of the fluent interface is additive, meaning it does not overwrite previous calls. Use of fluent-interface methods is recommended.
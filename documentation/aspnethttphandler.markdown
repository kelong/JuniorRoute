---
layout: documentation
title: AspNetHttpHandler
root: ../
documentationversions: [1.0]
---
ASP.net Integration - {{ page.title }}
=
Hooking into the ASP.net pipeline requires an implementation of ```IHttpHandler```. JuniorRoute provides the built-in ```AspNetHttpHandler``` implementation to accommodate that requirement. ```AspNetHttpHandler``` requires the following:
* A [route collection](route_collections.html)
* A cache implementation
* A set of [response generators](response_generators.html)
* A set of [response handlers](response_handlers.html)

```AspNetHttpHandler``` is a [front controller](http://en.wikipedia.org/wiki/Front_Controller_pattern); it handles all incoming HTTP requests to the Web application. HTTP requests are handled in three phases:
* Route matching
* Response generation
* Response handling

Route Matching
-
In the route matching phase, all routes in the route collection are matched against the HTTP request message. The results of every route match operation are supplied to the next phase.

Response Generation
-
In the response generation phase, the provided response generators are called in succession until one of them generates a [response](responses.html). During this phase, responses are merely suggestions; no response is actually written to the ASP.net pipeline. Responses may be generated even if no routes matched the HTTP request message; in fact, it is desirable to generate a 404 Not Found response when no routes match. If no response generators generate a response, an exception is thrown. The results of a successful response generation are supplied to the next phase.

Response Handling
-
In the response handling phase, the provided response handlers are called in succession until one of them writes the response to the ASP.net pipeline. Response handlers may choose to suggest a new response in place of the generated response; in this case, subsequent response handlers receive the suggested response. If no response handler writes a response to the ASP.net pipeline, an exception is thrown.
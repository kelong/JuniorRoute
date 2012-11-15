---
layout: documentation
title: Understanding HTTP 1.1
---
{{ page.title }}
=
Understanding [RFC 2616](http://www.w3.org/Protocols/rfc2616/rfc2616.html) is important to use JuniorRoute effectively. Certain aspects of the specification, especially caching, can be quite confusing and require study in order to correctly use the techniques. It is important to follow the specification as closely as possible, lest the Web become fragmented.

In most cases, JuniorRoute attempts to follow the specification as closely as possible. One notable omission from JuniorRoute is the use of Vary headers to control the server caching mechanism. At the time of this writing, JuniorRoute ignores all Vary headers with regards to caching.

For developer convenience, JuniorRoute includes classes that encapsulate all HTTP request headers. Developers interested in detailed header specifications should refer to [RFC 2616 Section 5.3](http://www.w3.org/Protocols/rfc2616/rfc2616-sec5.html#sec5.3).

Developers planning on using JuniorRoute's caching capabilities should refer to [RFC 2616 Section 13](http://www.w3.org/Protocols/rfc2616/rfc2616-sec13.html).
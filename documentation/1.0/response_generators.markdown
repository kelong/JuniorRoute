---
layout: documentation_1_0
title: Response Generators
root: ../../
documentationroot: ../../documentation/1.0/
---
ASP.net Integration - {{ page.title }}
=
Response generators analyze a set of route match results and optionally return a [response]({{ page.documentationroot }}responses.html). It's possible for response generators to handle multiple matching routes, in addition to the more common scenarios of a single matching route or no matching routes.

In the below example, a response generator is defined that generates a response containing the number of route match results.

{% highlight csharp %}
public class Generator : IResponseGenerator
{
  public ResponseResult GetResponse(HttpRequestBase request, IEnumerable<RouteMatchResult> routeMatchResults)
  {
    Response response = Response
      .OK()
      .TextPlain()
      .Content(routeMatchResults.Count().ToString(CultureInfo.InvariantCulture));

    return ResponseResult.ResponseGenerated(response);
  }
}
{% endhighlight %}

JuniorRoute provides three built-in implementations:
* ```MostMatchingRestrictionsGenerator```
* ```UnmatchedRestrictionsGenerator```
* ```NotFoundGenerator```

In most cases, these three generators should be provided to [```AspNetHttpHandler```]({{ page.documentationroot }}aspnethttphandler.html) in the order listed here.

MostMatchingRestrictionsGenerator
-
This implementation determines the route that best matched the HTTP request message. A "best match" is defined as the route with the most matched [restrictions]({{ page.documentationroot }}restrictions.html) and no unmatched restrictions.
Responses are generated in the following cases:
* if more than one route share the same number of matching restrictions, a 300 Multiple Choices response is generated
* if a single route had the most matching restrictions and the route failed authentication, a 401 Unauthorized is generated
* if a single route had the most matching restrictions and the route passed authentication, the route's response is returned

If the route returned a null response, an exception is thrown. If no route had all its restrictions match, no response is generated.

UnmatchedRestrictionsGenerator
-
This implementation generates responses in the case where no routes had all their restrictions match. The HTTP 1.1 specification defines several rules for when the Web server is unable to determine how to respond; JuniorRoute attempts to implement those rules.

For a route to be considered by this implementation, it must have matched at least one ```UrlRelativePathRestriction``` and must not have an unmatched ```UrlRelativePathRestriction```.

First, the route with the fewest unmatched restrictions is determined. If more than one route share the fewest unmatched restrictions, no response is generated. If a single route had the fewest number of unmatched restrictions, responses are generated in the following cases:
* if any ```MethodRestriction```s were unmatched, a 405 Method Not Allowed is generated with the appropriate Allow header
* if any ```HeaderRestriction<AcceptHeader>```s were unmatched, a 406 Not Acceptable is generated
* if any ```HeaderRestriction<AcceptCharsetHeader>```s were unmatched, a 406 Not Acceptable is generated
* if any ```HeaderRestriction<AcceptEncodingHeader>```s were unmatched, a 406 Not Acceptable is generated
* if any ```HeaderRestriction<ContentEncodingHeader>```s were unmatched, a 415 Unsupported Media Type is generated

NotFoundGenerator
-
This implementation always generates a 404 Not Found.

Extensibility
-
Developers may create their own response generators by implementing the ```IResponseGenerator``` interface.
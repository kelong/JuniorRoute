---
layout: documentation_1_0
title: Forms Authentication
root: ../../
documentationroot: ../../documentation/1.0/
---
Auto-routing - {{ page.title }}
=
In addition to [```FormsAuthenticationProvider```]({{ page.documentationroot }}authentication_providers.html), JuniorRoute provides two useful interfaces and implementations for working with forms authentication:
* ```IFormsAuthenticationData```, with its ```FormsAuthenticationData``` implementation
* ```IFormsAuthenticationHelper```, with its ```FormsAuthenticationHelper``` implementation

The interfaces are provided for dependency injection purposes. It is recommended to use the built-in implementations.

FormsAuthenticationData
-
This class deserializes the forms authentication ticket's JSON user data, if any, to the specified type.

In the below example, data is retrieved from within an endpoint method.

{% highlight csharp %}
public class Account
{
  private readonly IFormsAuthenticationData<dynamic> _formsAuthenticationData;

  public Account(IFormsAuthenticationData<dynamic> formsAuthenticationData)
  {
    _formsAuthenticationData = formsAuthenticationData;
  }

  public HtmlResponse Get()
  {
    dynamic data = _formsAuthenticationData.GetUserData();
    ...
  }
}
{% endhighlight %}

IFormsAuthenticationHelper
-
This class generates and removes forms authentication tickets and cookies, as well as optionally serializing user data to JSON for storage within the ticket. Developers may choose ticket expiration time, whether the ticket is persistent, the cookie name and the cookie path. Ticket removal is facilitated by ```FormsAuthentication.SignOut```.

In the below example, a ticket is created.

{% highlight csharp %}
public class LogIn
{
  private readonly IFormsAuthenticationHelper _formsAuthenticationHelper;
  private readonly ISystemClock _systemClock;

  public LogIn(IFormsAuthenticationHelper formsAuthenticationHelper, ISystemClock systemClock)
  {
    _formsAuthenticationHelper = formsAuthenticationHelper;
    _systemClock = systemClock;
  }

  public void Post(string username, string password)
  {
    _formsAuthenticationHelper.GenerateTicket(_systemClock.UtcDateTime, new { username });
  }
}
{% endhighlight %}

In the below example, a ticket is removed.

{% highlight csharp %}
public class LogOut
{
  private readonly IFormsAuthenticationHelper _formsAuthenticationHelper;

  public LogOut(IFormsAuthenticationHelper formsAuthenticationHelper)
  {
    _formsAuthenticationHelper = formsAuthenticationHelper;
  }

  public void Post()
  {
    _formsAuthenticationHelper.RemoveTicket();
  }
}
{% endhighlight %}
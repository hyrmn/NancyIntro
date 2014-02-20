
.. Nancy slides file, created by
   hieroglyph-quickstart on Tue Feb 18 23:30:42 2014.


====================
 Nancy
====================

	What, why, and why you care
 
The What
===========

Nancy is a low-ceremony, convention-based framework that loves HTTP (and you)::

	public class HomeModule : NancyModule
	{
	  public HomeModule()
	  {
	    Get["/"] = _ => "Hello World!";
	  }
	}

Yes, it looks funky. It really is C#, I promise.	
	
Wait, what?
==================

+--------------+-------------+
| Nancy        | ASP.NET     |
+==============+=============+
| Module       | Controller  |
+--------------+-------------+
| Bootstrapper | Global.asax |
+--------------+-------------+

Everything is configurable and replaceable in Nancy. 

If you don't like the conventions, it's pretty easy to override them (but why would you want to?)

Fine, but why?
==================

* It's fast, it's lightweight, it cares about testing
* Speaks HTTP verbs
* Run it on: ASP.NET, self-host, OWIN (including Helios and Katana)
* Runs on Mono!
* Just wait until you see the content negotiation

Testing
==================

* Testability and dependency injection are first-class concerns. 
* Nancy comes with an IoC container but you can override it with your own.
* Test harness that hits the actual endpoints of your app!
* And makes it trivial to swap in a test environment bootstrapper!
* Comes with CsQuery for easy assertions of your HTML
	
ok, ok, enough talk
=====================

We're going to build up a very simple Nancy application.

If you're following along at home, start with ``01_routing_and_testing directory``

Where to go from here
=====================

http://nancyfx.org/

http://samples.nancyfx.org/

https://jabbr.net/#/rooms/nancyfx

https://github.com/hyrmn/NancyIntro
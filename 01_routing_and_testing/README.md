Routing and Testing with Nancy
==============================
##Testing

One of the most compelling features of Nancy is how easy it is to test. It gives you a straightforward way to set up a test environment, 
including injecting in mocks, stubs or test doubles, depending on your poison. We'll gloss over the bootstrapper stuff for now; just know 
that its core to configuring Nancy. Since we're just using Nancy's defaults, we'll just use the default bootstrapper.

The Browser object provides a great way to wire up your application for testing, convenience methods for handling both requests and responses, as well as 
an interface to CsQuery for easily testing returned content.

```csharp
public class HomeModuleTests
{
	public void root_path_shouldnt_go_all_michael_bay_on_us()
	{
		var sut = new Browser(new DefaultNancyBootstrapper());

		var result = sut.Get("/");

		result.StatusCode.ShouldBe(HttpStatusCode.OK);
	}
}
```

That should be pretty straightforward.

Apart from all of the Nancy testing stuff, you might wonder about the funky test syntax I'm using. This has absolutely nothing to do with Nancy! 

I'm using a test harness framework called Fixie. It's conventaion-based. And, by default, it assumes any class that ends in 'Tests' is a test class and any 
public function within that class is a test. Fixie doesn't come with assertions either. While you're free to use anything, I love Shouldly. I think it gives things
a nice, readable signature.

##Routing
Yes, it looks weird. Yes, it really is C#. The first time you look at a Nancy module, it might look just a little magical.

```csharp
public class AboutModule : NancyModule
{
	public AboutModule()
	{
		Get["/about"] = parameters => "this is my about page. it's about things.";
	}
}
```

The above instructs Nancy to construct a route for /about for any requests that come in with an HTTP GET verb. This might seem weird. We're defining routing right
next to the code that handles the route and we're not using attributes to describe the verbs we care about.

The parameters argument that's passed into the lambda will hold any route parameters that are passed in. While we're not doing that above, imagine a route like

```csharp
  Get["/hi/{name}"] = parameters => "hi " + parameters.name;
```

If you're not using any route parameters, the convention is to just use an underscore. However, you can use whatever you like.

Things could get messy if we're just willy-nilly handling routes all over the place. After all, the above module is handling something off of root and we have a 
home module doing the same thing. Messy!

However, we can scope our module to a base path

```csharp
public class AboutModule : NancyModule
{
	public AboutModule() : base("about")
	{
		Get["/"] = _ => "this is my about page. it's about things.";
	}
}
```

Subtle, but incredibly useful when you have multiple routes at the same level.
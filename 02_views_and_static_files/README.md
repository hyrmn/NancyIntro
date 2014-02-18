Views and Static Files
======================
#Views and View Engines

Nancy comes with the Super Simple View Engine. It's super, and simple, and super simple. Actually, it's pretty good and works for many use cases.

However, Nancy can use many different view engines. For this project, we're going to use the Razor view engine. Now, one thing to call out. This isn't (currently) the
official Razor view engine. Rather, it's an open source alternative that supports the same language. The official Razor will come in soon (there were some license 
questions before)

The Razor view engine works like you'd expect with a couple small quirks. First, if you're using _ViewStart.cshtml, make sure your path to the layout doesn't start with a '~'

```csharp
@{
    Layout = "Views/Shared/SiteLayout.cshtml";
}
```

Second, you'll want to ensure the Razor view engine files are loaded. If you run the app, Nancy will do this by default. However, your tests won't load Razor by default.
The easiest way to accomplish this is a bit of a hack; just include a reference in your bootstrapper (at this point, it's time for a custom bootstrapper).

```csharp
public class Bootstrapper : DefaultNancyBootstrapper
{
	private RazorViewEngine ensureRazorIsLoaded;
}
```

There are some options for resolving views. The most straight-forward way is to specify the view that you want to return.

```csharp
public class AboutModule : NancyModule
{
	public AboutModule() : base("about")
	{
		Get["/"] = _ => View["About"];
	}
}
```

But, you can also use some of that convention-based Nancy goodness.

```csharp
public class HomeModule : NancyModule
{
	public HomeModule()
	{
		Get["/"] = _ =>
			{
				var quip = new Quip("Hello World!");

				return quip;
			};
	}
}
```

This will look for a view named Quip. (Quip.cshtml in our case since we're using Razor)

# Static files

By default, Nancy only handles things that Nancy cares about. In other words, dynamic content. If we want Nancy to handle static content for us, we'll have to
explicitly tell it what we want it to handle. We can also use the opportunity to control how we map requests to the physical content.

```csharp
public class Bootstrapper : DefaultNancyBootstrapper
{
	private RazorViewEngine ensureRazorIsLoaded;

	protected override void ConfigureConventions(NancyConventions conventions)
	{
		base.ConfigureConventions(conventions);

		conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/css", "content/css"));
		conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/fonts", "content/fonts"));
		conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/img", "content/img"));
		conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/js", "content/scripts"));

		conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddFile("/humans.txt", "content/humans.txt"));

	}
}
```

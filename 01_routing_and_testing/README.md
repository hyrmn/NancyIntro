Routing and Testing with Nancy
==============================

One of the most compelling features of Nancy is how easy it is to test. It gives you a straightforward way to set up a test environment, 
including injecting in mocks, stubs or test doubles, depending on your poison. We'll gloss over the bootstrapper stuff for now; just know 
that its core to configuring Nancy. Since we're just using Nancy's defaults, we'll just use the default bootstrapper.

The Browser object isn't actually making HTTP calls. Rather, it's calling into Nancy at the same point our host container would hand off a request.
This makes it very fast, and just as useful (assuming you're not trying to use unit tests to troubleshoot network problems!)

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

Apart from all of the Nancy testing stuff, you might wonder about the funky test syntax I'm using. This has absolutely nothing to do with Nancy! I'm just weird.

I'm using a test harness framework called Fixie. It's convention-based. And, by default, it assumes any class that ends in 'Tests' is a test class and any 
public function within that class is a test. Fixie doesn't come with assertions either. While you're free to use anything, I love Shouldly. I think it gives things
a nice, readable signature.
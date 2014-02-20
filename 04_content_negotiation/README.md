Content Negotiation
======================

So, content negotiation. Nancy gives you a lot of magic right out of the box but we can, of course, extend that or override its defaults.

Remember that killer Get handler for the root route?

```csharp
public class HomeModule : NancyModule
{
	public HomeModule(IDocumentSession session)
	{
		Get["/"] = _ =>
			{
				var randomMessage = session.Query<Quip>()
										 .Customize(x => x.RandomOrdering())
										 .Take(1).FirstOrDefault();

				return randomMessage;
			};
	}
}
```

Well, confession time. That's all I needed to enable the default path for content negotiation as well. If client sends a request for "text/html" or a variant, it will resolve to my Quip.cshtml view,
and return all of that Razor goodness. 

But, if I send in a request header of "application/json", Nancy will return json. 

Nancy also supports application/xml out of the box.

Nancy will also pick a serializer based on file extension (if it's mapped, more on that below). So, if I have a route for Get["/quip"] then I can either send in an accept header or send an HTTP GET to /quip.json.

And, since it's Nancy, if you don't like the default serializers (maybe you want to use the ServiceStack json serializer), it's very easy to extend.

Now, in my case, I really want to make this easy to curl into a commit message. I can do this with Nancy via a custom IRequestProcessor. Which Nancy autodiscovers and injects into the pipeline for me, of course.

```csharp
public class TextProcessor : IResponseProcessor
{
	public ProcessorMatch CanProcess(MediaRange requestedMediaRange, dynamic model, NancyContext context)
	{
		if (requestedMediaRange.Matches(MediaRange.FromString("text/plain")) && model is Quip)
		{
			return new ProcessorMatch
				{
					ModelResult = MatchResult.DontCare,
					RequestedContentTypeResult = MatchResult.ExactMatch
				};
		}

		return new ProcessorMatch { ModelResult = MatchResult.DontCare, RequestedContentTypeResult = MatchResult.NoMatch };
	}

	public Response Process(MediaRange requestedMediaRange, dynamic model, NancyContext context)
	{
		return new Response
			{
				StatusCode = HttpStatusCode.OK,
				Contents = stream =>
					{
						var writer = new StreamWriter(stream);
						writer.Write(model.Message);
						writer.Flush();
					},
				ContentType = "text/plain"
			};
	}

	public IEnumerable<Tuple<string, MediaRange>> ExtensionMappings
	{
		get
		{
			return new[] { new Tuple<string, MediaRange>("txt", MediaRange.FromString("text/plain")) };
		}
	}
}
```

Now, this is really simplistic, I'll grant you. I check if my request has a media type of text/plain and if my model is of type Quip. You can imagine much more interesting scenarios revolving around CSV files or PDFs or, heck, Word documents. 

What's really cool, I think, is that I can also easily tell Nancy that any request that ends in a .txt extension is a media type of text/plain. So, given the following route:

```csharp
public class HomeModule : NancyModule
{
	public HomeModule()
	{
		var quip = new Quip("Fixed some errors in the last commit");
		
		Get["/"] = _ => quip;

		Get["/quip"] = _ => quip;
	}
}
```

all of the following tests work:

```csharp
public class ContentNegotiationTests
{
	public void can_fetch_as_json_via_accept_header()
	{
		var sut = new Browser(new Bootstrapper());

		var result = sut.Get("/quip", with => with.Accept("application/json"));

		var returnedQuip = result.Body.DeserializeJson<Quip>();

		returnedQuip.Message.ShouldBe("Fixed some errors in the last commit");
	}

	public void can_fetch_as_json_via_extension()
	{
		var sut = new Browser(new Bootstrapper());

		var result = sut.Get("/quip.json");

		var returnedQuip = result.Body.DeserializeJson<Quip>();

		returnedQuip.Message.ShouldBe("Fixed some errors in the last commit");
	}

	public void can_fetch_as_text_via_header()
	{
		var sut = new Browser(new Bootstrapper());

		var result = sut.Get("/quip", with => with.Accept("text/plain"));

		result.ContentType.ShouldBe("text/plain");
		result.Body.AsString().ShouldBe("Fixed some errors in the last commit");
	}

	public void can_fetch_as_text_via_extension()
	{
		var sut = new Browser(new Bootstrapper());

		var result = sut.Get("/quip.txt");

		result.ContentType.ShouldBe("text/plain");
		result.Body.AsString().ShouldBe("Fixed some errors in the last commit");
	}
}
```

Pretty cool.
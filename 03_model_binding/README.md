Model Binding
=============

We haven't yet played with anything but returning some mock data from our module. Let's fix that by wiring up to RavenDB.

```csharp
public HomeModule(IDocumentSession session)
{
	Post["/quips"] = _ =>
		{
			var newQuip = this.Bind<Quip>();
			session.Store(newQuip);

			return Response.AsJson(newQuip, HttpStatusCode.Created);
		};
}
```

It doesn't look like asp.net mvc, I'll grant you that. Still it's not too rough. Nancy will deserialize the body from the post into our model. I'm then 
returning the model along with an appropriate HTTP status code.

The test is fairly straightforward as well.

```csharp
public void can_persist_a_funny_commit_message()
{
	var sut = new Browser(new Bootstrapper {DataStore = DataStoreForTest});

	var aFunnyMessage = new Quip {Message = "Fixed some bad code"};

	BrowserResponse result = sut.Post("/quips", with => with.JsonBody(aFunnyMessage));

	result.StatusCode.ShouldBe(HttpStatusCode.Created);
	var returnedQuip = result.Body.DeserializeJson<Quip>();
	returnedQuip.Id.ShouldBe("quips/1");
}
```

Here we're posting our model, serialized to json, and then testing the result. 

This is fairly basic; we'll explore some more interesting approaches in the next step.
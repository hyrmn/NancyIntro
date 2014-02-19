using Nancy;
using Nancy.Testing;
using Raven.Client;
using Raven.Client.Embedded;
using Shouldly;
using WhatTheNancy.Modules;

namespace WhatTheNancy.Tests
{
	public class HomeModuleTests
	{
		public void root_path_shouldnt_go_all_michael_bay_on_us()
		{
			var sut = new Browser(new Bootstrapper());

			var result = sut.Get("/");

			result.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		public void we_get_an_awesome_greeting_because_we_are_awesome()
		{
			var testingDocumentStore = new EmbeddableDocumentStore
			{
				Configuration = { RunInUnreliableYetFastModeThatIsNotSuitableForProduction = true, RunInMemory = true },
			};

			testingDocumentStore.RegisterListener(new NoStaleQueriesAllowed());
			testingDocumentStore.Initialize();

			var sut = new Browser(with =>
				{
					with.Dependency<IDocumentStore>(testingDocumentStore);
					with.Dependency<IDocumentSession>(testingDocumentStore.OpenSession());

					with.Module<HomeModule>();
				});

			var result = sut.Get("/");

			//result.Body["#totally_useful_commit_message"].ShouldExistOnce().And.ShouldContain("Hello World");
		}
	}
}
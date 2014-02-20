using System;
using System.Linq;
using Nancy;
using Nancy.Testing;
using Shouldly;
using WhatTheNancy.Models;

namespace WhatTheNancy.Tests
{
	public class HomeModuleTests : with_raven
	{
		public void root_path_shouldnt_go_all_michael_bay_on_us()
		{
			var sut = new Browser(new Bootstrapper { DataStore = DataStoreForTest });

			var result = sut.Get("/");

			result.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		public void can_persist_a_funny_commit_message()
		{
			var sut = new Browser(new Bootstrapper { DataStore = DataStoreForTest });

			var aFunnyMessage = new Quip { Message = "Fixed some bad code" };

			var result = sut.Post("/quips", with =>
				{
					with.JsonBody(aFunnyMessage);
					with.Accept("application/json");
				});

			result.StatusCode.ShouldBe(HttpStatusCode.Created);
			var returnedQuip = result.Body.DeserializeJson<Quip>();
			returnedQuip.Id.ShouldBe("quips/1");
		}

		public void can_get_an_awesome_commit_message()
		{
			var sut = new Browser(new Bootstrapper { DataStore = DataStoreForTest });

			var aFunnyMessage = new Quip { Message = "By works, I meant 'doesnt work'. Works now.." };

			var result = sut.Post("/quips", with => with.JsonBody(aFunnyMessage))
											.Then.Get("/");

			result.Body["#totally_useful_commit_message"].ShouldExistOnce().And.ShouldContain("works now", StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
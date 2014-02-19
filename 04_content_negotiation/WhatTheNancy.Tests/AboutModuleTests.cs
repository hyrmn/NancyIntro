using Nancy;
using Nancy.Testing;
using Shouldly;

namespace WhatTheNancy.Tests
{
	public class AboutModuleTests : with_raven
	{
		public void about_page_resolves_correctly()
		{
			var sut = new Browser(new Bootstrapper { DataStore = DataStoreForTest });

			var result = sut.Get("/about");

			result.StatusCode.ShouldBe(HttpStatusCode.OK);
		}
	}
}
using Nancy;
using Nancy.Testing;
using Shouldly;

namespace WhatTheNancy.Tests
{
	public class StaticContentTests : with_raven
	{
		public void can_fetch_css_because_css_is_cool()
		{
			var sut = new Browser(new Bootstrapper { DataStore = DataStoreForTest });

			var result = sut.Get("/css/main.css");

			result.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		public void Are_we_humans_txt_or_are_we_dancers()
		{
			var sut = new Browser(new Bootstrapper { DataStore = DataStoreForTest });

			var result = sut.Get("/humans.txt");

			result.StatusCode.ShouldBe(HttpStatusCode.OK);
		}
	}
}
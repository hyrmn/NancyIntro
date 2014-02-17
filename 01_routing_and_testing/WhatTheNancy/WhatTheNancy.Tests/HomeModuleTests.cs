using Nancy;
using Nancy.Testing;
using Shouldly;

namespace WhatTheNancy.Tests
{
	public class HomeModuleTests
	{
		public void root_path_shouldnt_go_all_michael_bay_on_us()
		{
			var sut = new Browser(new DefaultNancyBootstrapper());

			var result = sut.Get("/");

			result.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		public void we_get_an_awesome_greeting_because_we_are_awesome()
		{
			var sut = new Browser(new DefaultNancyBootstrapper());

			var result = sut.Get("/");

			result.Body.AsString().ShouldBe("Hello World");
		}

		public void to_waste_some_time_lets_fetch_by_id()
		{
			var sut = new Browser(new DefaultNancyBootstrapper());

			var result = sut.Get("/4630");

			result.Body.AsString().ShouldContain("4630");
		}
	}
}
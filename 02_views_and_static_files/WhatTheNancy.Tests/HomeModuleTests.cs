using Nancy;
using Nancy.Testing;
using Shouldly;

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
			var sut = new Browser(new Bootstrapper());

			var result = sut.Get("/");

			result.Body["#totally_useful_commit_message"].ShouldContain("Hello World");
		}
	}
}
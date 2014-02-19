using Nancy;
using Nancy.Testing;
using Shouldly;

namespace WhatTheNancy.Tests
{
	public class AboutModuleTests
	{
		public void about_page_resolves_correctly()
		{
			var sut = new Browser(new Bootstrapper());

			var result = sut.Get("/about");

			result.StatusCode.ShouldBe(HttpStatusCode.OK);
		}
	}
}
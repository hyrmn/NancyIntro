using System.Linq;
using Nancy;
using Nancy.Testing;
using Shouldly;
using WhatTheNancy.Models;

namespace WhatTheNancy.Tests
{
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
}
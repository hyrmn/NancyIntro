using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Testing;
using Shouldly;

namespace WhatTheNancy.Tests
{
	public class StaticContentTests
	{
		public void can_fetch_css_because_css_is_cool()
		{
			var sut = new Browser(new Bootstrapper());

			var result = sut.Get("/css/site.css");

			result.StatusCode.ShouldBe(HttpStatusCode.OK);

		}
	}
}

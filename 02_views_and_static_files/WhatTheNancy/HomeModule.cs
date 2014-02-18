using Nancy;
using WhatTheNancy.Models;

namespace WhatTheNancy
{
	public class HomeModule : NancyModule
	{
		public HomeModule()
		{
			Get["/"] = _ =>
				{
					var quip = new Quip("Hello World!");

					return quip;
				};
		}
	}
}
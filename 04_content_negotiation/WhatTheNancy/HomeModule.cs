using Nancy;
using WhatTheNancy.Models;

namespace WhatTheNancy
{
	public class HomeModule : NancyModule
	{
		public HomeModule()
		{
			var quip = new Quip("Fixed some errors in the last commit");
			
			Get["/"] = _ => quip;

			Get["/quip"] = _ => quip;
		}
	}
}
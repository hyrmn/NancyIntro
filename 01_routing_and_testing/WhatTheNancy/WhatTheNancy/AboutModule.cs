using Nancy;

namespace WhatTheNancy
{
	public class AboutModule : NancyModule
	{
		public AboutModule()
		{
			Get["/about"] = _ => "this is my about page. it's about things.";
		}
	}
}
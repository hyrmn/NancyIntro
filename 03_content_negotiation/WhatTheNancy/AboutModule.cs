using Nancy;

namespace WhatTheNancy
{
	public class AboutModule : NancyModule
	{
		public AboutModule() : base("about")
		{
			Get["/"] = _ => View["About"];
		}
	}
}
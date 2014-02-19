using Nancy;

namespace WhatTheNancy.Modules
{
	public class AboutModule : NancyModule
	{
		public AboutModule() : base("about")
		{
			Get["/"] = _ => View["About"];
		}
	}
}
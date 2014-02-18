using Nancy;

namespace WhatTheNancy
{
	public class HomeModule : NancyModule
	{
		public HomeModule()
		{
			Get["/"] = parameters => "Hello World";

			Get["/{id}"] = parameters => "you requested id " + parameters.id;
		}
	}
}
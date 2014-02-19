using System.Linq;
using Nancy;
using Raven.Client;
using WhatTheNancy.Models;

namespace WhatTheNancy.Modules
{
	public class HomeModule : NancyModule
	{
		public HomeModule(IDocumentSession session)
		{
			Get["/"] = _ =>
				{
					var randomMessage = session.Query<Quip>()
																		 .Customize(x => x.RandomOrdering())
																		 .Take(1).FirstOrDefault();

					return randomMessage;
				};
		}
	}
}
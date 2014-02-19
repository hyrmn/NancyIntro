using System.Linq;
using Nancy;
using Nancy.ModelBinding;
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

			Get["/add"] = _ => View["add", new Quip()];

			Post["/quips"] = _ =>
				{
					var newQuip = this.Bind<Quip>();
					session.Store(newQuip);

					return Response.AsJson(newQuip, HttpStatusCode.Created);
				};
		}

	}
}
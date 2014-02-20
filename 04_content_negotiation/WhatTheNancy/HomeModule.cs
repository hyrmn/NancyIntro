using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using Raven.Client;
using WhatTheNancy.Models;
using Nancy.Responses;

namespace WhatTheNancy
{
	public class HomeModule : NancyModule
	{
		public HomeModule(IDocumentSession session)
		{
			Get["/"] = _ =>
				{
					return session.Query<Quip>().Customize(x => x.RandomOrdering()).Take(1).FirstOrDefault();
				};

			Get["/quip"] = _ =>
				{
					return session.Query<Quip>().Customize(x => x.RandomOrdering()).Take(1).FirstOrDefault();
				};

			Get["/add"] = _ => View["add", new Quip()];

			Post["/quips"] = _ =>
			{
				var newQuip = this.Bind<Quip>();
				session.Store(newQuip);

				return Negotiate.WithMediaRangeResponse("text/html", Response.AsRedirect("/"))
				                .WithMediaRangeModel("application/json", () => Response.AsJson(newQuip, HttpStatusCode.Created));
			};
		}
	}
}
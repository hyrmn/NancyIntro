using System;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;
using Raven.Client;
using Raven.Client.Embedded;

namespace WhatTheNancy
{
	public class Bootstrapper : DefaultNancyBootstrapper
	{
		public Func<IDocumentStore> DataStore = () => new EmbeddableDocumentStore { ConnectionStringName = "MyData" };

		protected override void ConfigureApplicationContainer(TinyIoCContainer container)
		{
			base.ConfigureApplicationContainer(container);

			var store = DataStore();

			store.Initialize();

			container.Register<IDocumentStore>(store);
		}

		protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
		{
			base.ConfigureRequestContainer(container, context);

			var store = container.Resolve<IDocumentStore>();
			var documentSession = store.OpenSession();

			container.Register<IDocumentSession>(documentSession);
		}

		protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
		{
			base.RequestStartup(container, pipelines, context);

			pipelines.AfterRequest.AddItemToEndOfPipeline(
					(ctx) =>
					{
						var documentSession = container.Resolve<IDocumentSession>();

						if (ctx.Response.StatusCode != HttpStatusCode.InternalServerError)
						{
							documentSession.SaveChanges();
						}

						documentSession.Dispose();
					});
		}

		protected override void ConfigureConventions(NancyConventions conventions)
		{
			base.ConfigureConventions(conventions);

			conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/css", "content/css"));
			conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/fonts", "content/fonts"));
			conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/img", "content/img"));
			conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/js", "content/scripts"));

			conventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddFile("/humans.txt", "content/humans.txt"));

		}
	}
}
using Nancy;
using Nancy.Conventions;

namespace WhatTheNancy
{
	public class Bootstrapper : DefaultNancyBootstrapper
	{
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
using System;
using System.Collections.Generic;
using System.IO;
using Nancy;
using Nancy.IO;
using Nancy.Responses.Negotiation;
using WhatTheNancy.Models;

namespace WhatTheNancy
{
	public class TextProcessor : IResponseProcessor
	{
		public ProcessorMatch CanProcess(MediaRange requestedMediaRange, dynamic model, NancyContext context)
		{
			if (requestedMediaRange.Matches(MediaRange.FromString("text/plain")) && model is Quip)
			{
				return new ProcessorMatch
					{
						ModelResult = MatchResult.DontCare,
						RequestedContentTypeResult = MatchResult.ExactMatch
					};
			}

			return new ProcessorMatch { ModelResult = MatchResult.DontCare, RequestedContentTypeResult = MatchResult.NoMatch };
		}

		public Response Process(MediaRange requestedMediaRange, dynamic model, NancyContext context)
		{
			return new Response
				{
					StatusCode = HttpStatusCode.OK,
					Contents = stream =>
						{
							var writer = new StreamWriter(stream);
							writer.Write(model.Message);
							writer.Flush();
						},
					ContentType = "text/plain"
				};
		}

		public IEnumerable<Tuple<string, MediaRange>> ExtensionMappings
		{
			get
			{
				return new[] { new Tuple<string, MediaRange>("txt", MediaRange.FromString("text/plain")) };
			}
		}
	}
}
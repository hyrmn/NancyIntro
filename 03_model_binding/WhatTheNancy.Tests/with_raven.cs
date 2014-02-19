using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Listeners;

namespace WhatTheNancy.Tests
{
	public class NoStaleQueriesAllowed : IDocumentQueryListener
	{
		public void BeforeQueryExecuted(IDocumentQueryCustomization queryCustomization)
		{
			queryCustomization.WaitForNonStaleResults();
		}
	}

	public class with_raven
	{
		public EmbeddableDocumentStore TestingDocumentStore;
		public IDocumentSession TestingDocumentSession;
		
		public with_raven()
		{
			TestingDocumentStore = new EmbeddableDocumentStore
				{
					Configuration = { RunInUnreliableYetFastModeThatIsNotSuitableForProduction = true, RunInMemory = true },
				};

			TestingDocumentStore.RegisterListener(new NoStaleQueriesAllowed());
			TestingDocumentStore.Initialize();

			TestingDocumentSession = TestingDocumentStore.OpenSession();
		}
	}
}

using System;
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
		public Func<EmbeddableDocumentStore> DataStoreForTest = () => new EmbeddableDocumentStore
		{
			Configuration =
			{
				RunInUnreliableYetFastModeThatIsNotSuitableForProduction = true,
				RunInMemory = true
			},
		};

		public with_raven()
		{
			var dataStore = DataStoreForTest();
			dataStore.RegisterListener(new NoStaleQueriesAllowed());
		}
	}
}
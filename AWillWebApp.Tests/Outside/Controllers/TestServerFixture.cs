// <copyright file="TestServerFixture.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Tests.Outside.Controllers
{
	using System;
	using Microsoft.AspNetCore.TestHost;
	using Microsoft.Extensions.DependencyInjection;

	public abstract class TestServerFixture : IDisposable
	{
#pragma warning disable SA1401 // Fields must be private
#pragma warning disable CA1051 // Do not declare visible instance fields
		protected readonly TestServer TestServer;
#pragma warning restore CA1051 // Do not declare visible instance fields
#pragma warning restore SA1401 // Fields must be private

		protected TestServerFixture()
		{
			TestServer = TestServerFactory.CreateTestServer(collection =>
			{
#pragma warning disable CA2214 // Do not call overridable methods in constructors
				RegisterServices(collection);
#pragma warning restore CA2214 // Do not call overridable methods in constructors
				RegisterDefaultServices(collection);
			});
		}

#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
#pragma warning disable CA1063 // Implement IDisposable Correctly
		public virtual void Dispose()
#pragma warning restore CA1063 // Implement IDisposable Correctly
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
		{
			TestServer?.Dispose();
			Dispose(true);
			GC.SuppressFinalize(this);
			return;
		}

		protected virtual void Dispose(bool cleanManagedAndNative)
		{
		}

		protected T GetService<T>()
		{
			return TestServer.Host.Services.GetService<T>();
		}

		protected virtual void RegisterServices(IServiceCollection customServices)
		{
		}

		private void RegisterDefaultServices(IServiceCollection services)
		{
		}
	}
}

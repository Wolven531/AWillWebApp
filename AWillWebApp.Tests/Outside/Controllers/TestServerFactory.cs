// <copyright file="TestServerFactory.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Tests.Outside.Controllers
{
	using System;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.TestHost;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Moq;

	public enum LogLevel
	{
		Trace,
		Debug,
		Information,
		Warning,
		Error
	}

	public static class TestServerFactory
	{
		public static TestServer CreateTestServer()
		{
			return new TestServer(CreateWebHostBuilder<TestStartup>());
		}

		public static TestServer CreateTestServer(Action<IServiceCollection> customServices)
		{
			return new TestServer(CreateWebHostBuilder<TestStartup>().ConfigureServices(customServices));
		}

		public static IServiceScope CreateServiceScope(Action<IServiceCollection> customServices)
		{
			var mock = new Mock<IServiceScope>();
			var collection = new ServiceCollection();
			customServices(collection);
			mock.Setup(s => s.ServiceProvider).Returns(collection.BuildServiceProvider());
			return mock.Object;
		}

		private static IWebHostBuilder CreateWebHostBuilder<T>()
			where T : class
		{
			return new WebHostBuilder()
				.ConfigureAppConfiguration((context, builder) =>
				{
					builder.AddEnvironmentVariables();
					//if (context.HostingEnvironment.IsDevelopment())
					//{
					//	builder.AddUserSecrets("AWillWebApp");
					//}
				})
				.ConfigureLogging((context, builder) =>
				{
					//builder.ClearProviders();
					//var logLevelString = context.Configuration["LogLevel"];
					//var appLogLevel = LogLevel.Information;
					//if (logLevelString != null)
					//{
					//	appLogLevel = Enum.Parse<LogLevel>(logLevelString);
					//}
					//builder.AddFilter(level => level >= appLogLevel);
					//builder.AddConsole();
				})
				.UseStartup<T>();
		}
	}
}

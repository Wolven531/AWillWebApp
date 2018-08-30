// <copyright file="Program.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp
{
	using System.Diagnostics.CodeAnalysis;
	using Microsoft.AspNetCore;
	using Microsoft.AspNetCore.Hosting;

	[ExcludeFromCodeCoverage]
	public static class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}

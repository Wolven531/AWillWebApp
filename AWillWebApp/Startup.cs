﻿// <copyright file="Startup.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp
{
	using System.Diagnostics.CodeAnalysis;
	using System.IO;
	using System.IO.Compression;
	using System.Text;
	using AWillWebApp.Inside.Models;
	using AWillWebApp.Inside.Services;
	using AWillWebApp.Outside.Repositories;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.ResponseCompression;
	using Microsoft.Extensions.Configuration;
	//using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
	//using Microsoft.AspNetCore.SpaServices.StaticFiles;
	//using Microsoft.AspNetCore.SpaServices.Webpack;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.FileProviders;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json;

	[ExcludeFromCodeCoverage]
	public class Startup
	{
		private readonly ILogger<Startup> _logger;
		private readonly IConfiguration _configuration;
		private readonly IHostingEnvironment _hostingEnvironment;

		public Startup(ILogger<Startup> logger, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
		{
			_logger = logger;
			_configuration = configuration;
			_hostingEnvironment = hostingEnvironment;
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseHsts();
				//app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
				//{
				//	HotModuleReplacement = true,
				//	HotModuleReplacementServerPort = 6000,
				//	ReactHotModuleReplacement = false
				//});
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseResponseCompression();
			app.UseStaticFiles();
			app.UseStaticFiles(new StaticFileOptions()
			{
				FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "dist")),
				RequestPath = "/dist"
			});
			app.UseMvc(routes =>
			{
				routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
				routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
			});
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddLogging();
			services.AddHttpsRedirection(options =>
			{
				options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
				options.HttpsPort = 5001;
			});
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			_logger.LogCritical("Loading monsters from disk...");
			var monsters = LoadMonstersFromDisk();
			_logger.LogCritical($"Loaded {monsters.Length} monsters from disk.");

			_logger.LogCritical("Loading user accounts from disk...");
			var userAccounts = LoadUserAccountsFromDisk();
			_logger.LogCritical($"Loaded {userAccounts.Length} user accounts from disk.");

			services.AddSingleton<IMonsterRepository>(new MonsterRepository(monsters));
			services.AddSingleton<IMonsterService, MonsterService>();

			services.AddSingleton<IUserAccountRepository>(new UserAccountRepository(userAccounts));
			services.AddSingleton<IUserAuthenticationService, UserAuthenticationService>();

			// services.AddSingleton<BrotliCompressionService>();

			services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
			services.AddResponseCompression(options =>
			{
				// options.Providers.Add<BrotliCompressionService>();
				options.EnableForHttps = true;
				options.MimeTypes = new[]
				{
					// NOTE: Default MIME types
					"application/javascript",
					"application/json",
					"application/xml",
					"text/css",
					"text/html",
					"text/json",
					"text/plain",
					"text/xml",
					// NOTE: Custom MIME types
					"image/png",
					"image/svg+xml"
				};
			});

			//// In production, the React files will be served from this directory
			//services.AddSpaStaticFiles(configuration =>
			//{
			//	configuration.RootPath = Path.Combine("ClientApp", "public");
			//});
		}

		private static Monster[] LoadMonstersFromDisk()
		{
			var monsterData = File.ReadAllText(
				Path.GetRelativePath(
					Directory.GetCurrentDirectory(),
					Path.Join("Data", "monsters.json")),
				Encoding.UTF8);
			return JsonConvert.DeserializeObject<Monster[]>(monsterData);
		}

		private static UserAccount[] LoadUserAccountsFromDisk()
		{
			var userAccountData = File.ReadAllText(
				Path.GetRelativePath(
					Directory.GetCurrentDirectory(),
					Path.Join("Data", "userAccounts.json")),
				Encoding.UTF8);
			return JsonConvert.DeserializeObject<UserAccount[]>(userAccountData);
		}

		//app.UseStaticFiles(new StaticFileOptions() {
		//	FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "public")),
		//	RequestPath = "/"
		//});
		//app.GetService<ISpaStaticFileProvider>().FileProvider = new PhysicalFileProvider
		//env.WebRootPath = "ClientApp/public/";

		//app.UseSpaStaticFiles();

		//app.UseMvc(routes =>
		//	{
		//		routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
		//		routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
		//	});
		//app.UseMvc(routes =>
		//{
		//	routes.MapRoute(
		//		name: "default",
		//		template: "{controller}/{action=Index}/{id?}");
		//});
		//app.Use(request =>
		//{
		//	if (request.
		//	return request;
		//});

		//app.UseSpa(spa =>
		//{
		//spa.Options.DefaultPage = "ClientApp/public/index.html";
		//spa.Options.DefaultPage = Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "public", "index.html");
		//spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions()
		//{
		//	FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "dist")),
		//	RequestPath = "/dist"
		//};

		//spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions()
		//{
		//	FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "public"))
		//	//RequestPath = "/"
		//};
		//spa.Options.SourcePath = "ClientApp/src";
		//if (env.IsDevelopment())
		//{
		//	spa.UseReactDevelopmentServer(npmScript: "start");
		//}
		//});
	}
}

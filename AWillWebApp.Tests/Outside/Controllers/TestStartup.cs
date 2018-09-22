// <copyright file="TestStartup.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp
{
	using System.IO.Compression;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.ResponseCompression;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	//public class TestStartup : Startup
	public class TestStartup
	{
		//protected override void AddWebpackMiddleware(IApplicationBuilder app)
		//{
		//}

		public TestStartup(ILogger<Startup> logger, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
			//: base(logger, configuration, hostingEnvironment)
		{
		}

		// NOTE: Overwrite the Configure from Startup
		//public static new void Configure(IApplicationBuilder app, IHostingEnvironment env)
		public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseHsts();
			app.UseHttpsRedirection();
			app.UseResponseCompression();
			app.UseStaticFiles();
			app.UseMvc(routes =>
			{
				routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
				routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
			});
		}

		// NOTE: Overwrite the ConfigureServices from Startup
		//public new void ConfigureServices(IServiceCollection services)
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddLogging();
			services.AddHttpsRedirection(options =>
			{
				options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
				options.HttpsPort = 5001;
			});
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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
		}
	}

	//	public sealed class TestStartup
	//	{
	//		private readonly ILogger<TestStartup> _logger;

	//		public TestStartup(ILogger<TestStartup> logger)
	//		{
	//			_logger = logger;
	//		}

	//		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	//		public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
	//		{
	//			if (env.IsDevelopment())
	//			{
	//				app.UseDeveloperExceptionPage();
	//				app.UseHsts();
	//			}
	//			else
	//			{
	//				app.UseExceptionHandler("/Error");
	//				app.UseHsts();
	//			}

	//			app.UseHttpsRedirection();
	//			app.UseStaticFiles();
	//			//app.UseStaticFiles(new StaticFileOptions()
	//			//{
	//			//	FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "dist")),
	//			//	RequestPath = "/dist"
	//			//});
	//			app.UseMvc(routes =>
	//			{
	//				routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
	//				routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
	//			});
	//		}

	//		//// This method gets called by the runtime. Use this method to add services to the container.
	//		//// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
	//#pragma warning disable CA1822 // Dispose static method warning
	//		public void ConfigureServices(IServiceCollection services)
	//#pragma warning restore CA1822
	//		{
	//			services.AddLogging();
	//			services.AddHttpsRedirection(options =>
	//			{
	//				options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
	//				options.HttpsPort = 5001;
	//			});
	//			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
	//		}
	//	}
}

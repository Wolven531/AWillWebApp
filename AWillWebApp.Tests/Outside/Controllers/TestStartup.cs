// <copyright file="TestStartup.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp
{
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;

	public sealed class TestStartup
	{
		private readonly ILogger<TestStartup> _logger;

		public TestStartup(ILogger<TestStartup> logger)
		{
			_logger = logger;
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseHsts();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			//app.UseStaticFiles(new StaticFileOptions()
			//{
			//	FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ClientApp", "dist")),
			//	RequestPath = "/dist"
			//});
			app.UseMvc(routes =>
			{
				routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
				routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
			});
		}

		//// This method gets called by the runtime. Use this method to add services to the container.
		//// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddLogging();
			services.AddHttpsRedirection(options =>
			{
				options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
				options.HttpsPort = 5001;
			});
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}
	}
}

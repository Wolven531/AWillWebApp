// <copyright file="Startup.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp
{
	using System;
	using AWillWebApp.Inside.Models;
	using AWillWebApp.Outside.Repositories;
	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.DependencyInjection;

	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			var inMemoryMonsters = new Monster[]
			{
				new Monster("monster A", 1, false) { Id = Guid.NewGuid() },
				new Monster("monster B", 2, false) { Id = Guid.NewGuid() },
				new Monster("monster C", 3, true) { Id = Guid.NewGuid() },
				new Monster("monster D", 4, false) { Id = Guid.NewGuid() },
				new Monster("monster E", 5, true) { Id = Guid.NewGuid() }
			};
			services.AddMvc();

			//services.AddSingleton<IMonsterRepository, MonsterRepository>();
			services.AddSingleton<IMonsterRepository>(new MonsterRepository(inMemoryMonsters));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();

			app.UseMvc(routes =>
				{
					routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
				});
		}
	}
}

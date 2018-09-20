// <copyright file="HomeController.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	public class HomeController : Controller
	{
		// GET: /
		public IActionResult Index()
		{
			return View();
		}
	}
}

﻿using Microsoft.AspNetCore.Mvc;

namespace AWillWebApp.Controllers
{
	public class HomeController : Controller
	{
		// GET: /<controller>/
		public IActionResult Index()
		{
			return View();
		}
	}
}

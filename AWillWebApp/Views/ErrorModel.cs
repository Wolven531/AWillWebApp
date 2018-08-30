// <copyright file="ErrorModel.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Views
{
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;

	[ExcludeFromCodeCoverage]
	public class ErrorModel : PageModel
	{
		public string RequestId { get; set; }

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public void OnGet()
		{
			RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
		}
	}
}

// <copyright file="UserAuthenticationController.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Controllers
{
	using System;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;
	using AWillWebApp.Inside.Services;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json.Linq;

	[Route("api/auth")]
	[ApiController]
	public partial class UserAuthenticationController : ControllerBase
	{
		private readonly IUserAuthenticationService _UserAuthenticationService;
		private readonly ILogger<UserAuthenticationController> _logger;

		public UserAuthenticationController(IUserAuthenticationService userAuthenticationService, ILogger<UserAuthenticationController> logger)
		{
			_UserAuthenticationService = userAuthenticationService;
			_logger = logger;
		}

		// POST: api/auth
		[HttpPost]
		public async Task<JsonResult> AttemptToAuthenticateUser([FromBody] LoginData authenticationData)
		{
			_logger.LogDebug($"Attempting to authenticate `{authenticationData.Username}`...");
			var wasSuccessful = await _UserAuthenticationService.AuthenticateUserAsync(authenticationData);
			_logger.LogDebug($"Authentication result, wasSuccessful={wasSuccessful}");

			return new JsonResult(new JObject
			{
				["success"] = wasSuccessful
			});
		}
	}
}

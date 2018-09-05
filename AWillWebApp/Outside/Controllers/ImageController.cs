// <copyright file="ImageController.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// Inspiration from
// https://www.csharp-console-examples.com/general/c-base64-string-to-png-image/
// </copyright>

namespace AWillWebApp.Controllers
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.IO;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;
	using AWillWebApp.Inside.Services;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;

	[Route("api/images")]
	[ApiController]
	public class ImageController : ControllerBase
	{
		private readonly IMonsterService _MonsterService;
		private readonly ILogger<ImageController> _logger;

		public ImageController(IMonsterService monsterService, ILogger<ImageController> logger)
		{
			_MonsterService = monsterService;
			_logger = logger;
		}

		// GET: api/images/sleepy/8adb050b-3cad-4359-b5f9-b4cd4a07db00
		[Route("sleepy/{monsterId}")]
		[HttpGet]
		public async Task<ActionResult> GetMonsterImageByIdAsync([FromRoute] Guid monsterId)
		{
			_logger.LogDebug($"Getting monster image with Id={monsterId}...");
			var monster = await _MonsterService.GetMonsterByIdAsync(monsterId, true);
			LogResponseSize(monster.Image);

			var imgBytes = Convert.FromBase64String(monster.Image);
			var memoryStream = new MemoryStream(imgBytes);

			return new ImageResult(memoryStream, "image/png");
		}

		// GET: api/images/awake/8adb050b-3cad-4359-b5f9-b4cd4a07db00
		[Route("awake/{monsterId}")]
		[HttpGet]
		public async Task<ActionResult> GetMonsterAwakeImageByIdAsync([FromRoute] Guid monsterId)
		{
			_logger.LogDebug($"Getting monster awake image with Id={monsterId}...");
			var monster = await _MonsterService.GetMonsterByIdAsync(monsterId, true);
			LogResponseSize(monster.AwakenedImage);

			var imgBytes = Convert.FromBase64String(monster.AwakenedImage);
			var memoryStream = new MemoryStream(imgBytes);

			return new ImageResult(memoryStream, "image/png");
		}

		[ExcludeFromCodeCoverage]
		private void LogResponseSize(string responseString)
		{
			Utility.LogResponseSize(_logger, responseString);
		}
	}
}

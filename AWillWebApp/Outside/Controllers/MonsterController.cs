// <copyright file="MonsterController.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;
	using AWillWebApp.Inside.Services;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json;

	[Route("api/monsters")]
	[ApiController]
	public class MonsterController : ControllerBase
	{
		private readonly IMonsterService _MonsterService;
		private readonly ILogger<MonsterController> _logger;

		public MonsterController(IMonsterService monsterService, ILogger<MonsterController> logger)
		{
			_MonsterService = monsterService;
			_logger = logger;
		}

		// GET: api/monsters/names
		[Route("names")]
		[HttpGet]
		public Task<IEnumerable<SearchResult>> GetAllMonsterNames()
		{
			_logger.LogDebug("Searching MonsterService with string.Empty to fetch all results...");
			return _MonsterService.SearchMonsterNamesAsync(string.Empty);
		}

		// GET: api/monsters/names/fire
		[Route("names/{searchQuery}")]
		[HttpGet]
		public Task<IEnumerable<SearchResult>> SearchMonsterNames([FromRoute] string searchQuery)
		{
			_logger.LogDebug($"Searching MonsterService searchQuery='{searchQuery}'...");
			return _MonsterService.SearchMonsterNamesAsync(searchQuery);
		}

		// GET: api/monsters
		// GET: api/monsters?withImages=true
		[HttpGet]
		public async Task<IEnumerable<Monster>> GetAllMonstersAsync([FromQuery] bool withImages = false)
		{
			_logger.LogDebug("Getting all monsters from MonsterService...");
			var monsters = await _MonsterService.GetMonstersAsync(withImages);
			_logger.LogDebug($"Returning {monsters.Count()} monsters (withImages={withImages})...");
			LogResponseSize(JsonConvert.SerializeObject(monsters));

			return monsters;
		}

		// GET: api/monsters/8adb050b-3cad-4359-b5f9-b4cd4a07db00
		// GET: api/monsters/8adb050b-3cad-4359-b5f9-b4cd4a07db00?withImages=true
		[Route("{monsterId}")]
		[HttpGet]
		public async Task<Monster> GetMonsterByIdAsync([FromRoute] Guid monsterId, [FromQuery] bool withImages = false)
		{
			_logger.LogDebug($"Getting monster with Id={monsterId} (withImages={withImages})...");
			var monster = await _MonsterService.GetMonsterByIdAsync(monsterId, withImages);
			LogResponseSize(JsonConvert.SerializeObject(monster));

			return monster;
		}

		[ExcludeFromCodeCoverage]
		private void LogResponseSize(string responseString)
		{
			Utility.LogResponseSize(_logger, responseString);
		}

		//// POST: api/SampleDummyData
		//[HttpPost]
		//public void Post([FromBody] string value)
		//{
		//}

		//// PUT: api/SampleDummyData/5
		//[HttpPut("{id}")]
		//public void Put(int id, [FromBody] string value)
		//{
		//}

		//// DELETE: api/ApiWithActions/5
		//[HttpDelete("{id}")]
		//public void Delete(int id)
		//{
		//}
	}
}

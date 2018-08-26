// <copyright file="MonsterController.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Controllers
{
	using System.Collections.Generic;
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
		private const float KilobyteScale = 1000.0f;
		private const float MegabyteScale = 1000.0f * KilobyteScale;

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
		[HttpGet]
		public async Task<IEnumerable<Monster>> GetAllMonstersAsync([FromQuery] bool withImages = false)
		{
			_logger.LogDebug("Getting all monsters from MonsterService...");
			var monsters = await _MonsterService.GetMonstersAsync();
			_logger.LogDebug($"Returning {monsters.Count()} monsters (withImages={withImages})...");

			if (!withImages)
			{
				monsters = StripMonsterImages(monsters);
			}

			LogResponseSize(JsonConvert.SerializeObject(monsters));

			return monsters;
		}

		private static void LogResponseSize(ILogger<MonsterController> instanceLogger, string responseString)
		{
			var responseSize = responseString.Length;
			var sizeDisplay = string.Empty;

			if (responseSize > MegabyteScale)
			{
				var mbSize = responseSize / MegabyteScale;
				sizeDisplay = $"{mbSize:n2} Mb";
			}
			else if (responseSize > KilobyteScale)
			{
				var kbSize = responseSize / KilobyteScale;
				sizeDisplay = $"{kbSize:n2} Kb";
			}

			instanceLogger.LogDebug($"Response Size = {sizeDisplay} ({responseSize:n0} bytes)");
		}

		private static IEnumerable<Monster> StripMonsterImages(IEnumerable<Monster> monsters)
		{
			return monsters.Select(monster => new Monster(
				monster.AwakenedName,
				monster.Name,
				monster.Rating,
				false,
				monster.Element,
				string.Empty,
				string.Empty,
				monster.EarlyRuneList,
				monster.EarlyRuneValues,
				monster.LateRuneList,
				monster.LateRuneValues,
				monster.StatPriority));
		}

		private void LogResponseSize(string responseString)
		{
			LogResponseSize(_logger, responseString);
		}

		//// GET: api/monsters/2e846d8d-a45d-4548-9240-e2ed7fa91e3c
		//[HttpGet("{id}")]
		//public Task<Monster> GetMonsterById([FromRoute] Guid id)
		//{
		//	return _MonsterRepository.GetMonster(id);
		//}

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

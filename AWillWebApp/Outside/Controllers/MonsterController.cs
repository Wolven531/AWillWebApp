// <copyright file="MonsterController.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Controllers
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;
	using AWillWebApp.Inside.Services;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;

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
		[HttpGet]
		public Task<IEnumerable<Monster>> GetAllMonsters()
		{
			_logger.LogDebug("Getting all monsters from MonsterService...");
			return _MonsterService.GetMonstersAsync();
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

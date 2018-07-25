// <copyright file="MonsterController.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;
	using AWillWebApp.Outside.Repositories;
	using Microsoft.AspNetCore.Mvc;

	[Route("api/monsters")]
	[ApiController]
	public class MonsterController : ControllerBase
	{
		private readonly IMonsterRepository _MonsterRepository;

		public MonsterController(IMonsterRepository _monsterRepository)
		{
			_MonsterRepository = _monsterRepository;
		}

		// GET: api/monsters/names
		[Route("names")]
		[HttpGet]
		public Task<IEnumerable<string>> GetAllMonsterNames()
		{
			return _MonsterRepository.GetMonsterNames();
		}

		// GET: api/monsters
		[HttpGet]
		public Task<IEnumerable<Monster>> GetAllMonsters()
		{
			return _MonsterRepository.GetMonsters();
		}

		// GET: api/monsters/2e846d8d-a45d-4548-9240-e2ed7fa91e3c
		[HttpGet("{id}")]
		public Task<Monster> GetMonsterById([FromRoute] Guid id)
		{
			return _MonsterRepository.GetMonster(id);
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

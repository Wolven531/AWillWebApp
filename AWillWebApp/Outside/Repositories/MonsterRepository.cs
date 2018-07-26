// <copyright file="MonsterRepository.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Outside.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;

	public class MonsterRepository : IMonsterRepository
	{
		private readonly List<Monster> _monsters;

		public MonsterRepository(IEnumerable<Monster> monsters)
		{
			_monsters = new List<Monster>(monsters);
		}

		public Task<Monster> GetMonster(Guid id) => Task.FromResult(_monsters.FirstOrDefault(monster => monster.Id == id));

		public Task<IEnumerable<string>> GetMonsterNames() => Task.FromResult(_monsters.Select(monster => monster.Name));

		public Task<IEnumerable<Monster>> GetMonsters() => Task.FromResult(_monsters.AsEnumerable());

		public Task<Monster> AddMonster(Monster newMonster)
		{
			if (newMonster.Id == Guid.Empty)
			{
				newMonster.Id = Guid.NewGuid();
			}

			var existingMonster = _monsters.FirstOrDefault(monster => monster.Id == newMonster.Id);
			if (existingMonster != null)
			{
				return Task.FromResult(existingMonster);
			}

			_monsters.Add(newMonster);

			return Task.FromResult(newMonster);
		}
	}
}

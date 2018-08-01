// <copyright file="MonsterService.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Inside.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;
	using AWillWebApp.Outside.Repositories;

	public class MonsterService : IMonsterService
	{
		private readonly IMonsterRepository _monsterRepository;

		public MonsterService(IMonsterRepository monsterRepository)
		{
			_monsterRepository = monsterRepository;
		}

		public async Task<IEnumerable<string>> SearchMonsterNamesAsync(string searchQuery)
		{
			var allMonsters = await _monsterRepository.GetMonsters();
			if (string.IsNullOrEmpty(searchQuery) || searchQuery.Length < 1)
			{
				return ConvertMonstersToNames(allMonsters);
			}

			var searchWasElement = Enum.TryParse<Element>(searchQuery, out var searchElement);
			var filteredMonsters = new List<Monster>();

			if (searchWasElement)
			{
				filteredMonsters = allMonsters.Where(monster => monster.Element == searchElement).ToList();
			}
			else
			{
				filteredMonsters = allMonsters
					.Where(monster =>
						monster.AwakenedName.Contains(searchQuery, StringComparison.InvariantCultureIgnoreCase) ||
						monster.Name.Contains(searchQuery, StringComparison.InvariantCultureIgnoreCase))
					.ToList();
			}

			return ConvertMonstersToNames(filteredMonsters);
		}

		private static IEnumerable<string> ConvertMonstersToNames(IEnumerable<Monster> monsters) => monsters.SelectMany(monster => new[] { monster.SearchableName, monster.AwakenedName });
	}
}

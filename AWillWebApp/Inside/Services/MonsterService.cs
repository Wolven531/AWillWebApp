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
	using Microsoft.Extensions.Logging;

	public class MonsterService : IMonsterService
	{
		private readonly IMonsterRepository _monsterRepository;
		private readonly ILogger<MonsterService> _logger;

		public MonsterService(IMonsterRepository monsterRepository, ILogger<MonsterService> logger)
		{
			_monsterRepository = monsterRepository;
			_logger = logger;
		}

		public async Task<IEnumerable<string>> SearchMonsterNamesAsync(string searchQuery)
		{
			var allMonsters = await _monsterRepository.GetMonsters();
			if (string.IsNullOrEmpty(searchQuery) || searchQuery.Length < 1)
			{
				_logger.LogDebug("searchQuery was empty, returning all monsters...");
				return ConvertMonstersToNames(allMonsters);
			}

			var searchWasElement = Enum.TryParse<Element>(searchQuery, out var searchElement);
			var filteredMonsters = new List<Monster>();

			if (searchWasElement)
			{
				_logger.LogDebug($"searchQuery was element, filtering to monsters with searchElement={searchElement}...");
				filteredMonsters = allMonsters.Where(monster => monster.Element == searchElement).ToList();
			}
			else
			{
				_logger.LogDebug($"searchQuery was name, filtering to monsters with name or awakenedName={searchQuery}...");
				filteredMonsters = allMonsters
					.Where(monster =>
						monster.AwakenedName.Contains(searchQuery, StringComparison.InvariantCultureIgnoreCase) ||
						monster.Name.Contains(searchQuery, StringComparison.InvariantCultureIgnoreCase))
					.ToList();
			}

			return ConvertMonstersToNames(filteredMonsters);
		}

		public Task<IEnumerable<Monster>> GetMonstersAsync()
		{
			_logger.LogDebug("returning all monsters...");
			return _monsterRepository.GetMonsters();
		}

		private static IEnumerable<string> ConvertMonstersToNames(IEnumerable<Monster> monsters) => monsters.SelectMany(monster => new[] { monster.SearchableName, monster.AwakenedName });
	}
}

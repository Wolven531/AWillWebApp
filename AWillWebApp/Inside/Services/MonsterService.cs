﻿// <copyright file="MonsterService.cs" company="AWill Inc">
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

		public async Task<IEnumerable<SearchResult>> SearchMonsterNamesAsync(string searchQuery)
		{
			var allMonsters = await _monsterRepository.GetMonsters();
			if (string.IsNullOrEmpty(searchQuery) || searchQuery.Length < 1)
			{
				_logger.LogDebug("searchQuery was empty, returning all monsters...");
				return ConvertMonstersToNames(allMonsters);
			}

			searchQuery = searchQuery.Trim();

			var searchWasElement = Enum.TryParse<Element>(searchQuery, true, out var searchElement);
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

		public async Task<IEnumerable<Monster>> GetMonstersAsync(bool withImageData = false)
		{
			var monsters = await _monsterRepository.GetMonsters();

			if (!withImageData)
			{
				monsters = StripMonsterImages(monsters);
			}

			return monsters;
		}

		public async Task<Monster> GetMonsterByIdAsync(Guid monsterId, bool withImageData = false)
		{
			var monster = await _monsterRepository.GetMonster(monsterId);

			if (!withImageData)
			{
				monster = StripMonsterImages(monster);
			}

			return monster;
		}

		private static IEnumerable<SearchResult> ConvertMonstersToNames(IEnumerable<Monster> monsters)
		{
			var results = new List<SearchResult>();
			for (int i = 0; i < monsters.Count(); i++)
			{
				results.Add(new SearchResult(i + 1, monsters.ElementAt(i)));
			}

			return results;
		}

		private static IEnumerable<Monster> StripMonsterImages(IEnumerable<Monster> monsters) => monsters.Select(StripMonsterImages);

		private static Monster StripMonsterImages(Monster monster) =>
			new Monster(
				monster.AwakenedName,
				monster.Name,
				monster.Rating,
				monster.Element,
				string.Empty,
				string.Empty,
				monster.EarlyRuneList,
				monster.EarlyRuneValues,
				monster.LateRuneList,
				monster.LateRuneValues,
				monster.StatPriority)
			{
				Id = monster.Id,
				Number = monster.Number
			};
	}
}

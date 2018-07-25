// <copyright file="IMonsterRepository.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Outside.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;

	public interface IMonsterRepository
	{
		Task<Monster> GetMonster(Guid id);

		Task<IEnumerable<string>> GetMonsterNames();

		Task<IEnumerable<Monster>> GetMonsters();

		Task<Monster> AddMonster(Monster newMonster);
	}
}

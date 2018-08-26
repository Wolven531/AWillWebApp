﻿// <copyright file="IMonsterService.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Inside.Services
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;

	public interface IMonsterService
	{
		Task<Monster> GetMonsterByIdAsync(Guid monsterId);

		Task<IEnumerable<Monster>> GetMonstersAsync();

		Task<IEnumerable<SearchResult>> SearchMonsterNamesAsync(string searchQuery);
	}
}

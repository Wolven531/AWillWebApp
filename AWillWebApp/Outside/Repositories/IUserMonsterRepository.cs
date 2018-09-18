// <copyright file="IUserMonsterRepository.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Outside.Repositories
{
	using System;
	using System.Collections.Generic;
	using AWillWebApp.Inside.Models;

	public interface IUserMonsterRepository
	{
		IEnumerable<UserMonster> GetMonstersForUser(Guid userId);

		//void AddMonsterToUser(Guid monsterId, Guid userId);
	}
}

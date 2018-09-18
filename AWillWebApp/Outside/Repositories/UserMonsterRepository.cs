// <copyright file="UserMonsterRepository.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Outside.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using AWillWebApp.Inside.Models;
	using Microsoft.Extensions.Logging;

	public class UserMonsterRepository : IUserMonsterRepository
	{
		private readonly IEnumerable<UserMonster> _UserMonsterMappings;
		private readonly ILogger<UserMonsterRepository> _Logger;

		public UserMonsterRepository(IEnumerable<UserMonster> userMonsterMappings, ILogger<UserMonsterRepository> logger)
		{
			_UserMonsterMappings = userMonsterMappings;
			_Logger = logger;
		}

		//public void AddMonsterToUser(Guid monsterId, Guid userId)
		//{
		//	throw new NotImplementedException();
		//}

		public IEnumerable<UserMonster> GetMonstersForUser(Guid userId)
		{
			return _UserMonsterMappings.Where(userMonster => userMonster.User.Id == userId);
		}
	}
}

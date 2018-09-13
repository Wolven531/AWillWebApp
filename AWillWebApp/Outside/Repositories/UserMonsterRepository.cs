// <copyright file="UserMonsterRepository.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Outside.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
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

		public Task<IEnumerable<UserMonster>> GetMonstersForUser(Guid userId)
		{
			return Task.FromResult(_UserMonsterMappings);
		}
	}
}

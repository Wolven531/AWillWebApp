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
		private readonly IDictionary<UserAccount, IEnumerable<Monster>> _UserMonsterMappings;
		private readonly ILogger<UserMonsterRepository> _Logger;

		public UserMonsterRepository(IDictionary<UserAccount, IEnumerable<Monster>> userMonsterMappings, ILogger<UserMonsterRepository> logger)
		{
			_UserMonsterMappings = userMonsterMappings;
			_Logger = logger;
		}

		private IEnumerable<Guid> _UserIds => _UserMonsterMappings.Keys.Select(userAccount => userAccount.Id);

		//public void AddMonsterToUser(Guid monsterId, Guid userId)
		//{
		//	throw new NotImplementedException();
		//}

		public IEnumerable<UserMonster> GetMonstersForUser(Guid userId)
		{
			if (!_UserIds.Contains(userId))
			{
				return Enumerable.Empty<UserMonster>();
			}

			var user = _UserMonsterMappings.Keys.First(userAccount => userAccount.Id == userId);
			var relevantUserMonsters = _UserMonsterMappings[user].Select(monster => new UserMonster(user, monster));

			return relevantUserMonsters;
		}
	}
}

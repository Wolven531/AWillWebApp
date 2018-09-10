// <copyright file="UserMonsterService.cs" company="AWill Inc">
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

	/// <summary>
	/// This service is used to manipulate and query
	/// the relationships between the UserAccount and Monster models.
	/// </summary>
	public class UserMonsterService
	{
		//private IUserAccountRepository _UserAccountRepository;
		//private IMonsterRepository _MonsterRepository;
		private readonly IUserMonsterRepository _UserMonsterRepository;
		private ILogger<UserMonsterService> _Logger;

		//public UserMonsterService(IUserAccountRepository userAccountRepository, IMonsterRepository monsterRepository, ILogger<UserMonsterService> logger)
		//{
		//	_UserAccountRepository = userAccountRepository;
		//	_MonsterRepository = monsterRepository;
		//	_Logger = logger;
		//}

		public UserMonsterService(IUserMonsterRepository userMonsterRepository, ILogger<UserMonsterService> logger)
		{
			_UserMonsterRepository = userMonsterRepository;
			_Logger = logger;
		}

		/// <summary>
		/// Return the monster objects associated with a given user.
		/// </summary>
		/// <param name="userId" type="System.Guid">The unique identifier of the user.</param>
		/// <returns>A Task of type IEnumerable<Monster> representing the monsters associated with the userId.</returns>
		public Task<IEnumerable<Monster>> GetMonstersForUserAsync(Guid userId)
		{
			return Task.FromResult(Enumerable.Empty<Monster>());
		}
	}
}

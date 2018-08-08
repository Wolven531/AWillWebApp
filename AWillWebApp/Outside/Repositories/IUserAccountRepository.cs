// <copyright file="IUserAccountRepository.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Outside.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;

	public interface IUserAccountRepository
	{
		Task<IEnumerable<UserAccount>> GetUserAccounts();

		Task<UserAccount> GetUserAccountById(Guid id);

		Task<UserAccount> GetUserAccountByNumber(int number);

		Task<UserAccount> GetUserAccountByUsername(string username);

		Task<UserAccount> AddUserAccount(UserAccount newUserAccount);
	}
}

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
		Task<IEnumerable<UserAccount>> GetUserAccountsAsync();

		Task<UserAccount> GetUserAccountByIdAsync(Guid id);

		Task<UserAccount> GetUserAccountByNumberAsync(int number);

		Task<UserAccount> GetUserAccountByUsernameAsync(string username);

		Task<UserAccount> AddUserAccountAsync(UserAccount newUserAccount);
	}
}

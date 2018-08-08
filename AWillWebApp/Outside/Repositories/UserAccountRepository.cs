﻿// <copyright file="UserAccountRepository.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Outside.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;

	public class UserAccountRepository : IUserAccountRepository
	{
		private List<UserAccount> _userAccounts;

		public UserAccountRepository(IEnumerable<UserAccount> userAccounts)
		{
			_userAccounts = new List<UserAccount>(userAccounts);
		}

		public Task<UserAccount> AddUserAccount(UserAccount newUserAccount)
		{
			if (newUserAccount.Id == Guid.Empty)
			{
				newUserAccount.Id = Guid.NewGuid();
			}

			if (newUserAccount.Number < 1)
			{
				newUserAccount.Number = _userAccounts.Count + 1;
			}

			var existingUserAccount = _userAccounts.FirstOrDefault(userAccount => userAccount.Id == newUserAccount.Id);
			if (existingUserAccount != null)
			{
				return Task.FromResult(existingUserAccount);
			}

			_userAccounts.Add(newUserAccount);

			return Task.FromResult(newUserAccount);
		}

		public Task<UserAccount> GetUserAccountById(Guid id)
		{
			return Task.FromResult(_userAccounts.FirstOrDefault(userAccount => userAccount.Id == id));
		}

		public Task<UserAccount> GetUserAccountByNumber(int number)
		{
			return Task.FromResult(_userAccounts.FirstOrDefault(userAccount => userAccount.Number == number));
		}

		public Task<UserAccount> GetUserAccountByUsername(string username)
		{
			return Task.FromResult(_userAccounts.FirstOrDefault(userAccount => userAccount.Username == username));
		}

		public Task<IEnumerable<UserAccount>> GetUserAccounts()
		{
			return Task.FromResult(_userAccounts.AsEnumerable());
		}
	}
}

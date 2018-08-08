// <copyright file="UserAccountRepository.cs" company="AWill Inc">
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
			_userAccounts = new List<UserAccount>();
			foreach (var userAccount in userAccounts)
			{
				AddUserAccountAsync(userAccount);
			}
		}

		public Task<UserAccount> AddUserAccountAsync(UserAccount newUserAccount)
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

		public Task<UserAccount> GetUserAccountByIdAsync(Guid id)
		{
			return Task.FromResult(_userAccounts.FirstOrDefault(userAccount => userAccount.Id == id));
		}

		public Task<UserAccount> GetUserAccountByNumberAsync(int number)
		{
			return Task.FromResult(_userAccounts.FirstOrDefault(userAccount => userAccount.Number == number));
		}

		public Task<UserAccount> GetUserAccountByUsernameAsync(string username)
		{
			return Task.FromResult(_userAccounts.FirstOrDefault(userAccount => userAccount.Username == username));
		}

		public Task<IEnumerable<UserAccount>> GetUserAccountsAsync()
		{
			return Task.FromResult(_userAccounts.AsEnumerable());
		}
	}
}

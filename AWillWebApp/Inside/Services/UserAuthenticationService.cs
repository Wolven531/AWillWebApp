// <copyright file="UserAuthenticationService.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Inside.Services
{
	using System.Threading.Tasks;
	using AWillWebApp.Outside.Repositories;
	using Microsoft.Extensions.Logging;

	public class UserAuthenticationService : IUserAuthenticationService
	{
		private IUserAccountRepository _UserAccountRepository;
		private ILogger<UserAuthenticationService> _Logger;

		public UserAuthenticationService(IUserAccountRepository userAccountRepository, ILogger<UserAuthenticationService> logger)
		{
			_UserAccountRepository = userAccountRepository;
			_Logger = logger;
		}

		public async Task<bool> AuthenticateUser(string username, string password)
		{
			var userAccount = await _UserAccountRepository.GetUserAccountByUsernameAsync(username);

			return userAccount == null ? false : userAccount.VerifyPassword(password, userAccount.HashedPassword);
		}
	}
}

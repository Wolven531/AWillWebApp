﻿// <copyright file="UserAuthenticationService.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Inside.Services
{
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;
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

		public async Task<bool> AuthenticateUserAsync(LoginData loginData)
		{
			var userAccount = await _UserAccountRepository.GetUserAccountByUsernameAsync(loginData.Username);

			return userAccount == null ? false : userAccount.VerifyPassword(loginData.Password, userAccount.HashedPassword);
		}
	}
}

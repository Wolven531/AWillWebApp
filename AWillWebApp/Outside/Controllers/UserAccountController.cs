// <copyright file="UserAccountController.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;
	using AWillWebApp.Outside.Repositories;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;

	[Route("api/users")]
	[ApiController]
	public class UserAccountController : ControllerBase
	{
		private readonly IUserAccountRepository _UserAccountRepository;
		private readonly ILogger<UserAccountController> _logger;

		public UserAccountController(IUserAccountRepository userAccountRepository, ILogger<UserAccountController> logger)
		{
			_UserAccountRepository = userAccountRepository;
			_logger = logger;
		}

		// GET: api/users
		[HttpGet]
		public Task<IEnumerable<UserAccount>> GetAllUserAccounts()
		{
			_logger.LogDebug("Getting all user accounts from UserAccountRepository...");
			return _UserAccountRepository.GetUserAccountsAsync();
		}

		// GET: api/users/1
		// GET: api/users/tester
		// GET: api/users/b68a87be-173f-4f4a-829c-bcf08b5bad1e
		[Route("{userAccountIdentifier}")]
		[HttpGet]
		public Task<UserAccount> GetUserAccount([FromRoute] string userAccountIdentifier)
		{
			if (Guid.TryParse(userAccountIdentifier, out var userAccountId))
			{
				_logger.LogDebug($"Retrieving user account by id, userAccountId='{userAccountId}'...");
				return _UserAccountRepository.GetUserAccountByIdAsync(userAccountId);
			}

			if (int.TryParse(userAccountIdentifier, out var userAccountNumber))
			{
				_logger.LogDebug($"Retrieving user account by number, userAccountNumber='{userAccountNumber}'...");
				return _UserAccountRepository.GetUserAccountByNumberAsync(userAccountNumber);
			}

			_logger.LogDebug($"Retrieving user account by username, userAccountIdentifier='{userAccountIdentifier}'...");
			return _UserAccountRepository.GetUserAccountByUsernameAsync(userAccountIdentifier);
		}
	}
}

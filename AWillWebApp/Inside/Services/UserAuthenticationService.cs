// <copyright file="UserAuthenticationService.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Inside.Services
{
	using System.Threading.Tasks;

	public class UserAuthenticationService : IUserAuthenticationService
	{
		public Task<bool> AuthenticateUser(string username, string password)
		{
			return Task.FromResult(false);
		}
	}
}

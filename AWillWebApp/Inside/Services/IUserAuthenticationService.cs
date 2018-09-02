// <copyright file="IUserAuthenticationService.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Inside.Services
{
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;

	public interface IUserAuthenticationService
	{
		Task<bool> AuthenticateUserAsync(LoginData loginData);
	}
}

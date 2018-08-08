// <copyright file="UserAccount.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Inside.Models
{
	using System;

	public class UserAccount
	{
		public UserAccount(string username, string password)
		{
			Username = username;
			Password = password;
		}

		/// <summary>
		/// Gets or sets the Id property of this account
		/// This property should be used to uniquely identify this account within the current instance
		/// of the repository
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the Number property of this account
		/// This property should be used to uniquely identify this account across instances of the repository
		/// </summary>
		public int Number { get; set; }

		public string Username { get; }

		public string Password { get; }
	}
}

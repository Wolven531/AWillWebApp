// <copyright file="UserMonster.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Inside.Models
{
	using System;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;

	public class UserMonster
	{
		public UserMonster(UserAccount userAccount, Monster userMonster)
		{
			User = userAccount;
			Monster = userMonster;
		}

		public Monster Monster { get; }

		public UserAccount User { get; }
	}
}

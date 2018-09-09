// <copyright file="IUserMonsterService.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Inside.Services
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;

	/// <summary>
	/// This interface describes a service used to manipulate and query
	/// the relationships between the UserAccount and Monster models.
	/// </summary>
	public interface IUserMonsterService
	{
		/// <summary>
		/// Return the monster objects associated with a given user.
		/// </summary>
		/// <param name="userId" type="System.Guid">The unique identifier of the user.</param>
		/// <returns>A Task of type IEnumerable<Monster> representing the monsters associated with the userId.</returns>
		Task<IEnumerable<Monster>> GetMonstersForUserAsync(Guid userId);
	}
}

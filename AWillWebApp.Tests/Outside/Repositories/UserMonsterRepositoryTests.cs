// <copyright file="UserMonsterRepositoryTests.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Tests.Outside.Repositories
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;
	using AWillWebApp.Outside.Repositories;
	using FluentAssertions;
	using Microsoft.Extensions.Logging.Abstractions;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test names should contain underscores for readability")]
	[TestClass]
	public class UserMonsterRepositoryTests
	{
		private UserMonsterRepository _fixture;

		[TestMethod]
		public async Task GetMonstersForUser_WhenUserMonsterRepositoryIsEmpty_ShouldReturnEmptyList()
		{
			// Setup
			_fixture = new UserMonsterRepository(Enumerable.Empty<UserMonster>(), NullLogger<UserMonsterRepository>.Instance);
			var expected = Enumerable.Empty<UserMonster>();

			// Execute
			var actual = await _fixture.GetMonstersForUser(Guid.NewGuid());

			// Verify
			actual.Should().BeEmpty();
		}
	}
}

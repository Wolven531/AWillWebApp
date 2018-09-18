// <copyright file="UserMonsterRepositoryTests.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Tests.Outside.Repositories
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
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
		public void GetMonstersForUser_WhenUserMonsterRepositoryIsEmpty_ShouldReturnEmptyList()
		{
			// Setup
			_fixture = new UserMonsterRepository(Enumerable.Empty<UserMonster>(), NullLogger<UserMonsterRepository>.Instance);
			var expected = Enumerable.Empty<UserMonster>();

			// Execute
			var actual = _fixture.GetMonstersForUser(Guid.NewGuid());

			// Verify
			actual.Should().BeEmpty();
		}

		[TestMethod]
		public void GetMonstersForUser_WhenUserMonsterRepositoryHasMappingsAndUserExists_ShouldReturnListOfMonsters()
		{
			// Setup
			const string userId1 = "72f12a72-a059-4d9f-b2bb-764aa445c349";
			var userAccount1 = new UserAccount("user 1", "pass 1") { Id = Guid.Parse(userId1) };
			var userAccount2 = new UserAccount("user 2", "pass 2") { Id = Guid.Parse("4fa55841-9382-4633-b04f-69345e8f6602") };
			var monster1 = new Monster("awake 1", "name 1", 3, Element.Dark, "awake img 1", "img 1", "early runes 1", "early rune vals 1", "late runes 1", "late rune vals 1", string.Empty);
			var monster2 = new Monster("awake 2", "name 2", 4, Element.Fire, "awake img 2", "img 2", "early runes 2", "early rune vals 2", "late runes 2", "late rune vals 2", string.Empty);
			var expected = new[]
			{
				new UserMonster(userAccount1, monster1),
				new UserMonster(userAccount1, monster2)
			};

			_fixture = new UserMonsterRepository(
				new[]
				{
					new UserMonster(userAccount1, monster1),
					new UserMonster(userAccount1, monster2),
					new UserMonster(userAccount2, monster1),
				}, NullLogger<UserMonsterRepository>.Instance);

			// Execute
			var actual = _fixture.GetMonstersForUser(Guid.Parse(userId1));

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public void GetMonstersForUser_WhenUserMonsterRepositoryHasMappingsAndUserDoesNotExist_ShouldReturnEmptyList()
		{
			// Setup
			var userAccount1 = new UserAccount("user 1", "pass 1") { Id = Guid.Parse("72f12a72-a059-4d9f-b2bb-764aa445c349") };
			var userAccount2 = new UserAccount("user 2", "pass 2") { Id = Guid.Parse("4fa55841-9382-4633-b04f-69345e8f6602") };
			var monster1 = new Monster("awake 1", "name 1", 3, Element.Dark, "awake img 1", "img 1", "early runes 1", "early rune vals 1", "late runes 1", "late rune vals 1", string.Empty);
			var monster2 = new Monster("awake 2", "name 2", 4, Element.Fire, "awake img 2", "img 2", "early runes 2", "early rune vals 2", "late runes 2", "late rune vals 2", string.Empty);
			var expected = Enumerable.Empty<UserMonster>();

			_fixture = new UserMonsterRepository(
				new[]
				{
					new UserMonster(userAccount1, monster1),
					new UserMonster(userAccount1, monster2),
					new UserMonster(userAccount2, monster1),
				}, NullLogger<UserMonsterRepository>.Instance);

			// Execute
			var actual = _fixture.GetMonstersForUser(Guid.NewGuid());

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		//[TestMethod]
		//public void AddMonsterToUser_WhenUserMonsterHasNoMonsters_ShouldAddMonsterAndReturnUserMonster()
		//{
			// Setup
			//var userAccount1 = new UserAccount("user 1", "pass 1") { Id = Guid.Parse("72f12a72-a059-4d9f-b2bb-764aa445c349") };
			//var userAccount2 = new UserAccount("user 2", "pass 2") { Id = Guid.Parse("4fa55841-9382-4633-b04f-69345e8f6602") };
			//var monster1 = new Monster("awake 1", "name 1", 3, Element.Dark, "awake img 1", "img 1", "early runes 1", "early rune vals 1", "late runes 1", "late rune vals 1", string.Empty);
			//var monster2 = new Monster("awake 2", "name 2", 4, Element.Fire, "awake img 2", "img 2", "early runes 2", "early rune vals 2", "late runes 2", "late rune vals 2", string.Empty);
			//var expected = Enumerable.Empty<UserMonster>();

			//_fixture = new UserMonsterRepository(
			//	new[]
			//	{
			//		new UserMonster(userAccount1, monster1),
			//		new UserMonster(userAccount1, monster2),
			//		new UserMonster(userAccount2, monster1),
			//	}, NullLogger<UserMonsterRepository>.Instance);

			// Execute
			//_fixture.AddMonsterToUser(Guid.NewGuid(), Guid.NewGuid());

			// Verify
		//	actual.Should().BeEquivalentTo(expected);
		//}
	}
}

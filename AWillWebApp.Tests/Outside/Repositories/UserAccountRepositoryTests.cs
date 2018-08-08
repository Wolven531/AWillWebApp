// <copyright file="UserAccountRepositoryTests.cs" company="AWill Inc">
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
	using Xunit;

	[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test names should contain underscores for readability")]
	public class UserAccountRepositoryTests
	{
		private UserAccountRepository fixture;

		[Fact]
		public async Task GetUserAccounts_WhenRepositoryIsEmpty_ShouldReturnEmptyList()
		{
			// Setup
			var userAccounts = Enumerable.Empty<UserAccount>();
			var expected = Enumerable.Empty<UserAccount>();

			fixture = new UserAccountRepository(userAccounts);

			// Execute
			var actual = await fixture.GetUserAccounts();

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task GetUserAccounts_WhenRepositoryHasUserAccounts_ShouldReturnListOfUserAccounts()
		{
			// Setup
			var userAccounts = new UserAccount[]
			{
				new UserAccount("user 1", "pass 1"),
				new UserAccount("user 2", "pass 2")
			};
			var expected = new UserAccount[]
			{
				new UserAccount("user 1", "pass 1"),
				new UserAccount("user 2", "pass 2")
			};

			fixture = new UserAccountRepository(userAccounts);

			// Execute
			var actual = await fixture.GetUserAccounts();

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task GetUserAccountById_WhenRepositoryIsEmpty_ShouldReturnNull()
		{
			// Setup
			var userAccounts = Enumerable.Empty<UserAccount>();
			UserAccount expected = null;

			fixture = new UserAccountRepository(userAccounts);

			// Execute
			var actual = await fixture.GetUserAccountById(Guid.NewGuid());

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task GetUserAccountById_WhenRepositoryHasUserAccountsAndParamMatches_ShouldReturnUserAccount()
		{
			// Setup
			var userAccount = new UserAccount("user 2", "pass 2") { Number = 2, Id = Guid.Parse("2e846d8d-a45d-4548-9240-e2ed7fa91e3c") };
			var userAccounts = new UserAccount[]
			{
				new UserAccount("user 1", "pass 1") { Number = 1 },
				userAccount
			};
			var expected = userAccount;

			fixture = new UserAccountRepository(userAccounts);

			// Execute
			var actual = await fixture.GetUserAccountById(Guid.Parse("2e846d8d-a45d-4548-9240-e2ed7fa91e3c"));

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task GetUserAccountById_WhenRepositoryHasUserAccountsAndParamDoesNotMatch_ShouldReturnNull()
		{
			// Setup
			var userAccounts = new UserAccount[]
			{
				new UserAccount("user 1", "pass 1") { Number = 1 },
				new UserAccount("user 2", "pass 2") { Number = 2, Id = Guid.Parse("2e846d8d-a45d-4548-9240-e2ed7fa91e3c") } };
			UserAccount expected = null;

			fixture = new UserAccountRepository(userAccounts);

			// Execute
			var actual = await fixture.GetUserAccountById(Guid.NewGuid());

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task GetUserAccountByNumber_WhenRepositoryIsEmpty_ShouldReturnNull()
		{
			// Setup
			var userAccounts = Enumerable.Empty<UserAccount>();
			UserAccount expected = null;

			fixture = new UserAccountRepository(userAccounts);

			// Execute
			var actual = await fixture.GetUserAccountByNumber(1);

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task GetUserAccountByNumber_WhenRepositoryHasUserAccountsAndParamMatches_ShouldReturnUserAccount()
		{
			// Setup
			var userAccount = new UserAccount("user 2", "pass 2") { Number = 2 };
			var userAccounts = new UserAccount[]
			{
				new UserAccount("user 1", "pass 1") { Number = 1 },
				userAccount
			};
			var expected = userAccount;

			fixture = new UserAccountRepository(userAccounts);

			// Execute
			var actual = await fixture.GetUserAccountByNumber(2);

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task GetUserAccountByNumber_WhenRepositoryHasUserAccountsAndParamDoesNotMatch_ShouldReturnNull()
		{
			// Setup
			var userAccounts = new UserAccount[]
			{
				new UserAccount("user 1", "pass 1") { Number = 1 },
				new UserAccount("user 2", "pass 2") { Number = 2, Id = Guid.Parse("2e846d8d-a45d-4548-9240-e2ed7fa91e3c") } };
			UserAccount expected = null;

			fixture = new UserAccountRepository(userAccounts);

			// Execute
			var actual = await fixture.GetUserAccountByNumber(5);

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task GetUserAccountByUsername_WhenRepositoryIsEmpty_ShouldReturnNull()
		{
			// Setup
			var userAccounts = Enumerable.Empty<UserAccount>();
			UserAccount expected = null;

			fixture = new UserAccountRepository(userAccounts);

			// Execute
			var actual = await fixture.GetUserAccountByUsername("asdf");

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task GetUserAccountByUsername_WhenRepositoryHasUserAccountsAndParamMatches_ShouldReturnUserAccount()
		{
			// Setup
			var userAccount = new UserAccount("user 2", "pass 2") { Number = 2, Id = Guid.Parse("2e846d8d-a45d-4548-9240-e2ed7fa91e3c") };
			var userAccounts = new UserAccount[]
			{
				new UserAccount("user 1", "pass 1") { Number = 1 },
				userAccount
			};
			var expected = userAccount;

			fixture = new UserAccountRepository(userAccounts);

			// Execute
			var actual = await fixture.GetUserAccountByUsername("user 2");

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task GetUserAccountByUsername_WhenRepositoryHasUserAccountsAndParamDoesNotMatch_ShouldReturnNull()
		{
			// Setup
			var userAccounts = new UserAccount[]
			{
				new UserAccount("user 1", "pass 1") { Number = 1 },
				new UserAccount("user 2", "pass 2") { Number = 2, Id = Guid.Parse("2e846d8d-a45d-4548-9240-e2ed7fa91e3c") } };
			UserAccount expected = null;

			fixture = new UserAccountRepository(userAccounts);

			// Execute
			var actual = await fixture.GetUserAccountByUsername("asdf");

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		//[Fact]
		//public async Task GetMonster_WhenRepositoryIsEmpty_ShouldReturnNull()
		//{
		//	// Setup
		//	var monsters = Enumerable.Empty<Monster>();
		//	Monster expected = null;

		//	fixture = new MonsterRepository(monsters);

		//	// Execute
		//	var actual = await fixture.GetMonster(Guid.NewGuid());

		//	// Verify
		//	actual.Should().BeEquivalentTo(expected);
		//}

		//[Fact]
		//public async Task GetMonster_WhenRepositoryHasMonsters_ShouldReturnMonsterAsModelObject()
		//{
		//	// Setup
		//	var id1 = Guid.NewGuid();
		//	var id2 = Guid.NewGuid();
		//	var expected = new Monster("awake 2", "name 2", 3, false, Element.Wind, "awakenedimg2", "img2", null, null, null, null, null) { Id = id2 };
		//	var monsters = new Monster[]
		//	{
		//		new Monster("awake 1", "name 1", 3, false, Element.Light, "awakenedimg", "img", null, null, null, null, null) { Id = id1 },
		//		expected
		//	};

		//	fixture = new MonsterRepository(monsters);

		//	// Execute
		//	var actual = await fixture.GetMonster(id2);

		//	// Verify
		//	actual.Should().BeEquivalentTo(expected);
		//}

		[Fact]
		public async Task AddUserAccount_WhenRepositoryIsEmptyAndNewUserAccountLacksIdAndNumber_ShouldAddUserAccountAndReturnIt()
		{
			// Setup
			var userAccounts = Enumerable.Empty<UserAccount>();
			var newUserAccount = new UserAccount("user 1", "pass 1");
			var expected = new UserAccount("user 1", "pass 1") { Number = 1 };
			var originalId = expected.Id;

			fixture = new UserAccountRepository(userAccounts);

			// Execute
			var actual = await fixture.AddUserAccount(newUserAccount);

			// Verify
			actual.Should().BeEquivalentTo(expected, options => options.Excluding(userAccount => userAccount.Id));
			actual.Id.Should().NotBe(originalId);
		}

		[Fact]
		public async Task AddUserAccount_WhenRepositoryIsEmptyAndNewUserAccountHasExistingId_ShouldAddUserAccountWithIdAndReturnIt()
		{
			// Setup
			var userAccounts = Enumerable.Empty<UserAccount>();
			var newUserAccount = new UserAccount("user 1", "pass 1") { Id = Guid.Parse("2e846d8d-a45d-4548-9240-e2ed7fa91e3c") };
			var expected = new UserAccount("user 1", "pass 1") { Id = Guid.Parse("2e846d8d-a45d-4548-9240-e2ed7fa91e3c"), Number = 1 };

			fixture = new UserAccountRepository(userAccounts);

			// Execute
			var actual = await fixture.AddUserAccount(newUserAccount);

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}
	}
}

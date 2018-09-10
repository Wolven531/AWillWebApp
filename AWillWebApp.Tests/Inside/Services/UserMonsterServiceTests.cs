// <copyright file="UserMonsterServiceTests.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Tests.Inside.Services
{
	using System;
	//using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;
	using AWillWebApp.Inside.Services;
	using AWillWebApp.Outside.Repositories;
	using FluentAssertions;
	using Microsoft.Extensions.Logging;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Moq;

	[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test names should contain underscores for readability")]
	[TestClass]
	public class UserMonsterServiceTests
	{
		private UserMonsterService fixture;
		//private Mock<IMonsterRepository> _mockMonsterRepository;
		//private Mock<IUserAccountRepository> _mockUserAccountRepository;
		private Mock<IUserMonsterRepository> _mockUserMonsterRepository;

		public UserMonsterServiceTests()
		{
			//_mockMonsterRepository = new Mock<IMonsterRepository>();
			//_mockUserAccountRepository = new Mock<IUserAccountRepository>();
			_mockUserMonsterRepository = new Mock<IUserMonsterRepository>();
			//fixture = new UserMonsterService(_mockUserAccountRepository.Object, _mockMonsterRepository.Object, new Mock<ILogger<UserMonsterService>>().Object);
			fixture = new UserMonsterService(_mockUserMonsterRepository.Object, new Mock<ILogger<UserMonsterService>>().Object);
		}

		[TestMethod]
		public async Task GetMonstersForUserAsync_WhenUserRepositoryIsEmpty_ShouldReturnEmptyListOfUserMonsters()
		{
			// Setup
			var expected = Enumerable.Empty<Monster>();

			// Execute
			var actual = await fixture.GetMonstersForUserAsync(Guid.NewGuid());

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}
	}
}

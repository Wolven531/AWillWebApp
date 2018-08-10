// <copyright file="UserAuthenticationServiceTests.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Tests.Inside.Services
{
	using System.Diagnostics.CodeAnalysis;
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
	public class UserAuthenticationServiceTests
	{
		private UserAuthenticationService fixture;
		private Mock<IUserAccountRepository> _mockUserAccountRepository;

		public UserAuthenticationServiceTests()
		{
			_mockUserAccountRepository = new Mock<IUserAccountRepository>();
			fixture = new UserAuthenticationService(_mockUserAccountRepository.Object, new Mock<ILogger<UserAuthenticationService>>().Object);
		}

		[TestMethod]
		public async Task AuthenticateUser_WhenGivenEmptyUsernameAndPassword_ShouldReturnFalse()
		{
			// Setup
			string actualUsername = null;
			_mockUserAccountRepository
				.Setup(repository => repository.GetUserAccountByUsernameAsync(It.IsAny<string>()))
				.Callback<string>(username => actualUsername = username)
				.ReturnsAsync((UserAccount)null);
			var expected = false;

			// Execute
			var actual = await fixture.AuthenticateUser(string.Empty, string.Empty);

			// Verify
			_mockUserAccountRepository.VerifyAll();
			actualUsername.Should().Be(string.Empty);
			actual.Should().Be(expected);
		}

		[TestMethod]
		public async Task AuthenticateUser_WhenGivenUsernameAndPasswordThatDoNotMatch_ShouldReturnFalse()
		{
			// Setup
			string actualUsername = null;
			_mockUserAccountRepository
				.Setup(repository => repository.GetUserAccountByUsernameAsync(It.IsAny<string>()))
				.Callback<string>(username => actualUsername = username)
				.ReturnsAsync(new UserAccount("user 1", "pass 1"));
			var expected = false;

			// Execute
			var actual = await fixture.AuthenticateUser("user 1", "garbled");

			// Verify
			_mockUserAccountRepository.VerifyAll();
			actualUsername.Should().Be("user 1");
			actual.Should().Be(expected);
		}

		[TestMethod]
		public async Task AuthenticateUser_WhenGivenUsernameAndPasswordThatMatch_ShouldReturnTrue()
		{
			// Setup
			string actualUsername = null;
			_mockUserAccountRepository
				.Setup(repository => repository.GetUserAccountByUsernameAsync(It.IsAny<string>()))
				.Callback<string>(username => actualUsername = username)
				.ReturnsAsync(new UserAccount("user 1", "pass 1"));
			var expected = true;

			// Execute
			var actual = await fixture.AuthenticateUser("user 1", "pass 1");

			// Verify
			_mockUserAccountRepository.VerifyAll();
			actualUsername.Should().Be("user 1");
			actual.Should().Be(expected);
		}
	}
}

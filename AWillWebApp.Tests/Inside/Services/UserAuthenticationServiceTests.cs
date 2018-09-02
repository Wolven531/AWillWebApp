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
		public async Task AuthenticateUserAsync_WhenGivenEmptyUsernameAndPassword_ShouldReturnFalse()
		{
			// Setup
			string actualUsername = null;
			_mockUserAccountRepository
				.Setup(repository => repository.GetUserAccountByUsernameAsync(It.IsAny<string>()))
				.Callback<string>(username => actualUsername = username)
				.ReturnsAsync((UserAccount)null);
			var expected = false;

			// Execute
			var actual = await fixture.AuthenticateUserAsync(new LoginData { Username = string.Empty, Password = string.Empty });

			// Verify
			_mockUserAccountRepository.VerifyAll();
			actualUsername.Should().Be(string.Empty);
			actual.Should().Be(expected);
		}

		[TestMethod]
		public async Task AuthenticateUserAsync_WhenGivenUsernameAndPasswordThatDoNotMatch_ShouldReturnFalse()
		{
			// Setup
			string actualUsername = null;
			_mockUserAccountRepository
				.Setup(repository => repository.GetUserAccountByUsernameAsync(It.IsAny<string>()))
				.Callback<string>(username => actualUsername = username)
				.ReturnsAsync(new UserAccount("user 1", "pass 1"));
			var expected = false;

			// Execute
			var actual = await fixture.AuthenticateUserAsync(new LoginData { Username = "user 1", Password = "garbled" });

			// Verify
			_mockUserAccountRepository.VerifyAll();
			actualUsername.Should().Be("user 1");
			actual.Should().Be(expected);
		}

		[TestMethod]
		public async Task AuthenticateUserAsync_WhenGivenUsernameAndPasswordThatMatch_ShouldReturnTrue()
		{
			// Setup
			string actualUsername = null;
			_mockUserAccountRepository
				.Setup(repository => repository.GetUserAccountByUsernameAsync(It.IsAny<string>()))
				.Callback<string>(username => actualUsername = username)
				.ReturnsAsync(new UserAccount("user 1", "pass 1"));
			var expected = true;

			// Execute
			var actual = await fixture.AuthenticateUserAsync(new LoginData { Username = "user 1", Password = "pass 1" });

			// Verify
			_mockUserAccountRepository.VerifyAll();
			actualUsername.Should().Be("user 1");
			actual.Should().Be(expected);
		}
	}
}

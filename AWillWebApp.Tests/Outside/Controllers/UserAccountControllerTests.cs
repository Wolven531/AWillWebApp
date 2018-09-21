// <copyright file="UserAccountControllerTests.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Tests.Outside.Controllers
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Net;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test names should contain underscores for readability")]
	[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements must be ordered by access", Justification = "Degrades test readability")]
	[SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = "Test names do not need Async")]
	[TestClass]
	public class UserAccountControllerTests : TestServerFixture
	{
		private const string BaseUrl = "https://localhost:5001";

		// TODO: declare mock repo
		//private Mock<IMonsterRepository> _mockMonstersRepository;

		protected override void RegisterServices(IServiceCollection customServices)
		{
			//_mockMonstersRepository = new Mock<IMonsterRepository>();
			//customServices.AddSingleton(_mockMonstersRepository.Object);
		}

		[TestMethod]
		public async Task GetAllUserAccounts_WhenInvoked_ShouldReturnArrayOfUserAccounts()
		{
			// Setup
			//_mockMonstersRepository.SetupGet(r => r.Monsters).Returns(new[]
			//{
			//	new Monster(),
			//	new Monster()
			//}.ToAsyncEnumerable());

			var client = TestServer.CreateClient();

			// Execute
			var response = await client.GetAsync(new Uri($"{BaseUrl}/api/users"));

			// Verify
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Headers.ContentType.MediaType.Should().Be("application/json");

			var contentString = await response.Content.ReadAsStringAsync();
			var userAccounts = JsonConvert.DeserializeObject<JObject[]>(contentString);
			userAccounts.Should().ContainSingle();
		}

		[TestMethod]
		public async Task GetUserAccount_WhenInvokedWithUserAccountNumberInRoute_ShouldReturnUserAccount()
		{
			// Setup
			//_mockMonstersRepository.SetupGet(r => r.Monsters).Returns(new[]
			//{
			//	new Monster(),
			//	new Monster()
			//}.ToAsyncEnumerable());

			var client = TestServer.CreateClient();

			// Execute
			var response = await client.GetAsync(new Uri($"{BaseUrl}/api/users/1"));

			// Verify
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Headers.ContentType.MediaType.Should().Be("application/json");

			var contentString = await response.Content.ReadAsStringAsync();
			var userAccount = JsonConvert.DeserializeObject<JObject>(contentString);
			userAccount.Should().NotBeNull();
		}

		[TestMethod]
		public async Task GetUserAccount_WhenInvokedWithUsernameInRoute_ShouldReturnUserAccount()
		{
			// Setup
			//_mockMonstersRepository.SetupGet(r => r.Monsters).Returns(new[]
			//{
			//	new Monster(),
			//	new Monster()
			//}.ToAsyncEnumerable());

			var client = TestServer.CreateClient();

			// Execute
			var response = await client.GetAsync(new Uri($"{BaseUrl}/api/users/tester"));

			// Verify
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Headers.ContentType.MediaType.Should().Be("application/json");

			var contentString = await response.Content.ReadAsStringAsync();
			var userAccount = JsonConvert.DeserializeObject<JObject>(contentString);
			userAccount.Should().NotBeNull();
		}

		[TestMethod]
		public async Task GetUserAccount_WhenInvokedWithUserAccountGuidInRoute_ShouldReturnUserAccount()
		{
			// Setup
			//_mockMonstersRepository.SetupGet(r => r.Monsters).Returns(new[]
			//{
			//	new Monster(),
			//	new Monster()
			//}.ToAsyncEnumerable());

			var client = TestServer.CreateClient();

			// Execute
			var response = await client.GetAsync(new Uri($"{BaseUrl}/api/users/98df4a83-96ea-4276-a95b-faafa9dc3039"));

			// Verify
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Headers.ContentType.MediaType.Should().Be("application/json");

			var contentString = await response.Content.ReadAsStringAsync();
			var userAccount = JsonConvert.DeserializeObject<JObject>(contentString);
			userAccount.Should().NotBeNull();
		}

		[TestMethod]
		public async Task GetUserAccount_WhenInvokedWithNonsenseInRoute_ShouldReturnNull()
		{
			// Setup
			//_mockMonstersRepository.SetupGet(r => r.Monsters).Returns(new[]
			//{
			//	new Monster(),
			//	new Monster()
			//}.ToAsyncEnumerable());

			var client = TestServer.CreateClient();

			// Execute
			var response = await client.GetAsync(new Uri($"{BaseUrl}/api/users/blahblahblah"));

			// Verify
			var stringContent = await response.Content.ReadAsStringAsync();
			response.StatusCode.Should().Be(HttpStatusCode.NoContent);
			response.Content.Headers.ContentLength.Should().Be(0);
			stringContent.Should().BeEmpty();
		}
	}
}

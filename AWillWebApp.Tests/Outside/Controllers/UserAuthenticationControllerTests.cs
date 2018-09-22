// <copyright file="UserAuthenticationControllerTests.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Tests.Outside.Controllers
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Net;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;
	using FluentAssertions;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test names should contain underscores for readability")]
	[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements must be ordered by access", Justification = "Degrades test readability")]
	[SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = "Test names do not need Async")]
	[TestClass]
	public class UserAuthenticationControllerTests : TestServerFixture
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
		public async Task AttemptToAuthenticateUserAsync_WhenInvokedWithCorrectLoginData_ShouldReturnSuccessSetToTrue()
		{
			// Setup
			//_mockMonstersRepository.SetupGet(r => r.Monsters).Returns(new[]
			//{
			//	new Monster(),
			//	new Monster()
			//}.ToAsyncEnumerable());
			var client = TestServer.CreateClient();
			var postLoginData = new JSONContent(new LoginData { Password = "test", Username = "tester" });

			// Execute
			var response = await client.PostAsync(new Uri($"{BaseUrl}/api/auth"), postLoginData);

			// Verify
			var responseData = JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());

			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Headers.ContentType.MediaType.Should().Be("application/json");
			responseData.Should().ContainKey("success");
			responseData.GetValue("success", StringComparison.Ordinal).Value<bool>().Should().BeTrue();
		}

		[TestMethod]
		public async Task AttemptToAuthenticateUserAsync_WhenInvokedWithWrongLoginData_ShouldReturnSuccessSetToFalse()
		{
			// Setup
			//_mockMonstersRepository.SetupGet(r => r.Monsters).Returns(new[]
			//{
			//	new Monster(),
			//	new Monster()
			//}.ToAsyncEnumerable());
			var client = TestServer.CreateClient();
			var postLoginData = new JSONContent(new LoginData { Password = "asdf", Username = "qwer" });

			// Execute
			var response = await client.PostAsync(new Uri($"{BaseUrl}/api/auth"), postLoginData);

			// Verify
			var responseData = JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());

			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Headers.ContentType.MediaType.Should().Be("application/json");
			responseData.Should().ContainKey("success");
			responseData.GetValue("success", StringComparison.Ordinal).Value<bool>().Should().BeFalse();
		}
	}
}

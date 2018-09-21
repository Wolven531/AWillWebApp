// <copyright file="HomeControllerTests.cs" company="AWill Inc">
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

	[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test names should contain underscores for readability")]
	[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements must be ordered by access", Justification = "Degrades test readability")]
	[SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = "Test names do not need Async")]
	[TestClass]
	public class HomeControllerTests : TestServerFixture
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
		public async Task GetIndex_WhenInvoked_ShouldReturnReactAppPage()
		{
			// Setup
			//_mockMonstersRepository.SetupGet(r => r.Monsters).Returns(new[]
			//{
			//	new Monster(),
			//	new Monster()
			//}.ToAsyncEnumerable());

			var client = TestServer.CreateClient();

			// Execute
			var response = await client.GetAsync(new Uri($"{BaseUrl}/"));

			// Verify
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Headers.ContentType.MediaType.Should().Be("text/html");

			var actual = await response.Content.ReadAsStringAsync();
			actual.Should().NotBeNullOrEmpty();
		}
	}
}

// <copyright file="HomeControllerTests.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Tests.Outside.Controllers
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Net;
	using System.Threading.Tasks;
	using AWillWebApp.Controllers;
	using AWillWebApp.Inside.Models;
	using AWillWebApp.Outside.Repositories;
	using FluentAssertions;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.TestHost;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Moq;
	using Newtonsoft.Json.Linq;

	[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test names should contain underscores for readability")]
	[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements must be ordered by access", Justification = "Degrades test readability")]
	[TestClass]
	public class HomeControllerTests : TestServerFixture
	{
		// private const string BaseUrl = "https://localhost:5001";

		// TODO: declare mock repo
		//private Mock<IMonsterRepository> _mockMonstersRepository;

		//protected override void RegisterServices(IServiceCollection customServices)
		//{
		//	_mockMonstersRepository = new Mock<IMonsterRepository>();
		//	customServices.AddSingleton(_mockMonstersRepository.Object);
		//}

		//[TestMethod]
		//public async Task GetIndex_WhenInvoked_ShouldReturnReactAppPage()
		//{
		//	// Setup
		//	//_mockMonstersRepository.SetupGet(r => r.Monsters).Returns(new[]
		//	//{
		//	//	new Monster(),
		//	//	new Monster()
		//	//}.ToAsyncEnumerable());

		//	var client = TestServer.CreateClient();
		//	var homepage = new Uri($"{BaseUrl}");

		//	// Execute
		//	var response = await client.GetAsync(homepage);

		//	// Verify
		//	homepage.AbsoluteUri.Should().Be("https://localhost:5001/");
		//	response.StatusCode.Should().Be(HttpStatusCode.OK);
		//	response.Content.Headers.ContentType.MediaType.Should().Be("application/json");
		//	//var actual = JToken.Parse(await response.Content.ReadAsStringAsync());
		//}

#pragma warning disable CA1063 // Implement IDisposable Correctly
		public override void Dispose()
#pragma warning restore CA1063 // Implement IDisposable Correctly
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}

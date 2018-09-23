// <copyright file="MonsterControllerTests.cs" company="AWill Inc">
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
	using AWillWebApp.Inside.Services;
	using FluentAssertions;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Logging.Abstractions;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Moq;
	using Newtonsoft.Json;

	[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test names should contain underscores for readability")]
	[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements must be ordered by access", Justification = "Degrades test readability")]
	[SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = "Test names do not need Async")]
	[TestClass]
	public class MonsterControllerTests : TestServerFixture
	{
		private const string BaseUrl = "https://localhost:5001";

		private Mock<IMonsterService> _MockMonsterService;

		protected override void RegisterServices(IServiceCollection customServices)
		{
			//var monster1 = new Monster("awake 1", "name 1", 3, Element.Dark, "awake img 1", "img 1", "eRL1", "eRV1", "lRL1", "lRV1", string.Empty);
			//var monster2 = new Monster("awake 2", "name 2", 5, Element.Fire, "awake img 2", "img 2", "eRL2", "eRV2", "lRL2", "lRV2", string.Empty);
			//var searchResult1 = new SearchResult(1, monster1);
			//var searchResult2 = new SearchResult(2, monster2);
			_MockMonsterService = new Mock<IMonsterService>();
			//_MockMonsterService
			//	.Setup(service => service.SearchMonsterNamesAsync(string.Empty))
			//	.ReturnsAsync(new[]
			//	{
			//		searchResult1,
			//		searchResult2
			//	});
			customServices.AddSingleton(_MockMonsterService.Object);
			customServices.AddSingleton<ILogger<MonsterController>>(NullLogger<MonsterController>.Instance);
		}

		[TestMethod]
		public async Task GetAllMonstersAsync_WhenInvoked_ShouldReturnJSONArrayOfMonstersWithoutImageData()
		{
			// Setup
			var client = TestServer.CreateClient();

			// Execute
			var response = await client.GetAsync(new Uri($"{BaseUrl}/api/monsters"));

			// Verify
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Headers.ContentType.MediaType.Should().Be("application/json");

			var actual = JsonConvert.DeserializeObject<Monster[]>(await response.Content.ReadAsStringAsync());
			actual.Should().HaveCount(2);
			actual.Select(monster => monster.Image).Should().BeEquivalentTo(new[] { string.Empty, string.Empty });
			actual.Select(monster => monster.AwakenedImage).Should().BeEquivalentTo(new[] { string.Empty, string.Empty });
		}

		[TestMethod]
		public async Task GetAllMonstersAsync_WhenInvokedWithImageParam_ShouldReturnJSONArrayOfMonstersWithImageData()
		{
			// Setup
			var client = TestServer.CreateClient();

			// Execute
			var response = await client.GetAsync(new Uri($"{BaseUrl}/api/monsters?withImages=true"));

			// Verify
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Headers.ContentType.MediaType.Should().Be("application/json");

			var actual = JsonConvert.DeserializeObject<Monster[]>(await response.Content.ReadAsStringAsync());
			actual.Should().HaveCount(2);
			actual.Select(monster => monster.Image).Should().NotContain(image => image.Length == 0);
			actual.Select(monster => monster.AwakenedImage).Should().NotContain(awakenedImage => awakenedImage.Length == 0);
		}

		[TestMethod]
		public async Task GetMonsterByIdAsync_WhenInvoked_ShouldReturnJSONMonsterWithoutImageData()
		{
			// Setup
			var client = TestServer.CreateClient();

			// Execute
			var response = await client.GetAsync(new Uri($"{BaseUrl}/api/monsters/8adb050b-3cad-4359-b5f9-b4cd4a07db00"));

			// Verify
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Headers.ContentType.MediaType.Should().Be("application/json");

			var actual = JsonConvert.DeserializeObject<Monster>(await response.Content.ReadAsStringAsync());
			actual.Image.Should().BeEmpty();
			actual.AwakenedImage.Should().BeEmpty();
		}

		[TestMethod]
		public async Task GetMonsterByIdAsync_WhenInvokedWithImageParam_ShouldReturnJSONMonsterWithImageData()
		{
			// Setup
			var client = TestServer.CreateClient();

			// Execute
			var response = await client.GetAsync(new Uri($"{BaseUrl}/api/monsters/8adb050b-3cad-4359-b5f9-b4cd4a07db00?withImages=true"));

			// Verify
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Headers.ContentType.MediaType.Should().Be("application/json");

			var actual = JsonConvert.DeserializeObject<Monster>(await response.Content.ReadAsStringAsync());
			actual.Image.Should().NotBeEmpty();
			actual.AwakenedImage.Should().NotBeEmpty();
		}

		[TestMethod]
		public async Task GetAllMonsterNames_WhenInvoked_ShouldReturnArrayOfSearchResultObjects()
		{
			// Setup
			//string searchParam = null;

			//_MockMonsterService
			//	.Setup(service => service.SearchMonsterNamesAsync(It.IsAny<string>()))
			//	//.Callback<string>(searchQuery => searchParam = searchQuery)
			//	.ReturnsAsync(new[]
			//	{
			//		searchResult1,
			//		searchResult2
			//	});
			//_MockMonsterRepository
			//	.Setup(repo => repo.GetMonsters())
			//	.ReturnsAsync(new[]
			//	{
			//		monster1,
			//		monster2
			//	});

			// Execute
			var client = TestServer.CreateClient();
			var response = await client.GetAsync(new Uri($"{BaseUrl}/api/monsters/names"));

			// Verify
			//_MockMonsterService.Verify(s => s.SearchMonsterNamesAsync(string.Empty), Times.Once);
			//_MockMonsterRepository.VerifyAll();
			//searchParam.Should().BeEquivalentTo(string.Empty);

			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Headers.ContentType.MediaType.Should().Be("application/json");

			var actual = JsonConvert.DeserializeObject<SearchResult[]>(await response.Content.ReadAsStringAsync());
			//actual.Select(searchResult => searchResult.ResultNumber).Should().BeEquivalentTo(new[] { 1, 2 });
			//actual.Select(searchResult => searchResult.Name).Should().BeEquivalentTo(new[] { "", "" });
		}
	}
}

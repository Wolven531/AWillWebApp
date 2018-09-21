// <copyright file="ImageControllerTests.cs" company="AWill Inc">
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
	public class ImageControllerTests : TestServerFixture
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
		public async Task GetMonsterImageByIdAsync_WhenInvokedWithMonsterId_ShouldReturnImageData()
		{
			// Setup
			//_mockMonstersRepository.SetupGet(r => r.Monsters).Returns(new[]
			//{
			//	new Monster(),
			//	new Monster()
			//}.ToAsyncEnumerable());

			var client = TestServer.CreateClient();

			// Execute
			var response = await client.GetAsync(new Uri($"{BaseUrl}/api/images/sleepy/8adb050b-3cad-4359-b5f9-b4cd4a07db00"));

			// Verify
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Headers.ContentType.MediaType.Should().Be("image/png");
			response.Content.Headers.ContentLength.Should().Be(11578); // TODO: why not 15440, which is the length of the base 64 image data
		}

		[TestMethod]
		public async Task GetMonsterAwakeImageByIdAsync_WhenInvokedWithMonsterId_ShouldReturnAwakenedImageData()
		{
			// Setup
			//_mockMonstersRepository.SetupGet(r => r.Monsters).Returns(new[]
			//{
			//	new Monster(),
			//	new Monster()
			//}.ToAsyncEnumerable());

			var client = TestServer.CreateClient();

			// Execute
			var response = await client.GetAsync(new Uri($"{BaseUrl}/api/images/awake/8adb050b-3cad-4359-b5f9-b4cd4a07db00"));

			// Verify
			response.StatusCode.Should().Be(HttpStatusCode.OK);
			response.Content.Headers.ContentType.MediaType.Should().Be("image/png");
			response.Content.Headers.ContentLength.Should().Be(11554); // TODO: why not 15408, which is the length of the base 64 image data
		}
	}
}

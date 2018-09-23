// <copyright file="BasicTests.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Tests.Outside.Controllers
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Microsoft.AspNetCore.Mvc.Testing;
	using Microsoft.VisualStudio.TestTools.UnitTesting;
	using Xunit;

	[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test names should contain underscores for readability")]
	[SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:Elements must be ordered by access", Justification = "Degrades test readability")]
	[SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = "Test names do not need Async")]
	[TestClass]
	public class BasicTests
	: IClassFixture<WebApplicationFactory<Startup>>
	{
		private const string _BaseURL = "https://localhost:5001";
		private readonly WebApplicationFactory<Startup> _factory;

		public BasicTests(WebApplicationFactory<Startup> factory)
		{
			_factory = factory;
		}

		[Theory]
		[InlineData("/")]
#pragma warning disable CA1054 // Uri parameters should not be strings
		public async Task Get_WhenInvokedOnAllEndpoints_ShouldReturnSuccessAndCorrectContentType(string uriPath)
#pragma warning restore CA1054 // Uri parameters should not be strings
		{
			// Arrange
			var client = _factory.CreateClient();

			// Act
			var response = await client.GetAsync(new Uri($"{_BaseURL}{uriPath}"));

			// Assert
			response.EnsureSuccessStatusCode(); // Status Code 200-299
			response.Content.Headers.ContentType.ToString().Should().BeEquivalentTo("text/html; charset=utf-8");
			// Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
		}
	}
}

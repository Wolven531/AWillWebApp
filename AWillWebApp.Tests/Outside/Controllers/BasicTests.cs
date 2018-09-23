namespace AWillWebApp.Tests.Outside.Controllers
{
	using System;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Mvc.Testing;
	using Xunit;

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
		public async Task Get_WhenInvokedOnAllEndpoints_ShouldReturnSuccessAndCorrectContentType(string uriPath)
		{
			// Arrange
			var client = _factory.CreateClient();

			// Act
			var response = await client.GetAsync(new Uri($"{_BaseURL}{uriPath}"));

			// Assert
			response.EnsureSuccessStatusCode(); // Status Code 200-299
			Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
		}
	}
}

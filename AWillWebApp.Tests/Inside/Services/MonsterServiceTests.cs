// <copyright file="MonsterServiceTests.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Tests.Inside.Services
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
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
	public class MonsterServiceTests
	{
		private MonsterService fixture;
		private Mock<IMonsterRepository> _mockMonsterRepository;

		public MonsterServiceTests()
		{
			_mockMonsterRepository = new Mock<IMonsterRepository>();
			fixture = new MonsterService(_mockMonsterRepository.Object, new Mock<ILogger<MonsterService>>().Object);
		}

		[TestMethod]
		public async Task GetMonstersAsync_WhenRepositoryIsEmpty_ShouldReturnEmptyListOfMonsters()
		{
			// Setup
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(Enumerable.Empty<Monster>());

			var expected = Enumerable.Empty<string>();

			// Execute
			var actual = await fixture.GetMonstersAsync();

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public async Task GetMonstersAsync_WhenRepositoryHasMonsters_ShouldReturnListOfMonsters()
		{
			// Setup
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(GenerateMonsters(new[] { Element.Dark, Element.Fire }));

			var expected = GenerateMonsters(new[] { Element.Dark, Element.Fire });

			// Execute
			var actual = await fixture.GetMonstersAsync();

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public async Task GetMonsterByIdAsync_WhenRepositoryHasMonsters_ShouldReturnMonster()
		{
			// Setup
			const string targetGuidStr = "421dc26d-eb34-4be4-b3f5-2f8a80d3754d";
			var monsters = GenerateMonsters(new[] { Element.Dark, Element.Fire });
			monsters.ElementAt(0).Id = Guid.Parse("2b110876-430b-42eb-bebb-6718b03c6e18");
			monsters.ElementAt(1).Id = Guid.Parse(targetGuidStr);

			Guid? monsterIdUsed = null;
			_mockMonsterRepository
				.Setup(repository => repository.GetMonster(It.IsAny<Guid>()))
				.Callback<Guid>(monsterId => monsterIdUsed = monsterId)
				.ReturnsAsync(monsters.ElementAt(1));

			var expected = monsters.ElementAt(1);

			// Execute
			var actual = await fixture.GetMonsterByIdAsync(Guid.Parse(targetGuidStr));

			// Verify
			_mockMonsterRepository.VerifyAll();
			monsterIdUsed.Should().Be(Guid.Parse(targetGuidStr));
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public async Task SearchMonsterNamesAsync_WhenRepositoryIsEmpty_ShouldReturnEmptyListOfSearchResults()
		{
			// Setup
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(Enumerable.Empty<Monster>());
			var expected = Enumerable.Empty<SearchResult>();

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync(string.Empty);

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public async Task SearchMonsterNamesAsync_WhenRepositoryHasMonstersAndQueryIsEmpty_ShouldReturnListOfSearchResults()
		{
			// Setup
			var monsters = GenerateMonsters(new[] { Element.Dark, Element.Fire });
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(monsters);

			var expected = new[]
			{
				new SearchResult(1, monsters.ElementAt(0)),
				new SearchResult(2, monsters.ElementAt(1))
			};

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync(string.Empty);

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public async Task SearchMonsterNamesAsync_WhenRepositoryHasMonstersAndQueryDoesNotMatch_ShouldReturnEmptyListOfSearchResults()
		{
			// Setup
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(GenerateMonsters(new[] { Element.Dark, Element.Fire }));
			var expected = Enumerable.Empty<SearchResult>();

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("asdfqwer");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public async Task SearchMonsterNamesAsync_WhenRepositoryHasMonstersAndQueryMatchesElement_ShouldReturnSearchResultsWithElement()
		{
			// Setup
			var monsters = GenerateMonsters(new[] { Element.Dark, Element.Fire });
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(monsters);
			var expected = new[] { new SearchResult(1, monsters.ElementAt(0)) };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("Dark");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public async Task SearchMonsterNamesAsync_WhenRepositoryHasMonstersAndQueryWithSpaceMatchesElement_ShouldReturnSearchResultsWithElement()
		{
			// Setup
			var monsters = GenerateMonsters(new[] { Element.Dark, Element.Fire });
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(monsters);
			var expected = new[] { new SearchResult(1, monsters.ElementAt(0)) };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync(" dARk ");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public async Task SearchMonsterNamesAsync_WhenRepositoryHasMonstersAndQueryMatchesElementWithWeirdCasing_ShouldReturnSearchResultsWithElement()
		{
			// Setup
			var monsters = GenerateMonsters(new[] { Element.Dark, Element.Fire });
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(monsters);
			var expected = new[] { new SearchResult(1, monsters.ElementAt(0)) };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("DaRk");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public async Task SearchMonsterNamesAsync_WhenRepositoryHasMonstersAndQueryMatchesName_ShouldReturnSearchResults()
		{
			// Setup
			var monsters = GenerateMonsters(new[] { Element.Dark, Element.Fire });
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(monsters);
			var expected = new[] { new SearchResult(1, monsters.ElementAt(0)) };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("ame 1");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public async Task SearchMonsterNamesAsync_WhenRepositoryHasMonstersAndQueryWithSpaceMatchesName_ShouldReturnSearchResults()
		{
			// Setup
			var monsters = GenerateMonsters(new[] { Element.Dark, Element.Fire });
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(monsters);
			var expected = new[] { new SearchResult(1, monsters.ElementAt(0)) };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync(" nAMe 1 ");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public async Task SearchMonsterNamesAsync_WhenRepositoryHasMonstersAndQueryMatchesNameWithWeirdCasing_ShouldReturnSearchResults()
		{
			// Setup
			var monsters = GenerateMonsters(new[] { Element.Dark, Element.Fire });
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(monsters);
			var expected = new[] { new SearchResult(1, monsters.ElementAt(0)) };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("aME 1");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public async Task SearchMonsterNamesAsync_WhenRepositoryHasMonstersAndQueryMatchesAwakenedName_ShouldReturnSearchResults()
		{
			// Setup
			var monsters = GenerateMonsters(new[] { Element.Dark, Element.Fire });
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(monsters);
			var expected = new[] { new SearchResult(1, monsters.ElementAt(0)) };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("ake 1");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public async Task SearchMonsterNamesAsync_WhenRepositoryHasMonstersAndQueryWithSpaceMatchesAwakenedName_ShouldReturnSearchResults()
		{
			// Setup
			var monsters = GenerateMonsters(new[] { Element.Dark, Element.Fire });
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(monsters);
			var expected = new[] { new SearchResult(1, monsters.ElementAt(0)) };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync(" aWAke 1 ");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public async Task SearchMonsterNamesAsync_WhenRepositoryHasMonstersAndQueryMatchesAwakenedNameWithWeirdCasing_ShouldReturnSearchResults()
		{
			// Setup
			var monsters = GenerateMonsters(new[] { Element.Dark, Element.Fire });
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(monsters);
			var expected = new[] { new SearchResult(1, monsters.ElementAt(0)) };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("aKE 1");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		private static IEnumerable<Monster> GenerateMonsters(Element[] monsterElements)
		{
			var monsters = new List<Monster>();

			for (var a = 0; a < monsterElements.Length; a++)
			{
				monsters.Add(new Monster($"awake {a + 1}", $"name {a + 1}", 3, monsterElements[a], $"awakenedimg{a + 1}", $"img{a + 1}", null, null, null, null, null));
			}

			return monsters.AsEnumerable();
		}
	}
}

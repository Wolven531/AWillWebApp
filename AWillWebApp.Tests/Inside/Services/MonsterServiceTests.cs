// <copyright file="MonsterServiceTests.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Tests.Inside.Services
{
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;
	using AWillWebApp.Inside.Services;
	using AWillWebApp.Outside.Repositories;
	using FluentAssertions;
	using Microsoft.Extensions.Logging;
	using Moq;
	using Xunit;

	[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test names should contain underscores for readability")]
	public class MonsterServiceTests
	{
		private MonsterService fixture;
		private Mock<IMonsterRepository> _mockMonsterRepository;

		public MonsterServiceTests()
		{
			_mockMonsterRepository = new Mock<IMonsterRepository>();
			fixture = new MonsterService(_mockMonsterRepository.Object, new Mock<ILogger<MonsterService>>().Object);
		}

		[Fact]
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

		[Fact]
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

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryIsEmpty_ShouldReturnEmptyListOfStrings()
		{
			// Setup
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(Enumerable.Empty<Monster>());
			var expected = Enumerable.Empty<string>();

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync(string.Empty);

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryHasMonstersAndQueryIsEmpty_ShouldReturnListOfMonsterNamesAsStrings()
		{
			// Setup
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(GenerateMonsters(new[] { Element.Dark, Element.Fire }));

			var expected = new string[] { "Dark name 1", "awake 1", "Fire name 2", "awake 2" };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync(string.Empty);

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryHasMonstersAndQueryDoesNotMatch_ShouldReturnEmptyListOfStrings()
		{
			// Setup
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(GenerateMonsters(new[] { Element.Dark, Element.Fire }));
			var expected = Enumerable.Empty<string>();

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("asdfqwer");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryHasMonstersAndQueryMatchesElement_ShouldReturnMatchingNamesOfMonstersWithElement()
		{
			// Setup
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(GenerateMonsters(new[] { Element.Dark, Element.Fire }));
			var expected = new string[] { "Dark name 1", "awake 1" };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("Dark");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryHasMonstersAndQueryWithSpaceMatchesElement_ShouldReturnMatchingNamesOfMonstersWithElement()
		{
			// Setup
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(GenerateMonsters(new[] { Element.Dark, Element.Fire }));
			var expected = new string[] { "Dark name 1", "awake 1" };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync(" dARk ");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryHasMonstersAndQueryMatchesElementWithWeirdCasing_ShouldReturnMatchingNamesOfMonstersWithElement()
		{
			// Setup
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(GenerateMonsters(new[] { Element.Dark, Element.Fire }));
			var expected = new string[] { "Dark name 1", "awake 1" };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("DaRk");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryHasMonstersAndQueryMatchesName_ShouldReturnMatchingNamesOfMonsters()
		{
			// Setup
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(GenerateMonsters(new[] { Element.Dark, Element.Fire }));
			var expected = new string[] { "Dark name 1", "awake 1" };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("ame 1");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryHasMonstersAndQueryWithSpaceMatchesName_ShouldReturnMatchingNamesOfMonsters()
		{
			// Setup
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(GenerateMonsters(new[] { Element.Dark, Element.Fire }));
			var expected = new string[] { "Dark name 1", "awake 1" };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync(" nAMe 1 ");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryHasMonstersAndQueryMatchesNameWithWeirdCasing_ShouldReturnMatchingNamesOfMonsters()
		{
			// Setup
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(GenerateMonsters(new[] { Element.Dark, Element.Fire }));
			var expected = new string[] { "Dark name 1", "awake 1" };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("aME 1");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryHasMonstersAndQueryMatchesAwakenedName_ShouldReturnMatchingAwakenedNamesOfMonsters()
		{
			// Setup
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(GenerateMonsters(new[] { Element.Dark, Element.Fire }));
			var expected = new string[] { "Dark name 1", "awake 1" };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("ake 1");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryHasMonstersAndQueryWithSpaceMatchesAwakenedName_ShouldReturnMatchingAwakenedNamesOfMonsters()
		{
			// Setup
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(GenerateMonsters(new[] { Element.Dark, Element.Fire }));
			var expected = new string[] { "Dark name 1", "awake 1" };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync(" aWAke 1 ");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryHasMonstersAndQueryMatchesAwakenedNameWithWeirdCasing_ShouldReturnMatchingAwakenedNamesOfMonsters()
		{
			// Setup
			_mockMonsterRepository
				.Setup(repository => repository.GetMonsters())
				.ReturnsAsync(GenerateMonsters(new[] { Element.Dark, Element.Fire }));
			var expected = new string[] { "Dark name 1", "awake 1" };

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("aKE 1");

			// Verify
			_mockMonsterRepository.VerifyAll();
			actual.Should().BeEquivalentTo(expected);
		}

		private IEnumerable<Monster> GenerateMonsters(Element[] monsterElements)
		{
			var monsters = new List<Monster>();

			for (var a = 0; a < monsterElements.Length; a++)
			{
				monsters.Add(new Monster($"awake {a + 1}", $"name {a + 1}", 3, false, monsterElements[a], $"awakenedimg{a + 1}", $"img{a + 1}", null, null, null, null, null));
			}

			return monsters.AsEnumerable();
		}
	}
}

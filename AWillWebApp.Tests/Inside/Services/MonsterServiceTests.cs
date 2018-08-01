// <copyright file="MonsterServiceTests.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Tests.Inside.Services
{
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;
	using AWillWebApp.Inside.Services;
	using AWillWebApp.Outside.Repositories;
	using FluentAssertions;
	using Xunit;

	[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test names should contain underscores for readability")]
	public class MonsterServiceTests
	{
		private MonsterService fixture;

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryIsEmpty_ShouldReturnEmptyListOfStrings()
		{
			// Setup
			var monsters = Enumerable.Empty<Monster>();
			var expected = Enumerable.Empty<string>();

			fixture = new MonsterService(new MonsterRepository(monsters));

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync(string.Empty);

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryHasMonstersAndQueryIsEmpty_ShouldReturnListOfMonsterNamesAsStrings()
		{
			// Setup
			var monsters = new Monster[]
			{
				new Monster("awake 1", "name 1", 3, false, Element.Dark, "awakenedimg", "img", null, null, null, null, null),
				new Monster("awake 2", "name 2", 3, false, Element.Fire, "awakenedimg2", "img2", null, null, null, null, null),
			};
			var expected = new string[] { "Dark name 1", "awake 1", "Fire name 2", "awake 2" };

			fixture = new MonsterService(new MonsterRepository(monsters));

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync(string.Empty);

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryHasMonstersAndQueryDoesNotMatch_ShouldReturnEmptyListOfStrings()
		{
			// Setup
			var monsters = new Monster[]
			{
				new Monster("awake 1", "name 1", 3, false, Element.Dark, "awakenedimg", "img", null, null, null, null, null),
				new Monster("awake 2", "name 2", 3, false, Element.Fire, "awakenedimg2", "img2", null, null, null, null, null),
			};
			var expected = Enumerable.Empty<string>();

			fixture = new MonsterService(new MonsterRepository(monsters));

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("asdfqwer");

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryHasMonstersAndQueryMatchesElement_ShouldReturnMatchingNamesOfMonstersWithElement()
		{
			// Setup
			var monsters = new Monster[]
			{
				new Monster("awake 1", "name 1", 3, false, Element.Dark, "awakenedimg", "img", null, null, null, null, null),
				new Monster("awake 2", "name 2", 3, false, Element.Fire, "awakenedimg2", "img2", null, null, null, null, null),
			};
			var expected = new string[] { "Dark name 1", "awake 1" };

			fixture = new MonsterService(new MonsterRepository(monsters));

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("Dark");

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryHasMonstersAndQueryMatchesName_ShouldReturnMatchingNamesOfMonsters()
		{
			// Setup
			var monsters = new Monster[]
			{
				new Monster("awake 1", "name 1", 3, false, Element.Dark, "awakenedimg", "img", null, null, null, null, null),
				new Monster("awake 2", "name 2", 3, false, Element.Fire, "awakenedimg2", "img2", null, null, null, null, null),
			};
			var expected = new string[] { "Dark name 1", "awake 1" };

			fixture = new MonsterService(new MonsterRepository(monsters));

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("ame 1");

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task SearchMonsterNames_WhenRepositoryHasMonstersAndQueryMatchesAwakenedName_ShouldReturnMatchingAwakenedNamesOfMonsters()
		{
			// Setup
			var monsters = new Monster[]
			{
				new Monster("awake 1", "name 1", 3, false, Element.Dark, "awakenedimg", "img", null, null, null, null, null),
				new Monster("awake 2", "name 2", 3, false, Element.Fire, "awakenedimg2", "img2", null, null, null, null, null),
			};
			var expected = new string[] { "Dark name 1", "awake 1" };

			fixture = new MonsterService(new MonsterRepository(monsters));

			// Execute
			var actual = await fixture.SearchMonsterNamesAsync("ake 1");

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}
	}
}

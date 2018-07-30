// <copyright file="MonsterRepositoryTests.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Tests.Outside.Repositories
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Threading.Tasks;
	using AWillWebApp.Inside.Models;
	using AWillWebApp.Outside.Repositories;
	using FluentAssertions;
	using Xunit;

	[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test names should contain underscores for readability")]
	public class MonsterRepositoryTests
	{
		private MonsterRepository fixture;

		[Fact]
		public async Task GetMonsterNames_WhenRepositoryIsEmpty_ShouldReturnEmptyListOfStrings()
		{
			// Setup
			var monsters = Enumerable.Empty<Monster>();
			var expected = Enumerable.Empty<string>();

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.GetMonsterNames();

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task GetMonsterNames_WhenRepositoryHasMonsters_ShouldReturnListOfMonsterNamesAsStrings()
		{
			// Setup
			var monsters = new Monster[]
			{
				new Monster("awake 1", "name 1", 3, false, Element.Dark),
				new Monster("awake 2", "name 2", 3, false, Element.Fire)
			};
			var expected = new string[] { "name 1", "name 2" };

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.GetMonsterNames();

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task GetMonsters_WhenRepositoryIsEmpty_ShouldReturnEmptyListOfMonsters()
		{
			// Setup
			var monsters = Enumerable.Empty<Monster>();
			var expected = Enumerable.Empty<Monster>();

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.GetMonsters();

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task GetMonsters_WhenRepositoryHasMonsters_ShouldReturnListOfMonstersAsModelObjects()
		{
			// Setup
			var monsters = new Monster[]
			{
				new Monster("awake 1", "name 1", 3, false, Element.Light),
				new Monster("awake 2", "name 2", 3, false, Element.Water)
			};
			var expected = monsters;

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.GetMonsters();

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task GetMonster_WhenRepositoryIsEmpty_ShouldReturnNull()
		{
			// Setup
			var monsters = Enumerable.Empty<Monster>();
			Monster expected = null;

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.GetMonster(Guid.NewGuid());

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task GetMonster_WhenRepositoryHasMonsters_ShouldReturnMonsterAsModelObject()
		{
			// Setup
			var id1 = Guid.NewGuid();
			var id2 = Guid.NewGuid();
			var expected = new Monster("awake 2", "name 2", 3, false, Element.Wind) { Id = id2 };
			var monsters = new Monster[]
			{
				new Monster("awake 1", "name 1", 3, false, Element.Light) { Id = id1 },
				expected
			};

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.GetMonster(id2);

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task AddMonster_WhenRepositoryIsEmptyAndNewMonsterLacksId_ShouldAddMonsterAndReturnIt()
		{
			// Setup
			var monsters = Enumerable.Empty<Monster>();
			var expected = new Monster("awake 1", "name 1", 3, false, Element.Fire);
			var originalId = expected.Id;

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.AddMonster(expected);

			// Verify
			actual.Should().BeEquivalentTo(expected, options => options.Excluding(monster => monster.Id));
			actual.Id.Should().NotBe(originalId);
		}

		[Fact]
		public async Task AddMonster_WhenRepositoryIsEmptyAndNewMonsterHasExistingId_ShouldAddMonsterWithIdAndReturnIt()
		{
			// Setup
			var monsters = Enumerable.Empty<Monster>();
			var expected = new Monster("awake 1", "name 1", 3, false, Element.Wind) { Id = Guid.Parse("2e846d8d-a45d-4548-9240-e2ed7fa91e3c") };

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.AddMonster(expected);

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task AddMonster_WhenRepositoryIsEmptyAndNewMonsterHasNumberZero_ShouldAddMonsterWithNumberAndReturnIt()
		{
			// Setup
			var monsters = Enumerable.Empty<Monster>();
			var newMonster = new Monster("awake 1", "name 1", 3, false, Element.Wind);
			var expected = new Monster("awake 1", "name 1", 3, false, Element.Wind) { Number = 1 };

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.AddMonster(newMonster);

			// Verify
			actual.Should().BeEquivalentTo(expected, options => options.Excluding(monster => monster.Id));
		}

		[Fact]
		public async Task AddMonster_WhenRepositoryHasMonstersAndNewMonsterHasNumberZero_ShouldAddMonsterWithNumberAndReturnIt()
		{
			// Setup
			var newMonster = new Monster("awake 1", "name 1", 3, false, Element.Wind);
			var expected = new Monster("awake 1", "name 1", 3, false, Element.Wind) { Number = 2 };
			var monsters = new Monster[]
			{
				new Monster("awake 1", "name 1", 3, false, Element.Light)
			};

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.AddMonster(newMonster);

			// Verify
			actual.Should().BeEquivalentTo(expected, options => options.Excluding(monster => monster.Id));
		}
	}
}

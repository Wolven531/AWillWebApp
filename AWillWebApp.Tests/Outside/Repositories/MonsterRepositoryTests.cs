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
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test names should contain underscores for readability")]
	[TestClass]
	public class MonsterRepositoryTests
	{
		private MonsterRepository fixture;

		[TestMethod]
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

		[TestMethod]
		public async Task GetMonsterNames_WhenRepositoryHasMonsters_ShouldReturnListOfMonsterNamesAsStrings()
		{
			// Setup
			var monsters = new Monster[]
			{
				new Monster("awake 1", "name 1", 3, Element.Dark, "awakenedimg", "img", null, null, null, null, null),
				new Monster("awake 2", "name 2", 3, Element.Fire, "awakenedimg2", "img2", null, null, null, null, null),
			};
			var expected = new string[] { "Dark name 1", "awake 1", "Fire name 2", "awake 2" };

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.GetMonsterNames();

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
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

		[TestMethod]
		public async Task GetMonsters_WhenRepositoryHasMonsters_ShouldReturnListOfMonstersAsModelObjects()
		{
			// Setup
			var monsters = new Monster[]
			{
				new Monster("awake 1", "name 1", 3, Element.Light, "awakenedimg", "img", null, null, null, null, null),
				new Monster("awake 2", "name 2", 3, Element.Water, "awakenedimg2", "img2", null, null, null, null, null)
			};
			var expected = monsters;

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.GetMonsters();

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
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

		[TestMethod]
		public async Task GetMonster_WhenRepositoryHasMonsters_ShouldReturnMonsterAsModelObject()
		{
			// Setup
			var id1 = Guid.NewGuid();
			var id2 = Guid.NewGuid();
			var expected = new Monster("awake 2", "name 2", 3, Element.Wind, "awakenedimg2", "img2", null, null, null, null, null) { Id = id2 };
			var monsters = new Monster[]
			{
				new Monster("awake 1", "name 1", 3, Element.Light, "awakenedimg", "img", null, null, null, null, null) { Id = id1 },
				expected
			};

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.GetMonster(id2);

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public async Task AddMonster_WhenRepositoryIsEmptyAndNewMonsterLacksId_ShouldAddMonsterAndReturnIt()
		{
			// Setup
			var monsters = Enumerable.Empty<Monster>();
			var expected = new Monster("awake 1", "name 1", 3, Element.Fire, "awakenedimg", "img", null, null, null, null, null);
			var originalId = expected.Id;

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.AddMonster(expected);

			// Verify
			actual.Should().BeEquivalentTo(expected, options => options.Excluding(monster => monster.Id));
			actual.Id.Should().NotBe(originalId);
		}

		[TestMethod]
		public async Task AddMonster_WhenRepositoryIsEmptyAndNewMonsterHasExistingId_ShouldAddMonsterWithIdAndReturnIt()
		{
			// Setup
			var monsters = Enumerable.Empty<Monster>();
			var expected = new Monster("awake 1", "name 1", 3, Element.Wind, "awakenedimg", "img", null, null, null, null, null) { Id = Guid.Parse("2e846d8d-a45d-4548-9240-e2ed7fa91e3c") };

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.AddMonster(expected);

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}

		[TestMethod]
		public async Task AddMonster_WhenRepositoryIsEmptyAndNewMonsterHasNumberZero_ShouldAddMonsterWithNumberAndReturnIt()
		{
			// Setup
			var monsters = Enumerable.Empty<Monster>();
			var newMonster = new Monster("awake 1", "name 1", 3, Element.Wind, "awakenedimg", "img", null, null, null, null, null);
			var expected = new Monster("awake 1", "name 1", 3, Element.Wind, "awakenedimg", "img", null, null, null, null, null) { Number = 1 };

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.AddMonster(newMonster);

			// Verify
			actual.Should().BeEquivalentTo(expected, options => options.Excluding(monster => monster.Id));
		}

		[TestMethod]
		public async Task AddMonster_WhenRepositoryHasMonstersAndNewMonsterHasNumberZero_ShouldAddMonsterWithNumberAndReturnIt()
		{
			// Setup
			var newMonster = new Monster("awake 2", "name 2", 3, Element.Wind, "awakenedimg2", "img2", null, null, null, null, null);
			var expected = new Monster("awake 2", "name 2", 3, Element.Wind, "awakenedimg2", "img2", null, null, null, null, null) { Number = 2 };
			var monsters = new Monster[]
			{
				new Monster("awake 1", "name 1", 4, Element.Light, "awakenedimg", "img", null, null, null, null, null)
			};

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.AddMonster(newMonster);

			// Verify
			actual.Should().BeEquivalentTo(expected, options => options.Excluding(monster => monster.Id));
		}
	}
}

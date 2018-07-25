using AWillWebApp.Inside.Models;
using AWillWebApp.Outside.Repositories;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AWillWebApp.Tests.Outside.Repositories
{
	public class MonsterRepositoryTests
	{
		private MonsterRepository fixture;

		public MonsterRepositoryTests()
		{

		}

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
			var monsters = new Monster[] {
				new Monster("name 1", 3, false),
				new Monster("name 2", 3, false)
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
			var monsters = new Monster[] {
				new Monster("name 1", 3, false),
				new Monster("name 2", 3, false)
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
			var expected = new Monster("name 2", 3, false) { Id = id2 };
			var monsters = new Monster[] {
				new Monster("name 1", 3, false){ Id = id1 },
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
			var expected = new Monster("name 1", 3, false);
			var originalId = expected.Id;

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.AddMonster(expected);

			// Verify
			actual.Should().BeEquivalentTo(expected, options => options.Excluding(monster => monster.Id));
			actual.Id.Should().NotBe(originalId);
		}

		[Fact]
		public async Task AddMonster_WhenRepositoryIsEmptyAndNewMonsterHasNewId_ShouldAddMonsterWithIdAndReturnIt()
		{
			// Setup
			var monsters = Enumerable.Empty<Monster>();
			var expected = new Monster("name 1", 3, false) { Id = Guid.Parse("2e846d8d-a45d-4548-9240-e2ed7fa91e3c") };

			fixture = new MonsterRepository(monsters);

			// Execute
			var actual = await fixture.AddMonster(expected);

			// Verify
			actual.Should().BeEquivalentTo(expected);
		}
	}
}

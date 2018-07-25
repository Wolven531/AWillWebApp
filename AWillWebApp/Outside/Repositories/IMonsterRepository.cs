using AWillWebApp.Inside.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWillWebApp.Outside.Repositories
{
	public interface IMonsterRepository
	{
		Task<Monster> GetMonster(Guid id);

		Task<IEnumerable<string>> GetMonsterNames();

		Task<IEnumerable<Monster>> GetMonsters();

		Task<Monster> AddMonster(Monster newMonster);
	}
}

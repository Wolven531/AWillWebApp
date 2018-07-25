using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWillWebApp.Inside.Models
{
	public class Monster
	{
		public Monster(string name, int stars, Boolean isAwake)
		{
			Name = name;
			Stars = stars;
			IsAwake = isAwake;
		}

		public string Name { get; }

		public int Stars { get; }

		public bool IsAwake { get; }

		public Guid Id { get; set; }
	}
}

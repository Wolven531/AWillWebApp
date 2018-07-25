// <copyright file="Monster.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Inside.Models
{
	using System;

	public class Monster
	{
		public Monster(string name, int stars, bool isAwake)
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

// <copyright file="Monster.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Inside.Models
{
	using System;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Converters;

	public class Monster
	{
		public Monster(string awakenedName, string name, int stars, bool isAwake, Element element)
		{
			AwakenedName = awakenedName;
			Name = name;
			Stars = stars;
			IsAwake = isAwake;
			Element = element;
		}

		/// <summary>
		/// Gets or sets the Id property of this monster
		/// This property should be used to uniquely identify this monster within the current instance
		/// of the repository
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the Number property of this monster
		/// This property should be used to uniquely identify this monster across instances of the repository
		/// </summary>
		public int Number { get; set; }

		public string AwakenedName { get; }

		public string Name { get; }

		public int Stars { get; }

		public bool IsAwake { get; }

		[JsonConverter(typeof(StringEnumConverter))]
		public Element Element { get; }
	}
}

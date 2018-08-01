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
		public Monster(
			string awakenedName,
			string name,
			int rating,
			bool isAwake,
			Element element,
			string awakenedImage,
			string image,
			string earlyRuneList,
			string earlyRuneValues,
			string lateRuneList,
			string lateRuneValues,
			string statPriority)
		{
			AwakenedName = awakenedName;
			Name = name;
			Rating = rating;
			IsAwake = isAwake;
			Element = element;
			AwakenedImage = awakenedImage;
			Image = image;
			EarlyRuneList = earlyRuneList;
			EarlyRuneValues = earlyRuneValues;
			LateRuneList = lateRuneList;
			LateRuneValues = lateRuneValues;
			StatPriority = statPriority;
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

		public string SearchableName { get => $"{Element} {Name}"; }

		public int Rating { get; }

		public bool IsAwake { get; }

		[JsonConverter(typeof(StringEnumConverter))]
		public Element Element { get; }

		public string AwakenedImage { get; }

		public string Image { get; }

		public string EarlyRuneList { get; }

		public string EarlyRuneValues { get; }

		public string LateRuneList { get; }

		public string LateRuneValues { get; }

		public string StatPriority { get; }
	}
}

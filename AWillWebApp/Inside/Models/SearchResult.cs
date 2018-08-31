// <copyright file="SearchResult.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Inside.Models
{
	public class SearchResult
	{
		public SearchResult(int resultNumber, Monster monster)
		{
			ResultNumber = resultNumber;
			Name = monster.SearchableName;
			AwakenedName = monster.AwakenedName;
			Image = monster.Image;
			AwakenedImage = monster.AwakenedImage;
		}

		// public SearchResult(int resultNumber, string name, string awakenedName, string image, string awakenedImage)
		// {
		// 	ResultNumber = resultNumber;
		// 	Name = name;
		// 	AwakenedName = awakenedName;
		// 	Image = image;
		// 	AwakenedImage = awakenedImage;
		// }

		public int ResultNumber { get; }

		public string Name { get; }

		public string AwakenedName { get; }

		public string Image { get; }

		public string AwakenedImage { get; }
	}
}

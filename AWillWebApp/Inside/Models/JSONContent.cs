// <copyright file="JSONContent.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Inside.Models
{
	using System.Net.Http;
	using System.Net.Http.Headers;
	using Newtonsoft.Json;

	public class JSONContent : StringContent
	{
		public JSONContent(object data)
			: base(JsonConvert.SerializeObject(data))
		{
			Headers.ContentType = new MediaTypeHeaderValue("application/json");
		}
	}
}

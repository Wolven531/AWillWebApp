// <copyright file="Utility.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp
{
	using System.Diagnostics.CodeAnalysis;
	using Microsoft.AspNetCore;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;

	[ExcludeFromCodeCoverage]
	public static class Utility
	{
		private const float KilobyteScale = 1000.0f;
		private const float MegabyteScale = 1000.0f * KilobyteScale;

		public static void LogResponseSize(ILogger instanceLogger, string responseString)
		{
			var responseSize = responseString.Length;
			var sizeDisplay = string.Empty;

			if (responseSize > MegabyteScale)
			{
				var mbSize = responseSize / MegabyteScale;
				sizeDisplay = $"{mbSize:n2} Mb";
			}
			else if (responseSize > KilobyteScale)
			{
				var kbSize = responseSize / KilobyteScale;
				sizeDisplay = $"{kbSize:n2} Kb";
			}

			instanceLogger.LogDebug($"Response Size = {sizeDisplay} ({responseSize:n0} bytes)");
		}
	}
}

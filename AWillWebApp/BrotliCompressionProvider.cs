// <copyright file="BrotliCompressionProvider.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.IO;
	using System.IO.Compression;
	using Microsoft.AspNetCore;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.ResponseCompression;
	using Microsoft.Extensions.Logging;

	[ExcludeFromCodeCoverage]
	public class BrotliCompressionProvider : ICompressionProvider
	{
		private readonly ILogger<BrotliCompressionProvider> _Logger;

		public BrotliCompressionProvider(ILogger<BrotliCompressionProvider> logger)
		{
			_Logger = logger;
			Console.WriteLine($"[BrotliCompressionProvider] Creating compression provider (awill)...?");
			_Logger.LogCritical($"[BrotliCompressionProvider] Creating compression provider (awill)...?");
		}

		public string EncodingName => "br";

		public bool SupportsFlush => true;

		public Stream CreateStream(Stream outputStream)
		{
			Console.WriteLine($"[CreateStream] Creating stream (awill)...?");
			_Logger.LogCritical($"[CreateStream] Creating stream (awill)...?");
			return new BrotliStream(outputStream, CompressionMode.Compress);
		}
	}
}

﻿// <copyright file="BrotliCompressionService.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp
{
	using System.Diagnostics.CodeAnalysis;
	using System.IO;
	using System.IO.Compression;
	using Microsoft.AspNetCore.ResponseCompression;
	using Microsoft.Extensions.Logging;

	[ExcludeFromCodeCoverage]
	public class BrotliCompressionService : ICompressionProvider
	{
		private readonly ILogger<BrotliCompressionService> _Logger;

		public BrotliCompressionService(ILogger<BrotliCompressionService> logger)
		{
			_Logger = logger;
			_Logger.LogCritical("[BrotliCompressionProvider] Creating compression provider");
		}

		public string EncodingName => "br";

		public bool SupportsFlush => true;

		public Stream CreateStream(Stream outputStream)
		{
			return new BrotliStream(outputStream, CompressionMode.Compress);
		}
	}
}

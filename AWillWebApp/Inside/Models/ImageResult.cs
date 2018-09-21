// <copyright file="ImageResult.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// Inspired by
// https://blogs.msdn.microsoft.com/miah/2008/11/13/extending-mvc-returning-an-image-from-a-controller-action/
// </copyright>

namespace AWillWebApp.Inside.Models
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.IO;
	using System.Net;
	using Microsoft.AspNetCore.Mvc;

	[ExcludeFromCodeCoverage]
	public class ImageResult : ActionResult
	{
		public ImageResult(Stream imageStream, string contentType)
		{
			ImageStream = imageStream ?? throw new ArgumentNullException(nameof(imageStream));
			ContentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
		}

		public Stream ImageStream { get; private set; }

		public string ContentType { get; private set; }

		public override void ExecuteResult(ActionContext context)
		{
			base.ExecuteResult(context);

			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			var buffer = new byte[4096];
			var response = context.HttpContext.Response;
			var shouldReadStream = true;

			response.StatusCode = (int)HttpStatusCode.OK;
			response.ContentType = ContentType;
			response.ContentLength = ImageStream.Length;

			while (shouldReadStream)
			{
				var lastByteRead = ImageStream.Read(buffer, 0, buffer.Length);
				shouldReadStream = lastByteRead != 0; // NOTE: if last byte was not zero, keep going

				if (shouldReadStream)
				{
					response.Body.Write(buffer, 0, lastByteRead);
				}
			}

			//response.End();
		}
	}
}

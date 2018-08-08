// <copyright file="UserAccount.cs" company="AWill Inc">
// Copyright (c) 2018 AWill Inc; All rights reserved.
// </copyright>

namespace AWillWebApp.Inside.Models
{
	using System;
	using System.Security.Cryptography;

	public class UserAccount
	{
		private const int DerivedKeyLength = 128 / 8;
		private const int IterationCount = 10000;
		private const int SaltByteLength = 128 / 8;

		private byte[] _passwordSalt;

		public UserAccount(string username, string password)
		{
			Username = username;

			_passwordSalt = GenerateRandomSalt();
			HashedPassword = CreatePasswordHash(password, _passwordSalt);
		}

		/// <summary>
		/// Gets or sets the Id property of this account
		/// This property should be used to uniquely identify this account within the current instance
		/// of the repository
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the Number property of this account
		/// This property should be used to uniquely identify this account across instances of the repository
		/// </summary>
		public int Number { get; set; }

		public string Username { get; }

		public string HashedPassword { get; }

		// NOTE: see https://lockmedown.com/hash-right-implementing-pbkdf2-net/
		public bool VerifyPassword(string passwordGuess, string savedHash)
		{
			// Ingredient #1: password salt byte array
			// Ingredient #2: byte array of password
			var actualPasswordBytes = new byte[DerivedKeyLength];
			// Ingredient #3: saved hash bytes
			var savedHashBytes = Convert.FromBase64String(savedHash);
			// Ingredient #4: iteration count
			var iterationCountLength = savedHashBytes.Length - (_passwordSalt.Length + actualPasswordBytes.Length);
			var iterationCountBytes = new byte[iterationCountLength];

			Buffer.BlockCopy(savedHashBytes, 0, _passwordSalt, 0, SaltByteLength);
			Buffer.BlockCopy(savedHashBytes, SaltByteLength, actualPasswordBytes, 0, actualPasswordBytes.Length);
			Buffer.BlockCopy(savedHashBytes, _passwordSalt.Length + actualPasswordBytes.Length, iterationCountBytes, 0, iterationCountLength);

			var passwordGuessBytes = GenerateHashValue(passwordGuess, _passwordSalt, BitConverter.ToInt32(iterationCountBytes, 0));

			return ConstantTimeComparison(passwordGuessBytes, actualPasswordBytes);
		}

		private static bool ConstantTimeComparison(byte[] passwordGuess, byte[] actualPassword)
		{
			var difference = (uint)passwordGuess.Length ^ (uint)actualPassword.Length;
			for (var i = 0; i < passwordGuess.Length && i < actualPassword.Length; i++)
			{
				difference |= (uint)(passwordGuess[i] ^ actualPassword[i]);
			}

			return difference == 0;
		}

		private static string CreatePasswordHash(string password, byte[] salt)
		{
			var hashValue = GenerateHashValue(password, salt, IterationCount);
			var iterationCountBytes = BitConverter.GetBytes(IterationCount);
			var valueToSave = new byte[SaltByteLength + DerivedKeyLength + iterationCountBytes.Length];
			Buffer.BlockCopy(salt, 0, valueToSave, 0, SaltByteLength);
			Buffer.BlockCopy(hashValue, 0, valueToSave, SaltByteLength, DerivedKeyLength);
			Buffer.BlockCopy(iterationCountBytes, 0, valueToSave, salt.Length + hashValue.Length, iterationCountBytes.Length);
			return Convert.ToBase64String(valueToSave);
		}

		private static byte[] GenerateHashValue(string password, byte[] salt, int iterationCount)
		{
			byte[] hashValue;
			var valueToHash = string.IsNullOrEmpty(password) ? string.Empty : password;
			using (var pbkdf2 = new Rfc2898DeriveBytes(valueToHash, salt, iterationCount))
			{
				hashValue = pbkdf2.GetBytes(DerivedKeyLength);
			}

			return hashValue;
		}

		private static byte[] GenerateRandomSalt()
		{
			var cryptoRNG = new RNGCryptoServiceProvider();
			var salt = new byte[SaltByteLength];
			cryptoRNG.GetBytes(salt);
			return salt;
		}
	}
}

using System;

namespace ServerPlugin.GameManagement
{
	public static class RandomLetterGenarator
	{
		private const string supportedLetters = "abcdefghijklmnoprstuwz";
		private static Random random = new Random();

		public static char GetRandomLetter()
		{
			return supportedLetters[random.Next(0, supportedLetters.Length)];
		}
	}
}

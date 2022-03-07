using System;

namespace ServerPlugin.GameManagement
{
	public static class RandomLetterGenarator
	{
		public const string SupportedLetters = "abcdefghijklmnoprstuwz";
		private static Random random = new Random();

		public static char GetRandomLetter()
		{
			return SupportedLetters[random.Next(0, SupportedLetters.Length)];
		}
	}
}

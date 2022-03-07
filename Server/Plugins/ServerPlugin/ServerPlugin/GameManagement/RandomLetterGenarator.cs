using System;
using System.Collections.Generic;

namespace ServerPlugin.GameManagement
{
	public static class RandomLetterGenarator
	{
		public const string SupportedLetters = "abcdefghijklmnoprstuwz";
		private static Random random = new Random();

		public static char GetRandomLetter(List<char> usedLetters)
		{
			var lettersToChoose = SupportedLetters;
			foreach (var item in usedLetters)
			{
				lettersToChoose.Replace(item.ToString(), string.Empty);
			}
			return SupportedLetters[random.Next(0, SupportedLetters.Length)];
		}
	}
}

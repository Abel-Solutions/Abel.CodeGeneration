using System;

namespace Abel.MetaCode.Tests.Extensions
{
	public static class StringExtensions
	{
		public static string RemoveSpecialCharacters(this string text) => text
			.Replace("\t", "")
			.Replace(Environment.NewLine, "");
	}
}

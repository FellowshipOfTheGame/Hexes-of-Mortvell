using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace HexesOfMortvell.Util
{
	public static class CsvReader
	{
		public static IEnumerable<IEnumerable<T>> FromText<T>(
			string text,
			char[] separators,
			Func<string, T> evalFunction)
		{
			var wordMatrix = FromText(text, separators);
			return wordMatrix
				.Select(row => ConvertRow(row, evalFunction));
		}

		public static IEnumerable<IEnumerable<string>> FromText(
			string text,
			char[] separators)
		{
			using (var reader = new StringReader(text))
			{
				string row;
				while ((row = reader.ReadLine()) != null)
					yield return row.Split(separators);
			}
		}

		private static IEnumerable<T> ConvertRow<T>(
			IEnumerable<string> row,
			Func<string, T> evalFunction)
		{
			return row.Select(evalFunction);
		}
	}
}
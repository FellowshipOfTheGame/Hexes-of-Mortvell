using System;
using System.Text;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace HexesOfMortvell.Util
{
	public class CsvReader<T>
	{
		private StringReader reader;
		private string lineSeparator;
		private HashSet<char> entrySeparators;
		private Func<string, T> conversor;

		public CsvReader(
			string text,
			Func<string, T> conversor,
			char entrySeparator=',',
			string lineSeparator=null)
		{
			this.reader = new StringReader(text);
			this.conversor = conversor;
			this.lineSeparator = lineSeparator ?? Environment.NewLine;

			this.entrySeparators = new HashSet<char>();
			foreach (var newLineChar in this.lineSeparator)
				this.entrySeparators.Add(newLineChar);
			this.entrySeparators.Add(entrySeparator);
		}

		public List<List<T>> ToMatrix()
		{
			return GetRows()
				.Select(row => row.ToList())
				.ToList();
		}

		IEnumerable<IEnumerable<T>> GetRows()
		{
			IEnumerable<T> row;
			while ((row = NextRow()) != null)
				yield return row;
		}

		IEnumerable<T> NextRow()
		{
			var line = NextLine();
			return line?.Select(this.conversor);
		}

		IEnumerable<string> NextLine()
		{
			var line = this.reader.ReadLine();
			var separators = this.entrySeparators.ToArray();
			var entries = line?.Split(separators);
			var nonemptyEntries = entries?.TakeWhile(s => s.Length > 0);
			return nonemptyEntries;
		}
	}

	public class CsvReader : CsvReader<string>
	{
		public CsvReader(
			string text,
			string lineSeparator=null,
			char entrySeparator=',')
		: base(text, s => s, entrySeparator, lineSeparator) {}
	}
}
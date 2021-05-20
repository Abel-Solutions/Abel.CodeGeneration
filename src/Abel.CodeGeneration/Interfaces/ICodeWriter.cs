using System;
using System.Collections.Generic;

namespace Abel.CodeGeneration.Interfaces
{
	public interface ICodeWriter
	{
		void WriteLine(string line);

		void WriteLines(IEnumerable<string> lines);

		void WriteScoped<TGenerator>(string line, TGenerator generator, Action<TGenerator> action);
	}
}
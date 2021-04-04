using System;
using System.Collections.Generic;

namespace Abel.MetaCode.Interfaces
{
	public interface IGenerator<out TGenerator>
	{
		TGenerator AddLine();

		TGenerator AddLine(string line);

		TGenerator AddLines(IEnumerable<string> lines);

		TGenerator AddScoped<TScope>(string line, TScope generator, Action<TScope> action);

		TGenerator AddScoped(string line, Action<TGenerator> action);
	}
}
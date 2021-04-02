using System;
using System.Collections.Generic;

namespace Abel.MetaCode.Interfaces
{
	public interface IGenerator<out TGenerator>
	{
		TGenerator AddLine();

		TGenerator AddLine(string line);

		TGenerator AddLines(IEnumerable<string> lines);

		TGenerator AddScoped<TScope>(string line, TScope scopeGenerator, Action<TScope> action);
	}
}
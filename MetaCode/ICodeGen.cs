using System;
using System.Collections.Generic;

namespace MetaCode
{
	public interface ICodeGen
	{
		ICodeGen AddLine();

		ICodeGen AddLine(string line);

		ICodeGen AddLines(IEnumerable<string> lines);

		ICodeGen AddScoped(string line, Action<ICodeGen> action);

		string Generate();

		//ICodeGen AddScopes<T>(IEnumerable<T> enumerable, Func<T, string> func, Action<ICodeGen> action);
	}
}
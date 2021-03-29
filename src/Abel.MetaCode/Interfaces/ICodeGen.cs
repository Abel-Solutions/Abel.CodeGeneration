using System;
using System.Collections.Generic;

namespace Abel.MetaCode.Interfaces
{
	public interface ICodeGen
	{
		ICodeGen AddLine();

		ICodeGen AddLine(string line);

		ICodeGen AddLines(IEnumerable<string> lines);

		ICodeGen AddScoped(string line, Action<ICodeGen> action);

		string Generate();
	}
}
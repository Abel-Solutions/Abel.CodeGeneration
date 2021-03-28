using System;

namespace MetaCode
{
	public interface ICodeGen
	{
		ICodeGen AddLine();

		ICodeGen AddLine(string line);

		ICodeGen AddScoped(string line, Action<ICodeGen> action);

		string Generate();
	}
}
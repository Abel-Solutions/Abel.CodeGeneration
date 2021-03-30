using System.Collections.Generic;

namespace Abel.MetaCode.Interfaces
{
	public interface IMethodGenerator
	{
		IMethodGenerator AddLine();

		IMethodGenerator AddLine(string line);

		IMethodGenerator AddLines(IEnumerable<string> lines);
	}
}
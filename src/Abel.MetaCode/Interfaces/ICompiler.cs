using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Abel.MetaCode.Interfaces
{
	public interface ICompiler
	{
		Assembly Compile(string code, OutputKind outputKind = OutputKind.DynamicallyLinkedLibrary);

		ICompiler AddReference<T>();
	}
}
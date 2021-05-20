using System;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Abel.CodeGeneration.Interfaces
{
	public interface ICompiler
	{
		Assembly Compile(string code, OutputKind outputKind = OutputKind.DynamicallyLinkedLibrary);

		ICompiler WithReference<T>();

		ICompiler WithReference(Type type);
	}
}
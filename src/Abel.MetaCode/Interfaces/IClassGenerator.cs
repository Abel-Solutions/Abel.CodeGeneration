using System;
using System.Collections.Generic;
using System.Reflection;

namespace Abel.MetaCode.Interfaces
{
	public interface IClassGenerator
	{
		IClassGenerator AddLine();

		IClassGenerator AddLine(string line);

		IClassGenerator AddLines(IEnumerable<string> lines);

		IClassGenerator AddScoped(string line, Action<IMethodGenerator> action);

		IClassGenerator AddConstructor(Action<IMethodGenerator> action);

		IWithConstructor AddConstructor();

		IWithMethod AddMethod(string methodName);

		IWithMethod AddMethod<TResult>(string methodName);

		IClassGenerator AddMethod(MethodInfo methodInfo, Action<IMethodGenerator> action);
	}
}
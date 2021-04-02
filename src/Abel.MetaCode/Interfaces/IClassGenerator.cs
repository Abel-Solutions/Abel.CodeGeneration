using System;
using System.Collections.Generic;
using System.Reflection;

namespace Abel.MetaCode.Interfaces
{
	public interface IClassGenerator : IGenerator
	{
		IClassGenerator AddLine();

		IClassGenerator AddLine(string line);

		IClassGenerator AddLines(IEnumerable<string> lines);

		IClassGenerator AddScoped<TGenerator>(string line, TGenerator generator, Action<TGenerator> action);

		IClassGenerator AddConstructor(Action<IMethodGenerator> action);

		IClassGenerator AddConstructor(string parameters, Action<IMethodGenerator> action);

		IWithConstructor AddConstructor();

		IWithMethod AddMethod(string methodName);

		IWithMethod AddMethod<TResult>(string methodName);

		IClassGenerator AddMethod(string methodName, Action<IMethodGenerator> action);

		IClassGenerator AddMethod<TResult>(string methodName, Action<IMethodGenerator> action);

		IClassGenerator AddMethod(MethodInfo methodInfo, Action<IMethodGenerator> action);

		IClassGenerator AddProperty<T>(string propertyName, T value);

		IWithProperty AddProperty(string propertyName);

		IWithProperty AddProperty<T>(string propertyName);
	}
}
using System;
using System.Collections.Generic;
using System.Reflection;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class ClassGenerator : Generator, IClassGenerator
	{
		private readonly string _name;

		public ClassGenerator(string name, ICodeWriter codeWriter)
			: base(codeWriter) =>
			_name = name;

		public IClassGenerator AddLine() => AddLine(this);

		public IClassGenerator AddLine(string line) => AddLine(line, this);

		public IClassGenerator AddLines(IEnumerable<string> lines) => AddLines(lines, this);

		public IClassGenerator AddConstructor(Action<IMethodGenerator> action) =>
			AddScoped($"public {_name}()", ToMethodGenerator(), action, this);

		public IClassGenerator AddConstructor(string parameters, Action<IMethodGenerator> action) =>
			AddConstructor()
				.WithParameters(parameters)
				.WithContent(action);

		public IWithConstructor AddConstructor() =>
			new WithConstructor(_name, this);

		public IWithMethod AddMethod(string methodName) =>
			new WithMethod(methodName, this);

		public IWithMethod AddMethod<TResult>(string methodName) =>
			AddMethod(methodName)
				.WithReturnType<TResult>();

		public IClassGenerator AddMethod(string methodName, Action<IMethodGenerator> action) =>
			AddMethod(methodName)
				.WithContent(action);

		public IClassGenerator AddMethod<TResult>(string methodName, Action<IMethodGenerator> action) =>
			AddMethod<TResult>(methodName)
				.WithContent(action);

		public IClassGenerator AddMethod(MethodInfo methodInfo, Action<IMethodGenerator> action) =>
			AddMethod(methodInfo.Name)
				.WithReturnType(methodInfo.ReturnType)
				.WithParameters(methodInfo.GetParameters())
				.WithContent(action);

		public IClassGenerator AddProperty<T>(string propertyName, T value) =>
			AddLine($"public {typeof(T).Name} {propertyName} => {value};");

		public IWithProperty AddProperty(string propertyName) =>
			new WithProperty(propertyName, this);

		public IWithProperty AddProperty<T>(string propertyName) =>
			AddProperty(propertyName)
				.WithReturnType<T>();
	}
}

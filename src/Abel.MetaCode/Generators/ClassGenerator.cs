using System;
using System.Collections.Generic;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class ClassGenerator : IClassGenerator
	{
		private readonly string _name;
		private readonly ICodeWriter _codeWriter;

		public ClassGenerator(string name, ICodeWriter codeWriter)
		{
			_name = name;
			_codeWriter = codeWriter;
		}

		public IClassGenerator AddLine() => AddLine(string.Empty);

		public IClassGenerator AddLine(string line)
		{
			_codeWriter.WriteLine(line);
			return this;
		}

		public IClassGenerator AddLines(IEnumerable<string> lines)
		{
			_codeWriter.WriteLines(lines);
			return this;
		}

		public IClassGenerator AddConstructor(Action<IMethodGenerator> action) =>
			AddScoped($"public {_name}()", action);

		public IClassGenerator AddScoped(string line, Action<IMethodGenerator> action)
		{
			_codeWriter.WriteScoped(line, ToMethodGenerator(), action);
			return this;
		}

		public IWithConstructor AddConstructor() =>
			new WithConstructor(_name, this);

		public IWithMethod AddMethod(string methodName) =>
			new WithMethod(methodName, this);

		private IMethodGenerator ToMethodGenerator() =>
			new MethodGenerator(_codeWriter);
	}
}

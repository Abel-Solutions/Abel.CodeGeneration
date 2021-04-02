using System;
using System.Collections.Generic;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public abstract class Generator : IGenerator
	{
		protected readonly ICodeWriter _codeWriter; // todo why protected?

		protected Generator(ICodeWriter codeWriter) => _codeWriter = codeWriter;

		public IGenerator AddLine() => AddLine(string.Empty);

		public IGenerator AddLine(string line)
		{
			_codeWriter.WriteLine(line);
			return this;
		}

		public IGenerator AddLines(IEnumerable<string> lines)
		{
			_codeWriter.WriteLines(lines);
			return this;
		}

		public IGenerator AddScoped<T>(string line, T generator, Action<T> action)
		{
			_codeWriter.WriteScoped(line, generator, action);
			return this;
		}

		public IClassGenerator ToClassGenerator(string className) => new ClassGenerator(className, _codeWriter);

		public IPropertyGenerator ToPropertyGenerator() => new PropertyGenerator(_codeWriter);

		public IMethodGenerator ToMethodGenerator() => new MethodGenerator(_codeWriter);
	}
}

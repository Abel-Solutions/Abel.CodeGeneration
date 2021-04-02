using System;
using System.Collections.Generic;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public abstract class Generator<TGenerator> : IGenerator
	{
		protected readonly ICodeWriter _codeWriter; // todo why protected?

		protected Generator(ICodeWriter codeWriter) => _codeWriter = codeWriter;

		public TGenerator AddLine(TGenerator generator) => AddLine(string.Empty, generator);

		public TGenerator AddLine(string line, TGenerator generator)
		{
			_codeWriter.WriteLine(line);
			return generator;
		}

		public TGenerator AddLines(IEnumerable<string> lines, TGenerator generator)
		{
			_codeWriter.WriteLines(lines);
			return generator;
		}

		public TGenerator AddScoped<T>(string line, T generator, Action<T> action, TGenerator returnGenerator)
		{
			_codeWriter.WriteScoped(line, generator, action);
			return returnGenerator;
		}

		public IClassGenerator ToClassGenerator(string className) => new ClassGenerator(className, _codeWriter);

		public IPropertyGenerator ToPropertyGenerator() => new PropertyGenerator(_codeWriter);

		public IMethodGenerator ToMethodGenerator() => new MethodGenerator(_codeWriter);
	}
}

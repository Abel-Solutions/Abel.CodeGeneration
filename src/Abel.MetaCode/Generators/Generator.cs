using System;
using System.Collections.Generic;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public abstract class Generator : IGenerator
	{
		protected readonly ICodeWriter CodeWriter;

		protected Generator(ICodeWriter codeWriter) => CodeWriter = codeWriter;

		public TGenerator AddLine<TGenerator>(TGenerator generator) => AddLine(string.Empty, generator);

		public TGenerator AddLine<TGenerator>(string line, TGenerator generator)
		{
			CodeWriter.WriteLine(line);
			return generator;
		}

		public TGenerator AddLines<TGenerator>(IEnumerable<string> lines, TGenerator generator)
		{
			CodeWriter.WriteLines(lines);
			return generator;
		}

		public IGenerator AddScoped<T>(string line, T generator, Action<T> action)
		{
			CodeWriter.WriteScoped(line, generator, action);
			return this;
		}

		public IClassGenerator ToClassGenerator(string className) => new ClassGenerator(className, CodeWriter);

		public IPropertyGenerator ToPropertyGenerator() => new PropertyGenerator(CodeWriter);

		public IMethodGenerator ToMethodGenerator() => new MethodGenerator(CodeWriter);
	}
}

using System;
using System.Collections.Generic;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public abstract class Generator
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

		public TGenerator AddScoped<T, TGenerator>(string line, T generator, Action<T> action, TGenerator returnGenerator)
		{
			CodeWriter.WriteScoped(line, generator, action);
			return returnGenerator;
		}

		public IClassGenerator ToClassGenerator(string className) => new ClassGenerator(className, CodeWriter);

		public IPropertyGenerator ToPropertyGenerator() => new PropertyGenerator(CodeWriter);

		public IMethodGenerator ToMethodGenerator() => new MethodGenerator(CodeWriter);
	}
}

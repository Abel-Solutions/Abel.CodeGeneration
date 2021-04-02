using System;
using System.Collections.Generic;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public abstract class Generator : IGenerator
	{
		protected readonly ICodeWriter CodeWriter;

		protected Generator(ICodeWriter codeWriter) => CodeWriter = codeWriter;

		public IGenerator AddLine() => AddLine(string.Empty);

		public IGenerator AddLine(string line)
		{
			CodeWriter.WriteLine(line);
			return this;
		}

		public IGenerator AddLines(IEnumerable<string> lines)
		{
			CodeWriter.WriteLines(lines);
			return this;
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

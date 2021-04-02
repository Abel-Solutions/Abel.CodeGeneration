﻿using System;
using System.Collections.Generic;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public abstract class Generator<TGenerator> : IGenerator<TGenerator>
	{
		protected readonly ICodeWriter CodeWriter;

		private TGenerator CastedGenerator => (TGenerator)(object)this;

		protected Generator(ICodeWriter codeWriter) => CodeWriter = codeWriter;

		public TGenerator AddLine() => AddLine(string.Empty);

		public TGenerator AddLine(string line)
		{
			CodeWriter.WriteLine(line);
			return CastedGenerator;
		}

		public TGenerator AddLines(IEnumerable<string> lines)
		{
			CodeWriter.WriteLines(lines);
			return CastedGenerator;
		}

		public TGenerator AddScoped<TScope>(string line, TScope scopeGenerator, Action<TScope> action)
		{
			CodeWriter.WriteScoped(line, scopeGenerator, action);
			return CastedGenerator;
		}

		public IClassGenerator ToClassGenerator(string className) => new ClassGenerator(className, CodeWriter);

		public IPropertyGenerator ToPropertyGenerator() => new PropertyGenerator(CodeWriter);

		public IMethodGenerator ToMethodGenerator() => new MethodGenerator(CodeWriter);
	}
}

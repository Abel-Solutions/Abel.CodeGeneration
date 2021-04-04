using System;
using System.Collections.Generic;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public abstract class Generator<TGenerator> : IGenerator<TGenerator>
	{
		private readonly ICodeWriter _codeWriter;

		private TGenerator This => (TGenerator)(object)this;

		protected Generator(ICodeWriter codeWriter) => _codeWriter = codeWriter;

		public TGenerator AddLine() => AddLine(string.Empty);

		public TGenerator AddLine(string line)
		{
			_codeWriter.WriteLine(line);
			return This;
		}

		public TGenerator AddLines(IEnumerable<string> lines)
		{
			_codeWriter.WriteLines(lines);
			return This;
		}

		public TGenerator AddScoped<TScope>(string line, TScope generator, Action<TScope> action)
		{
			_codeWriter.WriteScoped(line, generator, action);
			return This;
		}

		public TGenerator AddScoped(string line, Action<TGenerator> action) => AddScoped(line, This, action);

		public IClassGenerator ToClassGenerator(string className) => new ClassGenerator(className, _codeWriter);

		public IPropertyGenerator ToPropertyGenerator() => new PropertyGenerator(_codeWriter);

		public IMethodGenerator ToMethodGenerator() => new MethodGenerator(_codeWriter);
	}
}

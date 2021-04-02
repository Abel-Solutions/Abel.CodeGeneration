using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class PropertyGenerator : Generator<IPropertyGenerator>, IPropertyGenerator
	{
		public PropertyGenerator(ICodeWriter codeWriter) : base(codeWriter)
		{
		}

		public IPropertyGenerator AddLine() => AddLine(this);

		public IPropertyGenerator AddLine(string line) => AddLine(line, this);

		public IPropertyGenerator Get<T>(T value)
		{
			AddLine($"get => {value};");
			return this;
		}

		public IPropertyGenerator Get(Action<IMethodGenerator> action)
		{
			_codeWriter.WriteScoped("get", ToMethodGenerator(), action);
			return this;
		}

		public IPropertyGenerator Set<T>(T value)
		{
			AddLine($"set => value = {value};");
			return this;
		}

		public IPropertyGenerator Set(Action<IMethodGenerator> action)
		{
			_codeWriter.WriteScoped("set", ToMethodGenerator(), action);
			return this;
		}

		private IMethodGenerator ToMethodGenerator() =>
			new MethodGenerator(_codeWriter);
	}
}

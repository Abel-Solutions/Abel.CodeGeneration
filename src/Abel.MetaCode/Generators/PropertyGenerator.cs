using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class PropertyGenerator : Generator, IPropertyGenerator
	{
		public PropertyGenerator(ICodeWriter codeWriter)
			: base(codeWriter)
		{
		}

		public IPropertyGenerator AddLine() => AddLine(this);

		public IPropertyGenerator AddLine(string line) => AddLine(line, this);

		public IPropertyGenerator Get<T>(T value) => AddLine($"get => {value};");

		public IPropertyGenerator Get(Action<IMethodGenerator> action) =>
			AddScoped("get", ToMethodGenerator(), action, this);

		public IPropertyGenerator Set<T>(T value) => AddLine($"set => value = {value};");

		public IPropertyGenerator Set(Action<IMethodGenerator> action) =>
			AddScoped("set", ToMethodGenerator(), action, this);
	}
}

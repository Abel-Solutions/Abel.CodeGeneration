using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class WithConstructor : With, IWithConstructor
	{
		private string _parameters = string.Empty;

		private string Line => $"{Modifiers} {Name}({_parameters})";

		public WithConstructor(string name, ICodeGenerator codeGenerator)
			: base(name, codeGenerator)
		{
		}

		public IWithConstructor WithParameters(string parameters)
		{
			_parameters = parameters;
			return this;
		}

		public override ICodeGenerator WithContent(Action<ICodeGenerator> action) =>
			CodeGenerator.AddScoped(Line, action);
	}
}

using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class WithConstructor : With, IWithConstructor
	{
		private string _parameters = string.Empty;

		public WithConstructor(string name, ICodeGen codeGen)
			: base(name, codeGen)
		{
		}

		public IWithConstructor WithParameters(string parameters)
		{
			_parameters = parameters;
			return this;
		}

		public override ICodeGen WithContent(Action<ICodeGen> action) =>
			CodeGen.AddScoped($"{Modifiers} {Name}({_parameters})", action);
	}
}

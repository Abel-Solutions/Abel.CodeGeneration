using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class WithClass : With
	{
		public WithClass(string name, ICodeGen codeGen) : base(name, codeGen)
		{
		}

		public override ICodeGen WithContent(Action<ICodeGen> action) => CodeGen.AddScoped($"{Modifiers} class {Name}", action);
	}
}

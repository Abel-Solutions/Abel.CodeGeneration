using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class WithConstructor : With
	{
		public WithConstructor(string name, ICodeGen codeGen) : base(name, codeGen)
		{
		}

		public override ICodeGen WithContent(Action<ICodeGen> action) => CodeGen.AddScoped($"{Modifiers} {Name}({Parameters})", action);
	}
}

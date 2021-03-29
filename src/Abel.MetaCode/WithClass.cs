using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class WithClass : With, IWithClass
	{
		private string _parentName;

		public WithClass(string name, ICodeGen codeGen) 
			: base(name, codeGen)
		{
		}

		public IWithClass WithParent(string parentName)
		{
			_parentName = parentName;
			return this;
		}

		public override ICodeGen WithContent(Action<ICodeGen> action) =>
			CodeGen.AddScoped($"{Modifiers} class {Name}{(_parentName == null ? string.Empty : $" : {_parentName}")}", action);
	}
}

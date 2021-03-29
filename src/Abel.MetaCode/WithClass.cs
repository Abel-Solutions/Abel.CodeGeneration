using System;
using System.Reflection;
using System.Text;
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

		public ICodeGen WithContent(Action<IClassGen> action)
		{
			var classGen = new ClassGen(Name, (StringBuilder)CodeGen.GetType().GetField("_sb", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(CodeGen)); // todo
			return CodeGen.AddScoped($"{Modifiers} class {Name}{(_parentName == null ? string.Empty : $" : {_parentName}")}", gen => action(classGen));
		}

		public override ICodeGen WithContent(Action<ICodeGen> action) => throw new NotImplementedException(); // todo
	}
}

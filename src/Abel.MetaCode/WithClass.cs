using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class WithClass : With, IWithClass
	{
		private string _parentName;

		private string Line => $"{Modifiers} class {Name}{(_parentName == null ? string.Empty : $" : {_parentName}")}";

		public WithClass(string name, ICodeGenerator codeGenerator)
			: base(name, codeGenerator)
		{
		}

		public IWithClass WithParent(string parentName)
		{
			_parentName = parentName;
			return this;
		}

		public ICodeGenerator WithContent(Action<IClassGenerator> action) =>
			CodeGenerator.AddScoped(Line, gen => action(CodeGenerator.ToClassGenerator(Name)));

		public override ICodeGenerator WithContent(Action<ICodeGenerator> action) =>
			throw new NotImplementedException(); // todo
	}
}

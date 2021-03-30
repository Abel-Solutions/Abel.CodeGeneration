using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class WithClass : IWithClass
	{
		private readonly ICodeGenerator _codeGenerator;
		private readonly string _name;

		private string _modifiers = "public";
		private string _parentName;

		private string Line => $"{_modifiers} class {_name}{(_parentName == null ? string.Empty : $" : {_parentName}")}";

		public WithClass(string name, ICodeGenerator codeGenerator)
		{
			_name = name;
			_codeGenerator = codeGenerator;
		}

		public IWithClass WithModifiers(string modifiers)
		{
			_modifiers = modifiers;
			return this;
		}

		public IWithClass WithParent(string parentName)
		{
			_parentName = parentName;
			return this;
		}

		public ICodeGenerator WithContent(Action<IClassGenerator> action) =>
			_codeGenerator.AddScoped(Line, _codeGenerator.ToClassGenerator(_name), action);
	}
}

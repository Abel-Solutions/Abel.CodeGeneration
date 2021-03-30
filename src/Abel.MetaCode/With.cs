using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public abstract class With : IWith
	{
		protected readonly ICodeGenerator CodeGenerator;
		protected readonly string Name;

		protected string Modifiers = "public";

		protected With(string name, ICodeGenerator codeGenerator)
		{
			Name = name;
			CodeGenerator = codeGenerator;
		}

		public IWith WithModifiers(string modifiers)
		{
			Modifiers = modifiers;
			return this;
		}

		public abstract ICodeGenerator WithContent(Action<ICodeGenerator> action);
	}
}

using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public abstract class With : IWith
	{
		protected readonly ICodeGen CodeGen;
		protected readonly string Name;

		protected string Modifiers = "public";

		protected With(string name, ICodeGen codeGen)
		{
			Name = name;
			CodeGen = codeGen;
		}

		public IWith WithModifiers(string modifiers)
		{
			Modifiers = modifiers;
			return this;
		}

		public abstract ICodeGen WithContent(Action<ICodeGen> action);
	}
}

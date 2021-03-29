using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class With : IWith
	{
		protected readonly ICodeGen CodeGen;

		protected readonly string Name;

		protected string Modifiers = "public";
		protected string Parameters = string.Empty;
		protected string ReturnTypeName = "void";

		public With(string name, ICodeGen codeGen)
		{
			Name = name;
			CodeGen = codeGen;
		}

		public IWith WithModifiers(string modifiers)
		{
			Modifiers = modifiers;
			return this;
		}

		public IWith WithParameters(string parameters)
		{
			Parameters = parameters;
			return this;
		}

		public IWith WithReturnType(string returnTypeName)
		{
			ReturnTypeName = returnTypeName;
			return this;
		}

		public virtual ICodeGen WithContent(Action<ICodeGen> action) => CodeGen.AddScoped($"{Modifiers} {ReturnTypeName} {Name}({Parameters})", action);
	}
}

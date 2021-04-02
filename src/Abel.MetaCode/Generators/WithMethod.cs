using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithMethod : With<IClassGenerator, IWithMethod>, IWithMethod
	{
		protected override string Line => $"{Modifiers} {ReturnTypeName} {Name}({Parameters})";

		public WithMethod(string name, IClassGenerator classGenerator)
			: base(name, classGenerator) =>
			ReturnTypeName = "void";

		public IClassGenerator WithContent(Action<IMethodGenerator> action) =>
			WithContent(Generator.ToMethodGenerator(), action);
	}
}

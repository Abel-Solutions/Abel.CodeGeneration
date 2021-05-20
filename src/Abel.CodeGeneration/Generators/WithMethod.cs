using System;
using Abel.CodeGeneration.Interfaces;

namespace Abel.CodeGeneration.Generators
{
	public class WithMethod : With<IClassGenerator, IWithMethod>, IWithMethod
	{
		protected override string Line => $"{Modifiers} {ReturnTypeName} {Name}({Parameters})";

		internal WithMethod(string name, IClassGenerator classGenerator)
			: base(name, classGenerator) =>
			ReturnTypeName = "void";

		public IClassGenerator WithContent(Action<IMethodGenerator> action) =>
			WithContent(Generator.ToMethodGenerator(), action);
	}
}

using System;
using Abel.CodeGeneration.Interfaces;

namespace Abel.CodeGeneration.Generators
{
	public class WithProperty : With<IClassGenerator, IWithProperty>, IWithProperty
	{
		protected override string Line => $"{Modifiers} {ReturnTypeName} {Name}";

		internal WithProperty(string name, IClassGenerator classGenerator)
			: base(name, classGenerator) =>
			ReturnTypeName = "object";

		public IClassGenerator WithContent(Action<IPropertyGenerator> action) =>
			WithContent(Generator.ToPropertyGenerator(), action);
	}
}

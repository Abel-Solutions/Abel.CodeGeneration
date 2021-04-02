using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithProperty : With<IClassGenerator, IWithProperty>, IWithProperty
	{
		protected override string Line => $"{Modifiers} {ReturnTypeName} {Name}";

		public WithProperty(string name, IClassGenerator classGenerator)
			: base(name, classGenerator) =>
			ReturnTypeName = "object";

		public IClassGenerator WithContent(Action<IPropertyGenerator> action) =>
			WithContent(Generator.ToPropertyGenerator(), action);
	}
}

using System;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode.Generators
{
	public class WithClass : With<ICodeGenerator, IWithClass>, IWithClass
	{
		protected override string Line => $"{Modifiers} class {Name}{Generics}{Parents}{Constraints}";

		internal WithClass(string name, ICodeGenerator generator)
			: base(name, generator)
		{
		}

		public ICodeGenerator WithContent(Action<IClassGenerator> action) =>
			WithContent(Generator.ToClassGenerator(Name), action);
	}
}

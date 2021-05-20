using System;
using Abel.CodeGeneration.Interfaces;

namespace Abel.CodeGeneration.Generators
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

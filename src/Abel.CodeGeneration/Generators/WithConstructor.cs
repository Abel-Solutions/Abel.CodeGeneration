using System;
using Abel.CodeGeneration.Interfaces;

namespace Abel.CodeGeneration.Generators
{
	public class WithConstructor : With<IClassGenerator, IWithConstructor>, IWithConstructor
	{
		protected override string Line => $"{Modifiers} {Name}({Parameters})";

		internal WithConstructor(string name, IClassGenerator classGenerator)
			: base(name, classGenerator)
		{
		}

		public IClassGenerator WithContent(Action<IMethodGenerator> action) =>
			WithContent(Generator.ToMethodGenerator(), action);
	}
}

using System;

namespace Abel.CodeGeneration.Interfaces
{
	public interface IPropertyGenerator : IGenerator<IPropertyGenerator>
	{
		IPropertyGenerator Get<T>(T value);

		IPropertyGenerator Get(Action<IMethodGenerator> action);

		IPropertyGenerator Set<T>(T value);

		IPropertyGenerator Set(Action<IMethodGenerator> action);
	}
}
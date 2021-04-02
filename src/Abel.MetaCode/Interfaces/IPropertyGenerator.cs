using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IPropertyGenerator
	{
		IPropertyGenerator AddLine();

		IPropertyGenerator AddLine(string line);

		IPropertyGenerator Get<T>(T value);

		IPropertyGenerator Get(Action<IMethodGenerator> action);

		IPropertyGenerator Set<T>(T value);

		IPropertyGenerator Set(Action<IMethodGenerator> action);
	}
}
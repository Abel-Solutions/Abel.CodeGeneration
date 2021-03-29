using System.Reflection;

namespace Abel.MetaCode.Interfaces
{
	public interface IWithMethod : IWith
	{
		IWithMethod WithReturnType(string returnTypeName);

		IWithMethod WithParameters(string parameters);

		IWithMethod WithParameters(ParameterInfo[] parameters);
	}
}

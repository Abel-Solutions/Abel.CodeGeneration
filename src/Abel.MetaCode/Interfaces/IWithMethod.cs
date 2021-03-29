namespace Abel.MetaCode.Interfaces
{
	public interface IWithMethod : IWith
	{
		IWithMethod WithReturnType(string returnTypeName);

		IWithMethod WithParameters(string parameters);
	}
}

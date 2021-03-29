namespace Abel.MetaCode.Interfaces
{
	public interface IWithConstructor : IWith
	{
		IWithConstructor WithParameters(string parameters);
	}
}
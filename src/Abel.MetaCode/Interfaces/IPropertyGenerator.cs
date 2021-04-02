namespace Abel.MetaCode.Interfaces
{
	public interface IPropertyGenerator
	{
		IPropertyGenerator AddLine();

		IPropertyGenerator AddLine(string line);

		IPropertyGenerator Get<T>(T value);

		IPropertyGenerator Set<T>(T value); 
	}
}
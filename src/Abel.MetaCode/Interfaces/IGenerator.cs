namespace Abel.MetaCode.Interfaces
{
	public interface IGenerator
	{
		IClassGenerator ToClassGenerator(string className);

		IPropertyGenerator ToPropertyGenerator();

		IMethodGenerator ToMethodGenerator();
	}
}
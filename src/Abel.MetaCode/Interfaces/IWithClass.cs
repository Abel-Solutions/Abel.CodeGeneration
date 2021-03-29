namespace Abel.MetaCode.Interfaces
{
	public interface IWithClass : IWith
	{
		IWith WithParent(string parentName);
	}
}
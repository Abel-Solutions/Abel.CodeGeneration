using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IWith<out TWith, out TGenerator, out TScopeGenerator> // todo not have this interface?
	{
		TWith WithModifier(string modifier); 

		TWith WithModifiers(params string[] modifiers);

		TGenerator WithContent(Action<TScopeGenerator> action);
	}
}
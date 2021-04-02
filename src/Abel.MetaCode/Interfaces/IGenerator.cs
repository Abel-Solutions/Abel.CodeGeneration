using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IGenerator<TGenerator>
	{
		TGenerator AddScoped<TScope>(string line, TScope scopeGenerator, Action<TScope> action);
	}
}
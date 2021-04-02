using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IGenerator
	{
		IGenerator AddScoped<TGenerator>(string line, TGenerator generator, Action<TGenerator> action);
	}
}
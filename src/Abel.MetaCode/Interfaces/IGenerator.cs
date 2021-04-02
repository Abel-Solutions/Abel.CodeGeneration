﻿using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IGenerator
	{
		IGenerator AddScoped<T>(string line, T generator, Action<T> action);

		IClassGenerator ToClassGenerator(string className);

		IPropertyGenerator ToPropertyGenerator();

		IMethodGenerator ToMethodGenerator();
	}
}
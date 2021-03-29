﻿using System;

namespace Abel.MetaCode.Interfaces
{
	public interface IWith
	{
		IWith WithModifiers(string modifiers);

		IWith WithParameters(string parameters);

		ICodeGen WithContent(Action<ICodeGen> action);
	}
}

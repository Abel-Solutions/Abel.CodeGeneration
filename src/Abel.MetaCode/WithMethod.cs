using System;
using System.Linq;
using System.Reflection;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class WithMethod : With, IWithMethod
	{
		private string _parameters = string.Empty;
		private string _returnTypeName = "void";

		private string Line => $"{Modifiers} {_returnTypeName} {Name}({_parameters})";

		public WithMethod(string name, ICodeGenerator codeGenerator)
			: base(name, codeGenerator)
		{
		}

		public IWithMethod WithReturnType(string returnTypeName)
		{
			_returnTypeName = returnTypeName;
			return this;
		}

		public IWithMethod WithParameters(string parameters)
		{
			_parameters = parameters;
			return this;
		}

		public IWithMethod WithParameters(ParameterInfo[] parameters) =>
			WithParameters(string.Join(", ", parameters.Select(p => $"{p.ParameterType.Name} {p.Name}")));

		public override ICodeGenerator WithContent(Action<ICodeGenerator> action) =>
			CodeGenerator.AddScoped(Line, action);
	}
}

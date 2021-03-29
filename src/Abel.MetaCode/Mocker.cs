using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class Mocker<TMockable>
		where TMockable : class
	{
		public TMockable Object => (TMockable)Activator.CreateInstance(BuildType(), _setups);

		private readonly Type _type = typeof(TMockable);

		private readonly IDictionary<string, Func<object>> _setups = new Dictionary<string, Func<object>>();

		private readonly ICompiler _compiler = new Compiler();

		public Mocker<TMockable> Setup<TResult>(Expression<Func<TMockable, TResult>> method, TResult result)
		{
			var methodBody = (MethodCallExpression)method.Body;
			var methodName = methodBody.Method.Name;
			_setups[methodName] = () => result;
			return this;
		}

		private Type BuildType()
		{
			var code = GenerateCode();

			return _compiler
				.AddReference<TMockable>()
				.Compile(code)
				.ExportedTypes
				.Single();
		}

		private string GenerateCode()
		{
			var newTypeName = $"{_type.Name}Proxy";

			var referenceNames = new List<string> { "System", "System.Collections.Generic" }
				.Concat(GetReferenceTypes().Select(r => r.Namespace))
				.Distinct();

			return new CodeGen()
				.AddUsings(referenceNames)
				.AddNamespace(_type.Namespace, nspace => nspace
					.AddClass(newTypeName)
						.WithParent(_type.Name)
						.WithContent(cl => 
						{
							cl
								.AddLine("IDictionary<string, Func<object>> _methods;")
								.AddLine()
								.AddConstructor(newTypeName)
								.WithParameters("IDictionary<string, Func<object>> methods")
								.WithContent(ctor => ctor
									.AddLine("_methods = methods;"));

							GetMockableMethods().ForEach(info => cl
								.AddMethod(info.Name)
									.WithReturnType(info.ReturnType.Name)
									.WithParameters(info.GetParameters())
									.WithContent(method => method
										.AddLine($"return ({info.ReturnType.Name})_methods[\"{info.Name}\"]();")));
					}))
				.Generate();
		}

		private IEnumerable<Type> GetReferenceTypes()
		{
			var mockableMethods = GetMockableMethods();
			return mockableMethods
				.Select(m => m.ReturnType)
				.Concat(mockableMethods
					.SelectMany(k => k.GetParameters()
						.Select(t => t.ParameterType)));
		}

		private IEnumerable<MethodInfo> GetMockableMethods() =>
			_type.GetMethods().Where(m => m.IsAbstract || m.IsVirtual);
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Abel.MetaCode.Extensions;
using Abel.MetaCode.Generators;
using Abel.MetaCode.Interfaces;

namespace Abel.MetaCode
{
	public class Mocker<TMockable>
		where TMockable : class
	{
		public TMockable Object => (TMockable)Activator.CreateInstance(BuildType(), _setups);

		private readonly IDictionary<string, Func<object>> _setups = new Dictionary<string, Func<object>>();

		private readonly ICompiler _compiler = new Compiler();

		private static readonly Type Type = typeof(TMockable);

		private IEnumerable<MethodInfo> MockableMethods { get; } = Type.GetMethods().Where(m => m.IsAbstract || m.IsVirtual);

		public Mocker<TMockable> Setup<TResult>(Expression<Func<TMockable, TResult>> method, TResult result)
		{
			var methodBody = (MethodCallExpression)method.Body;
			var methodName = methodBody.Method.Name;
			_setups[methodName] = () => result;
			return this;
		}

		private Type BuildType() =>
			_compiler
				.WithReference<TMockable>()
				.Compile(GenerateCode())
				.ExportedTypes
				.Single();

		private string GenerateCode()
		{
			var referenceNames = new List<string> { "System", "System.Collections.Generic" }
				.Concat(GetReferenceTypes().Select(r => r.Namespace))
				.Distinct();

			return new CodeGenerator()
				.AddUsings(referenceNames)
				.AddNamespace(Type.Namespace, nspace => nspace
					.AddClass($"{Type.Name}Proxy")
						.WithParent(Type.Name)
						.WithContent(cl =>
						{
							cl
								.AddLine("IDictionary<string, Func<object>> _methods;")
								.AddLine()
								.AddConstructor()
									.WithParameters("IDictionary<string, Func<object>> methods")
									.WithContent(ctor => ctor
										.AddLine("_methods = methods;"));

							MockableMethods.ForEach(info => cl
								.AddMethod(info.Name)
									.WithReturnType(info.ReturnType.Name)
									.WithParameters(info.GetParameters())
									.WithContent(method => method
										.AddLine($"return ({info.ReturnType.Name})_methods[\"{info.Name}\"]();")));
						}))
				.Generate();
		}

		private IEnumerable<Type> GetReferenceTypes() =>
			MockableMethods
				.Select(m => m.ReturnType)
				.Concat(MockableMethods
					.SelectMany(k => k.GetParameters()
						.Select(t => t.ParameterType)));
	}
}
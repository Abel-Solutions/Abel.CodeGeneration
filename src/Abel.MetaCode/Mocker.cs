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
		public TMockable Object => _object ??= CreateObject();

		private readonly Type _type = typeof(TMockable);

		private readonly IEnumerable<MethodInfo> _mockableMethods;

		private readonly IDictionary<string, Func<object>> _setups = new Dictionary<string, Func<object>>();

		private readonly ICodeGenerator _codeGenerator = new CodeGenerator();

		private readonly ICompiler _compiler = new Compiler();

		private TMockable _object;

		public Mocker() => _mockableMethods = GetMockableMethods();

		public Mocker<TMockable> Setup<TResult>(Expression<Func<TMockable, TResult>> method, TResult result)
		{
			var methodBody = (MethodCallExpression)method.Body;
			var methodName = methodBody.Method.Name;
			_setups[methodName] = () => result;
			return this;
		}

		private IEnumerable<MethodInfo> GetMockableMethods() =>
			_type.GetMethods().Where(m => m.IsAbstract || m.IsVirtual);

		private TMockable CreateObject() =>
			(TMockable)Activator.CreateInstance(BuildType(), _setups);

		private Type BuildType() =>
			_compiler
				.WithReference<TMockable>()
				.Compile(GenerateCode())
				.ExportedTypes
				.Single();

		private string GenerateCode() =>
			_codeGenerator
				.AddUsings(GetReferenceNames())
				.AddNamespace(_type.Namespace, nspace => nspace
					.AddClass($"{_type.Name}Proxy")
					.WithParent(_type.Name)
					.WithContent(cl =>
					{
						cl
							.AddLine("IDictionary<string, Func<object>> _methods;")
							.AddLine()
							.AddConstructor()
							.WithParameters("IDictionary<string, Func<object>> methods")
							.WithContent(ctor => ctor
								.AddLine("_methods = methods;"));

						_mockableMethods.ForEach(info => cl
							.AddMethod(info.Name)
							.WithReturnType(info.ReturnType.Name)
							.WithParameters(info.GetParameters())
							.WithContent(method => method
								.AddLine($"return ({info.ReturnType.Name})_methods[\"{info.Name}\"]();")));
					}))
				.Generate();

		private IEnumerable<string> GetReferenceNames() =>
			new List<string> { "System", "System.Collections.Generic" }
				.Concat(GetReferenceTypes().Select(r => r.Namespace))
				.Distinct();

		private IEnumerable<Type> GetReferenceTypes() =>
			_mockableMethods
				.Select(m => m.ReturnType)
				.Concat(_mockableMethods
					.SelectMany(k => k.GetParameters()
						.Select(t => t.ParameterType)));
	}
}
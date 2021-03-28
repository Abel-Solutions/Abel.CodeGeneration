using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MetaCode.Extensions;

namespace MetaCode
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

		private Type BuildType() =>
			_compiler
				.AddReference<TMockable>()
				.Compile(GenerateCode())
				.ExportedTypes
				.Single();

		private string GenerateCode()
		{
			var newTypeName = $"{_type.Name}Proxy";

			var referenceNames = new List<string> { "System", "System.Collections.Generic" }
				.Concat(GetReferenceTypes().Select(r => r.Namespace))
				.Distinct();

			return new CodeGen()
				.AddLines(referenceNames.Select(n => $"using {n};"))
				.AddLine()
				.AddScoped("namespace " + _type.Namespace, nspace =>
				{
					nspace.AddScoped($"public class {newTypeName} : {_type.Name}", cl =>
					{
						cl
							.AddLine("IDictionary<string, Func<object>> _methods;")
							.AddLine()
							.AddScoped($"public {newTypeName}(IDictionary<string, Func<object>> methods)", ctor =>
							{
								ctor.AddLine("_methods = methods;");
							});

						GetMockableMethods().ForEach(info =>
						{
							nspace.AddScoped($"public {info.ReturnType.Name} {info.Name}({string.Join(", ", info.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"))})", method =>
							{
								method.AddLine($"return ({info.ReturnType.Name})_methods[\"{info.Name}\"]();");
							});
						});
					});
				})
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

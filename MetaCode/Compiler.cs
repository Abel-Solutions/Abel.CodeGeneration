using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace MetaCode
{
	public class Compiler : ICompiler
	{
		private readonly IList<string> _referenceNames = new List<string>();

		public Assembly Compile(string sourceCode, OutputKind outputKind = OutputKind.DynamicallyLinkedLibrary)
		{
			var compilation = CreateCompilation(sourceCode, outputKind);

			return CreateAssembly(compilation);
		}

		public ICompiler AddReference<T>() => AddReference(typeof(T));

		private ICompiler AddReference(Type type)
		{
			_referenceNames.Add(type.GetTypeInfo().Assembly.Location);
			return this;
		}

		private Compilation CreateCompilation(string sourceCode, OutputKind outputKind)
		{
			var codeString = SourceText.From(sourceCode);
			var options = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp9);

			var parsedSyntaxTree = SyntaxFactory.ParseSyntaxTree(codeString, options);

			return CSharpCompilation.Create(
				"Hello.dll",
				new[] { parsedSyntaxTree },
				GetReferences(),
				new CSharpCompilationOptions(
					outputKind,
					optimizationLevel: OptimizationLevel.Debug,
					assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default));
		}

		private IEnumerable<MetadataReference> GetReferences()
		{
			AddReference<object>();
			AddReference(typeof(Console));

			foreach (var a in Assembly.GetEntryAssembly().GetReferencedAssemblies())
			{
				_referenceNames.Add(Assembly.Load(a).Location);
			}

			return _referenceNames.Distinct().Select(x => MetadataReference.CreateFromFile(x));
		}

		private static Assembly CreateAssembly(Compilation compilation)
		{
			using var ms = new MemoryStream();
			var result = compilation.Emit(ms);

			if (!result.Success)
			{
				var failures = result.Diagnostics.Where(diagnostic =>
					diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);

				throw new Exception("Compilation failed: " + string.Join("\n", failures.Select(f => f.GetMessage())));
			}

			ms.Seek(0, SeekOrigin.Begin);

			return Assembly.Load(ms.ToArray());
		}
	}
}
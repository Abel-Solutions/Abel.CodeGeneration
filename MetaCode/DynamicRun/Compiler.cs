using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace MetaCode.DynamicRun
{
	public class Compiler
	{
		private readonly IList<string> _referenceNames = new List<string>();

		public Compiler AddReference<T>() => AddReference(typeof(T));

		public Compiler AddReference(Type type)
		{
			_referenceNames.Add(type.GetTypeInfo().Assembly.Location);
			return this;
		}

		public Assembly Compile(string sourceCode)
		{
			var references = FindReferences();

			var compilation = GenerateCode(sourceCode, references);

			return CreateAssembly(compilation);
		}

		private IEnumerable<MetadataReference> FindReferences()
		{
			AddReference<object>();
			AddReference(typeof(Console));

			foreach (var a in Assembly.GetEntryAssembly()?.GetReferencedAssemblies())
			{
				_referenceNames.Add(Assembly.Load(a).Location);
			}

			return _referenceNames.Distinct().Select(x => MetadataReference.CreateFromFile(x));

			//var references = new List<MetadataReference>
			//{
			//	MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
			//	MetadataReference.CreateFromFile(typeof(Console).Assembly.Location)
			//};

			//Assembly.GetEntryAssembly()?.GetReferencedAssemblies().ToList()
			//	.ForEach(a => references.Add(MetadataReference.CreateFromFile(Assembly.Load(a).Location)));
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

		private static Compilation GenerateCode(string sourceCode, IEnumerable<MetadataReference> references)
		{
			var codeString = SourceText.From(sourceCode);
			var options = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp9);

			var parsedSyntaxTree = SyntaxFactory.ParseSyntaxTree(codeString, options);

			return CSharpCompilation.Create(
				"Hello.dll",
				new[] { parsedSyntaxTree },
				references,
				new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, // todo console
					optimizationLevel: OptimizationLevel.Debug,
					assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default));
		}
	}
}
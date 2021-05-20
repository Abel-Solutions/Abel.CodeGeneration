using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Abel.CodeGeneration.Extensions;
using Abel.CodeGeneration.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace Abel.CodeGeneration
{
	public class Compiler : ICompiler
	{
		private readonly IList<string> _referenceLocations = new List<string>();

		public Assembly Compile(string sourceCode, OutputKind outputKind = OutputKind.DynamicallyLinkedLibrary)
		{
			var compilation = CreateCompilation(sourceCode, outputKind);

			return CreateAssembly(compilation);
		}

		public ICompiler WithReference<T>() => WithReference(typeof(T));

		public ICompiler WithReference(Type type)
		{
			_referenceLocations.Add(type.GetTypeInfo().Assembly.Location);
			return this;
		}

		private Compilation CreateCompilation(string sourceCode, OutputKind outputKind) =>
			CSharpCompilation.Create(
				Guid.NewGuid().ToString(),
				new[] { SyntaxFactory.ParseSyntaxTree(
					SourceText.From(sourceCode),
					CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp9)) },
				GetReferences(),
				new CSharpCompilationOptions(outputKind));

		private IEnumerable<MetadataReference> GetReferences()
		{
			WithReference<object>();
			WithReference(typeof(Console));

			Assembly.GetEntryAssembly().GetReferencedAssemblies()
				.ForEach(assemblyName => _referenceLocations.Add(Assembly.Load((AssemblyName) assemblyName).Location));

			return _referenceLocations.Distinct().Select(x => MetadataReference.CreateFromFile(x));
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
# Abel.CodeGeneration

Abel.CodeGeneration is a suite of tools for generating, compiling, executing and mocking classes.

## Generate code

The CodeGenerator class helps generate code in a fluent way.

A simple console app which prints "Hello world":

```C#
var code = new CodeGenerator()
	.Using("System")
	.AddClass("Lol", @class => @class
		.AddMethod("Main")
		.WithModifiers("public static")
		.WithContent(method => method
			.AddLine("Console.WriteLine(\"Hello world\");")))
	.Generate();
```

A slightly more complex example which demonstrates inheritance, constructor, modifiers and parameters:

```C#
var code = _codeGenerator
	.Using("System")
	.Using("System.Text")
	.AddLine()
	.AddNamespace("MetaCode", nspace => nspace
		.AddClass("Lol")
		.WithParent("object")
		.WithContent(@class => @class
			.AddConstructor("string lol", ctor => ctor
				.AddLine("Console.WriteLine(lol);"))
			.AddMethod<int>("GetInt", method => method
				.AddLine("return 1337;"))))
	.Generate();
```

The generator contains lots of overloads to suit different needs.

## Compile and execute code at run-time

The Compiler class compiles source code and returns an Assembly.

This example compiles a console app and runs it:

```C#
new Compiler()
	.Compile(code, OutputKind.ConsoleApplication)
	.Execute();
```

OutputKind is optional, without it the assembly will be a DLL.

The Execute method is an extension method which invokes the entry method (usually `static void Main`).

## Mock classes

The Mocker class works similarly to Moq and demonstrates both code generation and compiling code at run-time.

This example shows it in action, together with FluentAssertions:

```C#
var mock = new Mocker<ISomething>();
mock.Setup(m => m.ToString(2), "2");
mock.Setup(m => m.GetDouble(2), 4);

mock.Object.ToString(2).Should().Be("2");
mock.Object.GetDouble(2).Should().Be(4);
```
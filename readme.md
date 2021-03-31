# Abel.MetaCode

Abel.MetaCode is a suite of tools for generating, compiling, executing and mocking classes.

## Code generation

The CodeGenerator class helps generate code in a fluent way.

A simple console app which prints "Hello world":

~~~
var code = new CodeGenerator()
	.Using("System")
	.AddClass("Lol", @class => @class
		.AddMethod("Main")
		.WithModifiers("public static")
		.WithContent(method => method
			.AddLine("Console.WriteLine(\"Hello world\");")))
	.Generate();
~~~

A more advanced example with namespace, inheritance, constructor and explicit modifiers and parameters:

~~~
var code = new CodeGenerator()
	.Using("System")
	.Using("System.Text")
	.AddLine()
	.AddNamespace("MetaCode", nspace => nspace
		.AddClass("Lol")
		.WithParent("object")
		.WithContent(@class => @class
			.AddConstructor()
			.WithParameters("string lol")
			.WithContent(ctor => ctor
				.AddLine("Console.WriteLine(lol);"))
			.AddMethod("Main")
			.WithModifiers("public static")
			.WithContent(method => method
				.AddLine("Console.WriteLine(\"foo\");"))))
	.Generate();
~~~

## Compile and execute code at run-time

The Compiler class compiles source code and returns an Assembly.

This example compiles a console app and runs it:

~~~
new Compiler()
	.Compile(code, OutputKind.ConsoleApplication)
	.Execute();
~~~

OutputKind is optional, without it the Assembly will be a DLL.

The Execute method is an extension method which invokes the entry method (usually `static void Main`).

## Mock classes

The Mocker class works similarly to Moq and demonstrates both code generation and compiling code at run-time.

This example shows it in action, together with FluentAssertions:

~~~
var mock = new Mocker<ISomething>();
mock.Setup(m => m.ToString(2), "2");
mock.Setup(m => m.GetDouble(2), 4);

mock.Object.ToString(2).Should().Be("2");
mock.Object.GetDouble(2).Should().Be(4);
~~~
# MiniCompiler

MiniCompiler is a C# Windows Forms application for exploring the fundamental stages of compiler construction through a small custom programming language.

The project is being developed incrementally to demonstrate how source code can be processed through lexical analysis, syntax analysis, symbol-table management, and intermediate-code generation.

## Overview

MiniCompiler provides a graphical environment for entering source code, examining compiler output, and experimenting with the individual stages of the compilation process.

The project is organized around the following compiler-development goals:

- Convert source code into a sequence of tokens.
- Validate source code against a defined grammar.
- Track identifiers and their associated information.
- Generate an intermediate representation.
- Display compiler results through a Windows Forms interface.
- Separate compiler logic into maintainable and testable components.

The generated intermediate representation is based on quadruples containing an operator, two operands, and a result. This representation can later be extended with optimization or execution stages.

## Project Status

MiniCompiler is under active development. The compiler is being organized into independent components, with each stage implemented, tested, and documented separately.

The current implementation includes:

- A structured token model.
- Token classification through `TokenKind`.
- A standalone lexical analyzer.
- Recognition of keywords, identifiers, integer literals, operators, punctuation, and statement delimiters.
- Line and column tracking for tokens.
- Lexer testing and diagnostic output.
- Integration with the Windows Forms project structure.

Future development will extend the project with syntax analysis, semantic analysis, symbol-table management, and intermediate-code generation.

## Features

### Lexical Analysis

The lexical analyzer reads source code character by character and converts it into a sequence of tokens.

The current lexer recognizes:

- Keywords
- Identifiers
- Integer literals
- Arithmetic operators
- Relational operators
- Assignment operators
- Parentheses
- Commas
- Semicolons
- Invalid characters
- End-of-file

Each token contains:

- Token kind
- Original lexeme
- Line number
- Column number

### Token Model

The token model provides a structured representation of the source program.

The `TokenKind` enumeration defines the categories supported by the lexer, while the `Token` class stores the details of each recognized token.

Example token categories include:

```text
KeywordInt
Identifier
IntegerLiteral
Assign
Plus
Semicolon
EndOfFile

Lexer Testing

The project includes a basic lexer test that processes sample source code and displays the generated token stream.

Example input:
$int$ $x$; $x$ := 25 + 5

The test output includes the token kind, lexeme, line number, and column number for each token.

Planned Syntax Analysis

The next development stage will be a syntax analyzer based on recursive descent parsing.

The planned parser will validate structures such as:

Variable declarations

Variable assignments

Arithmetic expressions

Conditional statements

while loops

Compound statements using begin and end

Planned Intermediate-Code Generation

The project is intended to generate quadruples as an intermediate representation.

Each quadruple follows this format:
Operator | Operand 1 | Operand 2 | Result

For example:
(+, $f, 6, T0)
(=, T0, null, $f)

This represents the expression:
$f = $f + 6

The syntax and output format will be finalized as the parser and intermediate-code generator are implemented.

Graphical User Interface

The Windows Forms interface is intended to provide:

Source-code input

Tokenization controls

Parsing controls

Token output

Intermediate-code output

Symbol-table display

Temporary-variable display

Compiler diagnostic messages

Clipboard support for copying compiler results

Some of these functions are part of the existing application structure and will be refined as the compiler components are redeveloped.

Technology Stack

| Component            | Technology                  |
|----------------------|-----------------------------|
| Programming language | C#                          |
| User interface       | Windows Forms               |
| Framework            | .NET Framework 4.7.2        |
| IDE                  | Visual Studio 2019 or later |
| Project type         | Desktop application         |
| Platform             | Windows                     |
| Version control      | Git and GitHub              |

System Requirements

Windows operating system

Visual Studio 2019 or later

.NET Framework 4.7.2

Minimum 4 GB RAM

Approximately 50 MB of available disk space

Getting Started
Clone the Repository

git clone https://github.com/Rubayat0007/MiniCompiler.git
cd MiniCompiler

Open the Project

Open the solution file:
compiler202124405005.sln

Open the solution in Visual Studio.

Confirm that .NET Framework 4.7.2 is installed.

Restore or verify project dependencies if Visual Studio prompts you.

Build the Application

In Visual Studio:

Select the desired build configuration.

Select Build > Build Solution.

Resolve any environment-specific configuration issues if prompted.

Run the Application

Run the application from Visual Studio by pressing F5 or selecting:
Debug > Start Debugging

The compiled executable is normally generated in a build directory such as:

bin/Debug/

or:

bin/Release/

Usage

Launch the MiniCompiler application.

Enter source code into the input area.

Use the available compiler controls.

Run the lexer to inspect the generated token stream.

Review token kinds, lexemes, and source positions.

Use the parser and intermediate-code features as they become available.

Example Input

The following example illustrates the intended style of the custom language:

$int$ $f$;
$f$ := 8;
$f$ := $f$ + 6;

Another example is:

$int$ $y$;
$y$ := 94;
$y$ := $y$ + 8;

The accepted syntax depends on the grammar implemented by the current compiler components. Input that does not follow the supported structure may produce a lexical or syntax error.

Architecture
1. Lexical Analysis

The lexer reads source code and converts it into tokens.

Example:

$int$  → KeywordInt
$x$    → Identifier
25     → IntegerLiteral
+      → Plus
;      → Semicolon
2. Syntax Analysis

The parser will consume the token stream and validate whether the tokens follow the language grammar.

The planned parser will support declarations, assignments, expressions, conditions, loops, and compound statements.

3. Semantic Analysis

The semantic-analysis stage will verify meaning-related rules such as:

Whether identifiers have been declared.

Whether variable types are compatible.

Whether assignments are valid.

Whether expressions use valid operands.

4. Symbol Table

The symbol table will store information about identifiers used in the source program.

Typical information may include:

| Name | Type | Value |
|------|------|-------|
| `$f$` | `int` | `8` |

5. Intermediate-Code Generation

The intermediate-code generator will convert validated statements and expressions into quadruples.

Example:

| Idx | Op | Opr1 | Opr2 | Result |
|-----|----|------|------|--------|
| 0 | `+` | `$f$` | `6` | `T0` |
| 1 | `=` | `T0` | `null` | `$f$` |

6. Temporary Variables

Temporary variables will be created when intermediate results are needed during expression evaluation.

Example:

| Name | Type | Expression |
|------|------|------------|
| `T0` | `int` | `$f + 6$` |

Project Structure

MiniCompiler/
├── Compiler/
│   └── Lexer/
│       ├── Lexer.cs
│       ├── LexerTest.cs
│       ├── README.md
│       ├── Token.cs
│       └── TokenKind.cs
├── Properties/
├── Resources/
├── App.config
├── Form1.cs
├── Form1.Designer.cs
├── Form1.resx
├── Program.cs
├── compiler202124405005.sln
├── compiler202124405021.csproj
├── .gitignore
└── README.md

Current Limitations

The language is intentionally small, and the current implementation is still under development.

Current limitations include:

Limited language syntax.

Lexer-focused implementation.

Parser redevelopment still in progress.

No complete semantic-analysis stage.

No complete machine-code backend.

No advanced optimization pipeline.

Limited compiler diagnostics.

Windows-specific graphical interface.

Limited automated test coverage.

Future Improvements

Planned improvements include:

Recursive descent parser implementation.

Symbol-table integration.

Semantic type checking.

Improved syntax and semantic error reporting.

Temporary-variable management.

Quadruple generation.

Constant folding.

Dead-code elimination.

Function declarations and calls.

Arrays and additional data types.

Expanded control-flow structures.

Abstract syntax tree visualization.

Three-address code generation.

LLVM IR or machine-code generation.

Automated compiler test cases.

Improved user interface and code-editing support.

Development Notes

The project is maintained through incremental implementation and testing. Compiler components are added separately so that each stage can be verified before being integrated into the complete application.

Author

Rubayat Karim

GitHub: Rubayat0007

License

This repository does not currently include a license file. Unless a license is added, the code should not be assumed to be available for unrestricted reuse or redistribution.

Repository

https://github.com/Rubayat0007/MiniCompiler 
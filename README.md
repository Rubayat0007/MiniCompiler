# MiniCompiler

A C# Windows Forms application for exploring the fundamental stages of compiler construction through a custom programming language.

MiniCompiler demonstrates how source code can be processed through lexical analysis, syntax analysis, symbol-table management, and intermediate-code generation. The application provides a graphical interface for entering source code, inspecting generated tokens, validating syntax, and viewing intermediate quadruples.

> **Project status:** This repository is a substantially modified and extended version of an existing educational compiler project. The current implementation includes modifications to the interface, project structure, and compiler workflow.

## Overview

MiniCompiler is an educational compiler-development project that processes a small custom programming language. The supported language includes variable declarations, assignments, arithmetic expressions, conditional statements, and loop structures.

The compiler workflow includes:

* Lexical analysis and tokenization
* Syntax analysis using recursive descent parsing
* Intermediate-code generation
* Symbol-table management
* Temporary-variable management
* Graphical user interface for compiler interaction

The generated intermediate representation uses quadruples containing an operator, two operands, and a result. This representation provides a foundation for future optimization or execution stages.

## Features

### Compiler Pipeline

* Converts source code into a sequence of tokens
* Validates token sequences against predefined grammar rules
* Generates intermediate code in quadruple format
* Displays compiler output through a desktop interface

### Lexical Analysis

The tokenizer scans source code character by character and identifies language elements such as:

* Keywords
* Identifiers
* Integer literals
* Arithmetic operators
* Relational operators
* Assignment operators
* Statement delimiters

### Syntax Analysis

The parser uses a recursive descent approach to process the token stream and validate the structure of the source program.

Supported language structures include:

* Variable declarations
* Variable assignments
* Arithmetic expressions
* Conditional statements
* `while` loops
* Compound statements using `begin` and `end`

### Intermediate-Code Generation

The compiler generates quadruples as an intermediate representation.

Each quadruple contains:

```text
Operator | Operand 1 | Operand 2 | Result
```

For example, an expression such as:

```text
$f = $f + 6
```

may be represented as:

```text
(+, $f, 6, T0)
(=, T0, null, $f)
```

### Symbol and Temporary Tables

The application maintains information about:

* Declared identifiers
* Variable types
* Stored values
* Temporary variables generated during expression evaluation

### Graphical User Interface

The Windows Forms interface provides:

* Source-code input
* Tokenization controls
* Parsing controls
* Token output
* Intermediate-code output
* Symbol-table display
* Temporary-variable display
* Clipboard support for copying compiler results

## Technology Stack

| Component            | Technology                   |
| -------------------- | ---------------------------- |
| Programming language | C#                           |
| User interface       | Windows Forms                |
| Framework            | .NET Framework 4.7 or higher |
| IDE                  | Visual Studio 2019 or later  |
| Project type         | Desktop application          |
| Platform             | Windows                      |

## System Requirements

* Windows operating system
* Visual Studio 2019 or later
* .NET Framework 4.7 or higher
* Minimum 4 GB RAM
* Approximately 50 MB of available disk space

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/Rubayat0007/MiniCompiler.git
cd MiniCompiler
```

### Open the Project

1. Open the solution file:

   ```text
   compiler202124405005.sln
   ```

2. Open the solution in Visual Studio.

3. Confirm that the required .NET Framework version is installed.

4. Restore or verify the project dependencies if prompted.

### Build the Application

In Visual Studio:

1. Select the desired build configuration.
2. Select **Build > Build Solution**.
3. Resolve any environment-specific configuration issues if prompted.

### Run the Application

Run the application from Visual Studio by pressing **F5** or selecting:

**Debug > Start Debugging**

The compiled executable can also be found in the appropriate build directory:

```text
bin/Debug/
```

or:

```text
bin/Release/
```

## Usage

1. Launch the MiniCompiler application.
2. Enter source code into the input area.
3. Select **Tokenize** to generate and inspect the token stream.
4. Select **Parse** to validate the syntax.
5. Review the generated intermediate code.
6. Inspect the symbol table and temporary-variable table.

## Example Input

The following example demonstrates variable declaration, assignment, arithmetic operations, a loop, and conditional statements:

```text
int$f;
$f=8;
$f=$f;
while($f>=$f*4)do
$f=$f+6;

if($f<$f*865)then
begin
$f=$f*5;
$f=$f+17;
end;
else
begin
$f=$f+6;
$f=$f+8;
end;
```

Another supported example is:

```text
int$y;
$y=94;
$y=8;
while($y+8>=$y+4)do
begin
$y=$y*580;
$y=$y+418;
end;

if($y*1>=$y)then
$y=$y+1;
else
$y=$y*64;
```

The language is intentionally limited and follows the grammar supported by the current tokenizer and parser. Input that does not follow the supported structure may produce a tokenization or parsing error.

## Architecture

### 1. Lexical Analysis

The tokenizer reads the source code and converts it into meaningful tokens.

Examples include:

```text
int       → kw_int
$f        → identifier
=         → assign
while     → kw_while
;         → semiColon
```

### 2. Syntax Analysis

The parser consumes the generated tokens and checks whether they follow the language grammar.

The parser handles constructs such as:

* Declarations
* Assignments
* Expressions
* Conditions
* Loops
* Compound statements

### 3. Intermediate-Code Generation

Expressions and statements are converted into quadruples.

Example:

```text
$f = $f + 6
```

Intermediate representation:

```text
Idx | Op | Opr1 | Opr2 | Result
----|----|------|------|-------
0   | +  | $f   | 6    | T0
1   | =  | T0   | null | $f
```

### 4. Symbol Table

The symbol table stores information about identifiers used in the source program.

Typical information includes:

```text
Name | Type | Value
-----|------|------
$f   | int  | 8
```

### 5. Temporary Variable Table

Temporary variables are created during expression evaluation and intermediate-code generation.

Example:

```text
Name | Type | Expression
-----|------|------------
T0   | int  | $f * 4
T1   | bool | $f >= T0
```

## Project Structure

```text
MiniCompiler/
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
```

## Current Limitations

The language is intentionally small and supports only the grammar implemented by the current tokenizer and parser.

Current limitations include:

* Limited language syntax
* Restricted expression structures
* No complete machine-code backend
* No function declarations or function calls
* Limited compiler diagnostics
* No advanced optimization pipeline
* Windows-specific graphical interface

## Future Improvements

Potential future improvements include:

* Constant folding
* Dead-code elimination
* Improved syntax and semantic error reporting
* Function declarations and calls
* Arrays and additional data types
* Expanded control-flow structures
* Abstract syntax tree visualization
* Three-address code generation
* LLVM IR or machine-code generation
* Automated compiler test cases
* Improved user interface and code-editing support

## Attribution

This project was adapted from the original educational compiler project:

https://github.com/4riful/custom-compiler

The current repository contains substantial modifications and extensions made for further development, experimentation, and learning.

## Author

**Rubayat Karim**

GitHub: [Rubayat0007](https://github.com/Rubayat0007)

## License

This repository does not currently include a license file. Unless a license is added, the code should not be assumed to be available for unrestricted reuse or redistribution.

## Repository

https://github.com/Rubayat0007/MiniCompiler

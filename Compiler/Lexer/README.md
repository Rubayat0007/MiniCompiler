\# Lexer Design



\## Purpose



The lexer converts MiniCompiler source text into a sequence of tokens.



Each token contains:



\- A token kind

\- The original text

\- Its source position



\## Current Language Elements



\### Keywords



\- `int`

\- `if`

\- `then`

\- `else`

\- `end`

\- `while`

\- `do`

\- `begin`

\- `for`



\### Punctuation



\- `;`

\- `,`

\- `(`

\- `)`



\### Arithmetic Operators



\- `+`

\- `-`

\- `\*`

\- `/`

\- `%`



\### Relational Operators



\- `<`

\- `<=`

\- `>`

\- `>=`

\- `!`

\- `!=`



\### Assignment



\- `:=`



\### Identifiers



The current language uses identifiers surrounded by dollar signs.



Example:



```text

$x$

$total1$


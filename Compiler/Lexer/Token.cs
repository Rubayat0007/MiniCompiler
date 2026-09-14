using System;

namespace compiler2026.Compiler.Lexer
{
    public class Token
    {
        public TokenKind Kind { get; private set; }

        public string Lexeme { get; private set; }

        public int Line { get; private set; }

        public int Column { get; private set; }

        public Token(TokenKind kind, string lexeme, int line, int column)
        {
            Kind = kind;
            Lexeme = lexeme;
            Line = line;
            Column = column;
        }

        public override string ToString()
        {
            return string.Format(
                "{0} '{1}' at line {2}, column {3}",
                Kind,
                Lexeme,
                Line,
                Column
            );
        }
    }
}
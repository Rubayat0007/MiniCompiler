using System;
using System.Collections.Generic;

namespace compiler2026.Compiler.Lexer
{
    public static class LexerTest
    {
        public static string Run()
        {
            Lexer lexer = new Lexer(
                "$int$ $x$; $x$ := 25 + 5"
            );

            List<Token> tokens = lexer.Tokenize();

            string result = string.Empty;

            foreach (Token token in tokens)
            {
                result += token.ToString() + Environment.NewLine;
            }

            return result;
        }
    }
}
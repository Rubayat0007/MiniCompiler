using System;
using System.Collections.Generic;

namespace compiler2026.Compiler.Lexer
{
    public class Lexer
    {
        private readonly string source;
        private int position;
        private int line;
        private int column;

        public Lexer(string sourceText)
        {
            source = sourceText ?? string.Empty;
            position = 0;
            line = 1;
            column = 1;
        }

        public List<Token> Tokenize()
        {
            List<Token> tokens = new List<Token>();

            while (position < source.Length)
            {
                SkipWhitespace();

                if (position >= source.Length)
                {
                    break;
                }

                tokens.Add(ReadNextToken());
            }

            tokens.Add(new Token(TokenKind.EndOfFile, "#", line, column));

            return tokens;
        }

        private void SkipWhitespace()
        {
            while (position < source.Length &&
                   char.IsWhiteSpace(source[position]))
            {
                Advance();
            }
        }

        private Token ReadNextToken()
        {
            int tokenLine = line;
            int tokenColumn = column;
            char current = source[position];

            if (char.IsDigit(current))
            {
                return ReadInteger(tokenLine, tokenColumn);
            }

            if (current == '$')
            {
                return ReadIdentifier(tokenLine, tokenColumn);
            }

            Advance();

            switch (current)
            {
                case ';':
                    return new Token(TokenKind.Semicolon, ";", tokenLine, tokenColumn);

                case ',':
                    return new Token(TokenKind.Comma, ",", tokenLine, tokenColumn);

                case '(':
                    return new Token(TokenKind.LeftParenthesis, "(", tokenLine, tokenColumn);

                case ')':
                    return new Token(TokenKind.RightParenthesis, ")", tokenLine, tokenColumn);

                case '+':
                    return new Token(TokenKind.Plus, "+", tokenLine, tokenColumn);

                case '-':
                    return new Token(TokenKind.Minus, "-", tokenLine, tokenColumn);

                case '*':
                    return new Token(TokenKind.Multiply, "*", tokenLine, tokenColumn);

                case '/':
                    return new Token(TokenKind.Divide, "/", tokenLine, tokenColumn);

                case '%':
                    return new Token(TokenKind.Modulo, "%", tokenLine, tokenColumn);

                case ':':
                    if (Match('='))
                    {
                        return new Token(TokenKind.Assign, ":=", tokenLine, tokenColumn);
                    }

                    return new Token(TokenKind.Invalid, ":", tokenLine, tokenColumn);

                case '<':
                    if (Match('='))
                    {
                        return new Token(TokenKind.LessThanOrEqual, "<=", tokenLine, tokenColumn);
                    }

                    return new Token(TokenKind.LessThan, "<", tokenLine, tokenColumn);

                case '>':
                    if (Match('='))
                    {
                        return new Token(TokenKind.GreaterThanOrEqual, ">=", tokenLine, tokenColumn);
                    }

                    return new Token(TokenKind.GreaterThan, ">", tokenLine, tokenColumn);

                case '!':
                    if (Match('='))
                    {
                        return new Token(TokenKind.NotEqual, "!=", tokenLine, tokenColumn);
                    }

                    return new Token(TokenKind.Not, "!", tokenLine, tokenColumn);

                default:
                    return new Token(
                        TokenKind.Invalid,
                        current.ToString(),
                        tokenLine,
                        tokenColumn
                    );
            }
        }

        private Token ReadInteger(int tokenLine, int tokenColumn)
        {
            int start = position;

            while (position < source.Length &&
                   char.IsDigit(source[position]))
            {
                Advance();
            }

            string lexeme = source.Substring(start, position - start);

            return new Token(
                TokenKind.IntegerLiteral,
                lexeme,
                tokenLine,
                tokenColumn
            );
        }

        private Token ReadIdentifier(int tokenLine, int tokenColumn)
        {
            int start = position;

            Advance();

            while (position < source.Length &&
                   (char.IsLetterOrDigit(source[position]) ||
                    source[position] == '_'))
            {
                Advance();
            }

            if (position < source.Length && source[position] == '$')
            {
                Advance();

                string lexeme = source.Substring(start, position - start);

                return new Token(
                    GetKeywordKind(lexeme),
                    lexeme,
                    tokenLine,
                    tokenColumn
                );
            }

            string invalidLexeme = source.Substring(start, position - start);

            return new Token(
                TokenKind.Invalid,
                invalidLexeme,
                tokenLine,
                tokenColumn
            );
        }

        private TokenKind GetKeywordKind(string lexeme)
        {
            switch (lexeme)
            {
                case "$int$":
                    return TokenKind.KeywordInt;

                case "$if$":
                    return TokenKind.KeywordIf;

                case "$then$":
                    return TokenKind.KeywordThen;

                case "$else$":
                    return TokenKind.KeywordElse;

                case "$end$":
                    return TokenKind.KeywordEnd;

                case "$while$":
                    return TokenKind.KeywordWhile;

                case "$do$":
                    return TokenKind.KeywordDo;

                case "$begin$":
                    return TokenKind.KeywordBegin;

                case "$for$":
                    return TokenKind.KeywordFor;

                default:
                    return TokenKind.Identifier;
            }
        }

        private bool Match(char expected)
        {
            if (position >= source.Length ||
                source[position] != expected)
            {
                return false;
            }

            Advance();
            return true;
        }

        private void Advance()
        {
            if (position >= source.Length)
            {
                return;
            }

            if (source[position] == '\n')
            {
                line++;
                column = 1;
            }
            else
            {
                column++;
            }

            position++;
        }
    }
}
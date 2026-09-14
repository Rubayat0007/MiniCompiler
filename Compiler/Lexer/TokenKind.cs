namespace compiler2026.Compiler.Lexer
{
    public enum TokenKind
    {
        EndOfFile,

        KeywordInt,
        KeywordIf,
        KeywordThen,
        KeywordElse,
        KeywordEnd,
        KeywordWhile,
        KeywordDo,
        KeywordBegin,
        KeywordFor,

        Identifier,
        IntegerLiteral,

        Semicolon,
        Comma,
        LeftParenthesis,
        RightParenthesis,

        Plus,
        Minus,
        Multiply,
        Divide,
        Modulo,

        Assign,

        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanOrEqual,
        Not,
        NotEqual,

        Invalid
    }
}
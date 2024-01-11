using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using lilulang.src.entity;

namespace lilulang.src {

    public class Lexer {

        private readonly string OPERATORS_SEQUENCE = "+-*/%()";
        private readonly Dictionary<string, TokenType> grammar;

        private int pos;
        
        public Lexer(string srcCode) {
            if (string.IsNullOrEmpty(srcCode)) {
                throw new ArgumentException("[INFO]: Empty data. Please add source code...");
            }

            pos = 0;
            SrcCode = srcCode;
            Tokens = new List<Token>();

            grammar = CreateGrammar();
        }

        private Dictionary<string, TokenType> CreateGrammar() {
            var grammar = new Dictionary<string, TokenType> {
                ["+"]   = TokenType.ADD,
                ["-"]   = TokenType.SUB,
                ["*"]   = TokenType.MUL,
                ["/"]   = TokenType.DIV,
                ["end"] = TokenType.END,
                ["num"] = TokenType.NUMBER      // NOTE: mutable value parameter
            };
            return grammar;
        }

        public List<Token> Tokenize() {

            // FIXME: Bad realization. need to be fixed.
            while (pos < SrcCode.Length) {
                var currentChar = Peek();

                if (char.IsDigit(currentChar)) {
                    TokenizeNumber();
                } else if (OPERATORS_SEQUENCE.Contains(currentChar.ToString())) {
                    TokenizeSpecialChar();
                } else {
                    // TODO: Whitespaces not implemented.
                    Step();
                }

            }
            return Tokens;
        }

        // Идет продвижение исходной строки с помощью утил метода Step()
        private void TokenizeNumber() {
            StringBuilder stringBuilder = new StringBuilder();
            char currentChar = Peek();

            while (char.IsDigit(currentChar)) {
                stringBuilder.Append(currentChar);
                currentChar = Step();
            }

            AddToken(grammar["num"], stringBuilder.ToString());
        }

        // Идет продвижение исходной строки с помощью утил метода Step()
        private void TokenizeSpecialChar() {
            // Костыль небольшой...
            AddToken(grammar["+"], "");
            Step();
        }
        
        private char Peek(int relative = 0) {
            int position = relative + pos;
            if (position >= SrcCode.Length) {
                return '\n';
            }
            return SrcCode[position];
        }

        private char Step() {
            pos++;
            return Peek();
        }

        private void AddToken(TokenType type, string value) {
            Token token = new Token(type, value);
            Tokens.Add(token);
        }

        private List<Token> Tokens { get; set; }

        private string SrcCode { get; set; }

        public int Pos { get => pos; 
            set => pos = value; }

        public override string ToString() {
            return "[Lilu] -> Lexical Analyzer [" + GetHashCode() + "]";
        }
    }
}

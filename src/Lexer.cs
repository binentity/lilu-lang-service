using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace lilulang.src {

    public class Lexer {

        private static readonly string OPERATORS = "+-*/%()";

        private List<Token> tokensList;
        private string srcCode;
        private int pos;

        private Dictionary<string, TokenType> terminals;
        
        public Lexer(string SrcCode) {
            if (string.IsNullOrEmpty(SrcCode)) {
                throw new ArgumentException("[INFO]: empty data. Please add text...");
            }

            pos = 0;
            this.SrcCode = SrcCode;
            tokensList = new List<Token>();

            CreateGrammarData();
        }

        public Dictionary<string, TokenType> CreateGrammarData() {
            terminals = new Dictionary<string, TokenType> {
                ["+"]   = TokenType.ADD,
                ["-"]   = TokenType.SUB,
                ["*"]   = TokenType.MUL,
                ["/"]   = TokenType.DIV,
                ["end"] = TokenType.END,
                ["num"] = TokenType.NUMBER      //mutable value
            };

            return terminals;
        }

        public List<Token> Tokenize() {

            // FIXME: Bad realization. need to be fixed.
            while (pos < SrcCode.Length) {
                char currentChar = Peek(0);

                if (char.IsDigit(currentChar)) {
                    TokenizeNumber();
                } else if (OPERATORS.Contains(currentChar.ToString())) {
                    TokenizeSpecialChar();
                } else {
                    // TODO: Whitespaces not implemented.
                    Step();
                }

            }
            return tokensList;
        }

        // Идет продвижение исходной строки с помощью утил метода Step()
        private void TokenizeNumber() {
            StringBuilder stringBuilder = new StringBuilder();
            char currentChar = Peek(0);
            while (char.IsDigit(currentChar)) {
                stringBuilder.Append(currentChar);
                currentChar = Step();
            }
            AddToken(TokenType.NUMBER, stringBuilder.ToString());
        }

        // Идет продвижение исходной строки с помощью утил метода Step()
        private void TokenizeSpecialChar() {
            // Костыль небольшой...
            AddToken(TokenType.ADD, "+");
            Step();
        }
        
        #region utilMethods

        //Utils method
        private void AddToken(TokenType type, string value) {
            Token token = new Token(type, value);
            tokensList.Add(token);
        }

        //Utils method
        private char Peek(int relativePosition = 0) {
            int position = relativePosition + pos;
            if (position >= SrcCode.Length) {
                return '\0';
            }
            return SrcCode[position];
        }

        //Utils method
        private char Step() {
            pos += pos + 1;
            return Peek(0);
        }
        #endregion

        public List<Token> Tokens {
            get => tokensList;
            set => tokensList = value;
        }

        public string SrcCode {
            get => srcCode;
            set => srcCode = value;
        }
        
        public override string ToString() {
            return "[Lilu] -> Lexical Analyzer [" + GetHashCode() + "]";
        }
    }
}

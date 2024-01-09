using System;
using System.Collections.Generic;
using System.Text;

namespace lilulang.src {

    public class Lexer {

        private string operators = "+-";

        public List<Token> tokensList;
        private string srcCode;
        private int pos;

        public Lexer(string SrcCode) {
            if (string.IsNullOrEmpty(SrcCode)) {
                throw new ArgumentException("[INFO]: empty data. Please add text...");
            }

            pos = 0;
            this.SrcCode = SrcCode;
            tokensList = new List<Token>();
        }

        private char Peek() {
            if (pos >= SrcCode.Length) {
                return '\0';
            }
            return SrcCode[pos];
        }

        private char Step() {
            pos++;
            return Peek();
        }
        
        public List<Token> Tokens {
            get => tokensList;
            set => tokensList = value;
        }

        public string SrcCode {
            get => srcCode;
            set => srcCode = value;
        }

        public List<Token> Tokenize() {


            while (pos < SrcCode.Length) {
                char currentChar = Peek();

                if (char.IsDigit(currentChar)) {
                    TokenizeNumber();
                } else if (operators.Contains(currentChar.ToString())) {
                    TokenizeSpecialChar();
                } else {
                    // TODO: Whitespaces
                    Step();
                }
                
                //Step();
            }
            return tokensList;
        }

        private void TokenizeNumber() {
            StringBuilder stringBuilder = new StringBuilder();
            char currentChar = Peek();
            while (char.IsDigit(currentChar)) {
                stringBuilder.Append(currentChar);
                currentChar = Step();
            }
            Token token = new Token(TokenType.NUMBER, stringBuilder.ToString());
            tokensList.Add(token);
        }

        private void TokenizeSpecialChar() {
            // Костыль небольшой...
            Token token = new Token(TokenType.ADD, "+");
            tokensList.Add(token);
            Step();
        }
    }
}

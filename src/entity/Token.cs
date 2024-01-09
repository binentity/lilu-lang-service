using System;


namespace lilulang.src {

    public enum TokenType {
        NUMBER = 0, 
        ADD    = 1,
        END    = 2,
        SUB    = 3,
        MUL    = 4,
        DIV    = 5
    }

    public class Token {

        private TokenType type;
        private string value;

        public Token(TokenType type, string value) {
            this.type = type;
            this.value = value;
        }

        public string Value { get => value;
            set => this.value = value; }
        
        public TokenType Type { get => type; 
            set => type = value; }
    }
}

using System;

using lilulang.src.entity;

namespace lilulang.src {

    public class LiluLang {

        public static void Main(string[] args) {

            // TODO: usage, help, manual for terminal and other information.

            //NOTE: Debug probe.
            Lexer lexer = new Lexer("5 + 5");
            Console.WriteLine(lexer.ToString());
            foreach (var token in lexer.Tokenize()) {
                Console.WriteLine(token.Type);
            }
            //NOTE: End Debug probe.

            Console.WriteLine("\nPress any key to continue...\n");
            Console.ReadKey();
        }
        

        public static void GetEnvInfo() {
            // TODO: вывести информацию окружения.
        }
    }
}

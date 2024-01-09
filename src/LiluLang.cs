using System;

namespace lilulang.src {

    public class LiluLang {

        public static void Main(string[] args) {

            //Probe
            const string srcCode = "2 + 12";
            Lexer lexer = new Lexer(srcCode);

            foreach (var token in lexer.Tokenize()) {
                Console.WriteLine(token.Type.ToString());
            }

            //Exit
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static void GetEnvInfo() {
            // TODO: вывести информацию окружения.
        }
    }
}

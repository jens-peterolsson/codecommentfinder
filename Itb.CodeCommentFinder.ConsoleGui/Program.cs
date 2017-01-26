using System;
using System.Threading.Tasks;

namespace Itb.CodeCommentFinder.ConsoleGui
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("GitHub User name: ");
            var userName = Console.ReadLine();
            Console.Write("GitHub password: ");
            var password = GetPassword();
            Console.Write("Github repo name: ");
            var repoName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(repoName))
            {
                Console.WriteLine("Nope, you got it wrong.");
                Console.WriteLine("T-E-R-M-I-N-A-T-I-N-G.");
                
                return;
            }

            Task t = MainAsync(userName, password, repoName);
            t.Wait();
        }

        private static async Task MainAsync(string userName, string password, string repoName)
        {
            var repo = SimplisticDi.GetRepository();
            var parser = SimplisticDi.GetFileParser();

            Console.WriteLine("Getting files...");
            var files = await repo.GetAllFilesAsync(userName, password, repoName);

            Console.Write("Parsing comments... ");
            var comments = parser.FindComments(files);

            Console.WriteLine("Done.");
            Console.WriteLine();
            Console.WriteLine("Output ->");
            Console.WriteLine();

            Console.Write(comments);
            Console.WriteLine();
            Console.Write("Press that key...");
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Thanks.");
            Console.WriteLine();
        }

        private static string GetPassword()
        {
            var result = string.Empty;

            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }

                result += key.KeyChar;
                Console.Write("*");
            }

            return result;
        }
    }

}


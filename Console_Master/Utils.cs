using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Master
{
    public class Utils
    {
        public static string GetUserInput(string message)
        {
            LogCyan(message);
            string input = Console.ReadLine();
            Console.Clear();
            return input;
        }

        public static void LogCyan(string message)
        {
            // Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public static void clrScreen()
        {
            Console.Clear();
        }
    }
}

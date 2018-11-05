using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_Master.Books;
using System.IO;
using System.Xml.Serialization;

namespace Console_Master
{
    class Program
    {
        static void Main(string[] args)
        {
            InitCategoryMenu();
        }

        private static void InitCategoryMenu()
        {
            #region CategoryMenu

            ConsoleKeyInfo input;
            do
            {
                Utils.LogCyan("| B : Book Manager | C: Car Manager | ESC: to Quit | ");

                input = Console.ReadKey();
                Utils.clrScreen();

                switch (input.Key.ToString())
                {
                    case "B":
                        BookManager.InitBookManager();
                        break;
                    case "C":
                        Console.WriteLine("not implemented");
                        break;
                    case "ESC":
                        Environment.Exit(0);
                        break;
                }

            } while (input.Key != ConsoleKey.Escape);
            #endregion
        }

        //private static void InitBooks()
        //{
        //    #region BookMenu

        //    ConsoleKeyInfo input;
        //    do
        //    {
        //        Utils.LogCyan("| C : Categories | B: Books | ESC: Back to Menu | ");

        //        input = Console.ReadKey();
        //        Utils.clrScreen();

        //        switch (input.Key.ToString())
        //        {
        //            case "C":
        //                InitCategoryMenu();
        //                break;
        //            case "B":
        //                BookManager.InitBookMenu();
        //                break;
        //            case "ESC":
        //                Environment.Exit(0);
        //                break;
        //        }

        //    } while (input.Key != ConsoleKey.Escape);
        //    #endregion
        //}
    }

    class Makers
    {
        public string Name { get; set; }
        public Guid Guid { get; set; }
    }

    class Cars
    {
        public string CarModel { get; set; }
        public Guid Guid { get; set; }
    }
}

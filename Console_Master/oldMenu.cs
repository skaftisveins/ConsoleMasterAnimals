//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using Console_Master.Books;

//namespace Console_Master
//{
//    class oldMenu
//    {
//        private static void InitCategoryMenu()
//        {
//            #region CategoryMenu

//            ConsoleKeyInfo input;
//            do
//            {
//                Utils.LogCyan("| B : Books Menu | C: Cars Menu | ESC: to Quit | ");

//                input = Console.ReadKey();
//                Utils.clrScreen();

//                switch (input.Key.ToString())
//                {
//                    case "B":
//                        InitBooks();
//                        break;
//                    case "C":
//                        Console.WriteLine("not implemented");
//                        break;
//                    case "ESC":
//                        Environment.Exit(0);
//                        break;
//                }

//            } while (input.Key != ConsoleKey.Escape);
//            #endregion
//        }

//        private static void InitBooks()
//        {
//            #region BookMenu

//            ConsoleKeyInfo input;
//            do
//            {
//                Utils.LogCyan("| C : Categories | B: Books | ESC: Back to Menu | ");

//                input = Console.ReadKey();
//                Utils.clrScreen();

//                switch (input.Key.ToString())
//                {
//                    case "C":
//                        InitCategoryMenu();
//                        break;
//                    case "B":
//                        break;
//                    case "ESC":
//                        Environment.Exit(0);
//                        break;
//                }

//            } while (input.Key != ConsoleKey.Escape);
//            #endregion
//        }
//    }
//}

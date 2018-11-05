using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Console_Master.Books
{
    public static class BookManager
    {
        public static string filePath = "../../Books/myfile.xml";

        public static void InitBookManager()
        {
            #region Bookmanager Menu

            ConsoleKeyInfo input;
            do
            {
                Console.WriteLine("Bookmanager initialized...");
                Utils.LogCyan("| A: Add | C: Create new list | D: Delete | E: Edit | L: List |S: Search | ESC: Back to Menu | ");

                input = Console.ReadKey();
                Utils.clrScreen();

                switch (input.Key.ToString())
                {
                    case "A":
                        // AddBook();
                        AddNewBook();
                        break;
                    case "C":
                        Console.WriteLine("not implemented");
                        break;
                    case "D":
                        Console.WriteLine("not implemented");
                        break;
                    case "E":
                        // EditBook(filePath, input);
                        break;
                    case "L":
                        ReadBooks();
                        break;
                    case "S":
                        Console.WriteLine("not implemented");
                        break;
                    case "ESC":
                        Environment.Exit(0);
                        break;
                }

            } while (input.Key != ConsoleKey.Escape);
            #endregion
        }

        public static void AddBook()
        {
            // Skrifa
            // Ná í eintak af XmlSerializer fyrir lista af Book object
            //Búa til eintak af bók og smella í lista
            // Hreinsa gögn í straum
            // Serializa ný gögn í straum
            FileStream readStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer bookListSerializer = new XmlSerializer(typeof(List<Book>));
            Guid guid1 = Guid.NewGuid();
            Book book1 = new Book() { Guid = guid1, Title = "Darkness", Author = "Reaper", Description = "Other World Encounters", ReleaseDate = "30/06/08" };
            List<Book> books = new List<Book> { book1 };
            readStream.SetLength(0);
            bookListSerializer.Serialize(readStream, books);
            readStream.Close();
        }

        public static void ReadBooks()
        {
            // lesa það sem er til úr file(deserialize)
            Stream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
            XmlSerializer xs = new XmlSerializer(typeof(List<Book>));
            List<Book> myBooks = (List<Book>)xs.Deserialize(stream);
            stream.Close();
            foreach(var b in myBooks)
            {
                Console.WriteLine($"Name: {b.Title}");
                Console.WriteLine($"Author: {b.Author}");
                Console.WriteLine($"Description: {b.Description}");
                Console.WriteLine($"Release Date: {b.ReleaseDate}");
                Console.WriteLine();
            }
        }

        public static void EditBook(List<Book> myBooks, string filePath, string userInput)
        {
            bool isSelected = false;
            Console.WriteLine("Please write name of book to edit:");
            string tempName = filePath + Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Now editing: " + tempName);
            Console.WriteLine("| 1. Name | 2. Author | 3. Description | 4. Release Date |");
            ConsoleKeyInfo input;
            input = Console.ReadKey();
            switch (input.Key.ToString())
            {
                case "1":
                    foreach (var b in myBooks)
                        Console.WriteLine($"Name: {b.Title}, {isSelected}");
                    break;
                case "2":
                    foreach (var b in myBooks)
                        Console.WriteLine($"Author: {b.Author},{isSelected}");
                    break;
                case "3":
                    foreach (var b in myBooks)
                        Console.WriteLine($"Description: {b.Description},{isSelected}");
                    break;
                case "4":
                    foreach (var b in myBooks)
                        Console.WriteLine($"Release Date: {b.ReleaseDate},{isSelected}");
                    break;
            }
            if (isSelected == true)
            { 
                Console.WriteLine();
                Console.WriteLine("Please edit current book now: (.append mode)");
            }
            string tempEdit = Console.ReadLine();
            EditBook(myBooks, tempName, tempEdit);
            FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            string timeStamp = DateTime.Now.ToString();
            sw.WriteLine(userInput);
            sw.Close();
            fs.Close();
        }

        #region Book class and functions
        public class Book
        {
            public Guid Guid { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string Description { get; set; }
            public string ReleaseDate { get; set; }
        }

        public static Book CreateBook(string Title, Guid guid)
        {
            string title = Title;
            Guid Guid = guid;
            return new Book { Title = title, Guid = guid };
        }

        public static Book AddNewBook()
        {
            Console.WriteLine("Please input the name of the book you wish to add");
            Console.WriteLine();
            string bookName = Utils.GetUserInput("Please input book name: ");
            Guid guid = Guid.NewGuid();
            CreateBook(bookName, guid);
            return CreateBook(bookName, guid);
        } 
        #endregion
        
    }
}

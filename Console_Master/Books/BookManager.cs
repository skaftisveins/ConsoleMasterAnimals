using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Console_Master.Books
{
    public static class BookManager
    {
        public static string filePath = "../../Books/myfile.xml";
        public static List<Book> books = new List<Book>();


        public static void AddBook()
        {
            // Skrifa
            // Ná í eintak af XmlSerializer fyrir lista af Book object
            //Búa til eintak af bók og smella í lista
            // Hreinsa gögn í straum
            // Serializa ný gögn í straum
            FileStream readStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer bookListSerializer = new XmlSerializer(typeof(List<Book>));
            Guid guid = Guid.NewGuid();
            Book book = new Book() { Guid = guid, Title = "Darkness", Author = "Reaper", Description = "Other World Encounters", ReleaseDate = "30/06/08" };
            List<Book> books = new List<Book> { book };
            readStream.SetLength(0);
            bookListSerializer.Serialize(readStream, books);
            readStream.Close();
        }

        public static void AddNewBook()
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Book>));
                Console.WriteLine("Please input the name of the book you wish to add: ");
                string bookName = Utils.GetInput("input name of book");
                Console.WriteLine("Please input the author: ");
                string bookAuthor = Utils.GetInput("input author");
                Console.WriteLine("Please input the description for the book: ");
                string bookDescription = Utils.GetInput("input description of the book");
                Console.WriteLine("Please input the release date for the book: ");
                string bookReleaseDate = Utils.GetInput("input release date");
            Guid guid = Guid.NewGuid();
            Book book = new Book() { Guid = guid, Title = bookName, Author = bookAuthor, Description = bookDescription, ReleaseDate = bookReleaseDate };

            bool xmlEmpty = false;
            if(xmlEmpty == true)
            {
                FileStream write = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                XmlSerializer bookListSerializer = new XmlSerializer(typeof(List<Book>));
                write.Close();
            }
            List<Book> books;
            FileStream readStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (readStream.Length != 0)
            {
                books = (List<Book>)xs.Deserialize(readStream);
                books.Add(book);
                readStream.Close();
            }
            else
            {
                books = new List<Book>() { book };
            }

            FileStream writeStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            xs.Serialize(writeStream, books);
            writeStream.Close();
        }

        public static void EditBook()
        {
            //List<Book> deserializeBookList = Deserialize()
            //deserializeBookList.SetLength(0);
            FileStream writeStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            XmlSerializer xs = new XmlSerializer(typeof(List<Book>));
            List<Book> books = new List<Book>();
            xs.Serialize(writeStream, books);
            bool isSelected = false;
            Console.WriteLine("Please write the name of the book you wish to edit: ");
            string tempName = Utils.GetInput("");
            Console.Write("Now editing: " + tempName);
            foreach (var b in books)
            {
                Console.WriteLine($"Name: {b.Title}{isSelected}");
                Console.WriteLine($"Author: {b.Author}{isSelected}");
                Console.WriteLine($"Description: {b.Description}{isSelected}");
                Console.WriteLine($"Release Date: {b.ReleaseDate}{isSelected}");
                Console.WriteLine();
            }

            if (isSelected == true)
            {
                Console.WriteLine($"Please edit {tempName}: (.append mode)");
            }

            writeStream.Close();
            Console.ReadKey();
        }

        public static void ListAllBooksAndInfo()
        {
            // lesa það sem er til úr file(deserialize)
            Stream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
            XmlSerializer xs = new XmlSerializer(typeof(List<Book>));
            List<Book> Books = (List<Book>)xs.Deserialize(stream);
            stream.Close();
            foreach(var b in Books)
            {
                Console.WriteLine($"Name: {b.Title}");
                Console.WriteLine($"Author: {b.Author}");
                Console.WriteLine($"Description: {b.Description}");
                Console.WriteLine($"Release Date: {b.ReleaseDate}");
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public static void ReadBook()
        {
            Stream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
            XmlSerializer xs = new XmlSerializer(typeof(List<Book>));
            FileStream readStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
            List <Book> myBooks = (List<Book>)xs.Deserialize(stream);
            foreach (var b in myBooks)
            {
                Console.WriteLine($"Name: {b.Title}");
                Console.WriteLine($"Author: {b.Author}");
                Console.WriteLine($"Description: {b.Description}");
                Console.WriteLine($"Release Date: {b.ReleaseDate}");
                Console.WriteLine();
            }
            Console.WriteLine("Please write the name of the book you wish to read: ");
            string bookName = Utils.GetInput("input name of book");
            Book book = new Book() { Title = bookName };
            readStream.Close();
            Console.ReadKey();
        }
    }

    [XmlRoot("Book")]
    public class Book
    {
        [XmlAttribute]
        public Guid Guid { get; set; }
        [XmlAttribute]
        public string Title { get; set; }
        [XmlAttribute]
        public string Author { get; set; }
        [XmlAttribute]
        public string Description { get; set; }
        [XmlAttribute]
        public string ReleaseDate { get; set; }
    }
}

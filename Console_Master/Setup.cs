using ConsoleTools;
using Console_Master.Animals;
using System;
using System.IO;

namespace Console_Master
{
    public static class Setup
    {
        public static string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "information");
        public static string fileName = Path.Combine(folderPath, "AnimalsAndCategories.xml");
        public static string fileRemoved = Path.Combine(folderPath, "RemovedAnimals.xml");

        public static AnimalCategories animalCategories = new AnimalCategories();
        public static AnimalCategories deletedItems = new AnimalCategories();

        public static void InitMenu()
        {
            Console.Title = "Animal Manager";
            new ConsoleMenu()
            .Add("Animals", () => AnimalManager.AnimalMenu())
            .Add("Categories", () => CategoryManager.CategoryMenu()) // TODO Bæta við categories
            .Add("Quit", () => Environment.Exit(0))
            .Configure(c =>
            {
                c.WriteHeaderAction = () => Console.WriteLine("Animal Manager\n");
                c.Selector = "--> ";
            })
            .Show();
        }
    }
}

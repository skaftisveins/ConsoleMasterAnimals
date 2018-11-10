using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Console_Master.Animals
{
    public static class CategoryManager
    {
        public static List<Categories> categories = Setup.animalCategories.Categories ?? new List<Categories>();

        public static void CategoryMenu()
        {
            #region Category Menu

            new ConsoleMenu()
                    .Add("Add Category", () => AddCategory())
                    .Add("Edit Category", () => Edit())
                    .Add("List Categories", () => ListCategories())
                    .Add("Back", ConsoleMenu.Close)
                    .Configure(c =>
                    {
                        c.Selector = "-->";
                        c.WriteHeaderAction = () => Console.WriteLine($"There are {categories.Count} categories\n");
                    })
                    .Show();
            #endregion
        }

        #region Add category
        public static void AddCategory()
        {
            string catName = Utils.GetInput("\nEnter category name: ");
            if (!categories.Exists(x => Utils.Equals(catName, x.Name)) && !string.IsNullOrEmpty(catName))
            {
                Categories categorie = new Categories()
                {
                    Name = catName,
                    ID = new Guid(),
                    Animals = new List<Guid>()
                };

                categories.Add(categorie);
                Utils.Save();
            }
        }
        #endregion

        #region Edit category
        public static void Edit()
        {
            bool exited = false;
            while (categories.Count > 0 && !exited)
            {
                ConsoleMenu menu = new ConsoleMenu();
                menu.Add("*Back*", () => exited = true);

                foreach (Categories c in categories)
                {
                    menu.Add(c.Name, () => EditCategory(c));
                }

                menu.Configure(c =>
                {
                    c.EnableFilter = true;
                    c.FilterPrompt = "\nSearch for category: ";
                    c.WriteHeaderAction = () => Console.WriteLine($"Select a category to edit\n");
                    c.Selector = "--> ";
                    c.RunOnce = true;
                })
                .Show();
            }
        }

        private static void EditCategory(Categories editCat)
        {

            bool exited = false;
            while (!exited)
            {
                new ConsoleMenu()
                .Add("List of animals", () => internalList())
                .Add("Add Animal", () => internalAddAnimal())
                .Add("Remove Animal", () => internalRemoveAnimal())
                .Add("Change category name", () => internalEditName())
                .Add("Delete category", () => internalDeleteCategory())
                .Add("Back", () => exited = true)
                .Configure(c =>
                {
                    c.Selector = "--> ";
                    c.WriteHeaderAction = () => Console.WriteLine($"Current category: {editCat.Name} - Animals: {editCat.Animals.Count}\n");
                    c.RunOnce = true;
                })
                .Show();
            }

            void internalAddAnimal()
            {
                Guid bookId = AnimalManager.Retrive(editCat.Animals);
                if (!editCat.Animals.Exists(x => x.Equals(bookId)) && bookId != Guid.Empty)
                {
                    editCat.Animals.Add(bookId);
                    Utils.Save();
                }
            }

            void internalRemoveAnimal()
            {
                bool exit = false;
                while (editCat.Animals.Count > 0 && !exit)
                {
                    ConsoleMenu remBook = new ConsoleMenu()
                        .Add("<-- Back", () => exit = true);

                    AnimalsInCategory(editCat, remBook, internalRemoveAnimalWarning)

                    .Configure(c =>
                    {
                        c.RunOnce = true;
                        c.Selector = "";
                        c.ItemForegroundColor = ConsoleColor.Red;
                        c.SelectedItemForegroundColor = ConsoleColor.White;
                        c.SelectedItemBackgroundColor = ConsoleColor.Red;
                        c.WriteHeaderAction = () => Console.WriteLine($"DELETING AN ANIMAL FROM {editCat.Name}\n");
                    })
                    .Show();
                }
            }

            void internalRemoveAnimalWarning(int bookPos)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                string warningText = Utils.GetInput("You are about to Remove an animal Y/n: ");

                if (Utils.Equals(warningText, "Y", true))
                {
                    editCat.Animals.RemoveAt(bookPos);
                    Utils.Save();
                }
                Console.ResetColor();
            }

            void internalEditName()
            {
                string inp = Utils.GetInput($"Rename {editCat.Name} to ?: ");
                if (!categories.Exists(x => Utils.Equals(inp, x.Name)))
                {
                    editCat.Name = inp;
                    Utils.Save();
                }
            }

            void internalDeleteCategory()
            {
                if (editCat.Animals.Count > 0)
                {
                    Console.WriteLine($"There are still {editCat.Animals.Count} animals");
                    Console.ReadKey(true);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    string warningText = Utils.GetInput($"This will wipe {editCat.Name} Y/n: ");
                    if (Utils.Equals(warningText, "Y", true))
                    {
                        DeleteCategory(categories.IndexOf(editCat));
                        exited = true;
                    }
                    Console.ResetColor();
                }

            }

            void internalList()
            {
                AnimalsInCategory(editCat);
                Console.ReadKey(true);
            }
        }
        #endregion

        #region Animals in category
        private static ConsoleMenu AnimalsInCategory(Categories c, ConsoleMenu ret = null, Action<int> action = null)
        {
            List<Guid> cleanUp = new List<Guid>();
            ListCategories(categories.IndexOf(c));
            c.Animals.ForEach(x =>
            {
                Animals s = AnimalManager.animals.Find(a => a.ID.Equals(x));
                if (s != null)
                {
                    if (ret != null)
                    {
                        ret.Add(s.ToString(), () => action(c.Animals.IndexOf(x)));
                    }
                    else
                    {
                        Console.WriteLine($"-- {c.Animals.IndexOf(x)}) {s}");
                    }
                }
                else
                {
                    cleanUp.Add(x);
                }
            }
            );

            cleanUp.ForEach(x => c.Animals.Remove(x));
            if (cleanUp.Any())
            {
                Utils.Save();
            }
            return ret;
        }
        #endregion

        #region Delete a category
        public static void DeleteCategory()
        {
            ListCategories();
            if (int.TryParse(Utils.GetInput("Enter a category to delete: "), out int inp))
            {
                if (Utils.InRange(inp, categories.Count))
                {
                    DeleteCategory(inp);
                }
            }
        }

        private static void DeleteCategory(int inp)
        {
            categories.RemoveAt(inp);
            Utils.Save();
        }
        #endregion

        #region Show all categories
        public static void ListCategories()
        {
            for (int i = 0; i < categories.Count; i++)
            {
                ListCategories(i);
            }
            Console.WriteLine();
        }

        public static void ListCategories(int categiryID)
        {
            Console.WriteLine($"{categiryID}) {categories[categiryID].Name} Animals: {categories[categiryID].Animals.Count}");
        }
        #endregion

        #region Tuple IEnumerable
        private static IEnumerable<Tuple<string, Action>> CatList(Action action)
        {
            List<Tuple<string, Action>> ret = new List<Tuple<string, Action>>();
            foreach (Categories c in categories)
            {
                ret.Add(new Tuple<string, Action>($"{c.Name} - Books: {c.Animals.Count}", action)); // TODO Locate in program
            }
            return ret;
        }
        #endregion
    }
}

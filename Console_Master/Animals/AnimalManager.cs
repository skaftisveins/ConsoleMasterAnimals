using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Console_Master.Animals
{
    public static class AnimalManager
    {
        public static List<Animals> animals = Setup.animalCategories.Animals ?? new List<Animals>();
        public static List<Animals> deletedAnimals = Setup.deletedItems.Animals ?? new List<Animals>();

        public static void AnimalMenu()
        {
            #region AnimalManagerMenu

            new ConsoleMenu()
                    .Add("Back", ConsoleMenu.Close)
                    .Add("Add a new animal", () => AddAnimal())
                    .Add("Edit an animal", () => EditAnimal())
                    .Add("Delete an animal", () => DeleteAnimal())
                    .Add("Restore an animal", () => RestoreAnimal())
                    .Configure(c =>
                    {
                        c.Selector = "-->";
                        c.WriteHeaderAction = () => Console.WriteLine("Animal Manager\n");
                    })
                    .Show();
            #endregion
        }

        #region Add animal
        public static Guid AddAnimal()
        {
            string name = Utils.GetInput("Name: ", false);
            string type = Utils.GetInput("Type: ", false);
            if (animals.Exists(x => Utils.Equals(x.Name, name)))
            {
                return animals.Find(x => Utils.Equals(x.Name, name)).ID;
            }
            else
            {
                string breed = Utils.GetInput("Breed: ", false);
                if (DateTime.TryParseExact(Utils.GetInput("Birthdate dd/MM/yyyy: ", false), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.AssumeUniversal, out DateTime date))
                {
                    Animals dog = new Animals()
                    {
                        Type = type,
                        Name = name,
                        Breed = breed,
                        Birthdate = date,
                        ID = Guid.NewGuid()
                    };

                    animals.Add(dog);
                    Utils.Save();
                    return dog.ID;
                }
                return Guid.Empty;
            }
        }
        #endregion

        #region Retrive animal
        public static Guid Retrive(List<Guid> guids)
        {
            Guid a = Guid.Empty;
            ConsoleMenu menu = new ConsoleMenu()
                .Add("*Back*", ConsoleMenu.Close)
                .Add("*Add new animal*", () => a = AddAnimal());

            foreach (Animals animal in animals.Where(x => !guids.Contains(x.ID)))
            {
                menu.Add(animal.ToString(), () => a = animal.ID);
            }

            menu.Configure(c =>
            {
                c.Selector = "--> ";
                c.RunOnce = true;
            })
            .Show();
            return a;
        }
        #endregion

        #region Edit animal
        public static void EditAnimal()
        {
            bool exited = false;
            while (animals.Count > 0 && !exited)
            {
                ConsoleMenu menu = new ConsoleMenu();
                menu.Add("*Back*", () => exited = true);

                foreach (Animals a in animals)
                {
                    menu.Add(a.ToString(), () => internalEdit(a));
                }
                menu.Configure(c =>
                {
                    c.EnableFilter = true;
                    c.FilterPrompt = "\nSearch for animal: ";
                    c.WriteHeaderAction = () => Console.WriteLine($"Select an animal to edit\n");
                    c.Selector = "-->";
                    c.RunOnce = true;
                })
                .Show();
            }

            void internalEdit(Animals a)
            {
                string inpA = Utils.GetInput("Change the animal name: ", false);
                if (!string.IsNullOrEmpty(inpA))
                {
                    a.Name = inpA;
                }

                string inpT = Utils.GetInput("Change the animal type: ", false);
                if (!string.IsNullOrEmpty(inpT))
                {
                    a.Type = inpT;
                }

                string inpB = Utils.GetInput("Change the animal breed", false);
                if (!string.IsNullOrEmpty(inpB))
                {
                    a.Breed = inpB;
                }

                if (DateTime.TryParseExact(Utils.GetInput("Change the birthdate dd/MM/yyyy: ", false), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.AssumeUniversal, out DateTime date))
                {
                    a.Birthdate = date;
                }

                Utils.Save();
            }
        }
        #endregion

        #region Delete animal
        public static void DeleteAnimal()
        {
            bool exited = false;
            while (animals.Count > 0 && !exited)
            {
                ConsoleMenu menu = new ConsoleMenu().Add("Back", () => exited = true);
                foreach (Animals a in animals)
                {
                    menu.Add(a.ToString(), () => internalDelete(a));
                }
                menu.Configure(c =>
                {
                    c.Selector = "--> ";
                    c.WriteHeaderAction = () => Console.WriteLine("Select an animal to delete\n");
                    c.ItemForegroundColor = ConsoleColor.Red;
                    c.SelectedItemForegroundColor = ConsoleColor.White;
                    c.SelectedItemBackgroundColor = ConsoleColor.Red;
                    c.RunOnce = true;
                });
                menu.Show();
            }

            void internalDelete(Animals a)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                string warningText = Utils.GetInput("You are about to Delete an animal Y/n: ");

                if (Utils.Equals(warningText, "Y", true))
                {
                    deletedAnimals.Add(a.Copy());
                    animals.Remove(a);
                    Utils.Save();
                }
                Console.ResetColor();
            }
        }
        #endregion

        #region Restore deleted animals
        private static void RestoreAnimal()
        {
            bool exited = false;
            while (animals.Count > 0 && !exited)
            {
                ConsoleMenu menu = new ConsoleMenu().Add("Back", () => exited = true);
                foreach (Animals a in deletedAnimals)
                {
                    menu.Add(a.ToString(), () =>
                    {
                        animals.Add(a.Copy());
                        deletedAnimals.Remove(a);
                        Utils.Save();
                    });
                }

                menu.Configure(c =>
                {
                    c.Selector = "--> ";
                    c.WriteHeaderAction = () => Console.WriteLine("Select an animal to restore\n");
                    c.RunOnce = true;
                });
                menu.Show();
            }
        }
        #endregion

        #region NOT USED Show all animals in list
        public static void ListAnimals()
        {
            for (int i = 0; i < animals.Count; i++)
            {
                Console.WriteLine($"{i} {animals[i]}");
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        public static void ListAnimalsNoReadKey()
        {
            for (int i = 0; i < animals.Count; i++)
            {
                Console.WriteLine($"{i} {animals[i]}");
            }
            Console.WriteLine();
        }
        #endregion
    }
}

      
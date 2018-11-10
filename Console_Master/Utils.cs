using Console_Master.Animals;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Console_Master
{
    public static class Utils
    {
        public static XmlSerializer xml = new XmlSerializer(typeof(AnimalCategories));

        public static string GetInput(string message = null, bool clear = true)
        {
            Console.Write(message);
            string inp = Console.ReadLine().Trim();
            if (clear)
            {
                Console.Clear();
            }
            return inp;
        }

        public static void Save()
        {
            if (!Directory.Exists(Setup.folderPath))
            {
                Directory.CreateDirectory(Setup.folderPath);
            }

            using (FileStream fs = new FileStream(Setup.fileRemoved, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.SetLength(0);
                xml.Serialize(fs, Setup.deletedItems);
            }

            using (FileStream fs = new FileStream(Setup.fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.SetLength(0);
                xml.Serialize(fs, Setup.animalCategories);
            }
        }

        public static void Load()
        {
            if (!File.Exists(Setup.fileName) || !File.Exists(Setup.fileRemoved))
            {
                Save();
            }

            using (FileStream fs = new FileStream(Setup.fileRemoved, FileMode.OpenOrCreate, FileAccess.Read))
            {
                Setup.deletedItems = (AnimalCategories)xml.Deserialize(fs);
            }
            using (FileStream fs = new FileStream(Setup.fileName, FileMode.OpenOrCreate, FileAccess.Read))
            {
                Setup.animalCategories = (AnimalCategories)xml.Deserialize(fs);
            }
        }

        public static bool Equals(string s1, string s2, bool caseSensitive = false)
        {
            if (caseSensitive)
            {
                return string.Equals(s1, s2, StringComparison.Ordinal);
            }
            return string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
        }

        public static bool InRange(int i, int j, int k = 0)
        {
            return k <= i && i < j;
        }
    }
}

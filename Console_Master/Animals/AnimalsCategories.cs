using System.Collections.Generic;
using System.Xml.Serialization;

namespace Console_Master.Animals
{
    [XmlRoot("AnimalsCategory")]
    public class AnimalCategories
    {
        [XmlArray("Animals")]
        public List<Animals> Animals { get; set; }

        [XmlArray("Categories")]
        public List<Categories> Categories { get; set; }
    }

}

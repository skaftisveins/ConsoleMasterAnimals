using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Console_Master.Animals
{
    [XmlRoot("Categorie")]
    public class Categories
    {
        [XmlAttribute]
        public Guid ID { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlArrayItem]
        public List<Guid> Animals { get; set; }
    }
}

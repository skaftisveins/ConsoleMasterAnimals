using System;
using System.Xml.Serialization;

namespace Console_Master.Animals
{
    [XmlRoot("Animal")]
    public class Animals
    {
        [XmlAttribute]
        public Guid ID { get; set; }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Type { get; set; }
        [XmlAttribute]
        public string Breed { get; set; }
        [XmlAttribute]
        public DateTime Birthdate { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} | Type: {Type} | Breed: {Breed} | Birthdate: {Birthdate.ToString("dd/MM/yyyy")}";
        }

        public Animals Copy()
        {
            return (Animals)this.MemberwiseClone();
        }
    }

}

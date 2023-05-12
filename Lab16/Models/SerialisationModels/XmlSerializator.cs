using System.Text;
using System.Xml.Serialization;
using Lab12;
using UtilityLibraries;

namespace Lab16.Models.SerializationModels;

public class XmlSerializator : ICollectionSerializator
{
    XmlSerializer xmlSerializer { get; set; }= new XmlSerializer(typeof(MyLinkedList<Person>));
    public void SerializeCollection(IEnumerable<Person> persons, string path)
    {
        using (var sw = new StreamWriter(path, false, Encoding.Unicode))
        {
            xmlSerializer.Serialize(sw, persons);
        }
    }
    public IEnumerable<Person> DeserializeCollection(string path)
    {
        IEnumerable<Person> Persons;
        using(var sr = new StreamReader(path))
        {
           Persons = (xmlSerializer.Deserialize(sr) as MyLinkedList<Person>) ?? 
                        new MyLinkedList<Person>().AsEnumerable<Person>();
        }
        return Persons;
    }
}
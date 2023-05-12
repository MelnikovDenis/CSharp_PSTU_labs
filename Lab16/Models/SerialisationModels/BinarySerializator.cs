using System.Runtime.Serialization.Formatters.Binary;
using Lab12;
using UtilityLibraries;

namespace Lab16.Models.SerializationModels;
#pragma warning disable SYSLIB0011
public class BinarySerializator : ICollectionSerializator
{
      public BinaryFormatter formatter { get; set; } = new BinaryFormatter();
      public void SerializeCollection(IEnumerable<Person> persons, string path)
      {
            using (var fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                  formatter.Serialize(fs, persons);                
            }
      }
      public IEnumerable<Person> DeserializeCollection(string path)
      {
            IEnumerable<Person> Persons;
            using(var fs = new FileStream(path, FileMode.Open))
            {
                  
                  Persons = (MyLinkedList<Person>)formatter.Deserialize(fs);
            }
            return Persons;
      }
}
#pragma warning restore SYSLIB0011
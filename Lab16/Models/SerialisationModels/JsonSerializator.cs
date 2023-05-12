using System.Text;
using Newtonsoft.Json;
using Lab12;
using UtilityLibraries;

namespace Lab16.Models.SerializationModels;

public class JsonSerializator : ICollectionSerializator
{
    JsonSerializerSettings settings { get; set; }= new JsonSerializerSettings(){
        TypeNameHandling = TypeNameHandling.All,
        Formatting = Formatting.Indented

    };
    public void SerializeCollection(IEnumerable<Person> persons, string path)
    {
        using (var sw = new StreamWriter(path, false, Encoding.Unicode))
        {
            sw.WriteLine(JsonConvert.SerializeObject(persons, settings));
        }
    }
    public IEnumerable<Person> DeserializeCollection(string path)
    {
        IEnumerable<Person> Persons;
        using(var sr = new StreamReader(path))
        {
            Persons = JsonConvert.DeserializeObject<MyLinkedList<Person>>(sr.ReadToEnd(), settings).AsEnumerable<Person>();
        }
        return Persons;
    }
}
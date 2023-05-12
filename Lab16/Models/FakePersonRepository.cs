using Lab16.Models.SerializationModels;
using UtilityLibraries;
using Lab12;
namespace Lab16.Models;
//Псевдо БД, которая сохраняет данные в json
public class FakePersonRepository : IPersonRepository
{
    ICollectionSerializator Serializator {get; set;} = new JsonSerializator();
    string Path { get; set; } = "wwwroot/files/persons.json";
    private MyLinkedList<Person> persons = new MyLinkedList<Person>();
    public IEnumerable<Person> Persons 
    { 
        get
        {
            return persons.AsEnumerable<Person>();
        }
        set
        {
            persons = new MyLinkedList<Person>(value);
        }
    }
    
    public FakePersonRepository()
    {
        Persons = Serializator.DeserializeCollection(Path);
    }
    public void Add(Person newPerson)
    {
        persons.Add(newPerson);
        Serializator.SerializeCollection(Persons, Path);
    }
    public void Remove(Person toRemove)
    {
        persons.Remove(toRemove);
        Serializator.SerializeCollection(Persons, Path);
    }
}
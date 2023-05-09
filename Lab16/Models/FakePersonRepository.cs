using Lab16.Models.SerializationModels;
using UtilityLibraries;
using Lab12;
namespace Lab16.Models;
//Псевдо БДы, которая сохраняет данные в json
public class FakePersonRepository : IPersonRepository
{
    ICollectionSerializator Serializator {get; set;} = new JsonSerializator();
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
        Persons = Serializator.DeserializeCollection("wwwroot/files/persons.json");
    }
    public void Add(Person newPerson)
    {
        persons.Add(newPerson);
        Serializator.SerializeCollection(Persons, "wwwroot/files/persons.json");
    }
    public void Remove(Person toRemove)
    {
        persons.Remove(toRemove);
        Serializator.SerializeCollection(Persons, "wwwroot/files/persons.json");
    }
}
using Lab16.Models.SerializationModels;
using UtilityLibraries;
using Lab12;

namespace Lab16.Models;
//Псевдо БД, которая сохраняет данные в json
public class FakePersonRepository : IPersonRepository
{
    public ISerializator Serializator {get; set;} = new BinarySerializator();
    public string Path { get {
            return "wwwroot/files/persons." + Serializator.FileType;
        }
    }
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
            SerializeToFile();
        }
    }
    
    public FakePersonRepository()
    {
        DeserializeFromFile();
    }
    public void Add(Person newPerson)
    {
        persons.Add(newPerson);
        SerializeToFile();
    }
    public void Remove(Person toRemove)
    {
        persons.Remove(toRemove);
        SerializeToFile();
    }
    //Десериализация из файла
    private void DeserializeFromFile()
    {        
        using(var fs = new FileStream(Path, FileMode.Open, FileAccess.Read))
        {
            this.persons = Serializator.Deserialize<MyLinkedList<Person>>(fs);
        }
    }
    //Сериализация в файл
    private void SerializeToFile()
    {
        using(var fs = new FileStream(Path, FileMode.Create))
        {
            var ms = Serializator.Serialize<MyLinkedList<Person>>(persons);
            ms.WriteTo(fs);
        }
    }
}
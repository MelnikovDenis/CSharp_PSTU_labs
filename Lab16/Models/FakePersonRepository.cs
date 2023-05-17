using Lab16.Models.SerializationModels;
using UtilityLibraries;
using Lab13;

namespace Lab16.Models;
//Псевдо БД, которая сохраняет данные в json
public class FakePersonRepository : IPersonRepository
{
    public ISerializator Serializator {get; set;} = new JsonSerializator();
    public string Path { get {
            return "wwwroot/files/persons." + Serializator.FileType;
        }
    }
    private MyExtendedLinkedList<Person> persons = new MyExtendedLinkedList<Person>();
    public IEnumerable<Person> Persons 
    { 
        get
        {
            return persons.AsEnumerable<Person>();
        }
        set
        {
            persons = new MyExtendedLinkedList<Person>(value);
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
    public void UpdateByIndex(Person newPerson, int index)
    {
        if(persons.Count() > 0)
        {
            if(index >= 0 && index < persons.Count())
            {
                persons[index] = newPerson;
                SerializeToFile();
            }
            else
            {
                throw new IndexOutOfRangeException($"Индекс выходит за границы списка, допустимые значения находятся в диапозоне [0, {persons.Count()})");
            }
        }
        else
        {
            throw new ArgumentException("Невозможно обновить элемент в пустом списке");
        }
    }
    //Десериализация из файла
    private void DeserializeFromFile()
    {        
        using(var fs = new FileStream(Path, FileMode.Open, FileAccess.Read))
        {
            this.persons = Serializator.Deserialize<MyExtendedLinkedList<Person>>(fs);
        }
    }
    //Сериализация в файл
    private void SerializeToFile()
    {
        using(var fs = new FileStream(Path, FileMode.Create))
        {
            var ms = Serializator.Serialize<MyExtendedLinkedList<Person>>(persons);
            ms.WriteTo(fs);
        }
    }
}
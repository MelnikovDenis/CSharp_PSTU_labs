using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Lab12;
using UtilityLibraries;

namespace Lab16.Models.SerializationModels;

public class JsonSerializator : ICollectionSerializator
{
    public void SerializeCollection(IEnumerable<Person> persons, string path)
    {
        using (var sw = new StreamWriter(path, false, Encoding.Unicode))
        {
            sw.WriteLine(JsonConvert.SerializeObject(persons, Formatting.Indented));
        }
    }
    public IEnumerable<Person> DeserializeCollection(string path)
    {
        MyLinkedList<Person> Persons = new MyLinkedList<Person>();
        string json;
        using(var sr = new StreamReader(path))
        {
            json = sr.ReadToEnd();
        }
        dynamic jarray = JArray.Parse(json);
        foreach(var item in jarray)
        {
            if(item.department is not null)
            {
                Persons.Add(new Administrator((string?)item.first_name, (string?)item.surname, 
                    (string?)item.patronymic, (int)item.salary, (int)item.work_length, (string?)item.department));
            }
            else if(item.speciality is not null)
            {
                Persons.Add(new Engineer((string?)item.first_name, (string?)item.surname, (string?)item.patronymic, 
                    (int)item.salary, (int)item.work_length, (string?)item.speciality));
            }
            else if(item.salary is not null && item.work_length is not null)
            {
                Persons.Add(new Employee((string?)item.first_name, (string?)item.surname, (string?)item.patronymic, 
                    (int)item.salary, (int)item.work_length));
            }
            else
            {
                Persons.Add(new Person((string?)item.first_name, (string?)item.surname, (string?)item.patronymic));
            }            
        }
        return Persons.AsEnumerable<Person>();
    }
}
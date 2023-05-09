using Newtonsoft.Json.Linq;
using UtilityLibraries;
using Lab12;
namespace Lab16.Models;
public class FakePersonRepository : IPersonRepository
{
    public MyLinkedList<Person> Persons { get; set;} = new MyLinkedList<Person>();
    
    public FakePersonRepository()
    {
        string json;
        using(var sr = new StreamReader("wwwroot/files/persons.json"))
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
    }
    public void Add(Person newPerson)
    {
        Persons.Add(newPerson);
    }
    public void Remove(Person toRemove)
    {
        Persons.Remove(toRemove);
    }
}
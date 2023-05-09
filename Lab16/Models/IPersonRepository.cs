using UtilityLibraries;
using Lab12;
namespace Lab16.Models;

public interface IPersonRepository
{
      IEnumerable<Person> Persons {get;}
      void Add(Person newPerson);
      void Remove(Person toRemove);
}
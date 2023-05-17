using UtilityLibraries;
namespace Lab16.Models;

public interface IPersonRepository
{
      IEnumerable<Person> Persons { get; set; }
      void Add(Person newPerson);
      void Remove(Person toRemove);
      void UpdateByIndex(Person newPerson, int index);
}
using UtilityLibraries;
namespace Lab16.Models.SerializationModels;

public interface ICollectionSerializator
{
    public void SerializeCollection(IEnumerable<Person> persons, string path);
    public IEnumerable<Person> DeserializeCollection(string path);
}
using UtilityLibraries;
using Lab14;
using Lab13;
using static UtilityLibraries.Person; 
using static UtilityLibraries.Employee;
using static UtilityLibraries.Administrator;
using static UtilityLibraries.Engineer;
namespace Lab14Test;

[TestClass]
public class MyEventListExtensionTest
{
    private static MyEventLinkedList<Person> persons = GenerateEventList();
    [TestMethod]
    public void GetMatchesTest()
    {
        var matches = persons.GetMatches((Person p) => p.first_name == "1");
        Assert.AreEqual<int>(3, matches.Count());
        foreach(var item in matches)
        {
            Assert.AreEqual<string>("1", item!.first_name);
        }
    }
    [TestMethod]
    public void GetSalarySumTest()
    {
        var salarySum = persons.GetSalarySum();
        Assert.AreEqual<int>(33, salarySum);
    }
    [TestMethod]
    public void DescOrderByFullNameTest()
    {
        var sorted = persons.DescOrderByFullName();
        var sorted1 = new MyEventLinkedList<Person>(new Person[]{
            new Engineer("4", "7", "8", 12, 5, Engineer.specialties[1]),
            new Engineer("3", "6", "9", 11, 4, Engineer.specialties[0]),
            new Engineer("1", "5", "10", 10, 3, Engineer.specialties[0]),            
            new Person("3", "4", "5"),
            new Person("1", "2", "3"),
            new Person("1", "1", "1")           
        }, "Список");
        foreach(var tuple in sorted1.Zip(sorted, Tuple.Create))
        {
            Assert.AreEqual<Person>(tuple.Item1, tuple.Item2);
        }
    }
    private static MyEventLinkedList<Person> GenerateEventList()
    {
        var persons = new MyEventLinkedList<Person>("Список");
        persons.Add(new Person("1", "1", "1"));
        persons.Add(new Person("1", "2", "3"));
        persons.Add(new Person("3", "4", "5"));
        persons.Add(new Engineer("1", "5", "10", 10, 3, Engineer.specialties[0]));
        persons.Add(new Engineer("3", "6", "9", 11, 4, Engineer.specialties[0]));
        persons.Add(new Engineer("4", "7", "8", 12, 5, Engineer.specialties[1]));
        return persons;
    }
}
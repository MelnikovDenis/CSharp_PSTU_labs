using UtilityLibraries;
using Lab14;
using static UtilityLibraries.Person; 
using static UtilityLibraries.Employee;
using static UtilityLibraries.Administrator;
using static UtilityLibraries.Engineer;
namespace Lab14Test;

[TestClass]
public class DictStackExtensionTest
{
    public static Dictionary<string, Stack<Person>> company = GetCompany();
    [TestMethod]
    public void GetMatchesTest()
    {
        var matches = company.GetMatches((Person p) => p.first_name == "1");
        Assert.AreEqual<int>(3, matches.Count());
        foreach(var item in matches)
        {
            Assert.AreEqual<string>("1", item!.first_name);
        }
    }
    [TestMethod]
    public void GetGroupBySpecialityTest()
    {
        var groups = company.GetGroupBySpeciality();
        var specialties = groups.Select((IGrouping<string, Person> group) => group.Key);
        var persons = groups.SelectMany(group => group);
        Assert.AreEqual<int>(2, specialties.Count());        
        Assert.AreEqual<int>(3, persons.Count());        
    }
    [TestMethod]
    public void GetAverageSalaryTest()
    {
        var avSalary = company.GetAverageOfSalary();
        Assert.AreEqual<double>(11d, avSalary);
    }
    [TestMethod]
    public void GetAverageWorkLengthTest()
    {
        var avWorkLength = company.GetAverageOfWorkLength();
        Assert.AreEqual<double>(4d, avWorkLength);
    }
    public static Dictionary<string, Stack<Person>>GetCompany()
    {
        var company = new Dictionary<string, Stack<Person>>();
        company.Add(Administrator.departments[0], new Stack<Person>());
            company[Administrator.departments[0]].Push(new Person("1", "1", "1"));
            company[Administrator.departments[0]].Push(new Person("1", "2", "3"));
            company[Administrator.departments[0]].Push(new Person("3", "4", "5"));
        company.Add(Administrator.departments[1], new Stack<Person>());
            company[Administrator.departments[1]].Push(new Engineer("1", "5", "10", 10, 3, Engineer.specialties[0]));
            company[Administrator.departments[1]].Push(new Engineer("3", "6", "9", 11, 4, Engineer.specialties[0]));
            company[Administrator.departments[1]].Push(new Engineer("4", "7", "8", 12, 5, Engineer.specialties[1]));
        return company;
    }
}
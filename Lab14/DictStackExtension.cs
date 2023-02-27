using UtilityLibraries;
namespace Lab14;
//методы расширения для Dictionary<string, Stack<Person>>
public static class DictStackExtension
{
    //получить все объекты соответствующие определённому условию и отсортированные по ФИО
    public static IEnumerable<Person> GetMatches(this Dictionary<string, Stack<Person>> company, Predicate<Person> Condition)
    {
        var result = from department in company
            from person in department.Value
            where Condition(person)
            orderby (person.surname + person.first_name + person.patronymic) 
            select person;
        return result;
    }
    //группировка по специальности инженеров
    public static IEnumerable<IGrouping<string, Person>> GetGroupBySpeciality(this Dictionary<string, Stack<Person>> company)
    {
        var result = from department in company
            from person in department.Value
            where person is Engineer
            orderby (person.patronymic + person.first_name + person.surname) 
            group person by ((Engineer)person).speciality;
        return result;
    }
    //получить размер средней зарплаты
    public static double GetAverageOfSalary(this Dictionary<string, Stack<Person>> company)
    {
        var result = from department in company
            from person in department.Value
            where person is Employee
            select ((Employee)person).salary;
        return result.Average();
    }    
    //получить размер средней зарплаты
    public static double GetAverageOfWorkLength(this Dictionary<string, Stack<Person>> company)
    {
        var result = from department in company
            from person in department.Value
            where person is Employee
            select ((Employee)person).work_length;
        return result.Average();
    }
}
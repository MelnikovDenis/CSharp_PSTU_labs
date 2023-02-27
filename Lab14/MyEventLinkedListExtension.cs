using UtilityLibraries;
using Lab13;
namespace Lab14;
//методы расширения для MyEventLinkedList<T>
public static class MyEventLinkedListExtension
{
    //получить все объекты соответствующие определённому условию и отсортированные по ФИО
    public static IEnumerable<Person> GetMatches(this MyEventLinkedList<Person> persons, Predicate<Person> Condition)
    {
        var result = from person in persons
            where Condition(person)
            orderby (person.surname + person.first_name + person.patronymic) 
            select person;
        return result;
    }
    //получить сумму всех зарплат
    public static int GetSalarySum(this MyEventLinkedList<Person> persons)
    {
        var result = from person in persons
            where person is Employee
            select ((Employee)person).salary;
        return result.Aggregate<int>((int a, int b) => a + b);
    }
    //отсортировать по ФИО в обратном порядке
    public static IEnumerable<Person> DescOrderByFullName(this MyEventLinkedList<Person> persons)
    {
        var result = from person in persons
            orderby (person.surname + person.first_name + person.patronymic) descending
            select person.BasePerson;
        return result;
    }
}
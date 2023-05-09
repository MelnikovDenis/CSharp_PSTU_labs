using Newtonsoft.Json;
namespace UtilityLibraries;
[Serializable]
public class Employee : Person
{
    [JsonIgnore]
    public const int MIN_SALARY = 10000; //включительно
    [JsonIgnore]
    public const int MAX_SALARY = 100000; //не включительно
    [JsonIgnore]
    public const int MIN_WORK_LENGTH = 0; //включительно
    [JsonIgnore]
    public const int MAX_WORK_LENGTH = 70; //не включительно
    public int salary { get; protected set; } = 0;
    public int work_length { get; protected set; } = 0;
    public Employee() : base()
    {

    }
    public Employee(Random rnd) : base(rnd)
    {
        this.RandomInit(rnd);
    }
    public Employee(string? first_name, string? surname, string? patronymic, int salary, int work_length) : base(first_name, surname, patronymic)
    {
        this.salary = salary;
        this.work_length = work_length;
    }
    public override string ToString()
    {
        return $"Employee:\t\t{surname ?? "NULL"} {first_name ?? "NULL"} {patronymic ?? "NULL"}\tЗарплата: {salary}\tСтаж: {work_length}";
    }
    public new void RandomInit(Random rnd)
    {
        this.salary = rnd.Next(MIN_SALARY, MAX_SALARY);
        this.work_length = rnd.Next(MIN_WORK_LENGTH, MAX_WORK_LENGTH);
    }
    public override object Clone()
    {
        return new Employee(this.first_name, this.surname, this.patronymic, this.salary, this.work_length);
    }
}
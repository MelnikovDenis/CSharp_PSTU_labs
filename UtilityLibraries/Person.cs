namespace UtilityLibraries;
using System.Collections;
public class Person : IRandomInit, IComparable<Person>, ICloneable
{
    protected static readonly string[] names = { "Денис", "Иван", "Олег", "Василий", "Сергей", "Павел", "Вячеслав", "Виктор", "Михаил", "Анатолий", "Владислав", "Валерий" };
    protected static readonly string[] surnames = { "Мельников", "Мазунин", "Гостев", "Иванов", "Кузнецов", "Глазырин", "Власов", "Омутных", "Воронин", "Соколков" };
    protected static readonly string[] patronymics = { "Денисович", "Иванович", "Олегович", "Васильевич", "Вячеславович", "Михайлович", "Анатольевич", "Валерьевич"};
    public string? first_name { get; protected set; } = null;
    public string? surname { get; protected set; } = null;
    public string? patronymic { get; protected set; } = null;
    //возвращает объект базового класса
    public Person BasePerson
    {
        get
        {
            return new Person(this.first_name, this.surname, this.patronymic);
        }
    }
    public Person(){
        
    }
    public Person(Random rnd)
    {
        this.RandomInit(rnd);
    }
    public Person(string? first_name, string? surname, string? patronymic)
    {
        this.first_name = first_name;
        this.surname = surname;
        this.patronymic = patronymic;
    }
    public static Person InputPerson()
    {
        string? name, surname, patronymic;            
        Console.Write("Введите фамилию: ");
        surname = Console.ReadLine();
        Console.Write("Введите имя: ");
        name = Console.ReadLine();
        Console.Write("Введите отчество: ");
        patronymic = Console.ReadLine();
        return new Person(name, surname, patronymic);
    }
    public override string ToString()
    {
        return $"Person:\t\t{surname ?? "NULL"} {first_name ?? "NULL"} {patronymic ?? "NULL"}";
    }
    public virtual void RandomInit(Random rnd)
    {
        this.first_name = names[rnd.Next(0, names.Length - 1)];
        this.surname = surnames[rnd.Next(0, surnames.Length - 1)];
        this.patronymic = patronymics[rnd.Next(0, patronymics.Length - 1)];
    } 
    public int CompareTo(Person? p)
    {
        if(p is not null)
            return string.Compare(this.surname + this.first_name + this.patronymic, p.surname + p.first_name + p.patronymic);
        else return -1;       
    }
    public virtual object Clone()
    {
        return new Person(this.first_name, this.surname, this.patronymic);
    }
    public override bool Equals(object? obj)
    {
        if(obj is not null)
            return (obj is Person && ((Person)obj).first_name == this.first_name && 
                ((Person)obj).surname == this.surname && ((Person)obj).patronymic == this.patronymic);
        else return false;
    }
    public override int GetHashCode(){
        return (surname + first_name + patronymic).GetHashCode();
    }
}


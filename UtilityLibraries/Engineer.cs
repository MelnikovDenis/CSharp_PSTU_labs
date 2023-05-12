using Newtonsoft.Json;
using System.Xml.Serialization;
namespace UtilityLibraries;
[Serializable]
[XmlInclude(typeof(Administrator))]
public class Engineer : Employee
{
    [JsonIgnore]
    [NonSerialized]
    public static readonly string[] specialties = { "Инженер-судостроитель", "Инженер-электрик", "Инженер-нефтяник", 
        "Инженер-технолог", "Инженер-конструктор", "Инженер-аналитик", "Программный инженер" };
    [JsonProperty("speciality")]
    public string? speciality { get; set; } = null;
    public Engineer() : base(){

    }
    public Engineer(Random rnd) : base(rnd)
    {
        this.RandomInit(rnd);
    }
    public Engineer(string? first_name, string? surname, string? patronymic, int salary, int work_length, string? speciality) : base(first_name, surname, patronymic, salary, work_length)
    {
        this.speciality = speciality;
    }
    public new void RandomInit(Random rnd)
    {
        this.speciality = specialties[rnd.Next(0, specialties.Length - 1)];
    }
    public override string ToString()
    {
        return $"Engineer:\t\t{surname ?? "NULL"} {first_name ?? "NULL"} {patronymic ?? "NULL"}\tЗарплата: {salary}\tСтаж: {work_length}\tСпециальность: {speciality ?? "NULL"}";
    }
    public override object Clone()
    {
        return new Engineer(this.first_name, this.surname, this.patronymic, this.salary, this.work_length, this.speciality);
    }
}
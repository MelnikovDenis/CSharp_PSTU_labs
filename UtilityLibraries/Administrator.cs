namespace UtilityLibraries;
public class Administrator : Employee
{
    public static readonly string[] departments = { "Отдел продаж", "Отдел техподдержки", "Отдел маркетинга", 
        "Информационный отдел", "Отдел связей с общественностью"};
    public string? department { get; internal set; } = null;
    public Administrator() : base(){
        
    }
    public Administrator(Random rnd) : base(rnd)
    {
        this.RandomInit(rnd);
    }
    public Administrator(string? first_name, string? surname, string? patronymic, int salary, int work_length, string? department) : base(first_name, surname, patronymic, salary, work_length)
    {
        this.department = department;
    }
    public new void RandomInit(Random rnd)
    {
        this.department = departments[rnd.Next(0, departments.Length - 1)];
    }
    public override string ToString()
    {
        return $"Administrator:\t{surname ?? "NULL"} {first_name ?? "NULL"} {patronymic ?? "NULL"}\tЗарплата: {salary}\tСтаж: {work_length}\tОтдел: {department ?? "NULL"}";
    }
    public override object Clone()
    {
        return new Administrator(this.first_name, this.surname, this.patronymic, this.salary, this.work_length, this.department);
    }
}


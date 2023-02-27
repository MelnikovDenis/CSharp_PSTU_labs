using UtilityLibraries;
using static UtilityLibraries.ConsoleIOLibrary;
using static UtilityLibraries.CollectionLibrary;
using static UtilityLibraries.PersonLibrary;
using Lab13;
namespace Lab14;
internal static class UserInterface
{
    internal static int MIN_LENGTH {get; set;} = 10;  //минимальная длина генерируемых коллекций (включительно)
    internal static int MAX_LENGTH {get; set;} = 20;  //максимальная длина генерируемых коллекций(не включительно)
    private static MyEventLinkedList<Person> Persons {get; set;} = ListGenerator.GenerateMyEventLinkedList("Список");
    private static Dictionary<string, Stack<Person>>? companyDict = null;
    private static Dictionary<string, Stack<Person>>? СompanyDict {
        get
        {
            return companyDict;
        }
        set
        {
            companyDict = value;
            AverageSalary = companyDict?.GetAverageOfSalary();
            AverageWorkLength = companyDict?.GetAverageOfWorkLength();
        } 
    } 
    private static double? AverageSalary {get; set;} = null; //средняя зарплата в компании
    private static double? AverageWorkLength {get; set;} = null; //средняя продолжительность работы в компании
    //главный метод приложения
    internal static void Execute(){
        СompanyDict = GenerateCompany();
        bool needExit = false;
        while (!needExit)
        {                
            ColorDisplay("Номера команд:\n1. Сгенерировать и вывести новую компанию\n2. Получить всех инженеров с определённой специальностью и их кол-во (выборка и счётчик)\n3. Получить среднюю зарплату и средний стаж (агрегирование)" +
            "\n4. Получить всех работников, у которых зарплата > средней, а стаж < среднего (операции над множествами)\n5. Получить всех инженеров сгруппированных по специальности (группировка)\n6. Вывести сумму всех зарплат в связном списке" +
            "\n7. Вывести все ФИО в связном списке отсортированными в обратном порядке\n8. Получить всех людей с фамилией начинаюейся на 'М' в связном списке\n9. Сгенерировать список заново\n0. Выход\n", ConsoleColor.Green);
            ColorDisplay("Исходная компания:\n", ConsoleColor.Magenta);
            DisplayCompany(СompanyDict);
            int command = GetInt("Введите номер команды: ", "Несуществующая команда, повторите ввод =>\n", (int num) => num >= 0 && num <= 9);                
            switch (command)
            {
                case 1:
                    СompanyDict = GenerateCompany();
                    ColorDisplay("Новая компания:\n", ConsoleColor.Magenta);
                    DisplayCompany(СompanyDict);                    
                    break;    
                case 2:
                    string speciality = InputSpeciality();
                    var queryResult = СompanyDict.GetMatches((Person p) => (p as Engineer)?.speciality == speciality);
                    Display(queryResult);
                    ColorDisplay($"Количество инженеров с выбранной специальностью: {queryResult.Count()}\n", ConsoleColor.Magenta);
                    break;
                case 3:
                    ColorDisplay($"Средняя зарплата: {AverageSalary:C2}\n", ConsoleColor.Magenta);
                    ColorDisplay($"Средний стаж: {AverageWorkLength:F1}\n", ConsoleColor.Magenta);
                    break;
                case 4:
                    ColorDisplay("Люди у которых зарплата > средней, а стаж < среднего:\n", ConsoleColor.Magenta);
                    Display(СompanyDict.GetMatches((Person p) => (p as Employee)?.salary > AverageSalary)
                        .Intersect(СompanyDict.GetMatches((Person p) => (p as Employee)?.work_length < AverageWorkLength)));
                    break;
                case 5:
                    DisplayGroups(СompanyDict.GetGroupBySpeciality());
                    break;
                case 6:
                    Display(Persons, "Исходный список:\n");
                    ColorDisplay($"Сумма зарплат в связном списке: {Persons.GetSalarySum():C2}\n", ConsoleColor.Magenta);
                    break;
                case 7:
                    Display(Persons, "Исходный список:\n");
                    Display(Persons.DescOrderByFullName(), "ФИО в обратном порядке:\n");
                    break;
                case 8:
                    Display(Persons, "Исходный список:\n");
                    Display(Persons.GetMatches((Person p) => 'М'.Equals(p?.surname?[0])), "Все люди с фамилией на 'М':\n");
                    break;
                case 9:
                    Display(Persons, "Исходный список:\n");
                   ListGenerator.RegenerateMyEventLinkedList(Persons);
                    Display(Persons, "Новый список:\n");
                    break;    
                case 0:
                    needExit = true;                        
                    break;
            }
            ColorDisplay("Для продолжения нажмите любую клавишу...", ConsoleColor.Yellow);
            Console.ReadKey();
            Console.Clear();
        }            
    }
    //вывести компанию
    private static void DisplayCompany(Dictionary<string, Stack<Person>> companyDict)
    {
        foreach(var keyValuePair in companyDict)
        {
            ColorDisplay($"{keyValuePair.Key}:\n", ConsoleColor.Magenta);
            Display(keyValuePair.Value);
        }
    }
    //вывести сгруппированные данные (результат groupby)
    private static void DisplayGroups(IEnumerable<IGrouping<string, Person>> groups)
    {
        foreach(var group in groups)
        {
            ColorDisplay($"{group.Key}:\n", ConsoleColor.Magenta);
            int counter = 0;
            foreach(var person in group)
            {                
                Console.WriteLine(counter++.ToString() + ". " + person.ToString());
            }
        }
    }
    //генерация компании
    private static Dictionary<string, Stack<Person>> GenerateCompany()
    {
        var companyDict = new Dictionary<string, Stack<Person>>(MAX_LENGTH * 2);
        for(int i = 0; i < Administrator.departments.Count(); ++i)
        {
            int departmentLen = rnd.Next(MIN_LENGTH, MAX_LENGTH);
            var departmentStack = new Stack<Person>(MAX_LENGTH * 2);
            for(int j = 0; j < departmentLen; ++j)
            {
                departmentStack.Push(GeneratePersonWithDepartment(Administrator.departments[i]));
            }
            companyDict.Add(Administrator.departments[i], departmentStack);
        }
        return companyDict;
    }
    
}
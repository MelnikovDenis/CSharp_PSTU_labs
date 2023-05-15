using UtilityLibraries;
using static UtilityLibraries.ConsoleIOLibrary;
using static UtilityLibraries.CollectionLibrary;
using static UtilityLibraries.PersonLibrary;
namespace Lab12;
public static class UserInterface
{
    internal static int MIN_LENGTH = 5;  //минимальная длина генерируемого списка (включительно)
    internal static int MAX_LENGTH = 10;  //максимальная длина генерируемого списка (не включительно
    
    //основной метод интерфейса взаимойдействия с пользователем
    internal static void Execute(){
        bool needExit = false;
        var myLinkedList = GenerateMyLinkedList();
        while (!needExit)
        {                
            ColorDisplay("Номера команд:\n1. Сгенерировать и вывести новый связный список\n2. Добавить элемент\n3. Удалить элемент" +
            "\n4. Очистить список\n5. Проверить наличие элемента в списке\n6. Добавить элементы на все нечётные позиции" +
            "\n7. Поверхностное копирование с помощью CopyTo()\n8. Глубокое копирование с помощью ICloneable\n0. Выход\n", ConsoleColor.Green);
            ColorDisplay("Исходный список:\n", ConsoleColor.Magenta);
            Display(myLinkedList);
            int command = GetInt("Введите номер команды: ", "Несуществующая команда, повторите ввод =>\n", (int num) => num >= 0 && num <= 8);                
            Console.Clear();
            ColorDisplay("Исходный список:\n", ConsoleColor.Magenta);
            Display(myLinkedList);
            switch (command)
            {
                case 1:
                    myLinkedList = GenerateMyLinkedList();
                    ColorDisplay("Новый связный список:\n", ConsoleColor.Magenta);
                    Display(myLinkedList);
                    break;
                case 2:                        
                    myLinkedList.Add(GenerateHuman());
                    ColorDisplay("Список после добавления нового элемента:\n", ConsoleColor.Magenta);
                    Display(myLinkedList);
                    break;
                case 3:
                    var toRemove = Person.InputPerson();
                    Console.WriteLine($"Объект для удаления: {toRemove}");
                    Console.WriteLine("Введённый элемент удалён: " + myLinkedList.Remove(toRemove));
                    ColorDisplay("Список после удаления элемента:\n", ConsoleColor.Magenta);
                    Display(myLinkedList);
                    break;
                case 4:
                    myLinkedList.Clear();
                    ColorDisplay("Текущий список:\n", ConsoleColor.Magenta);
                    Display(myLinkedList);
                    break;
                case 5:
                    Console.WriteLine("Список содержит введённый элемент: " + myLinkedList.Contains(Person.InputPerson()));
                    break;
                case 6:
                    AddOnOdd(myLinkedList);
                    ColorDisplay("Текущий список:\n", ConsoleColor.Magenta);
                    Display(myLinkedList);
                    break;
                case 7:
                    Person[] array = new Person[myLinkedList.Count];
                    myLinkedList.CopyTo(array, 0);
                    ColorDisplay("Поверхнстная копия (массив):\n", ConsoleColor.Magenta);
                    if(array.Length > 0){
                        int counter = 0;
                        foreach(var person in array){
                            Console.WriteLine(counter++.ToString() + ". " + person);
                        }       
                    }
                    else{
                        ColorDisplay("Массив пуст!\n", ConsoleColor.Red);
                    }
                    break;
                case 8:
                    var clone = (MyLinkedList<Person>)myLinkedList.Clone();
                    ColorDisplay("Глубокая копия:\n", ConsoleColor.Magenta);
                    Display(clone);
                    ColorDisplay("Исходный список после очищения:\n", ConsoleColor.Magenta);
                    myLinkedList.Clear();
                    Display(myLinkedList);
                    ColorDisplay("Глубокая копия:\n", ConsoleColor.Magenta);
                    Display(clone);
                    myLinkedList = (MyLinkedList<Person>)clone.Clone();
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
    //добавление новых элементов на все нечётные позиции
    private static void AddOnOdd(MyLinkedList<Person> myLinkedList){
        for(int i = 1; i < myLinkedList.Count; i += 2){
            myLinkedList.AddTo(GenerateHuman(), i);
        }
    } 
    //генерация нового связного списка
    public static MyLinkedList<Person> GenerateMyLinkedList(){
        int len = rnd.Next(MIN_LENGTH, MAX_LENGTH);
        var myLinkedList = new MyLinkedList<Person>(); 
        for(int i = 0; i < len; ++i){
            myLinkedList.Add(GenerateHuman());
        }
        return myLinkedList;
    }        
}


namespace UtilityLibraries;
public static class PersonLibrary{
      public static Random rnd = new Random(); //единая переменная для ГСЧ (инициализируется 1 раз (это важно))
      //генерация человека
      public static Person GenerateHuman(){
            switch (rnd.Next(0, 4))
            {
                  case 0:
                        return new Person(rnd);
                  case 1:
                        return new Employee(rnd);
                  case 2:
                        return new Engineer(rnd);
                  case 3:
                        return new Administrator(rnd);
                  default:
                        return new Person(rnd);
            }
      }
      //сгенерировать человека работающего в определённом отделе
      public static Person GeneratePersonWithDepartment(string department)
      {
            switch (rnd.Next(0, 4))
            {
                  case 0:
                        return new Person(rnd);
                  case 1:
                        return new Employee(rnd);
                  case 2:
                        return new Engineer(rnd);
                  case 3:
                        var administrator = new Administrator(rnd);
                        administrator.department = department;
                        return administrator;
                  default:
                        return new Person(rnd);
            }
      }
      //ввод специальности  
      public static string InputSpeciality()
      {
            ConsoleIOLibrary.ColorDisplay("Список специальностей:\n", ConsoleColor.Magenta);
            CollectionLibrary.Display<string>(Engineer.specialties);
            return Engineer.specialties[ConsoleIOLibrary.GetInt("Введите номер специальности: ", 
                  "Такого номера нет в списке, повторите ввод =>\n", 
                  (int num) => num >= 0 && num < Engineer.specialties.Count())];
      }
      //ввод отдела
      public static string InputDepartment()
      {
            ConsoleIOLibrary.ColorDisplay("Список отделов:\n", ConsoleColor.Magenta);
            CollectionLibrary.Display<string>(Administrator.departments);
            return Administrator.departments[ConsoleIOLibrary.GetInt("Введите номер специальности: ", 
                  "Такого номера нет в списке, повторите ввод =>\n", 
                  (int num) => num >= 0 && num < Administrator.departments.Count())];
      }
}
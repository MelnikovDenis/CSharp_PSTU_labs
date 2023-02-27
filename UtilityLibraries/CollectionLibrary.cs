namespace UtilityLibraries;
public static class CollectionLibrary{
      //вывод коллекции
      public static void Display<T>(IEnumerable<T> collection){            
            if(collection.Count() > 0){
                  int counter = 0;
                  foreach(var item in collection){
                        Console.WriteLine(counter++.ToString() + ". " + (item?.ToString() ?? "NULL"));
                  }
            }
            else{
                  ConsoleIOLibrary.ColorDisplay("Коллекция пуста!\n", ConsoleColor.Red);
            }            
      }
      public static void Display<T>(IEnumerable<T> collection, string label){     
            ConsoleIOLibrary.ColorDisplay(label, ConsoleColor.Magenta);
            if(collection.Count() > 0){
                  int counter = 0;
                  foreach(var item in collection){
                        Console.WriteLine(counter++.ToString() + ". " + (item?.ToString() ?? "NULL"));
                  }
            }
            else{
                  ConsoleIOLibrary.ColorDisplay("Коллекция пуста!\n", ConsoleColor.Red);
            }            
      }
}
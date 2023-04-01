using System.Text;
namespace GraphComponentLab;
public static class UserInterface{
      const int MAX_LENGTH = 10; //максимальное кол-во вершин графа
      //главный цикл приложения
      public static void Execute(){
            bool needExit = false;
            (bool, bool) options = SetGraphOptions(); //Item1 - взвешенынй или нет, Item2 - ориентированный или нет
            UnweightedGraph? unwGraph = null;
            WeightedGraph? wGraph = null;
            if(options.Item1)
                  unwGraph = InputUnweightedGraph(options.Item2);
            else
                  wGraph = InputWeightedGraph(options.Item2);
            while (!needExit)
            {
                  Console.Clear();
                  ColorDisplay("1. Найти компоненты сильной связности графа\n2. Построить ярусно-параллельную форму\n3. Ввести новую матрицу\n4. Метод Краскала\n0. Выйти\n", ConsoleColor.Green);
                  ColorDisplay("Матрица смежности графа:\n", ConsoleColor.Magenta);
                  if(unwGraph is not null)
                        Console.Write(unwGraph.ToString());
                  else if(wGraph is not null)
                        Console.Write(wGraph.ToString());
                  int command = GetInt("Введите номер команды: ", "Номер команды должен быть целым числом в диапозоне [0, 4], повторите ввод =>\n",
                        (int num) => num >= 0 && num <= 4);                  
                  switch (command)
                  {
                        case 1:
                              if(unwGraph is not null)
                                    DisplayStronglyConnectedComponent(unwGraph);
                              else if(wGraph is not null)
                                    DisplayStronglyConnectedComponent(wGraph.ToUnweighted());
                              break;
                        case 2:
                              if(unwGraph is not null)
                                    DisplayTieredParallelForm(unwGraph);
                              else if(wGraph is not null)
                                    DisplayTieredParallelForm(wGraph.ToUnweighted());
                              break;
                        case 3:
                              options = SetGraphOptions(); //Item1 - взвешенынй или нет, Item2 - ориентированный или нет
                              if(options.Item1){
                                    unwGraph = InputUnweightedGraph(options.Item2);
                                    wGraph = null;
                              }                                    
                              else{
                                    wGraph = InputWeightedGraph(options.Item2);
                                    unwGraph = null;
                              }                                    
                              break;
                        case 4:
                              if(options.Item1 && !options.Item2){
                                    
                              }
                              else{
                                    ColorDisplay("Остов возможно найти только для ориентированного взвешенного графа\n", ConsoleColor.Red);
                              }
                              break;                        
                        case 0:
                              needExit = true;
                              break;
                  }
                  ColorDisplay("Нажмите любую клавишу для продолжения...", ConsoleColor.Yellow);
                  Console.ReadKey();
            }
      }
      static (bool, bool) SetGraphOptions()
      {
            (bool, bool) options;
            options.Item1 = GetInt($"Вы хотите ввести взвешенный граф ('1' - да, '0' - нет)?: ", 
                                    $"Введите '1' или '0', повторите ввод =>\n",
                                    (int num) => num >= 0 && num <= 1) == 1;
            options.Item2 = GetInt($"Вы хотите ввести ориентированный граф ('1' - да, '0' - нет)?: ", 
                                    $"Введите '1' или '0', повторите ввод =>\n",
                                    (int num) => num >= 0 && num <= 1) == 1;
            return options;
      }
      //вывод ярусно-параллельной формы графа
      static void DisplayTieredParallelForm(UnweightedGraph graph){
            try{
                  var tieredParallelForm = graph.GetTieredParallelForm();
                  ColorDisplay("Параллельно-ярусная форма:\n", ConsoleColor.Magenta);
                  foreach(var tier in tieredParallelForm){                  
                        foreach(var vertex in tier){
                              Console.Write((graph.Names[vertex]).ToString() + " ");
                        }
                        Console.WriteLine();
                  }
            }
            catch(Exception ex){
                  ColorDisplay(ex.Message + '\n', ConsoleColor.Red);
            }
            
      }
      //вывод информации о компонентах сильной связности
      static void DisplayStronglyConnectedComponent(UnweightedGraph graph){
            var strConnComps = graph.GetStronglyСonnectedСomponent();            
            ColorDisplay("Матрица компонентов сильной связности:\n", ConsoleColor.Magenta);
            Console.Write(ToString(graph.GetStronglyConnectedComponentMatrix(), graph.Names));
            ColorDisplay("Компоненты сильной связности графа:\n", ConsoleColor.Magenta);
            foreach(var strConnComp in strConnComps){
                  Console.Write("{ ");
                  foreach(var vertex in strConnComp){
                        Console.Write((graph.Names[vertex]).ToString() + " ");
                  }
                  Console.WriteLine("}");
            }
      }
      //ввод графа
      static UnweightedGraph InputUnweightedGraph(bool isOrientied = false)
      {
            int len = GetInt("Введите кол-во вершин графа: ", 
                  $"Кол-во вершин должно быть в диапозоне [2; {MAX_LENGTH}], повторите ввод =>\n",
                  (int num) => num > 2 && num <= MAX_LENGTH);
            var adjacencyMatrix = new bool[len, len];
            var names = new char[len];
            for (int i = 0; i < len; ++i) {
                  names[i] = GetChar($"Введите имя [{i + 1}] вершины (маленький латинский символ): ",
                        "Необходимо ввести маленький латинский символ, повторите ввод =>\n",
                        (char symbol) => (int)symbol >= 97 && (int)symbol <= 122 && !names.Contains(symbol));
            }            
            if(isOrientied)
            {
                  for(int i = 0; i < len; ++i){
                        for(int j = 0; j < len; ++j){
                              adjacencyMatrix[i, j] = GetInt($"Длина пути из вершины '{names[i]}' в '{names[j]}': ", 
                                    $"Введите целое число > 0, если дорога есть, и '0', если - нет, повторите ввод =>\n",
                                    (int num) => num >= 0 && num <= 1) == 1;
                        }  
                  }
            }
            else
            {
                  for(int i = 0; i < len; ++i){
                        for(int j = i; j < len; ++j){
                              adjacencyMatrix[i, j] = GetInt($"Длина пути из вершины '{names[i]}' в '{names[j]}': ", 
                                    $"Введите целое число > 0, если дорога есть, и '0', если - нет, повторите ввод =>\n",
                                    (int num) => num >= 0 && num <= 1) == 1;
                              adjacencyMatrix[j, i] = adjacencyMatrix[i, j];
                        }  
                  }
            }
            return new UnweightedGraph(adjacencyMatrix, names);
      }
      static WeightedGraph InputWeightedGraph(bool isOrientied = false)
      {
            int len = GetInt("Введите кол-во вершин графа: ", 
                  $"Кол-во вершин должно быть в диапозоне [2; {MAX_LENGTH}], повторите ввод =>\n",
                  (int num) => num > 2 && num <= MAX_LENGTH);
            var adjacencyMatrix = new int[len, len];
            var names = new char[len];
            for (int i = 0; i < len; ++i) {
            names[i] = GetChar($"Введите имя [{i + 1}] вершины (маленький латинский символ): ",
                  "Необходимо ввести маленький латинский символ, повторите ввод =>\n",
                  (char symbol) => (int)symbol >= 97 && (int)symbol <= 122 && !names.Contains(symbol));
            }
            if(isOrientied)
            {
                  for(int i = 0; i < len; ++i){
                        for(int j = 0; j < len; ++j){
                              adjacencyMatrix[i, j] = GetInt($"Длина пути из вершины '{names[i]}' в '{names[j]}': ", 
                                    $"Введите целое число > 0, если дорога есть, и '0', если - нет, повторите ввод =>\n",
                                    (int num) => num >= 0);
                        }  
                  }
            }
            else
            {
                  for(int i = 0; i < len; ++i){
                        for(int j = i; j < len; ++j){
                              adjacencyMatrix[i, j] = GetInt($"Длина пути из вершины '{names[i]}' в '{names[j]}': ", 
                                    $"Введите целое число > 0, если дорога есть, и '0', если - нет, повторите ввод =>\n",
                                    (int num) => num >= 0);
                              adjacencyMatrix[j, i] = adjacencyMatrix[i, j];
                        }  
                  }
            }
            return new WeightedGraph(adjacencyMatrix, names);
      }
      //ввод целого числа с консоли с обработкой ошибок
      static int GetInt(string inputMessage, string errorMessage, Predicate<int> condition)
      {
            int result;
            bool isCorrect;
            do
            {
                  Console.Write(inputMessage);
                  isCorrect = int.TryParse(Console.ReadLine(), out result) && condition(result);
                  if (!isCorrect)
                        ColorDisplay(errorMessage, ConsoleColor.Red);
            } while (!isCorrect);
            return result;
      }
      //ввод символа с консоли с обработкой ошибок
      static char GetChar(string inputMessage, string errorMessage, Predicate<char> condition)
      {
            char result;
            bool isCorrect;
            do
            {
                  Console.Write(inputMessage);
                  isCorrect = char.TryParse(Console.ReadLine(), out result) && condition(result);
                  if (!isCorrect)
                        ColorDisplay(errorMessage, ConsoleColor.Red);
            } while (!isCorrect);
            return result;
      }
      //цветной вывод текста в консоль
      static void ColorDisplay(string message, ConsoleColor color)
      {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.Gray;
      }
      public static string ToString(bool[,] matrix, char[] names){
            StringBuilder strBuilder = new StringBuilder(matrix.Length * 3);
            strBuilder.Append('\t');
            for(int i = 0; i < names.Length; ++i){
                  strBuilder.Append(names[i]).Append('\t');
            }
            strBuilder.Append('\n');
            for(int i = 0; i < matrix.GetLength(0); ++i){
                  strBuilder.Append(names[i]).Append('\t');
                  for(int j = 0; j < matrix.GetUpperBound(1); ++j){
                        strBuilder.Append(matrix[i, j] ? 1 : 0).Append('\t');
                  }
                  strBuilder.Append(matrix[i, matrix.GetUpperBound(1)] ? 1 : 0).Append('\n');
            }    
            return strBuilder.ToString();
      } 
}
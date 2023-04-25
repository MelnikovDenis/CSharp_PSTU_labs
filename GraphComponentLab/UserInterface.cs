using System.Text;
namespace GraphComponentLab;
public static class UserInterface{
      const int MAX_LENGTH = 10; //максимальное кол-во вершин графа
      //главный цикл приложения
      public static void Execute(){
            bool needExit = false;
           var graph = new Graph(new int[,]
            {
                  {0, 1, 0, 0, 0},
                  {0, 0, 0, 1, 0},
                  {0, 1, 0, 0, 0},
                  {0, 0, 1, 0, 0},
                  {1, 0, 1, 1, 0}
            }, new char[]{'a', 'b', 'c', 'd', 'e'});
            while (!needExit)
            {
                  Console.Clear();
                  ColorDisplay("1. Найти компоненты сильной связности графа\n2. Построить ярусно-параллельную форму\n3. Ввести новую матрицу\n4. Метод Краскала\n0. Выйти\n", ConsoleColor.Green);
                  ColorDisplay("Матрица смежности графа:\n", ConsoleColor.Magenta);
                  Console.WriteLine(ToString(graph.Matr, graph.Names));             
                  int command = GetInt("Введите номер команды: ", "Номер команды должен быть целым числом в диапозоне [0, 4], повторите ввод =>\n",
                        (int num) => num >= 0 && num <= 4);                  
                  switch (command)
                  {
                        case 1:
                              DisplayStronglyConnectedComponent(graph);
                              break;
                        case 2:
                              
                              break;
                        case 3:
                                                                  
                              break;
                        case 4:
                              
                              break;                        
                        case 0:
                              needExit = true;
                              break;
                  }
                  ColorDisplay("Нажмите любую клавишу для продолжения...", ConsoleColor.Yellow);
                  Console.ReadKey();
            }
      }
      //вывод ярусно-параллельной формы графа
      static void DisplayTieredParallelForm(Graph graph){
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
      static void DisplayStronglyConnectedComponent(Graph graph){
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
      static Graph InputWeightedGraph(bool isOrientied = false)
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
            return new Graph(adjacencyMatrix, names);
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
      public static string ToString(int[,] matrix, char[] names){
            StringBuilder strBuilder = new StringBuilder(matrix.Length * 3);
            strBuilder.Append('\t');
            for(int i = 0; i < names.Length; ++i){
                  strBuilder.Append(names[i]).Append('\t');
            }
            strBuilder.Append('\n');
            for(int i = 0; i < matrix.GetLength(0); ++i){
                  strBuilder.Append(names[i]).Append('\t');
                  for(int j = 0; j < matrix.GetUpperBound(1); ++j){
                        strBuilder.Append(matrix[i, j]).Append('\t');
                  }
                  strBuilder.Append(matrix[i, matrix.GetUpperBound(1)]).Append('\n');
            }    
            return strBuilder.ToString();
      } 
}
using System.Text;
using static UtilityLibraries.ConsoleIOLibrary;
namespace GraphComponentLab;
internal static class UserInterface{
      const int MAX_LENGTH = 10; //максимальное кол-во вершин графа
      const int MIN_LENGTH = 3; //минимальное кол-во вершин

      //Краскал - неориентированный
      //Компоненты сильной связности - невзвешенный, ориентированный - (есть путь из любой вершины до любой)
      //Клика графа - неориентированный, невзвешенный - (любые 2 вершины соединены между собой)
      //Ярусно-параллельная форма - ориентированный, невзвешенный, ацикличный

      //ориентированный - частный случай неориентированного
      //взвешенный - частный случай невзвешенного

      //главный цикл приложения      
      public static void Execute(){
            bool needExit = false;
            var graph = new Graph(new int[,]
            {
                  {0, 1, 0, 0, 0},
                  {1, 0, 1, 1, 0},
                  {0, 1, 0, 1, 1},
                  {0, 1, 1, 0, 0},
                  {0, 0, 1, 0, 0}
            }, new char[]{'a', 'b', 'c', 'd', 'e'});
            while (!needExit)
            {
                  Console.Clear();
                  ColorDisplay("1. Найти компоненты сильной связности графа\n2. Построить ярусно-параллельную форму\n3. Ввести новую матрицу\n4. Метод Краскала\n5. Максимальная клика\n0. Выйти\n", ConsoleColor.Green);
                  DisplayMatrix(graph.Matr, graph.Names, "Матрица смежности графа:");             
                  int command = GetInt("Введите номер команды: ", "Номер команды должен быть целым числом в диапозоне [0, 5], повторите ввод =>\n",
                        (int num) => num >= 0 && num <= 5);                  
                  switch (command)
                  {
                        case 1:
                              DisplayStronglyConnectedComponent(graph);
                              break;
                        case 2:
                              DisplayTieredParallelForm(graph);
                              break;
                        case 3:
                              graph = InputGraph();                                
                              break;
                        case 4:
                              DisplaySpanningTree(graph);
                              break;  
                        case 5:
                              DisplayCliques(graph);
                              break;                      
                        case 0:
                              needExit = true;
                              break;
                  }
                  ColorDisplay("Нажмите любую клавишу для продолжения...", ConsoleColor.Yellow);
                  Console.ReadKey();
            }
      }
      //вывод клик
      static void DisplayCliques(Graph graph)
      {
            try{
                  var cliques = graph.BronKerbosh();
                  if(cliques.Count() > 0)
                  {
                        var sorted_cluques = from clique in cliques orderby clique.Count() descending select clique;
                        ColorDisplay("Клики:\n", ConsoleColor.Magenta);
                        foreach(var clique in sorted_cluques)
                        {
                              Console.Write($"Размер: {clique.Count()}, {{ ");
                              foreach(var vertex in clique)
                              {
                                    Console.Write($"{graph.Names[vertex]} ");
                              }
                              Console.WriteLine("}");
                        }
                  }
                  else
                        throw new ArgumentException("У данного графа нет клик");
            }
            catch(Exception ex){
                  ColorDisplay(ex.Message + '\n', ConsoleColor.Red);
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
            if(!graph.isNonOrientied()){
                  var strConnComps = graph.GetStronglyСonnectedСomponent();           
                  DisplayMatrix(graph.GetStronglyConnectedComponentMatrix(), graph.Names, "Матрица компонентов сильной связности:");
                  ColorDisplay("Компоненты сильной связности графа:\n", ConsoleColor.Magenta);
                  foreach(var strConnComp in strConnComps){
                        Console.Write("{ ");
                        foreach(var vertex in strConnComp){
                              Console.Write((graph.Names[vertex]).ToString() + " ");
                        }
                        Console.WriteLine("}");
                  }
            }
            else
                  ColorDisplay("Компоненты сильной связности можно найти только у ориентированного графа!\n", ConsoleColor.Red);
      }
      //вывод остова
      static void DisplaySpanningTree(Graph graph)
      {
            if(graph.isNonOrientied() && graph.isWeighted() &&  graph.GetStronglyСonnectedСomponent().Count == 1)
            {
                  var kruskal = graph.Kruskal();
                  ColorDisplay("Минимальный остов графа:\n", ConsoleColor.Magenta);
                  int sum = 0;
                  foreach(var edge in kruskal)
                  {
                        Console.WriteLine($"{graph.Names[edge.Item1]} <-> {graph.Names[edge.Item2]} - Длина: {graph.Matr[edge.Item1, edge.Item2]}");
                        sum += graph.Matr[edge.Item1, edge.Item2];
                  }
                  ColorDisplay($"Длина минимального остова: {sum}\n", ConsoleColor.Magenta);
            }
            else
                  ColorDisplay("Минимальный остов по методу Краскала можно найти только у связного, неориентированного и взвешенного графа\n", ConsoleColor.Red);
      }
      //ввод графа      
      static Graph InputGraph()
      {
            int len = GetInt("Введите кол-во вершин графа: ", 
                  $"Кол-во вершин должно быть в диапозоне [{MIN_LENGTH}; {MAX_LENGTH}], повторите ввод =>\n",
                  (int num) => num >= MIN_LENGTH && num <= MAX_LENGTH);
            var adjacencyMatrix = new int[len, len];
            var names = new char[len];
            for (int i = 0; i < len; ++i) {
            names[i] = GetChar($"Введите имя [{i + 1}] вершины (маленький латинский символ): ",
                  "Необходимо ввести маленький латинский символ, повторите ввод =>\n",
                  (char symbol) => (int)symbol >= 97 && (int)symbol <= 122 && !names.Contains(symbol));
            }            
            bool isOrientied = GetInt("Вы хотите ввести ориентированный граф? 1 - да, 0 - нет: ", 
                  $"Вы должны ввести 0 или 1, повторите ввод =>\n",
                  (int num) => num == 0 || num == 1) == 1;
            bool isWeighted = GetInt("Вы хотите ввести взвешенный граф? 1 - да, 0 - нет: ", 
                  $"Вы должны ввести 0 или 1, повторите ввод =>\n",
                  (int num) => num == 0 || num == 1) == 1;            
            if(isOrientied)
            {
                  if(isWeighted)
                  {
                        for(int i = 0; i < len; ++i){
                              for(int j = 0; j < len; ++j){
                                    if(i != j)
                                          adjacencyMatrix[i, j] = GetInt($"Длина пути из вершины '{names[i]}' в '{names[j]}': ", 
                                                $"Введите целое число > 0, если дорога есть, и '0', если - нет, повторите ввод =>\n",
                                                (int num) => num >= 0);
                              }  
                        }
                  }
                  else
                  {
                        for(int i = 0; i < len; ++i){
                              for(int j = 0; j < len; ++j){
                                    if(i != j)
                                          adjacencyMatrix[i, j] = GetInt($"Наличие пути из вершины '{names[i]}' в '{names[j]}': ", 
                                                $"Введите '1', если дорога есть, и '0', если - нет, повторите ввод =>\n",
                                                (int num) => num >= 0);
                              }  
                        }
                  }
            }
            else
            {
                  if(isWeighted)
                  {
                        for(int i = 0; i < len; ++i){
                              for(int j = i + 1; j < len; ++j){
                                    if(i != j)
                                          adjacencyMatrix[i, j] = GetInt($"Длина пути из вершины '{names[i]}' в '{names[j]}': ", 
                                                $"Введите целое число > 0, если дорога есть, и '0', если - нет, повторите ввод =>\n",
                                                (int num) => num >= 0);
                                          adjacencyMatrix[j, i] = adjacencyMatrix[i, j];
                              }  
                        }
                  }
                  else
                  {
                        for(int i = 0; i < len; ++i){
                              for(int j = i + 1; j < len - 1; ++j){
                                    if(i != j)
                                          adjacencyMatrix[i, j] = GetInt($"Наличие пути из вершины '{names[i]}' в '{names[j]}': ", 
                                                $"Введите '1', если дорога есть, и '0', если - нет, повторите ввод =>\n",
                                                (int num) => num >= 0);
                                          adjacencyMatrix[j, i] = adjacencyMatrix[i, j];
                              }  
                        }
                  }
            }
            return new Graph(adjacencyMatrix, names);
      }
      //вывод матрицы в консоль
      static void DisplayMatrix(int[,] matrix, char[] names, string caption){
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
            ColorDisplay(caption + "\n", ConsoleColor.Magenta);
            Console.Write(strBuilder.ToString());
      } 
}
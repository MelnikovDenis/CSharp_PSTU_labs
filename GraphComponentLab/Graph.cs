namespace GraphComponentLab;
public class Graph
{
      //строки (откуда) - нулевой индекс
      //столбцы (куда) - первый индекс
      public int[,] Matr {get; set;} //матрица
      public char[] Names { get; set; } = new char[]{'a', 'b', 'c', 'd', 'e', 'f', 'g'}; //имена вершин
      //кол-во вершин графа
      public int Count {get{
            return Matr.GetLength(0);
      }}
      public Graph(int[,] Matr)
      {
            if(Matr.GetLength(0) == Matr.GetLength(1))
                  this.Matr = Matr;
             else
                  throw new ArgumentException("Длина измерений матрицы смежности должна быть одинаковой");
      }  
      public Graph(int[,] Matr, char[] Names) :this(Matr){
            if(Matr.GetLength(0) == Names.Length){                  
                  this.Names = Names;
            }                  
            else
                  throw new ArgumentException("Длина измерений матрицы смежности должна равняться длине массива имён");
      }
      //получаем соседей вершины
      public HashSet<int> GetNeighbors(int vertex)
      {
            if(!isNonOrientied())
                  throw new ArgumentException("Для получения соседей граф должен быть неориентированным");
            var result = new HashSet<int>(Count);
            for(int i = 0; i < Count; ++i)
                  if(Matr[i, vertex] > 0 && i != vertex)                  
                        result.Add(i);

            return result;
      }
      //not НЕ содержит вершины, СОЕДИНЕННОЙ СО ВСЕМИ вершинами из candidates
      public bool isNotConnectedToAll(HashSet<int> candidates, HashSet<int> not)
      {
            foreach(var vertex in not)
                  if(candidates.Except(GetNeighbors(vertex)).Count() == 0)
                        return false;
            return true;
      }
      //возвращаем new_set, созданный на основе set, но без вершин НЕ СОЕДИНЕННЫХ с v
      public HashSet<int> DeleteNotConnected(HashSet<int> set, int v)
      {
            var new_set = set.Intersect(GetNeighbors(v)).ToHashSet();            
            return new_set; 
      }
      //метод поиска всех клик по алгоритму Брона-Кербоша
      public List<HashSet<int>> BronKerbosh()
      {
            var result = new List<HashSet<int>>(Count); //список всех клик
            var compsub = new HashSet<int>(Count); //клика
            var candidates = new HashSet<int>(Count); //вершины пригодные для рассмотрения
            for(int i = 0; i < Count; ++i)
                  candidates.Add(i);
            var not = new HashSet<int>(Count); //исключенные из рассмотрения вершины
            void extend(HashSet<int> candidates, HashSet<int> not)
            {                 
                  //ПОКА candidates НЕ пусто И not НЕ содержит вершины, СОЕДИНЕННОЙ СО ВСЕМИ вершинами из candidates, 
                  while(candidates.Count() > 0 && isNotConnectedToAll(candidates, not))
                  { 
                        //Выбираем вершину v из candidates и добавляем ее в compsub
                        int v = candidates.First();
                        compsub.Add(v);
                        //Формируем new_candidates и new_not, путём удаления из candidates и not вершин, НЕ СОЕДИНЕННЫХ с v     
                        var new_candidates = DeleteNotConnected(candidates, v);
                        var new_not = DeleteNotConnected(not, v);
                        //ЕСЛИ new_candidates и new_not пусты
                        if(new_candidates.Count() == 0 && new_not.Count() == 0)
                              //ТО compsub – клика
                              result.Add(new HashSet<int>(compsub.ToArray())); 
                                                                                
                        else
                              //ИНАЧЕ рекурсивно вызываем extend(new_candidates, new_not)
                              extend(new_candidates, new_not);
                        //Удаляем v из compsub и candidates и помещаем в not
                        not.Add(v);
                        compsub.Remove(v);
                        candidates.Remove(v);
                  }
            }
            extend(candidates, not);
            return result;
      }
      //проверка графа на взвешенность
      public bool isWeighted()
      {
            foreach(var item in Matr)
                  if(item > 1)
                        return true;
            return false;
      }
      //проверка графа на ориентированность
      public bool isNonOrientied()
      {
            for(int i = 0; i < Count; ++i)
                  for(int j = 0; j < Count; ++j)
                        if(i != j && Matr[i, j] != Matr[j, i])
                              return false;
            return true;
      }
      //поиск остова по краскалу
      public List<(int, int)> Kruskal()
      {
            var edges = new List<(int, int)>(Matr.Length); //список всех рёбер в виде - индекс1, индекс2
            var resultEdges = new List<(int, int)>(Count - 1); //остов
            for(int i = 0; i < Matr.GetLength(0); ++i)
                  for(int j = i + 1; j < Matr.GetLength(1); ++j)
                        if(Matr[i, j] != 0)
                              edges.Add((i, j));
            var sortedEdges = from edge in edges orderby Matr[edge.Item1, edge.Item2] select edge; //сортируем все рёбра по длине            
            foreach(var edge in sortedEdges)
            {        
                  resultEdges.Add(edge); //добавляем ребро
                  if(HaveCycle(resultEdges)) //если теперь рёбра образуют цикл
                        resultEdges.Remove(edge); //удаляем добавленное ребро
                  //рёбер на 1 меньше чем вершин
                  if(resultEdges.Count == Count - 1)
                        break;
            }            
            
            return resultEdges;
      }
      //проверка на наличие циклов
      private bool HaveCycle(List<(int, int)> edges)
      {
            var vertices = new Dictionary<int, bool>(Count); //все задействованные вершины в формате индекс_вершины: была_ли_обойдена_в_dfs
            bool result = false;  
            //добавляем задействованные вершины
            foreach(var edge in edges)
            {
                  if(!vertices.ContainsKey(edge.Item1))
                        vertices.Add(edge.Item1, false);
                  if(!vertices.ContainsKey(edge.Item2))
                        vertices.Add(edge.Item2, false);
            }
            //поиск в глубину
            void dfs(int vertex, int partner = -1){
                  vertices[vertex] = true;
                  var containsIndex1 = from edge in edges where edge.Item1 == vertex select edge.Item2;
                  var containsIndex2 = from edge in edges where edge.Item2 == vertex select edge.Item1;
                  containsIndex1 = containsIndex1.Concat(containsIndex2);
                  foreach(var child in containsIndex1)
                  {
                        if(!vertices[child])
                              dfs(child, vertex);
                        else if(child != partner)
                              result = true;
                  }
            }
            //если вершины не были связаны, то пробуем начать dfs из них
            foreach(var vertex in vertices.Keys)
                  if(!vertices[vertex])
                        dfs(vertex);      
            return result;
      }

      //не поэлементное логическое умножение матриц
      private static int[,] BooleanMultiplyByMatrix(int[,] firstMatrix, int[,] secondMatrix){
            if(firstMatrix.GetLength(1) == secondMatrix.GetLength(0)){
                  int[,] resultMatrix = new int[firstMatrix.GetLength(0), secondMatrix.GetLength(1)];
                  for(int i = 0; i < resultMatrix.GetLength(0); ++i){
                        for(int j = 0; j < resultMatrix.GetLength(1); ++j){
                              for (int k = 0; k < firstMatrix.GetLongLength(1); ++k){
                                    resultMatrix[i, j] += (firstMatrix[i, k] > 0) && (secondMatrix[k, j] > 0) ? 1 : 0;
                              }
                        }
                  }
                  return resultMatrix;
            }
            else{
                  throw new ArgumentException("Кол-во столбцов первой матрицы не равно кол-ву строк второй, умножение невозможно");
            }
      }
      //поэлементное логическое сложение матриц
      private static int[,] MatrixOR(int[,] firstMatrix, int[,] secondMatrix){
            if(firstMatrix.GetLength(0) == secondMatrix.GetLength(0) && firstMatrix.GetLength(1) == secondMatrix.GetLength(1)){
                  int[,] resultMatrix = new int[firstMatrix.GetLength(0), secondMatrix.GetLength(1)];
                  for(int i = 0; i < resultMatrix.GetLength(0); ++i){
                        for(int j = 0; j < resultMatrix.GetLength(1); ++j){
                              resultMatrix[i, j] = (firstMatrix[i, j] > 0) || (secondMatrix[i, j] > 0) ? 1 : 0;
                        }
                  }
                  return resultMatrix;
            }
            else{
                  throw new ArgumentException("Размерности матриц не совпадают, сложение невозможно");
            }
      }

      //поэлементное логическое умножение матриц
      private static int[,] MatrixAND(int[,] firstMatrix, int[,] secondMatrix){
            if(firstMatrix.GetLength(0) == secondMatrix.GetLength(0) && firstMatrix.GetLength(1) == secondMatrix.GetLength(1)){
                  int[,] resultMatrix = new int[firstMatrix.GetLength(0), secondMatrix.GetLength(1)];
                  for(int i = 0; i < resultMatrix.GetLength(0); ++i){
                        for(int j = 0; j < resultMatrix.GetLength(1); ++j){
                              resultMatrix[i, j] = (firstMatrix[i, j] > 0) && (secondMatrix[i, j] > 0) ? 1 : 0;
                        }
                  }
                  return resultMatrix;
            }
            else{
                  throw new ArgumentException("Размерности матриц не совпадают, логическое умножение невозможно");
            }
      }
      //получение единичной матрицы
      private int[,] GetIdentityMatrix(){
            int[,] resultMatrix = new int[Count, Count];
            for(int i = 0; i < Count; ++i){
                  resultMatrix[i, i] = 1;
            }
            return resultMatrix;
      }
      //устанавливает значение 0 для всех ячеек в определённом столбце и строчке
      private static void SetZeroOnRowAndColumn(int[,] matrix, int rowAndColumn){
            for(int i = 0; i < matrix.GetLength(1); ++i){
                  matrix[rowAndColumn, i] = 0;
                  matrix[i, rowAndColumn] = 0;
            }
      }
      //проверяет есть ли в столбце хотя бы одно значение >0
      private static bool ContainsPathInColumn(int[,] matrix, int column){
            for(int i = 0; i < matrix.GetLength(0); ++i){
                  if(matrix[i, column] > 0)
                        return true;
            }
            return false;
      }
      //получить ярусно-параллельную форму графа
      public List<List<int>> GetTieredParallelForm(){
            var adjacencyMatrixCopy = (int[,])Matr.Clone(); //клонируем матрицу смежности, чтобы столбцы занулялись не в основной матрице
            var tieredParallelForm = new List<List<int>>(Count); //итоговый список списков с вершинами разбитыми на ярусы
            var unprocessed = new LinkedList<int>(); //список необработанных столбцов
            //добавляем номера всех вершин в список необработанных
            for(int i = 0; i < Count; ++i){
                  unprocessed.AddLast(i);
            }
            //пока есть необработанные столбцы                 
            while(unprocessed.Count > 0){
                  var tier = new List<int>(); //список вершин в текущем ярусе
                  //перебираем оставшиеся необработанные столбцы                                     
                  foreach(var i in unprocessed){
                        //если в столбце нет пути
                        if(!ContainsPathInColumn(adjacencyMatrixCopy, i)){
                              tier.Add(i); //добавляем вершину в ярус
                        }  
                  }
                  //перебираем обработанные вершины в ярусе
                  foreach(var i in tier){
                        SetZeroOnRowAndColumn(adjacencyMatrixCopy, i); //зануляем обработанные столбцы и строки
                        unprocessed.Remove(i); //удаляем из списка те столбцы, что обработали
                  }
                  if(tier.Count == 0 && unprocessed.Count > 0){
                        throw new ArgumentException("Невозможно построить ярусно-параллельную форму, потому что в графе есть компоненты сильной связности");
                  } 
                  tieredParallelForm.Add(tier);
            } 
            return tieredParallelForm;                        
      }
      //получить матрицу компонентов сильной связности
      public int[,] GetStronglyConnectedComponentMatrix(){
            var strConnCompMatrix = GetIdentityMatrix(); //единичная матрица
            foreach(var exp in GetBooleanMatrixExp()){
                  strConnCompMatrix = MatrixOR(strConnCompMatrix, exp);
            } //получение суммы E^0 + E^1 + ... + E^Count = R
            strConnCompMatrix = MatrixAND(strConnCompMatrix, Transpose(strConnCompMatrix)); //получение R & Transpose(R) = S
            return strConnCompMatrix;
      }
      //получить компноненты сильной связности
      public List<List<int>> GetStronglyСonnectedСomponent(){                       
            var strConnCompMatrix = GetStronglyConnectedComponentMatrix();
            var StrConnComps = new List<List<int>>(Count);            
            for(int i = 0; i < Count; ++i){
                  var strConnComp = new List<int>();
                  for(int j = 0; j < Count; ++j){
                        if(strConnCompMatrix[i, j] > 0)
                              strConnComp.Add(j);                                                     
                  }
                  for(int j = 0; j < strConnComp.Count; ++j){
                        SetZeroOnRowAndColumn(strConnCompMatrix, strConnComp[j]);
                  }                       
                  if(strConnComp.Count > 0)
                        StrConnComps.Add(strConnComp);
            }
            return StrConnComps;
      }
      //итератор для степеней матрицы
      private IEnumerable<int[,]> GetBooleanMatrixExp(){
            var expMatrix = Matr;
            for(int i = 0; i < Count; ++i){
                  yield return expMatrix;
                  expMatrix = BooleanMultiplyByMatrix(expMatrix, Matr);
            }                  
      }
      //транспонирование
      private static int[,] Transpose(int[,] Matr){
            var resultMatr = new int[Matr.GetLength(1), Matr.GetLength(0)];
            for(int i = 0; i < resultMatr.GetLength(0); ++i){
                  for(int j = 0; j < resultMatr.GetLength(1); ++j){
                        resultMatr[i, j] = Matr[j, i];
                  }
            }
            return resultMatr;
      }
}
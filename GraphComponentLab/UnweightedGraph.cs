using System.Text; //для StringBuilder
namespace GraphComponentLab;
class UnweightedGraph : Graph<bool>{      
      public UnweightedGraph(bool[,] Matr, char[] Names) : base(Matr, Names){
           
      }
      //не перегружаю object.ToString, чтобы можно было любую промежуточную матрицу вывести
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
      //умножение логических матриц (не поэлементное)
      private static bool[,] BooleanMultiplyByMatrix(bool[,] firstMatrix, bool[,] secondMatrix){
            if(firstMatrix.GetLength(1) == secondMatrix.GetLength(0)){
                  bool[,] resultMatrix = new bool[firstMatrix.GetLength(0), secondMatrix.GetLength(1)];
                  for(int i = 0; i < resultMatrix.GetLength(0); ++i){
                        for(int j = 0; j < resultMatrix.GetLength(1); ++j){
                              for (int k = 0; k < firstMatrix.GetLongLength(1); ++k){
                                    resultMatrix[i, j] |= firstMatrix[i, k] && secondMatrix[k, j];
                              }
                        }
                  }
                  return resultMatrix;
            }
            else{
                  throw new ArgumentException("Кол-во столбцов первой матрицы не равно кол-ву строк второй, умножение невозможно");
            }
      }
      //сложение логических матриц
      private static bool[,] BooleanMatrixOR(bool[,] firstMatrix, bool[,] secondMatrix){
            if(firstMatrix.GetLength(0) == secondMatrix.GetLength(0) && firstMatrix.GetLength(1) == secondMatrix.GetLength(1)){
                  bool[,] resultMatrix = new bool[firstMatrix.GetLength(0), secondMatrix.GetLength(1)];
                  for(int i = 0; i < resultMatrix.GetLength(0); ++i){
                        for(int j = 0; j < resultMatrix.GetLength(1); ++j){
                              resultMatrix[i, j] = firstMatrix[i, j] || secondMatrix[i, j];
                        }
                  }
                  return resultMatrix;
            }
            else{
                  throw new ArgumentException("Размерности матриц не совпадают, сложение невозможно");
            }
      }
      //умножение логических матриц (поэлементное)
      private static bool[,] BooleanMatrixAND(bool[,] firstMatrix, bool[,] secondMatrix){
            if(firstMatrix.GetLength(0) == secondMatrix.GetLength(0) && firstMatrix.GetLength(1) == secondMatrix.GetLength(1)){
                  bool[,] resultMatrix = new bool[firstMatrix.GetLength(0), secondMatrix.GetLength(1)];
                  for(int i = 0; i < resultMatrix.GetLength(0); ++i){
                        for(int j = 0; j < resultMatrix.GetLength(1); ++j){
                              resultMatrix[i, j] = firstMatrix[i, j] && secondMatrix[i, j];
                        }
                  }
                  return resultMatrix;
            }
            else{
                  throw new ArgumentException("Размерности матриц не совпадают, логическое умножение невозможно");
            }
      }
      
      //получение единичной логической матрицы
      private bool[,] GetIdentityBooleanMatrix(){
            bool[,] resultMatrix = new bool[Count, Count];
            for(int i = 0; i < Count; ++i){
                  resultMatrix[i, i] = true;
            }
            return resultMatrix;
      }
      //устанавливает значение false для всех ячеек в определённом столбце и строчке
      private static void SetFalseOnRowAndColumn(bool[,] matrix, int rowAndColumn){
            for(int i = 0; i < matrix.GetLength(1); ++i){
                  matrix[rowAndColumn, i] = false;
                  matrix[i, rowAndColumn] = false;
            }
      }
      //проверяет есть ли в столбце хотя бы одно значение true
      private static bool ContainsTrueInColumn(bool[,] matrix, int column){
            for(int i = 0; i < matrix.GetLength(0); ++i){
                  if(matrix[i, column])
                        return true;
            }
            return false;
      }
      //получить ярусно-параллельную форму графа
      public List<List<int>> GetTieredParallelForm(){
            var adjacencyMatrixCopy = (bool[,])Matr.Clone(); //клонируем матрицу смежности, чтобы столбцы занулялись не в основной матрице
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
                        //если в столбце нет true
                        if(!ContainsTrueInColumn(adjacencyMatrixCopy, i)){
                              tier.Add(i); //добавляем вершины в ярус
                        }  
                  }
                  //перебираем обработанные вершины в ярусе
                  foreach(var i in tier){
                        SetFalseOnRowAndColumn(adjacencyMatrixCopy, i); //зануляем обработанные столбцы и строки
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
      public bool[,] GetStronglyConnectedComponentMatrix(){
            var strConnCompMatrix = GetIdentityBooleanMatrix(); //единичная матрица
            foreach(var exp in GetBooleanMatrixExp()){
                  strConnCompMatrix = UnweightedGraph.BooleanMatrixOR(strConnCompMatrix, exp);
            } //получение суммы E^0 + E^1 + ... + E^Count = R
            strConnCompMatrix = UnweightedGraph.BooleanMatrixAND(strConnCompMatrix, new Matrix<bool>(strConnCompMatrix).Transpose().Matr); //получение R & Transpose(R) = S
            return strConnCompMatrix;
      }
      //получить компноненты сильной связности
      public List<List<int>> GetStronglyСonnectedСomponent(){                       
            var strConnCompMatrix = GetStronglyConnectedComponentMatrix();
            var StrConnComps = new List<List<int>>(Count);            
            for(int i = 0; i < Count; ++i){
                  var strConnComp = new List<int>();
                  for(int j = 0; j < Count; ++j){
                        if(strConnCompMatrix[i, j])
                              strConnComp.Add(j);                                                     
                  }
                  for(int j = 0; j < strConnComp.Count; ++j){
                        SetFalseOnRowAndColumn(strConnCompMatrix, strConnComp[j]);
                  }                       
                  if(strConnComp.Count > 0)
                        StrConnComps.Add(strConnComp);
            }
            return StrConnComps;
      }
      //итератор для степеней матрицы
      private IEnumerable<bool[,]> GetBooleanMatrixExp(){
            var expMatrix = Matr;
            for(int i = 0; i < Count; ++i){
                  yield return expMatrix;
                  expMatrix = BooleanMultiplyByMatrix(expMatrix, Matr);
            }                  
      }
}
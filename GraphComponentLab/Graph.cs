namespace GraphComponentLab;
public class Graph<T> : Matrix<T>
{
      //строки (откуда) - нулевой индекс
      //столбцы (куда) - первый индекс
      public char[] Names { get; set; } //имена вершин
      //кол-во вершин графа
      public int Count {get{
            return Matr.GetLength(0);
      }}
      public Graph(T[,] Matr, char[] Names) : base(Matr){
            if(Matr.GetLength(0) == Matr.GetLength(1) && Matr.GetLength(0) == Names.Length){                  
                  this.Names = Names;
            }                  
            else
                  throw new ArgumentException("Длина измерений матрицы смежности должна быть одинаковой и равняться длине массива имён");
      }
}
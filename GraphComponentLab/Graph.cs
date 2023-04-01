using System.Text;
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
      public override string ToString(){
            StringBuilder strBuilder = new StringBuilder(Matr.Length * 3);
            strBuilder.Append('\t');
            for(int i = 0; i < Names.Length; ++i){
                  strBuilder.Append(Names[i]).Append('\t');
            }
            strBuilder.Append('\n');
            for(int i = 0; i < Matr.GetLength(0); ++i){
                  strBuilder.Append(Names[i]).Append('\t');
                  for(int j = 0; j < Matr.GetUpperBound(1); ++j){
                        strBuilder.Append(Matr[i, j]).Append('\t');
                  }
                  strBuilder.Append(Matr[i, Matr.GetUpperBound(1)]).Append('\n');
            }    
            return strBuilder.ToString();
      }
}
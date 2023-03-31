namespace GraphComponentLab;
public class Matrix<T>
{
      public T[,] Matr {get; set;} //матрица
      public Matrix(T[,] Matr)
      {
            this.Matr = Matr;
      }      
      //транспонирование
      public Matrix<T> Transpose(){
            T[,] resultMatr = new T[Matr.GetLength(1), Matr.GetLength(0)];
            for(int i = 0; i < resultMatr.GetLength(0); ++i){
                  for(int j = 0; j < resultMatr.GetLength(1); ++j){
                        resultMatr[i, j] = Matr[j, i];
                  }
            }
            return new Matrix<T>(resultMatr);
      }
      //индексатор
      public virtual T this[int index1, int index2]
      {
            get{
                  if(index1 < Matr.GetLength(0) && index2 < Matr.GetLength(1) && index1 >= 0 && index2 >= 0)
                  {
                        return Matr[index1, index2];
                  }
                  else
                  {
                        throw new IndexOutOfRangeException("Индекс выходит за границы матрицы.");
                  }
            }
            set{
                  if(index1 < Matr.GetLength(0) && index2 < Matr.GetLength(1) && index1 >= 0 && index2 >= 0)
                  {
                        Matr[index1, index2] = value;
                  }
                  else
                  {
                        throw new IndexOutOfRangeException("Индекс выходит за границы матрицы.");
                  }
            }
      }
}
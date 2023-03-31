using System.Text; //для StringBuilder
namespace GraphComponentLab;
public class WeightedGraph : Graph<int>
{
      public WeightedGraph(int[,] Matr, char[] Names) : base(Matr, Names){
          
      }
      //не перегружаю object.ToString, чтобы можно было любую промежуточную матрицу вывести
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
                  resultEdges.Add(edge);          
                  if(HaveCycle(resultEdges))
                        resultEdges.Remove(edge);                  
                  //рёбер на 1 меньше чем вершин
                  if(resultEdges.Count == Count - 1)
                        break;
            }            
            
            return resultEdges;
      }
      //првоерка на наличие циклов
      private bool HaveCycle(List<(int, int)> edges)
      {
            var vertices = new Dictionary<int, bool>(Count);
            bool result = false;  
            //задействованные вершины
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

}
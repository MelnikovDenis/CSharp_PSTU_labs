using System.Text; //для StringBuilder
namespace GraphComponentLab;
public class WeightedGraph : Graph<int>
{
      public WeightedGraph(int[,] Matr, char[] Names) : base(Matr, Names){
          
      }
      public UnweightedGraph ToUnweighted()
      {
            var matr = new bool[Count, Count];
            for(int i = 0; i < Count; ++i){
                  for(int j = 0; j < Count; ++j){
                        matr[i, j] = Matr[i, j] > 1;
                  }
            }
            var unwGraph = new UnweightedGraph(matr, Names);
            return unwGraph;
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

}
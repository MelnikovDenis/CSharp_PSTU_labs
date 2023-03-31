using GraphComponentLab;
using System.Diagnostics;
namespace GraphComponentLabTest;

[TestClass]
public class WeightedGraphTest
{
    WeightedGraph wg = new WeightedGraph(new int[,]{{0, 10, 0, 5, 0, 6, 0},
                                                    {10, 0, 6, 1, 4, 0, 5},
                                                    {0, 6, 0, 3, 1, 2, 0},
                                                    {5, 1, 3, 0, 3, 0, 5},
                                                    {0, 4, 1, 3, 0, 4, 2},                                                    
                                                    {6, 0, 2, 0, 4, 0, 0},
                                                    {0, 5, 0, 5, 2, 0, 0}}, 
                                        new char[]{'a', 'b', 'c', 'd', 'e', 'f', 'g'});
    [TestMethod]
    public void KruskalSumTest()
    {
        var Kruskal = wg.Kruskal();
        var sum = (from edge in Kruskal select wg.Matr[edge.Item1, edge.Item2]).Sum();
        foreach(var edge in Kruskal)
        {
            Trace.WriteLine($"[{edge.Item1}, {edge.Item2}]: {wg.Matr[edge.Item1, edge.Item2]}");
        }
        Assert.AreEqual<int>(14, sum);
    }
    [TestMethod]
    public void KruskalCountTest()
    {
        var Kruskal = wg.Kruskal();        
        Assert.AreEqual<int>(6, Kruskal.Count);
    }
}
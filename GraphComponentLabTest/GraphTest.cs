using GraphComponentLab;
using System.Diagnostics;
namespace GraphComponentLabTest;

[TestClass]
public class GraphTest
{
    Graph<int> g = new Graph<int>(new int[,]{{0, 10, 0, 5, 0, 6, 0},
                                            {10, 0, 6, 1, 4, 0, 5},
                                            {0, 6, 0, 3, 1, 2, 0},
                                            {5, 1, 3, 0, 3, 0, 5},
                                            {0, 4, 1, 3, 0, 4, 2},                                                    
                                            {6, 0, 2, 0, 4, 0, 0},
                                            {0, 5, 0, 5, 2, 0, 0}}, 
                                new char[]{'a', 'b', 'c', 'd', 'e', 'f', 'g'});
    [TestMethod]
    public void CountTest()
    {
        Assert.AreEqual<int>(7, g.Count);
    }
    [TestMethod]
    public void SymmetricTest()
    {        
        Assert.AreEqual<int>(5, g.Matr[0, 3]);
        Assert.AreEqual<int>(10, g[0, 1]);
        Assert.AreEqual<char>('a', g.Names[0]);
        for(int i = 0; i < g.Count; ++i)
        {
            for(int j = 0; j < g.Count; ++j)
            {
                Trace.WriteLine($"g[{i}, {j}]: {g[i, j]} - g[{j}, {i}]: {g[j, i]}");
                Assert.AreEqual<int>(g[i, j], g[j, i]);                
            }
        }
    }
}
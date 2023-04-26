using GraphComponentLab;
using System.Diagnostics;
namespace GraphComponentLabTest;

[TestClass]
public class GraphTest
{
    Graph kruskalGraph = new Graph(new int[,]
    {
        {0, 10, 0, 5, 0, 6, 0},
        {10, 0, 6, 1, 4, 0, 5},
        {0, 6, 0, 3, 1, 2, 0},
        {5, 1, 3, 0, 3, 0, 5},
        {0, 4, 1, 3, 0, 4, 2},                                                    
        {6, 0, 2, 0, 4, 0, 0},
        {0, 5, 0, 5, 2, 0, 0}
    });
    Graph tierdParallelGraph = new Graph(new int[,]
    {
        {0, 1, 0, 0, 0, 1, 1, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 1, 1, 0, 0, 0},
        {1, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 1, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 1, 0, 0, 1, 0, 0, 0}
    }

    );
    Graph stronglyConnectedGraph = new Graph(new int[,]
    {
        {0, 1, 0, 0, 0},
        {0, 0, 0, 1, 0},
        {0, 1, 0, 0, 0},
        {0, 0, 1, 0, 0},
        {1, 0, 1, 1, 0}
    });
    Graph cliqueGraph = new Graph(new int[,]
    {
        {0, 1, 0, 0, 0},
        {1, 0, 1, 1, 0},
        {0, 1, 0, 1, 1},
        {0, 1, 1, 0, 0},
        {0, 0, 1, 0, 0}
    });
    [TestMethod]
    public void KruskalTest()
    {
        var Kruskal = kruskalGraph.Kruskal();
        var sum = (from edge in Kruskal select kruskalGraph.Matr[edge.Item1, edge.Item2]).Sum();
        foreach(var edge in Kruskal)
        {
            Trace.WriteLine($"[{edge.Item1}, {edge.Item2}]: {kruskalGraph.Matr[edge.Item1, edge.Item2]}");
        }
        Assert.AreEqual<int>(14, sum);
        Assert.AreEqual<int>(6, Kruskal.Count);
    }
    [TestMethod]
    public void TierdParallelFormTest()
    {
        var tierdParallelForm = tierdParallelGraph.GetTieredParallelForm();
        Assert.AreEqual(4, tierdParallelForm.Count());

        Assert.IsTrue(tierdParallelForm[0].Contains(2));
        Assert.IsTrue(tierdParallelForm[0].Contains(7));

        Assert.IsTrue(tierdParallelForm[1].Contains(3));
        Assert.IsTrue(tierdParallelForm[1].Contains(4));

        Assert.IsTrue(tierdParallelForm[2].Contains(0));

        Assert.IsTrue(tierdParallelForm[3].Contains(1));
        Assert.IsTrue(tierdParallelForm[3].Contains(5));
        Assert.IsTrue(tierdParallelForm[3].Contains(6));

    }
    [TestMethod]
    public void StronglyConnectedComponentTest()
    {
        var stronglyConnectedComponent = stronglyConnectedGraph.GetStronglyСonnectedСomponent();
        Assert.AreEqual(3, stronglyConnectedComponent.Count());

        Assert.IsTrue(stronglyConnectedComponent[0].Contains(0));

        Assert.IsTrue(stronglyConnectedComponent[1].Contains(1));
        Assert.IsTrue(stronglyConnectedComponent[1].Contains(2));
        Assert.IsTrue(stronglyConnectedComponent[1].Contains(3));

        Assert.IsTrue(stronglyConnectedComponent[2].Contains(4));
    }
    [TestMethod]
    public void StronglyConnectedMatrixTest()
    {
        var stronglyConnectedMatrix = stronglyConnectedGraph.GetStronglyConnectedComponentMatrix();
        Assert.AreEqual(1, stronglyConnectedMatrix[0, 0]);

        for(int i = 1; i < 4; ++i)
            for(int j = 1; j < 4; ++j)
                Assert.AreEqual(1, stronglyConnectedMatrix[i, j]);

        Assert.AreEqual(1, stronglyConnectedMatrix[4, 4]);
        int sum = 0;
        foreach(var item in stronglyConnectedMatrix)
            sum += item;
        Assert.AreEqual(11, sum);    
    }
    [TestMethod]
    public void NeighborsTest()
    {
        var neighbors1 = cliqueGraph.GetNeighbors(1);
        Assert.AreEqual(3, neighbors1.Count());
        Assert.IsTrue(neighbors1.Contains(0));
        Assert.IsTrue(neighbors1.Contains(2));
        Assert.IsTrue(neighbors1.Contains(3));
    }
    [TestMethod]
    public void DeleteNotConnectedTest()
    {
        var set = new HashSet<int>(cliqueGraph.Count);
        for(int i = 0; i < cliqueGraph.Count; ++i)
            set.Add(i);
        var new_set = cliqueGraph.DeleteNotConnected(set, 1);
        Assert.AreEqual(3, new_set.Count());
        Assert.IsTrue(new_set.Contains(0));
        Assert.IsTrue(new_set.Contains(2));
        Assert.IsTrue(new_set.Contains(3));
    }
   
    
    [TestMethod]
    public void IsNotConnectedToAllTest()
    {
        //not НЕ содержит вершины, СОЕДИНЕННОЙ СО ВСЕМИ вершинами из candidates
        var candidates = new HashSet<int>();
        candidates.Add(1);
        candidates.Add(3);
        var not = new HashSet<int>();
        not.Add(2);
        Assert.IsFalse(cliqueGraph.isNotConnectedToAll(candidates, not));
    }
}
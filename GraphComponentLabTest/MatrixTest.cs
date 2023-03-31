using GraphComponentLab;
using System.Diagnostics;
namespace GraphComponentLabTest;

[TestClass]
public class MatrixTest
{
    Matrix<int> matrix = new Matrix<int>(new int[,]{{0, 10, 0, 5, 0, 6, 0},
                                                {10, 0, 6, 1, 4, 0, 5},
                                                {0, 6, 0, 3, 1, 2, 0},
                                                {5, 1, 3, 0, 3, 0, 5},
                                                {0, 4, 1, 3, 0, 4, 2},                                                    
                                                {6, 0, 2, 0, 4, 0, 0},
                                                {0, 5, 0, 5, 2, 0, 0}});
    [TestMethod]
    public void MatrixCountTest()
    {
        Assert.AreEqual<int>(49, matrix.Matr.Length);
    }    
}
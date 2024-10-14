using MathNet.Numerics.LinearAlgebra;
//straight forward matrix multiply using MathNet
namespace icCodeChallenge
{
public static class MultiplyMatrices
{
    public static Matrix<double> Multiply(Matrix<double> matrixA, Matrix<double> matrixB)
    {
         if (!matrixA.ColumnCount.Equals(matrixB.RowCount))
    {
        throw new ArgumentException("Matrix dimensions are incompatible for multiplication.");
    }
    return matrixA * matrixB;
    }
}
}
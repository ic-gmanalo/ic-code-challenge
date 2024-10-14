using MathNet.Numerics.LinearAlgebra;
//very straight foward matrix multiply unit test, only tested the pass case
namespace icCodeChallengeTests
{
    [TestFixture]
    public class MultiplyMatricesTest
    {
        [Test]
        public void Multiply_Success()
        {
            
            var matrixA = Matrix<double>.Build.Dense(2, 2, [1, 2, 1, 2 ]);
            var matrixB = Matrix<double>.Build.Dense(2, 2, [1, 2, 1, 2 ]);
            var expectedResult = Matrix<double>.Build.Dense(2, 2, [3, 6, 3, 6]);

            
            var result = matrixA * matrixB;

           
            Assert.That(result, Is.EqualTo(expectedResult));
        }         
    }
}

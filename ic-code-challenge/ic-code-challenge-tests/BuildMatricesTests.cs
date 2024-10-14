using MathNet.Numerics.LinearAlgebra;
using System.Text.Json;
using NUnit.Framework.Legacy;
//unit test for the build matrices, not going to comment every line, the code attempts to be
//as close to the main code as possible, and account for the pass/fail api response
//and simulate what happens when building a matrix, its not perfect 
namespace ic_code_challenge_tests
{
    [TestFixture]
    public class BuildMatricesTests
    {
        private static SizeFormat MockGetInitializeMatrix(int size)
        {
            if (size >= 2 && size <= 1000)
            {
               var SizeString = "{\"Value\": 2, \"Cause\": null, \"Success\": true}";
               return JsonSerializer.Deserialize<SizeFormat>(SizeString);
            }
            else
            {
               var SizeString = "{\"Value\": 0, \"Cause\": \"Size must be within [2..1000]\", \"Success\": false}";
                return JsonSerializer.Deserialize<SizeFormat>(SizeString);
            }
        }

        public static ArrayFormat MockGetRowCol(string dataset, string type, int idx)
        {
            if (idx >= 0 && idx < 1000 && (dataset == "A" || dataset == "B") && (type == "row" || type == "col"))
            {
                var ArrayString = "{\"Value\": [1, 2], \"Cause\": null, \"Success\": true}";
                return JsonSerializer.Deserialize<ArrayFormat>(ArrayString);
            }
            else
            {
                var ArrayString = "{\"Value\": null, \"Cause\": \"Invalid Parameter\", \"Success\": false}";
                return JsonSerializer.Deserialize<ArrayFormat>(ArrayString);
            }
        }

        [Test]
        public async Task BuildMatrixA_Success()
        {
            var initResponse = MockGetInitializeMatrix(2);
            var jsonString = JsonSerializer.Serialize(initResponse);

            if (initResponse.Success)
            {
                var tasks = Enumerable.Range(0, 2).Select(x => Task.Run(() => MockGetRowCol("A", "row", x)));
                var completedTasks = await Task.WhenAll(tasks);
                var rows = new List<Vector<double>>();

                foreach (var content in completedTasks)
                {
                    var data = content;
                    rows.Add(Vector<double>.Build.Dense(data!.Value.Count, x => data.Value[x]));
                }

                var matrix = Matrix<double>.Build.Dense(rows.Count, 2, rows.SelectMany(row => row).ToArray());

                Assert.That(matrix[0, 0], Is.EqualTo(1));
                Assert.That(matrix[1, 0], Is.EqualTo(2));
                Assert.That(matrix[0, 1], Is.EqualTo(1));
                Assert.That(matrix[1, 1], Is.EqualTo(2));
            }
            else
            {
                
                Console.WriteLine($"Error getting initial data: {initResponse.Cause}");
            }
        }
    
        [Test]
        public async Task BuildMatrixB_Success()
        {
            var initResponse = MockGetInitializeMatrix(2);
            var jsonString = JsonSerializer.Serialize(initResponse);

            if (initResponse.Success)
            {
                var tasks = Enumerable.Range(0, 2).Select(x => Task.Run(() => MockGetRowCol("B", "row", x)));
                var completedTasks = await Task.WhenAll(tasks);
                var rows = new List<Vector<double>>();

                foreach (var content in completedTasks)
                {
                    var data = content;
                    rows.Add(Vector<double>.Build.Dense(data!.Value.Count, x => data.Value[x]));
                }

                var matrix = Matrix<double>.Build.Dense(rows.Count, 2, rows.SelectMany(row => row).ToArray());

                Assert.That(matrix[0, 0], Is.EqualTo(1));
                Assert.That(matrix[1, 0], Is.EqualTo(2));
                Assert.That(matrix[0, 1], Is.EqualTo(1));
                Assert.That(matrix[1, 1], Is.EqualTo(2));
            }
            else
            {
                
                Console.WriteLine($"Error getting initial data: {initResponse.Cause}");
            }
        }

        [Test]
        public void GetInitializeMatrix_Fail()
        {
            var initResponse = MockGetInitializeMatrix(1);
            ClassicAssert.IsFalse(initResponse.Success);
            Assert.That(initResponse.Cause, Is.EqualTo("Size must be within [2..1000]"));
        }
        [Test]
        public void GetInitializeMatrix_Success()
        {
            var initResponse = MockGetInitializeMatrix(2);
            Assert.That(initResponse.Success, Is.True);
            Assert.That(initResponse.Value, Is.EqualTo(2));
        }

        [Test]
        public void MockGetRowCol_Success()
        {
        var Array = MockGetRowCol("A", "row", 2);
        Assert.That(Array.Success, Is.True);
        var expectedList = new List<int> { 1, 2 };
        Assert.That(Array.Value, Is.EqualTo(expectedList));
        }
       

        [Test]
        public void MockGetRowCol_Fail()
        {
            var Array = MockGetRowCol("C", "row", 2);
            Assert.That(Array.Success, Is.False);
            Assert.That(Array.Value, Is.Null);
        }     
    public class ArrayFormat
    {
        public required List<int>? Value { get; set; }
        public required string? Cause { get; set; }
        public bool Success { get; set; }
    }

        public class SizeFormat
    {
        public required int Value { get; set; }
        public required string? Cause { get; set; }
        public bool Success { get; set; }
    }
}
}
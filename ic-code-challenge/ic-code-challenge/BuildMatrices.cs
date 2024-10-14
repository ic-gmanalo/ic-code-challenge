using MathNet.Numerics.LinearAlgebra;
using System.Text.Json;

namespace icCodeChallenge
{
public class BuildMatrices
{
    //Task that uses GetMatrices and takes the httpclient and dataset as its inputs 
    public static async Task<Matrix<double>> GetMatrices(IApiHttpClient apiClient, string dataset)
    {
        //asynchronously retrieves the initial matrix size from the api using the size specified in Program.cs
        var InitializeMatrix = await apiClient.GetInitializeMatrix(MatrixSize.Size);
        //Checks if the initalialize api response is successful before proceeding to create a matrix
        if (InitializeMatrix.IsSuccessStatusCode)
        {
            //creates a sequence of intergets from 0 to matrix size - 1, (like a for loop) that will iterate through
            //over the apiClient.GetRowCol GET requests and generates a Task<HttpResponseMessage> object sequence = tasks.
            var tasks = Enumerable.Range(0, MatrixSize.Size).Select(x => apiClient.GetRowCol(dataset, "row", x));
            
            //creates an array of HttpResponseMessage objects, waits for all the tasks to complete and then assigns the to completedTasks
            var completedTasks = await Task.WhenAll(tasks);

            //initializes an empty list to contain the matrix
            var rows = new List<Vector<double>>();

            //iterates over each HttpResponseMessage in completedTasks
            foreach (var result in completedTasks)
            {
                //checks if the http response code is successful, and if not, throws the exeption
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception($"Failed to fetch row/column data from the API. Please verify the dataset name and index values.");
                }
                //reads the content of the responses as a string
                var content = await result.Content.ReadAsStringAsync();
                //deserializes the JSON string into a JSON object that follows the JsonFormat class
                var data = JsonSerializer.Deserialize<JsonFormat>(content);
                //creates a vector double object using MathNets Build.Dense method, then populates the vector with the data.Value
                rows.Add(Vector<double>.Build.Dense(data!.Value.Count, x => data.Value[x]));
            }
            //Builds and returns the Matrix by flattening the rows list and populating it into a matrix
            return Matrix<double>.Build.Dense(MatrixSize.Size, MatrixSize.Size, rows.SelectMany(row => row).ToArray());
        }
        //throws an exception if the InitializeMatrix fails
        throw new Exception($"Could not retrieve initial matrix size from the API. Check the API endpoint and credentials.");
    }
}
}
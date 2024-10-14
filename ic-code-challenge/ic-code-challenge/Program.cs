using System.Text;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
namespace icCodeChallenge
{
//Creates the matrix size
public static class MatrixSize
{
    public const int Size = 1000;
}

class Program
{

//the main code that calls all of the needed methods and classes
public static async Task Main()
{
    //creates a new ServiceCollecton instance
    var services = new ServiceCollection();
    //registers the services to create only 1 instance
    services.AddSingleton<IValidate, Validate>();
    //registers the ApiHttpClient class as a singleton, creates a factory method 
    //that is used to construct the apiHttpclient instance
    services.AddSingleton<IApiHttpClient, ApiHttpClient>(provider =>
    {
        return new ApiHttpClient(new HttpClient());
        
    });
    //builds an IServiceProvider instance 
    var serviceProvider = services.BuildServiceProvider();

    //assigns the serviceprovider to apiClient 
    var apiClient = serviceProvider.GetRequiredService<IApiHttpClient>();

    //Create the Matrices A and B
    var stopwatch = Stopwatch.StartNew();
    var matrixA = await BuildMatrices.GetMatrices(apiClient, "A");
    var matrixB = await BuildMatrices.GetMatrices(apiClient, "B");
    stopwatch.Stop();
    Console.WriteLine("Time to create matrices: " + stopwatch.ElapsedMilliseconds + " ms");


    //Multiply Matrices A and B to get C
    stopwatch.Restart();
    var matrixC = MultiplyMatrices.Multiply(matrixA, matrixB);
    stopwatch.Stop();
    Console.WriteLine("Time to multiply matrices: " + stopwatch.ElapsedMilliseconds + " ms");

    // Concatenate MatrixC: row1row2row3row4row5row6row7...rowN
    stopwatch.Restart();
    string concatenatedString = new StringBuilder().AppendJoin("", matrixC.EnumerateRows().SelectMany(row => row).Select(value => value.ToString())).ToString();

    // Generate MD5 hash
    string md5Hash = MD5Hash.GenerateMD5Hash(concatenatedString);
    stopwatch.Stop();
    Console.WriteLine("Time to create hash: " + stopwatch.ElapsedMilliseconds + " ms");
    Console.WriteLine("MD5 Hash: " + md5Hash);

    // Validate the MD5 hash
    var Validate = serviceProvider.GetRequiredService<IValidate>();
    await Validate.ValidateHash(md5Hash);
    Console.WriteLine();
    }
}
}
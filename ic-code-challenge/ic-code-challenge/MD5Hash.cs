using System.Security.Cryptography;
using System.Text;

namespace icCodeChallenge
{
public static class MD5Hash
{
    //creates the Hash class
    public static string GenerateMD5Hash(string input)
        {
            //sends in the concatenated matrix, and Hashes it using the MD5 method
            byte[] data = MD5.HashData(Encoding.UTF8.GetBytes(input));
            //Console.WriteLine(BitConverter.ToString(data));/
            //takes the returned bytes array and converts it to it ASCII equivalent, and further concatenates them to one string
            return data.Aggregate(new StringBuilder(), (s, i) => s.Append(i)).ToString();
        }
        
}
    //defines an instance for the POST validate api
    public class Validate : IValidate
    {
    private readonly IApiHttpClient _apiClient;

    public Validate(IApiHttpClient apiClient)
    {
        _apiClient = apiClient;
    }

    //a task that call ValidateHash and sends in the md5Hash string as a json object without the brackets
    public async Task ValidateHash(string md5Hash)
    {
        string jsonData = $"\"{md5Hash}\"";

        var body = new StringContent(jsonData, Encoding.UTF8, "application/json");

        
        var response = await _apiClient.PostValidate(body);

       //checks that the api was successful
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
        }
        else
        {
            Console.WriteLine($"Hash validation failed: {response.StatusCode}");
        }
    }
    }

    public interface IValidate
    {
        Task ValidateHash(string md5Hash);
    }
}
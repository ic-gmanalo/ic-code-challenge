namespace icCodeChallenge
{
//this is pretty straightforward, contains the apis
public class ApiHttpClient : IApiHttpClient
{
    private readonly HttpClient _httpClient;

    public ApiHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> GetInitializeMatrix(int size)
    {
        return await _httpClient.GetAsync($"https://recruitment-test.investcloud.com/api/numbers/init/size?size={size}");
    }

    public async Task<HttpResponseMessage> GetRowCol(string dataset, string type, int idx)
    {
        return await _httpClient.GetAsync($"https://recruitment-test.investcloud.com/api/numbers/dataset/type/idx?dataset={dataset}&type={type}&idx={idx}");
    }

    public async Task<HttpResponseMessage> PostValidate(StringContent body)
    {
        return await _httpClient.PostAsync($"https://recruitment-test.investcloud.com/api/numbers/validate", body);
    }
}
public class JsonFormat
{
    public required List<int> Value { get; set; }
    public required string Cause { get; set; }
    public bool Success { get; set; }

}
public interface IApiHttpClient
{
    Task<HttpResponseMessage> GetInitializeMatrix(int size);
    Task<HttpResponseMessage> GetRowCol(string dataset, string type, int idx);
    Task<HttpResponseMessage> PostValidate(StringContent body);
}

}
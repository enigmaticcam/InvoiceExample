namespace Invoice_BlazorWASM.Services.Core;

public partial interface IClient
{
    public HttpClient HttpClient { get; }
}

public partial class Client
{
    [ActivatorUtilitiesConstructor]
    public Client(System.Net.Http.HttpClient httpClient, IAPIConnection connection)
    {
        _httpClient = httpClient;
        httpClient.BaseAddress = new Uri(connection.URI);
        httpClient.Timeout = TimeSpan.FromSeconds(300);
        Initialize();
    }

    public HttpClient HttpClient => _httpClient;
}

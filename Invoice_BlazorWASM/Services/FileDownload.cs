using Invoice_BlazorWASM.Services.Core;
using Microsoft.JSInterop;

namespace Invoice_BlazorWASM.Services;

public interface IFileDownload
{
    Task Download(string uri, string fileName);
}

public class FileDownload : IFileDownload
{
    private IClient _client;
    private IJSRuntime _js;

    public FileDownload(IClient client, IJSRuntime js)
    {
        _client = client;
        _js = js;
    }

    public async Task Download(string uri, string fileName)
    {
        var fullUri = _client.HttpClient.BaseAddress?.ToString() + uri;
        var response = await _client.HttpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        var bytes = await response.Content.ReadAsByteArrayAsync();
        await _js.InvokeVoidAsync("downloadFileFromBytes", fileName, bytes);
    }
}

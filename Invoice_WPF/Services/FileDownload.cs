using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace Invoice_WPF.Services;

public interface IFileDownload
{
    Task Download(string uri, string location, bool? openAfterDownload = false);
}

public class FileDownload : IFileDownload
{
    private HttpClient _client;

    public FileDownload(HttpClient client)
    {
        _client = client;
    }

    public async Task Download(string uri, string location, bool? openAfterDownload = false)
    {
        var fullUri = _client.BaseAddress?.ToString() + uri;
        using var stream = await _client.GetStreamAsync(fullUri);
        using var target = new FileStream(location, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true);
        await stream.CopyToAsync(target);
        if (openAfterDownload.HasValue && openAfterDownload.Value)
        {
            var process = new ProcessStartInfo
            {
                FileName = location,
                UseShellExecute = true
            };
            Process.Start(process);
        }
    }
}

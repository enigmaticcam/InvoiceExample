using Invoice_Logic.Data.DTOs;
using Microsoft.Extensions.Options;

namespace Invoice_Logic.Servers;

public interface IWebServer
{
    Task<string> AllocateFile();
    WebServerDTO Get();
}

public class WebServer : IWebServer
{
    private IOptions<WebServerDTO> _localServer;

    public WebServer(IOptions<WebServerDTO> localServer)
    {
        _localServer = localServer;
    }

    private string LocalDirectory => _localServer?.Value.LocalDirectory ?? "";
    private int PurgeInDays => _localServer?.Value.PurgeInDays ?? 7;

    public async Task<string> AllocateFile()
    {
        await PurgeFiles(
            location: LocalDirectory,
            purgeInDays: PurgeInDays);
        var name = Guid.NewGuid();
        return Path.Combine(LocalDirectory, name.ToString());
    }

    public WebServerDTO Get()
    {
        return new WebServerDTO(LocalDirectory, PurgeInDays);
    }

    private Task PurgeFiles(string location, int purgeInDays)
    {
        return Task.Run(() =>
        {
            var directory = new DirectoryInfo(location);
            var files = directory.GetFiles().OrderBy(x => x.CreationTime);
            foreach (var file in files)
            {
                if (file.CreationTime < DateTime.Now.AddDays(purgeInDays * -1))
                {
                    file.Delete();
                }
                else
                {
                    break;
                }
            }
        });
    }
}

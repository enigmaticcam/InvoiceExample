namespace Invoice_Logic.Data.DTOs;

//public record WebServerDTO(
//    string LocalDirectory,
//    int PurgeInDays
//);

public class WebServerDTO
{
    public WebServerDTO() { }
    public WebServerDTO(string localDirectory, int purgeInDays)
    {
        LocalDirectory = localDirectory;
        PurgeInDays = purgeInDays;
    }

    public string LocalDirectory { get; set; } = "";
    public int PurgeInDays { get; set; }
}
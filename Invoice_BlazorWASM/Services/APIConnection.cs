namespace Invoice_BlazorWASM.Services;

public interface IAPIConnection
{
    string URI { get; }
    string Name { get; }
}

public class APIConnectionDev : IAPIConnection
{
    public string URI => "https://localhost:7206";

    public string Name => "DEV";
}

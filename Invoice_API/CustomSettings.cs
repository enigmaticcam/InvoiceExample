using Invoice_Logic.API;

namespace Invoice_API;

public class CustomSettings : ICustomSettings
{
    private IConfiguration _configuration;

    public CustomSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string ConnectionString
    {
        get
        {
            var value = _configuration.GetConnectionString("Default");
            if (value == null)
            {
                throw new NullReferenceException("Default Connection String cannot be null");
            }
            return value;
        }
    }
}

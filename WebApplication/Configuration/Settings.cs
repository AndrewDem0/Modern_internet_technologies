namespace WebApplication.Configuration


{

    public class WebAppConfiguration
    {
        public LoggingConfig? Logging { get; set; }
        public string? AllowedHosts { get; set; }

        public ConnectionStringsConfig? ConnectionStrings { get; set; }
        public string? ApplicationName { get; set; }
        public MySpecificSettingConfig? MySpecificSetting { get; set; }
    }

    public class LoggingConfig
    {
        public LogLevelConfig? LogLevel { get; set; }
    }

    public class LogLevelConfig
    {
        public string? Default { get; set; }
        public string? MicrosoftAspNetCore { get; set; }
    }

    public class ConnectionStringsConfig
    {
        public string? DefaultConnection { get; set; }
    }

    public class MySpecificSettingConfig
    {
        public string? ApiUrl { get; set; }
        public string? ApiKey { get; set; }
    }
}
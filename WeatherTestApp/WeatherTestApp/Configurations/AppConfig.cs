using WeatherTestApp.Database;

namespace WeatherTestApp.Configurations
{
    public class AppConfig
    {
        public static AppConfig Instance { get; } = new AppConfig();

        public LocalDatabase LocalDatabase { get; set; } = new LocalDatabase();
    }
}
using SQLite;

namespace WeatherTestApp.Database.Entities
{
    public class TownEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Town { get; set; }
    }
}
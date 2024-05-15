namespace GlobalThings.Domain.Configuration
{
    public class MonitorConfiguration
    {
        public Databasesettings DatabaseSettings { get; set; }
    }

    public class Databasesettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

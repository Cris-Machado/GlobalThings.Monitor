namespace GlobalThings.Domain.Configuration
{
    public class MonitorConfiguration
    {
        public Databasesettings DatabaseSettings { get; set; }
        public Emailsettings Emailsettings { get; set; }
        public Smtpsettings SmtpSettings { get; set; }
    }

    public class Databasesettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public class Smtpsettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Emailsettings
    {
        public string FromAddress { get; set; }
        public string FromName { get; set; }
        public string ToAddress { get; set; }
        public string Subject { get; set; }
    }

}

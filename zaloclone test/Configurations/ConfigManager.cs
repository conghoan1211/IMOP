namespace zaloclone_test.Configurations
{
    public class ConfigManager
    {
        private static ConfigManager _instance;
        private readonly IConfiguration _configuration;

        public string EmailDisplayName { get; set; }
        public string EmailHost { get; set; }
        public string EmailUsername { get; set; }
        public string EmailPassword { get; set; }

        public static ConfigManager gI()
        {
            return _instance;
        }

        public static void CreateManager(IConfiguration configuration)
        {
            _instance = null;
            _instance = new ConfigManager(configuration);
        }

        public ConfigManager(IConfiguration configuration)
        {
            _configuration = configuration;
            Init();
        }

        public void Init()
        {
            try
            {
                EmailDisplayName = _configuration.GetSection("EmailSettings").GetSection("EmailDisplayName").Value;
                EmailHost = _configuration.GetSection("EmailSettings").GetSection("EmailHost").Value;
                EmailUsername = _configuration.GetSection("EmailSettings").GetSection("EmailUsername").Value;
                EmailPassword = _configuration.GetSection("EmailSettings").GetSection("EmailPassword").Value;
            }
            catch (Exception e)
            {
                EmailDisplayName = "Interactive Messaging Online Platform";
                EmailHost = "smtp.gmail.com";
                EmailUsername = "hoanpche170404@fpt.edu.vn";
                EmailPassword = "icfswakmfidrsfgi";
            }
        }
    }
}

namespace Ecommerce_2023.Shared
{
    public class Variables
    {
        public static string connectionString = GetConnectionString();
        public static string GetConnectionString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return configuration.GetSection("ConnectionStrings")["connectionStr_ecommerce_rs1"];
        }
    }
}

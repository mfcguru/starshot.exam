
namespace Starshot.Api
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string HardcodedUsername { get; set; }
        public string HardcodedPasswword { get; set; }
        public string Secret { get; set; }
        public int TokenExpiration { get; set; }
    }
}

using Starshot.Frontend.Services.Command;

namespace Starshot.Frontend
{
    public class AppSettings
    {
        public string ApiBaseUri { get; set; }
        public CommandServiceType CommandServiceType { get; set; } = CommandServiceType.Api;
        public string HardcodedUsername { get; set; }
        public string HardcodedPasswword { get; set; }
    }
}

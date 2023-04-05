using MassTransit;
using Starshot.Frontend.Services.Api;

namespace Starshot.Frontend.Services.Command
{
    public sealed class CommandServiceFactory
    {
        private readonly IServiceProvider serviceProvider;
        public CommandServiceFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICommandService CreateInstance(CommandServiceType commandServiceType, string token = "")
        {
            switch (commandServiceType)
            {
                case CommandServiceType.Api:
                    if (string.IsNullOrEmpty(token))
                    {
                        throw new ArgumentNullException("Token cannot be null or empty for an API command service", nameof(token));
                    }

                    var apiService = serviceProvider.GetService<IApiService>();
                    return new ApiCommandService(token, apiService);
                    
                case CommandServiceType.MassTransit:
                    var publishEndpoint = serviceProvider.GetService<IPublishEndpoint>();
                    return new MassTransitCommandService(publishEndpoint);

                default:
                    throw new ArgumentException("Invalid command service type was supplied", nameof(commandServiceType));
            }
        }
    }
}

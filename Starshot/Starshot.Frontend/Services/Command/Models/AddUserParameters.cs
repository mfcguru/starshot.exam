﻿
namespace Starshot.Frontend.Services.Command.Models
{
    public record AddUserParameters
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public DateTime TimeIn { get; init; }
        public DateTime timeOut { get; init; }
    }
}

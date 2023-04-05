# Overview
This reporistory contains code that implements Starshot's take home project as part of the screening process for.Net Developer applicants.

# Getting started
## Code-First Migration
To generate the database, please run the following commands:
````
Add-Migration Initial -Context DataContext
Update-Database Initial -Context DataContext
````
NOTE: You may need to change the connection string in appSettings and also the one inside the DataContext class

# Solution Projects
## Starshot.Api
This is the backend implementation and can receive requests via Web API or MassTransit endpoints as both are implemented.

## Starshot.Frontend
This is the frontend impelementation and can only use either Web API or MassTransit. You can specify which in the appSettings. By default it will use the Web API.

```
"AppSettings": {
    "ApiBaseUri": "https://localhost:44319/api",
    "HardcodedUsername": "admin",
    "HardcodedPasswword": "g#wK5;jN]F&x+e>",
    "CommandServiceType": 1
  }
```
To use MassTransit you can change the CommandServiceType to 2.
````
"CommandServiceType": 2
````

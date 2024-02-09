
# DCS BIOS MATRIC Middleware

DCS BIOS MATRIC Middleware is a program which provides integration between DCS World flight simulator and MATRIC utilizing DCS-BIOS to read data from DCS in real time and push it to MATRIC as variables.
This enables MATRIC users to bind control states to actual flightsim state (e.g. indicators, displays etc.). 
DCS BIOS Middleware is based on excellent work done by [charliefoxtwo](https://github.com/charliefoxtwo) DCS-BIOS-Communicator library https://github.com/charliefoxtwo/DCS-BIOS-Communicator.



<img src="https://raw.githubusercontent.com/charliefoxtwo/DCS-BIOS-Communicator/main/DcsBiosCommunicator/resources/airplane.png" alt="DCS BIOS Communicator logo - a vector outline of an airplane" width="150" />

## Features

- Parses data from DCS-BIOS
- Handles pesky UTF-8 symbols
- Might buy you an ice cream sandwich if you're lucky
- Cross platform


## Usage/Examples

```c#
// create a new UDP client for talking to DCS-BIOS
var client = new BiosUdpClient(IPAddress.Parse("239.255.50.10"), 7778, 5010, logger);
client.OpenConnection();

// create your own translator class which implements IBiosTranslator
var biosListener = new BiosListener(client, translator, logger);

// register the json configuration files
var configLocation = "%userprofile%/Saved Games/DCS.openbeta/Scripts/DCS-BIOS/doc/json/";
foreach (var config in await AircraftBiosConfiguration.AllConfigurations(configLocation))
{
    biosListener.RegisterConfiguration(config);
}

// start the listener
biosListener.Start();

```


## Roadmap

- Unit tests


## Acknowledgements

- [Package icon](https://www.flaticon.com/authors/good-ware)
- [readme tools](https://readme.so)
- [badges](https://shields.io)

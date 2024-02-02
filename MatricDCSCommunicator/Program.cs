using DcsBios.Communicator;
using DcsBios.Communicator.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;

namespace MatricDCSCommunicator {
    public class Program {
        static void Main(string[] args) {
            Listen();
            Console.ReadLine();
        }

        static async void Listen() {
            using var loggerFactory = LoggerFactory.Create(builder => {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
            });
            //ILogger logger = loggerFactory.CreateLogger();
            // create a new UDP client for talking to DCS-BIOS
            var client = new BiosUdpClient(IPAddress.Parse("239.255.50.10"), 7778, 5010, loggerFactory.CreateLogger<BiosUdpClient>());
            client.OpenConnection();

            MatricDCSTranslator translator = new MatricDCSTranslator();

            // create your own translator class which implements IBiosTranslator
            var biosListener = new BiosListener(client, translator, loggerFactory.CreateLogger<BiosListener>());

            // register the json configuration files
            var configLocation = "%userprofile%/Saved Games/DCS.openbeta/Scripts/DCS-BIOS/doc/json/";
            //
            //foreach (var config in await AircraftBiosConfiguration.AllConfigurations(configLocation)) {
            //    biosListener.RegisterConfiguration(config);
            //}
            foreach (AircraftBiosConfiguration config in await AircraftBiosConfiguration.AllConfigurations("AircraftAliases.json", null, configLocation)) {
                biosListener.RegisterConfiguration(config);
            }
            // start the listener
            biosListener.Start();
        }
    }
}

using DCSMATRICFeeder.Properties;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DCSMATRICFeeder {
    internal static class Program {
        internal static ILoggerFactory loggerFactory;
        internal static ILogger logger;

        [STAThread]
        static void Main() {
            if (Debugger.IsAttached) {
                Settings.Default.Reset();
            }                
            loggerFactory = LoggerFactory.Create(builder => {
                builder
                .AddConsole()
                .AddDebug();
            });
            logger = loggerFactory.CreateLogger("DCS-BIOS-MATRIC");

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ApplicationConfiguration.Initialize();
            Application.Run(new frmMain());
        }
    }
}
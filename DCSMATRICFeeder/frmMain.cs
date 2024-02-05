using DcsBios.Communicator;
using DcsBios.Communicator.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json.Nodes;

namespace DCSMATRICFeeder {
    public partial class frmMain : Form {

        bool _isRunning = false;
        MiddlewareConfig _appConfig;
        MatricDCSTranslator translator;
        BiosUdpClient biosClient;
        BiosListener biosListener;
        public frmMain() {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e) {
            dlgFindDCS.InitialDirectory = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Saved Games");
            DialogResult result = dlgFindDCS.ShowDialog();
            if (result == DialogResult.OK) {
                txtDCSBiosInstancePath.Text = dlgFindDCS.SelectedPath;
            }
        }

        /// <summary>
        /// Searches the registry and returns detected DCS user directories in Saved Games
        /// </summary>
        /// <returns>List of DCS user directories in Saved Games (full paths)</returns>
        List<string> GetDCSUserPaths() {
            List<string> dcsUserPaths = new List<string>();
            string savedGamesPath = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "saved games");
            if (Directory.Exists(savedGamesPath)) {
                if (Directory.Exists(Path.Combine(savedGamesPath, "DCS"))) {
                    dcsUserPaths.Add(Path.Combine(savedGamesPath, "DCS"));
                }
                if (Directory.Exists(Path.Combine(savedGamesPath, "DCS.openbeta"))) {
                    dcsUserPaths.Add(Path.Combine(savedGamesPath, "DCS.openbeta"));
                }
            }
            return dcsUserPaths;
        }

        /// <summary>
        /// Gets all DCS-BIOS configuration paths
        /// </summary>
        /// <param name="dcsInstallPaths">List of DCS World user directory paths</param>
        /// <returns>List of valid DCS-BIOS configuration paths</returns>
        List<string> GetDCSBiosPaths(List<string> dcsInstallPaths) {
            List<string> dcsBiosConfigPaths = new List<string>();
            foreach (string dcsPath in dcsInstallPaths) {
                //string path = Path.Combine(dcsPath, @"\Scripts\DCS-BIOS\doc\json");
                string path = Path.Combine(dcsPath, "Scripts", "DCS-BIOS", "doc", "json");
                if (Directory.Exists(path)) {
                    dcsBiosConfigPaths.Add(path);
                }
            }
            return dcsBiosConfigPaths;
        }

        (bool, int) GetMatricConfig() {
            int port = 1234;
            bool enabled = false;
            try {
                string matricConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MATRIC", "config", "config.json");
                if (File.Exists(matricConfigPath)) {
                    JsonObject matricConf = (JsonObject)JsonObject.Parse(File.ReadAllText(matricConfigPath));
                    port = matricConf["NET_IntegrationAPIUDPPort"].GetValue<int>();
                    enabled = matricConf["EnableIntegrationAPI"].GetValue<bool>();
                }
                else {
                    throw new Exception($@"MATRIC configuration file not found at ""{matricConfigPath}""");
                }
            }
            catch (Exception e) {
                Program.logger.LogError($"Couldn't read MATRIC configuration {e.Message}");
            }
            return (enabled, port);
        }

        async void ToggleProcessing() {
            if (_isRunning) {
                biosClient.Close();
                biosListener.Stop();

                //biosListener.Dispose();
                translator.UpdateSentNotification -= Translator_UpdateSentNotification;
                translator.UpdateBufferSizeNotification -= Translator_UpdateBufferSizeNotification;
                translator.Dispose();
                _isRunning = false;
                btnStartStop.Text = "START";
                return;
            }

            //check DCS
            if (!Directory.Exists(txtDCSBiosInstancePath.Text)) {
                MessageBox.Show($@"Path ""{txtDCSBiosInstancePath.Text}"" doesn't exist.", "Invalid configuration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            _appConfig.DCSBIOSJsonPath = txtDCSBiosInstancePath.Text;
            _appConfig.ListenPort = (int)txtBIOSListenPort.Value;

            (bool matricIntegrationEnabled, int matricIntegrationPort) = GetMatricConfig();

            if (!matricIntegrationEnabled) {
                MessageBox.Show($@"MATRIC integration is not enabled, please enable it in MATRIC settings and restart MATRIC Server", "MATRIC configuration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // create a new UDP client for talking to DCS-BIOS
            biosClient = new BiosUdpClient(IPAddress.Parse(_appConfig.ListenAddress), 7778, _appConfig.ListenPort, Program.loggerFactory.CreateLogger<BiosUdpClient>());
            biosClient.OpenConnection();
            translator = new MatricDCSTranslator(matricIntegrationPort, Program.logger);
            translator.UpdateSentNotification += Translator_UpdateSentNotification;
            translator.UpdateBufferSizeNotification += Translator_UpdateBufferSizeNotification;

            biosListener = new BiosListener(biosClient, translator, Program.loggerFactory.CreateLogger<BiosListener>());

            foreach (AircraftBiosConfiguration config in await AircraftBiosConfiguration.AllConfigurations("AircraftAliases.json", null, _appConfig.DCSBIOSJsonPath)) {
                biosListener.RegisterConfiguration(config);
            }

            //Save settings
            SaveSettings();

            // start the listener
            biosListener.Start();
            _isRunning = true;
            btnStartStop.Text = "STOP";
        }

        private void Translator_UpdateBufferSizeNotification(object? sender, MatricDCSTranslator.TxRxNotificationEventArgs e) {
            if (pbBuffer.InvokeRequired) {
                pbBuffer.Invoke(new Action(() => pbBuffer.Value = e.Count));
            }
            else {
                pbBuffer.Value = e.Count;
            }
        }

        private void Translator_UpdateSentNotification(object? sender, MatricDCSTranslator.TxRxNotificationEventArgs e) {
            if (pbSentVars.InvokeRequired) {
                pbSentVars.Invoke(new Action(() => pbSentVars.Value = e.Count));
            }
            else {
                pbSentVars.Value = e.Count;
            }
        }

        private void btnStartStop_Click(object sender, EventArgs e) {
            ToggleProcessing();
        }

        private void frmMain_Load(object sender, EventArgs e) {
            LoadSettings();
            //Populate list of DCS-BIOS instances
            List<string> biosPaths = GetDCSBiosPaths(GetDCSUserPaths());
            foreach (string biosPath in biosPaths) {
                txtDCSBiosInstancePath.Items.Add(biosPath);
            }
            if (Directory.Exists(_appConfig.DCSBIOSJsonPath)) {
                txtDCSBiosInstancePath.Text = _appConfig.DCSBIOSJsonPath;
            }
            else {
                if (biosPaths.Count > 0) {
                    txtDCSBiosInstancePath.Text = biosPaths.FirstOrDefault();
                }
            }
        }

        private void LoadSettings() {
            _appConfig = new MiddlewareConfig() {
                ListenAddress = Properties.Settings.Default.ListenAddress,
                ListenPort = Properties.Settings.Default.ListenPort,
                DCSBIOSJsonPath = Properties.Settings.Default.DCSBIOSJsonPath
            };
            txtBIOSListenPort.Value = _appConfig.ListenPort;
            txtListenIp.Text = _appConfig.ListenAddress;
            txtDCSBiosInstancePath.Text = _appConfig.DCSBIOSJsonPath;
        }

        private void SaveSettings() {
            Properties.Settings.Default.ListenAddress = _appConfig.ListenAddress;
            Properties.Settings.Default.ListenPort = _appConfig.ListenPort;
            Properties.Settings.Default.DCSBIOSJsonPath = _appConfig.DCSBIOSJsonPath;
            Properties.Settings.Default.Save();
        }

        private void lblBiosListenIP_Click(object sender, EventArgs e) {
            txtListenIp.Focus();
        }

        private void lblBiosListenPort_Click(object sender, EventArgs e) {
            txtBIOSListenPort.Focus();
        }

        private void lblBiosInstance_Click(object sender, EventArgs e) {
            txtDCSBiosInstancePath.Focus();
        }
    }
}

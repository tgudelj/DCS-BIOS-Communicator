using DcsBios.Communicator;
using DcsBios.Communicator.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Security.Policy;
using System.Text.Json.Nodes;

namespace EXM.DBMM {
    public partial class FormMain : Form {

        bool _isRunning = false;
        MatricDCSTranslator translator;
        BiosUdpClient biosClient;
        BiosListener biosListener;
        public FormMain() {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e) {
            dlgFindDCS.InitialDirectory = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Saved Games");
            DialogResult result = dlgFindDCS.ShowDialog();
            if (result == DialogResult.OK) {
                txtDCSBiosInstancePath.Text = dlgFindDCS.SelectedPath;
            }
        }

        private void Translator_UpdateBufferSizeNotification(object? sender, MatricDCSTranslator.TxRxNotificationEventArgs e) {
            //Update only if form is visible and not minimized
            if (!this.Visible || WindowState == FormWindowState.Minimized) { return; }
            if (pbBuffer.InvokeRequired) {
                pbBuffer.Invoke(new Action(() => pbBuffer.Value = e.Count));
            }
            else {
                pbBuffer.Value = e.Count;
            }
        }

        private void Translator_UpdateSentNotification(object? sender, MatricDCSTranslator.TxRxNotificationEventArgs e) {
            //Update only if form is visible and not minimised
            if (!this.Visible || WindowState == FormWindowState.Minimized) { return; }
            if (pbSentVars.InvokeRequired) {
                pbSentVars.Invoke(new Action(() => pbSentVars.Value = e.Count));
            }
            else {
                pbSentVars.Value = e.Count;
            }
        }

        private void frmMain_Load(object sender, EventArgs e) {
            LoadSettings();
            //Populate list of DCS-BIOS instances
            List<string> biosPaths = GetDCSBiosPaths(GetDCSUserPaths());
            foreach (string biosPath in biosPaths) {
                txtDCSBiosInstancePath.Items.Add(biosPath);
            }
            if (Directory.Exists(Program.mwSettings.DCSBIOSJsonPath)) {
                txtDCSBiosInstancePath.Text = Program.mwSettings.DCSBIOSJsonPath;
            }
            else {
                if (biosPaths.Count > 0) {
                    txtDCSBiosInstancePath.Text = biosPaths.FirstOrDefault();
                }
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

        private void LoadSettings() {
            Program.mwSettings = new MiddlewareSettings() {
                ListenAddress = Properties.Settings.Default.ListenAddress,
                ListenPort = Properties.Settings.Default.ListenPort,
                DCSBIOSJsonPath = Properties.Settings.Default.DCSBIOSJsonPath,
                UpdateFrequency = Properties.Settings.Default.UpdateFrequency

            };
            if (string.IsNullOrEmpty(Properties.Settings.Default.AircraftVariables)) {
                Program.mwSettings.AircraftVariables = new Dictionary<string, List<string>>();
            }
            else {
                try {
                    Program.mwSettings.AircraftVariables = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(Properties.Settings.Default.AircraftVariables);
                }
                catch (Exception ex) {
                    MessageBox.Show($@"Could not deserialize AircraftVariable settings", "Load settings exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Program.mwSettings.AircraftVariables = new Dictionary<string, List<string>>();
                }
            }
            txtBIOSListenPort.Value = Program.mwSettings.ListenPort;
            txtListenIp.Text = Program.mwSettings.ListenAddress;
            txtDCSBiosInstancePath.Text = Program.mwSettings.DCSBIOSJsonPath;
            tbUpdateFrequency.Value = Program.mwSettings.UpdateFrequency;
            lblUpdateFrequency.Text = Program.mwSettings.UpdateFrequency.ToString();
            txtDCSBIOSImportPort.Value = Program.mwSettings.ImportPort;
        }

        private void SaveSettings() {
            Properties.Settings.Default.ListenAddress = Program.mwSettings.ListenAddress;
            Properties.Settings.Default.ListenPort = Program.mwSettings.ListenPort;
            Properties.Settings.Default.DCSBIOSImportPort = Program.mwSettings.ImportPort;
            Properties.Settings.Default.DCSBIOSJsonPath = Program.mwSettings.DCSBIOSJsonPath;
            Properties.Settings.Default.AircraftVariables = JsonConvert.SerializeObject(Program.mwSettings.AircraftVariables);
            Properties.Settings.Default.UpdateFrequency = Program.mwSettings.UpdateFrequency;
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

        private void lblUpdatesLabel_Click(object sender, EventArgs e) {
            tbUpdateFrequency.Focus();
        }

        private void btnStartStop_Click(object sender, EventArgs e) {
            ToggleProcessing();
        }

        /// <summary>
        /// Loads all configurations from json files
        /// </summary>
        /// <param name="path">Path to directory containing json files</param>
        private async Task<AircraftBiosConfiguration[]> LoadDCSBiosConfig(string path) {
            AircraftBiosConfiguration[] configurations = await AircraftBiosConfiguration.AllConfigurations("AircraftAliases.json", null, path);
            Program.aircraftBiosConfigurations.Clear();
            foreach (var config in configurations) {
                Program.aircraftBiosConfigurations.Add(config.AircraftName, config);
            }
            return configurations;
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
            Program.mwSettings.DCSBIOSJsonPath = txtDCSBiosInstancePath.Text;
            Program.mwSettings.ListenPort = (int)txtBIOSListenPort.Value;
            Program.mwSettings.ImportPort = (int)txtDCSBIOSImportPort.Value;

            (bool matricIntegrationEnabled, int matricIntegrationPort) = GetMatricConfig();

            if (!matricIntegrationEnabled) {
                MessageBox.Show($@"MATRIC integration is not enabled, please enable it in MATRIC settings and restart MATRIC Server", "MATRIC configuration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // create a new UDP client for talking to DCS-BIOS
            biosClient = new BiosUdpClient(IPAddress.Parse(Program.mwSettings.ListenAddress), Program.mwSettings.ImportPort, Program.mwSettings.ListenPort, Program.loggerFactory.CreateLogger<BiosUdpClient>());
            biosClient.OpenConnection();
            translator = new MatricDCSTranslator(matricIntegrationPort, biosClient, Program.logger);
            translator.UpdateSentNotification += Translator_UpdateSentNotification;
            translator.UpdateBufferSizeNotification += Translator_UpdateBufferSizeNotification;

            biosListener = new BiosListener(biosClient, translator, Program.loggerFactory.CreateLogger<BiosListener>());
            AircraftBiosConfiguration[] aircraftConfigs = await LoadDCSBiosConfig(Program.mwSettings.DCSBIOSJsonPath);
            foreach (AircraftBiosConfiguration config in aircraftConfigs) {
                biosListener.RegisterConfiguration(config);
            }

            SaveSettings();

            // start the listener
            biosListener.Start();
            _isRunning = true;
            btnStartStop.Text = "STOP";
        }

        private async void btnFilterDialog_Click(object sender, EventArgs e) {
            if (!Directory.Exists(txtDCSBiosInstancePath.Text)) {
                MessageBox.Show($@"Path ""{txtDCSBiosInstancePath.Text}"" doesn't exist.", "Invalid configuration", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            _ = await LoadDCSBiosConfig(txtDCSBiosInstancePath.Text);
            FormVariablesConfiguration frmVars = new FormVariablesConfiguration();
            frmVars.ShowDialog();
        }

        private void tbUpdateFrequency_Scroll(object sender, EventArgs e) {
            Program.mwSettings.UpdateFrequency = tbUpdateFrequency.Value;
            lblUpdateFrequency.Text = tbUpdateFrequency.Value.ToString();
        }

        private void manualToolStripMenuItem_Click(object sender, EventArgs e) {
            Process.Start(new ProcessStartInfo() {
                UseShellExecute = true,
                FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DCSMATRICManual.pdf")
            });
        }

        private void projectWebToolStripMenuItem_Click(object sender, EventArgs e) {
            Process.Start(new ProcessStartInfo() {
                UseShellExecute = true,
                FileName = "https://github.com/tgudelj/DCS-BIOS-MATRIC-Middleware"
            });
        }

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e) {
            Process.Start(new ProcessStartInfo() {
                UseShellExecute = true,
                FileName = "https://github.com/tgudelj/DCS-BIOS-MATRIC-Middleware/releases"
            });
        }

        private void exportVariablesListToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog() {
                Title = "Export variables configuration",
                Filter = "JSON files|*.json",
                Multiselect = false,
                DefaultExt = ".json",
                ShowPinnedPlaces = true,
                CheckFileExists = false
            };

            DialogResult dialogResult = ofd.ShowDialog();
            if (dialogResult == DialogResult.OK) {
                string file = ofd.FileName;
                string content = JsonConvert.SerializeObject(Program.mwSettings.AircraftVariables, Formatting.Indented);
                File.WriteAllText(file, content, System.Text.Encoding.UTF8);
            }
        }

        private async void importVariablesListToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog() {
                Title = "Import variables configuration",
                Filter = "JSON files (*.json)|*.json;|All files (*.*)|*.*",
                FilterIndex = 0,
                Multiselect = false,
                DefaultExt = ".json",
                ShowPinnedPlaces = true,
                CheckFileExists = true
            };
            DialogResult dialogResult = ofd.ShowDialog();
            if (dialogResult != DialogResult.OK) {
                return;
            }
            string file = ofd.FileName;
            try { 
                string content = await File.ReadAllTextAsync(file);
                Dictionary<string, List<string>>? dataToMerge = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(content);
                if(dataToMerge ==  null) {
                    throw new Exception($@"Could not deserialize data in {file}");
                }
                foreach(string moduleName in dataToMerge.Keys) {
                    if(!Program.mwSettings.AircraftVariables.ContainsKey(moduleName)) {
                        Program.mwSettings.AircraftVariables.Add(moduleName, new List<string>());
                    }
                    foreach (string variableName in dataToMerge[moduleName]) {
                        if (!Program.mwSettings.AircraftVariables[moduleName].Contains(variableName)) {
                            Program.mwSettings.AircraftVariables[moduleName].Add(variableName);
                        }
                    }
                }
                Properties.Settings.Default.AircraftVariables = JsonConvert.SerializeObject(Program.mwSettings.AircraftVariables);
                Properties.Settings.Default.Save();
            } catch(Exception ex) {
                MessageBox.Show($@"Could not import variables from {file}{Environment.NewLine}{ex.Message}", "Import failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }            
        }
    }
}

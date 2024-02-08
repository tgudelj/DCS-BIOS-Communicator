using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DCSMATRICFeeder {
    public partial class FormVariablesConfiguration : Form {
        internal BindingList<string> _aircraftList = new BindingList<string>();
        internal BindingList<string> _categoriesList = new BindingList<string>();
        internal BindingList<string> _availableVariables = new BindingList<string>();
        internal BindingList<string> _configuredVariables = new BindingList<string>();
        internal Dictionary<string, List<string>> _tempConfig = new Dictionary<string, List<string>>();
        internal string _currentAircraftId;
        const string ALL_ITEMS = "-- ALL --";

        public FormVariablesConfiguration() {
            InitializeComponent();
        }

        /// <summary>
        /// Makes a copy of current setting to _tempSettings so we can discard them if we cancel insted of save
        /// </summary>
        public void LoadTempSettings() {
            _tempConfig = new Dictionary<string, List<string>>();
            foreach (KeyValuePair<string, List<string>> item in Program.mwSettings.AircraftVariables) {
                _tempConfig.Add(item.Key, new List<string>());
                foreach(string varName in  item.Value) {
                    _tempConfig[item.Key].Add(varName);
                }
            }
        }

        /// <summary>
        /// Gets all DCS-BIOS variables in specified category for currently selected aircraft
        /// </summary>
        /// <param name="categoryName">Category name</param>
        /// <returns>List of variables</returns>
        private List<string> GetVariablesForCategory(string categoryName) {
            if (Program.aircraftBiosConfigurations[_currentAircraftId].ContainsKey(categoryName)) {
                return Program.aircraftBiosConfigurations[_currentAircraftId][categoryName].Keys.ToList<string>();
            }

            //Category not found or all items category selected
            if (categoryName == ALL_ITEMS || string.IsNullOrEmpty(categoryName)) {
                List<string> result = new List<string>();
                foreach (string category in Program.aircraftBiosConfigurations[_currentAircraftId].Keys) {
                    result.AddRange(Program.aircraftBiosConfigurations[_currentAircraftId][category].Keys.ToList<string>());
                }
                return result;
            }
            
            //Fall through - return empty list
            return new List<string>();
        }

        private void FormVariablesFilter_Load(object sender, EventArgs e) {
            _aircraftList = new BindingList<string>(Program.aircraftBiosConfigurations.Keys.ToList<string>());
            ddAircraft.DataSource = _aircraftList;
            ddCategory.DataSource = _categoriesList;
            lstAvailableVariables.DataSource = _availableVariables;
            lstConfiguredVariables.DataSource = _configuredVariables;
            LoadTempSettings();
        }

        //Selected aircraft changed
        private void ddAircraft_SelectedIndexChanged(object sender, EventArgs e) {
            if (!Program.aircraftBiosConfigurations.ContainsKey(ddAircraft.SelectedItem.ToString())) {
                return;
            }
            _currentAircraftId = ddAircraft.SelectedItem.ToString();

            //Categories
            ddCategory.SelectedIndex = -1;
            _categoriesList.Clear();
            _categoriesList.Add(ALL_ITEMS);
            foreach (string categoryName in Program.aircraftBiosConfigurations[_currentAircraftId].Keys.ToList<string>()) {
                _categoriesList.Add(categoryName);
            }

            //Configured variables
            _configuredVariables.Clear();
            if (!_tempConfig.ContainsKey(_currentAircraftId)) {
                _tempConfig.Add(_currentAircraftId, new List<string>());
            }
            foreach (string varName in _tempConfig[_currentAircraftId]) {
                _configuredVariables.Add(varName);
            }

            //Available DCS-BIOS variables
            _availableVariables.Clear();
            foreach (string variableName in GetVariablesForCategory(ALL_ITEMS)) {
                if (!_configuredVariables.Contains(variableName)) {
                    _availableVariables.Add(variableName);
                }
            }
        }

        //Selected category changed
        private void ddCategory_SelectedIndexChanged(object sender, EventArgs e) {
            if (ddCategory.SelectedIndex == -1) {
                return;
            }
            _availableVariables.Clear();
            foreach (string variableName in GetVariablesForCategory(ddCategory.SelectedItem.ToString())) {
                if (!_configuredVariables.Contains(variableName)) {
                    _availableVariables.Add(variableName);
                }
            }
        }

        private void btnAddSelected_Click(object sender, EventArgs e) {
            foreach (string varName in lstAvailableVariables.SelectedItems) {
                _configuredVariables.Add(varName);
            }
            foreach (string varName in _configuredVariables) {
                _availableVariables.Remove(varName);
            }
            lstAvailableVariables.SelectedIndex = -1;
            if (!_tempConfig.ContainsKey(_currentAircraftId)) { _tempConfig.Add(_currentAircraftId, new List<string>()); }
            _tempConfig[_currentAircraftId].Clear();
            _tempConfig[_currentAircraftId].AddRange(_configuredVariables);
        }

        private void btnAddAll_Click(object sender, EventArgs e) {
            foreach (string varName in _availableVariables) {
                _configuredVariables.Add(varName);
            }
            _availableVariables.Clear();
            lstAvailableVariables.SelectedIndex = -1;
            if (!_tempConfig.ContainsKey(_currentAircraftId)) { _tempConfig.Add(_currentAircraftId, new List<string>()); }
            _tempConfig[_currentAircraftId].Clear();
            _tempConfig[_currentAircraftId].AddRange(_configuredVariables);
        }

        private void btnRemoveAll_Click(object sender, EventArgs e) {
            _configuredVariables.Clear();
            lstConfiguredVariables.SelectedIndex = -1;
            //Restore available variables for selected category
            _availableVariables.Clear();
            foreach (string variableName in GetVariablesForCategory(ddCategory.SelectedItem.ToString())) {
                if (!_configuredVariables.Contains(variableName)) {
                    _availableVariables.Add(variableName);
                }
            }
            if (!_tempConfig.ContainsKey(_currentAircraftId)) { _tempConfig.Add(_currentAircraftId, new List<string>()); }
            _tempConfig[_currentAircraftId].Clear();
            _tempConfig[_currentAircraftId].AddRange(_configuredVariables);
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e) {
            List<string> toRemove = new List<string>();

            foreach (string varName in lstConfiguredVariables.SelectedItems) {
                toRemove.Add(varName);
            }

            foreach (string varName in toRemove) {
                _configuredVariables.Remove(varName);
            }
            //Restore available variables for selected category
            _availableVariables.Clear();
            foreach (string variableName in GetVariablesForCategory(ddCategory.SelectedItem.ToString())) {
                if (!_configuredVariables.Contains(variableName)) {
                    _availableVariables.Add(variableName);
                }
            }
            if (!_tempConfig.ContainsKey(_currentAircraftId)) { _tempConfig.Add(_currentAircraftId, new List<string>()); }
            _tempConfig[_currentAircraftId].Clear();
            _tempConfig[_currentAircraftId].AddRange(_configuredVariables);
        }

        //Filter the variables list
        private void txtAvailableFilter_KeyUp(object sender, KeyEventArgs e) {
            string searchString = txtAvailableFilter.Text;
            if (searchString.Length < 2) {
                _availableVariables.Clear();
                foreach (string variableName in GetVariablesForCategory(ddCategory.SelectedItem.ToString())) {
                    if (!_configuredVariables.Contains(variableName)) {
                        _availableVariables.Add(variableName);
                    }
                }
                return;
            }
            List<string> filteredAvailable = _availableVariables.Where(v => v.ToLower().Contains(searchString.ToLower())).ToList<string>();
            _availableVariables.Clear();
            foreach (string variableName in filteredAvailable) {
                _availableVariables.Add(variableName);
            }
        }

        private void btnClearAvailableFilter_Click(object sender, EventArgs e) {
            txtAvailableFilter.Clear();
            _availableVariables.Clear();
            foreach (string variableName in GetVariablesForCategory(ddCategory.SelectedItem.ToString())) {
                if (!_configuredVariables.Contains(variableName)) {
                    _availableVariables.Add(variableName);
                }
            }
        }

        private void txtConfiguredFilter_KeyUp(object sender, KeyEventArgs e) {
            string searchString = txtConfiguredFilter.Text;
            if (searchString.Length < 2) {
                _configuredVariables.Clear();
                if (!_tempConfig.ContainsKey(_currentAircraftId)) {
                    _tempConfig.Add(_currentAircraftId, new List<string>());
                }

                foreach (string varName in _tempConfig[_currentAircraftId]) {
                    _configuredVariables.Add(varName);
                }
                return;
            }
            List<string> filteredConfigured = _configuredVariables.Where(v => v.ToLower().Contains(searchString.ToLower())).ToList<string>();
            _configuredVariables.Clear();
            foreach (string variableName in filteredConfigured) {
                _configuredVariables.Add(variableName);
            }
        }

        private void btnClearConfiguredFilter_Click(object sender, EventArgs e) {
            txtConfiguredFilter.Clear();
            _configuredVariables.Clear();
            if (!_tempConfig.ContainsKey(_currentAircraftId)) {
                _tempConfig.Add(_currentAircraftId, new List<string>());
            }

            foreach (string varName in _tempConfig[_currentAircraftId]) {
                _configuredVariables.Add(varName);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            //Copy temporary settings to settings that persist
            foreach (var acName in _tempConfig.Keys) {
                if (!Program.mwSettings.AircraftVariables.ContainsKey(acName)) {
                    Program.mwSettings.AircraftVariables.Add(acName, new List<string>());
                }
                Program.mwSettings.AircraftVariables[acName].Clear();
                foreach (string varName in _tempConfig[acName]) {
                    Program.mwSettings.AircraftVariables[acName].Add(varName);
                }
            }
            Properties.Settings.Default.AircraftVariables = JsonConvert.SerializeObject(Program.mwSettings.AircraftVariables);
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void lblAircraft_Click(object sender, EventArgs e) {
            ddAircraft.Focus();
        }

        private void lblCategory_Click(object sender, EventArgs e) {
            ddCategory.Focus();
        }

    }
}

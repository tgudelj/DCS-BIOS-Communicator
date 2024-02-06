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
    public partial class FormVariablesFilter : Form {

        internal List<string> AllVariablesForAircraft = new List<string>();

        public FormVariablesFilter() {
            InitializeComponent();
        }

        private void FormVariablesFilter_Load(object sender, EventArgs e) {
            ddAircraft.DataSource = Program.aircraftBiosConfigurations.Keys.ToList<string>();
            ddAircraft.DisplayMember = "AircraftName";
        }

        private void ddAircraft_SelectedIndexChanged(object sender, EventArgs e) {
            if (!Program.aircraftBiosConfigurations.ContainsKey(ddAircraft.SelectedItem.ToString())) {
                return;
            }
            txtFilter.Text = "";
            foreach (KeyValuePair<string, DcsBios.Communicator.Configuration.BiosCategory> category in Program.aircraftBiosConfigurations[ddAircraft.SelectedItem.ToString()]) {
                foreach (string variableName in category.Value.Keys) {
                    txtFilter.Text += variableName + Environment.NewLine;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}

namespace DCSMATRICFeeder {
    public partial class frmMain : Form {
        public frmMain() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            dlgFindDCS.ShowDialog();
        }
    }
}

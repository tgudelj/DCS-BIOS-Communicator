namespace DCSMATRICFeeder {
    partial class frmMain {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            txtBIOSListenPort = new NumericUpDown();
            label1 = new Label();
            comboBox1 = new ComboBox();
            label2 = new Label();
            trayIcon = new NotifyIcon(components);
            ctxMenu = new ContextMenuStrip(components);
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripMenuItem2 = new ToolStripMenuItem();
            lblDCSBiosConfigStatus = new Label();
            lblUDPListenerStatus = new Label();
            lblDataReceivedStatus = new Label();
            btnStartStop = new Button();
            lblMATRICStatus = new Label();
            pictureBox1 = new PictureBox();
            dlgFindDCS = new FolderBrowserDialog();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)txtBIOSListenPort).BeginInit();
            ctxMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtBIOSListenPort
            // 
            txtBIOSListenPort.Location = new Point(178, 57);
            txtBIOSListenPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            txtBIOSListenPort.Name = "txtBIOSListenPort";
            txtBIOSListenPort.Size = new Size(146, 23);
            txtBIOSListenPort.TabIndex = 2;
            txtBIOSListenPort.Value = new decimal(new int[] { 5010, 0, 0, 0 });
            // 
            // label1
            // 
            label1.Location = new Point(17, 20);
            label1.Name = "label1";
            label1.Size = new Size(146, 34);
            label1.TabIndex = 2;
            label1.Text = "Listening on IP (default 239.255.50.10 multicast)";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "239.255.50.10" });
            comboBox1.Location = new Point(17, 57);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(146, 23);
            comboBox1.TabIndex = 1;
            // 
            // label2
            // 
            label2.Location = new Point(178, 20);
            label2.Name = "label2";
            label2.Size = new Size(146, 34);
            label2.TabIndex = 4;
            label2.Text = "Listening port (default 5010)";
            // 
            // trayIcon
            // 
            trayIcon.ContextMenuStrip = ctxMenu;
            trayIcon.Icon = (Icon)resources.GetObject("trayIcon.Icon");
            trayIcon.Text = "notifyIcon1";
            trayIcon.Visible = true;
            // 
            // ctxMenu
            // 
            ctxMenu.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripSeparator1, toolStripMenuItem2 });
            ctxMenu.Name = "ctxMenu";
            ctxMenu.Size = new Size(104, 54);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(103, 22);
            toolStripMenuItem1.Text = "Show";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(100, 6);
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(103, 22);
            toolStripMenuItem2.Text = "Quit";
            // 
            // lblDCSBiosConfigStatus
            // 
            lblDCSBiosConfigStatus.BackColor = Color.IndianRed;
            lblDCSBiosConfigStatus.BorderStyle = BorderStyle.FixedSingle;
            lblDCSBiosConfigStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDCSBiosConfigStatus.ForeColor = Color.White;
            lblDCSBiosConfigStatus.Location = new Point(17, 134);
            lblDCSBiosConfigStatus.Name = "lblDCSBiosConfigStatus";
            lblDCSBiosConfigStatus.Padding = new Padding(12);
            lblDCSBiosConfigStatus.Size = new Size(146, 45);
            lblDCSBiosConfigStatus.TabIndex = 5;
            lblDCSBiosConfigStatus.Text = "DCS-BIOS config";
            lblDCSBiosConfigStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUDPListenerStatus
            // 
            lblUDPListenerStatus.BackColor = Color.LimeGreen;
            lblUDPListenerStatus.BorderStyle = BorderStyle.FixedSingle;
            lblUDPListenerStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUDPListenerStatus.ForeColor = Color.White;
            lblUDPListenerStatus.Location = new Point(17, 194);
            lblUDPListenerStatus.Name = "lblUDPListenerStatus";
            lblUDPListenerStatus.Padding = new Padding(12);
            lblUDPListenerStatus.Size = new Size(146, 45);
            lblUDPListenerStatus.TabIndex = 6;
            lblUDPListenerStatus.Text = "Listening";
            lblUDPListenerStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDataReceivedStatus
            // 
            lblDataReceivedStatus.BackColor = Color.LimeGreen;
            lblDataReceivedStatus.BorderStyle = BorderStyle.FixedSingle;
            lblDataReceivedStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDataReceivedStatus.ForeColor = Color.White;
            lblDataReceivedStatus.Location = new Point(178, 194);
            lblDataReceivedStatus.Name = "lblDataReceivedStatus";
            lblDataReceivedStatus.Padding = new Padding(12);
            lblDataReceivedStatus.Size = new Size(146, 45);
            lblDataReceivedStatus.TabIndex = 7;
            lblDataReceivedStatus.Text = "Data recv";
            lblDataReceivedStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnStartStop
            // 
            btnStartStop.BackColor = Color.Red;
            btnStartStop.FlatStyle = FlatStyle.Flat;
            btnStartStop.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnStartStop.ForeColor = Color.White;
            btnStartStop.Location = new Point(362, 29);
            btnStartStop.Name = "btnStartStop";
            btnStartStop.Size = new Size(100, 51);
            btnStartStop.TabIndex = 3;
            btnStartStop.Text = "START";
            btnStartStop.UseVisualStyleBackColor = false;
            // 
            // lblMATRICStatus
            // 
            lblMATRICStatus.BackColor = Color.IndianRed;
            lblMATRICStatus.BorderStyle = BorderStyle.FixedSingle;
            lblMATRICStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMATRICStatus.ForeColor = Color.White;
            lblMATRICStatus.Location = new Point(178, 134);
            lblMATRICStatus.Name = "lblMATRICStatus";
            lblMATRICStatus.Padding = new Padding(12);
            lblMATRICStatus.Size = new Size(146, 45);
            lblMATRICStatus.TabIndex = 9;
            lblMATRICStatus.Text = "MATRIC";
            lblMATRICStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(354, 19);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(117, 71);
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(43, 261);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 11;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(494, 331);
            Controls.Add(button1);
            Controls.Add(lblMATRICStatus);
            Controls.Add(btnStartStop);
            Controls.Add(lblDataReceivedStatus);
            Controls.Add(lblUDPListenerStatus);
            Controls.Add(lblDCSBiosConfigStatus);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Controls.Add(txtBIOSListenPort);
            Controls.Add(pictureBox1);
            Name = "frmMain";
            Text = "DCS MATRIC feeder";
            ((System.ComponentModel.ISupportInitialize)txtBIOSListenPort).EndInit();
            ctxMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private NumericUpDown txtBIOSListenPort;
        private Label label1;
        private ComboBox comboBox1;
        private Label label2;
        private NotifyIcon trayIcon;
        private ContextMenuStrip ctxMenu;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toolStripMenuItem2;
        private Label lblDCSBiosConfigStatus;
        private Label lblUDPListenerStatus;
        private Label lblDataReceivedStatus;
        private Button btnStartStop;
        private Label lblMATRICStatus;
        private PictureBox pictureBox1;
        private FolderBrowserDialog dlgFindDCS;
        private Button button1;
    }
}

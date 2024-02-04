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
            lblBiosListenIP = new Label();
            txtListenIp = new ComboBox();
            lblBiosListenPort = new Label();
            trayIcon = new NotifyIcon(components);
            ctxMenu = new ContextMenuStrip(components);
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripMenuItem2 = new ToolStripMenuItem();
            btnStartStop = new Button();
            pictureBox1 = new PictureBox();
            dlgFindDCS = new FolderBrowserDialog();
            btnBrowse = new Button();
            txtDCSBiosInstancePath = new ComboBox();
            lblBiosInstance = new Label();
            printPreviewDialog1 = new PrintPreviewDialog();
            ((System.ComponentModel.ISupportInitialize)txtBIOSListenPort).BeginInit();
            ctxMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtBIOSListenPort
            // 
            txtBIOSListenPort.Font = new Font("Segoe UI", 10F);
            txtBIOSListenPort.Location = new Point(178, 57);
            txtBIOSListenPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            txtBIOSListenPort.Name = "txtBIOSListenPort";
            txtBIOSListenPort.Size = new Size(146, 25);
            txtBIOSListenPort.TabIndex = 2;
            txtBIOSListenPort.Value = new decimal(new int[] { 5010, 0, 0, 0 });
            // 
            // lblBiosListenIP
            // 
            lblBiosListenIP.Location = new Point(17, 20);
            lblBiosListenIP.Name = "lblBiosListenIP";
            lblBiosListenIP.Size = new Size(146, 34);
            lblBiosListenIP.TabIndex = 2;
            lblBiosListenIP.Text = "Listening on IP (default 239.255.50.10 multicast)";
            // 
            // txtListenIp
            // 
            txtListenIp.Font = new Font("Segoe UI", 10F);
            txtListenIp.FormattingEnabled = true;
            txtListenIp.Items.AddRange(new object[] { "239.255.50.10" });
            txtListenIp.Location = new Point(17, 57);
            txtListenIp.Name = "txtListenIp";
            txtListenIp.Size = new Size(146, 25);
            txtListenIp.TabIndex = 1;
            txtListenIp.Text = "239.255.50.10";
            // 
            // lblBiosListenPort
            // 
            lblBiosListenPort.Location = new Point(178, 20);
            lblBiosListenPort.Name = "lblBiosListenPort";
            lblBiosListenPort.Size = new Size(146, 34);
            lblBiosListenPort.TabIndex = 4;
            lblBiosListenPort.Text = "Listening port (default 5010)";
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
            // btnStartStop
            // 
            btnStartStop.BackColor = Color.Red;
            btnStartStop.FlatStyle = FlatStyle.Flat;
            btnStartStop.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnStartStop.ForeColor = Color.White;
            btnStartStop.Location = new Point(447, 194);
            btnStartStop.Name = "btnStartStop";
            btnStartStop.Size = new Size(100, 51);
            btnStartStop.TabIndex = 3;
            btnStartStop.Text = "START";
            btnStartStop.UseVisualStyleBackColor = false;
            btnStartStop.Click += btnStartStop_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(439, 184);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(117, 71);
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(477, 118);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 11;
            btnBrowse.Text = "Browse...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // txtDCSBiosInstancePath
            // 
            txtDCSBiosInstancePath.Font = new Font("Segoe UI", 10F);
            txtDCSBiosInstancePath.FormattingEnabled = true;
            txtDCSBiosInstancePath.Location = new Point(17, 118);
            txtDCSBiosInstancePath.Name = "txtDCSBiosInstancePath";
            txtDCSBiosInstancePath.Size = new Size(454, 25);
            txtDCSBiosInstancePath.TabIndex = 12;
            // 
            // lblBiosInstance
            // 
            lblBiosInstance.Location = new Point(17, 93);
            lblBiosInstance.Name = "lblBiosInstance";
            lblBiosInstance.Size = new Size(307, 22);
            lblBiosInstance.TabIndex = 13;
            lblBiosInstance.Text = "DCS-BIOS instance (DCS-BIOS/doc/json)";
            // 
            // printPreviewDialog1
            // 
            printPreviewDialog1.AutoScrollMargin = new Size(0, 0);
            printPreviewDialog1.AutoScrollMinSize = new Size(0, 0);
            printPreviewDialog1.ClientSize = new Size(400, 300);
            printPreviewDialog1.Enabled = true;
            printPreviewDialog1.Icon = (Icon)resources.GetObject("printPreviewDialog1.Icon");
            printPreviewDialog1.Name = "printPreviewDialog1";
            printPreviewDialog1.Visible = false;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(559, 261);
            Controls.Add(lblBiosInstance);
            Controls.Add(txtDCSBiosInstancePath);
            Controls.Add(btnBrowse);
            Controls.Add(btnStartStop);
            Controls.Add(lblBiosListenPort);
            Controls.Add(txtListenIp);
            Controls.Add(lblBiosListenIP);
            Controls.Add(txtBIOSListenPort);
            Controls.Add(pictureBox1);
            Name = "frmMain";
            Text = "DCS-BIOS MATRIC Middleware";
            Load += frmMain_Load;
            ((System.ComponentModel.ISupportInitialize)txtBIOSListenPort).EndInit();
            ctxMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private NumericUpDown txtBIOSListenPort;
        private Label lblBiosListenIP;
        private ComboBox txtListenIp;
        private Label lblBiosListenPort;
        private NotifyIcon trayIcon;
        private ContextMenuStrip ctxMenu;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toolStripMenuItem2;
        private Button btnStartStop;
        private PictureBox pictureBox1;
        private FolderBrowserDialog dlgFindDCS;
        private Button btnBrowse;
        private ComboBox txtDCSBiosInstancePath;
        private Label lblBiosInstance;
        private PrintPreviewDialog printPreviewDialog1;
    }
}

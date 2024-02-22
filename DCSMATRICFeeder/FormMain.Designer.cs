﻿namespace EXM.DBMM {
    partial class FormMain {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            txtBIOSListenPort = new NumericUpDown();
            lblBiosListenIP = new Label();
            txtListenIp = new ComboBox();
            lblBiosListenPort = new Label();
            btnStartStop = new Button();
            dlgFindDCS = new FolderBrowserDialog();
            btnBrowse = new Button();
            txtDCSBiosInstancePath = new ComboBox();
            lblBiosInstance = new Label();
            pbSentVars = new ProgressBar();
            pbBuffer = new ProgressBar();
            label1 = new Label();
            label2 = new Label();
            groupBox1 = new GroupBox();
            btnFilterDialog = new Button();
            lblUpdatesLabel = new Label();
            tbUpdateFrequency = new TrackBar();
            lblUpdateFrequency = new Label();
            lblDCSBIOSImport = new Label();
            txtDCSBIOSImportPort = new NumericUpDown();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exportVariablesListToolStripMenuItem = new ToolStripMenuItem();
            importVariablesListToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            manualToolStripMenuItem = new ToolStripMenuItem();
            projectWebToolStripMenuItem = new ToolStripMenuItem();
            checkForUpdateToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)txtBIOSListenPort).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbUpdateFrequency).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtDCSBIOSImportPort).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // txtBIOSListenPort
            // 
            txtBIOSListenPort.Font = new Font("Segoe UI", 11F);
            txtBIOSListenPort.Location = new Point(436, 137);
            txtBIOSListenPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            txtBIOSListenPort.Name = "txtBIOSListenPort";
            txtBIOSListenPort.Size = new Size(227, 27);
            txtBIOSListenPort.TabIndex = 4;
            txtBIOSListenPort.Value = new decimal(new int[] { 5010, 0, 0, 0 });
            // 
            // lblBiosListenIP
            // 
            lblBiosListenIP.Font = new Font("Segoe UI", 10F);
            lblBiosListenIP.Location = new Point(12, 101);
            lblBiosListenIP.Name = "lblBiosListenIP";
            lblBiosListenIP.Size = new Size(418, 29);
            lblBiosListenIP.TabIndex = 2;
            lblBiosListenIP.Text = "DCS-BIOS listener address (default 239.255.50.10 multicast)";
            lblBiosListenIP.Click += lblBiosListenIP_Click;
            // 
            // txtListenIp
            // 
            txtListenIp.Font = new Font("Segoe UI", 11F);
            txtListenIp.FormattingEnabled = true;
            txtListenIp.Items.AddRange(new object[] { "239.255.50.10" });
            txtListenIp.Location = new Point(436, 101);
            txtListenIp.Name = "txtListenIp";
            txtListenIp.Size = new Size(227, 28);
            txtListenIp.TabIndex = 3;
            txtListenIp.Text = "239.255.50.10";
            // 
            // lblBiosListenPort
            // 
            lblBiosListenPort.FlatStyle = FlatStyle.Flat;
            lblBiosListenPort.Font = new Font("Segoe UI", 10F);
            lblBiosListenPort.Location = new Point(12, 136);
            lblBiosListenPort.Name = "lblBiosListenPort";
            lblBiosListenPort.Size = new Size(418, 29);
            lblBiosListenPort.TabIndex = 4;
            lblBiosListenPort.Text = "DCS-BIOS listener port (default 5010)";
            lblBiosListenPort.Click += lblBiosListenPort_Click;
            // 
            // btnStartStop
            // 
            btnStartStop.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnStartStop.Location = new Point(436, 272);
            btnStartStop.Name = "btnStartStop";
            btnStartStop.Size = new Size(227, 43);
            btnStartStop.TabIndex = 7;
            btnStartStop.Text = "START";
            btnStartStop.UseVisualStyleBackColor = true;
            btnStartStop.Click += btnStartStop_Click;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(588, 58);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 29);
            btnBrowse.TabIndex = 2;
            btnBrowse.Text = "Browse...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // txtDCSBiosInstancePath
            // 
            txtDCSBiosInstancePath.Font = new Font("Segoe UI", 11F);
            txtDCSBiosInstancePath.FormattingEnabled = true;
            txtDCSBiosInstancePath.Location = new Point(12, 58);
            txtDCSBiosInstancePath.Name = "txtDCSBiosInstancePath";
            txtDCSBiosInstancePath.Size = new Size(570, 28);
            txtDCSBiosInstancePath.TabIndex = 1;
            // 
            // lblBiosInstance
            // 
            lblBiosInstance.Font = new Font("Segoe UI", 10F);
            lblBiosInstance.Location = new Point(12, 29);
            lblBiosInstance.Name = "lblBiosInstance";
            lblBiosInstance.Size = new Size(570, 22);
            lblBiosInstance.TabIndex = 13;
            lblBiosInstance.Text = "DCS-BIOS instance (<DCS user folder>\\Scripts\\DCS-BIOS\\doc\\json)";
            lblBiosInstance.Click += lblBiosInstance_Click;
            // 
            // pbSentVars
            // 
            pbSentVars.BackColor = Color.FromArgb(66, 66, 66);
            pbSentVars.ForeColor = Color.FromArgb(244, 157, 15);
            pbSentVars.Location = new Point(74, 389);
            pbSentVars.Maximum = 200;
            pbSentVars.Name = "pbSentVars";
            pbSentVars.Size = new Size(583, 23);
            pbSentVars.Step = 1;
            pbSentVars.Style = ProgressBarStyle.Continuous;
            pbSentVars.TabIndex = 14;
            // 
            // pbBuffer
            // 
            pbBuffer.BackColor = Color.FromArgb(66, 66, 66);
            pbBuffer.ForeColor = Color.FromArgb(244, 157, 15);
            pbBuffer.Location = new Point(74, 360);
            pbBuffer.Maximum = 200;
            pbBuffer.Name = "pbBuffer";
            pbBuffer.Size = new Size(583, 23);
            pbBuffer.Step = 1;
            pbBuffer.Style = ProgressBarStyle.Continuous;
            pbBuffer.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(11, 32);
            label1.Name = "label1";
            label1.Size = new Size(45, 19);
            label1.TabIndex = 16;
            label1.Text = "Buffer";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new Font("Segoe UI", 10F);
            label2.Location = new Point(11, 61);
            label2.Name = "label2";
            label2.Size = new Size(21, 19);
            label2.TabIndex = 17;
            label2.Text = "Tx";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Segoe UI", 10F);
            groupBox1.Location = new Point(12, 328);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(651, 101);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            groupBox1.Text = "Status";
            // 
            // btnFilterDialog
            // 
            btnFilterDialog.Location = new Point(12, 273);
            btnFilterDialog.Name = "btnFilterDialog";
            btnFilterDialog.Size = new Size(227, 43);
            btnFilterDialog.TabIndex = 6;
            btnFilterDialog.Text = "Configure export variables";
            btnFilterDialog.UseVisualStyleBackColor = true;
            btnFilterDialog.Click += btnFilterDialog_Click;
            // 
            // lblUpdatesLabel
            // 
            lblUpdatesLabel.FlatStyle = FlatStyle.Flat;
            lblUpdatesLabel.Font = new Font("Segoe UI", 10F);
            lblUpdatesLabel.Location = new Point(12, 176);
            lblUpdatesLabel.Name = "lblUpdatesLabel";
            lblUpdatesLabel.Size = new Size(371, 29);
            lblUpdatesLabel.TabIndex = 20;
            lblUpdatesLabel.Text = "Update frequency [updates/second]";
            lblUpdatesLabel.Click += lblUpdatesLabel_Click;
            // 
            // tbUpdateFrequency
            // 
            tbUpdateFrequency.Location = new Point(471, 168);
            tbUpdateFrequency.Maximum = 25;
            tbUpdateFrequency.Minimum = 1;
            tbUpdateFrequency.Name = "tbUpdateFrequency";
            tbUpdateFrequency.Size = new Size(192, 45);
            tbUpdateFrequency.TabIndex = 5;
            tbUpdateFrequency.TickStyle = TickStyle.Both;
            tbUpdateFrequency.Value = 1;
            tbUpdateFrequency.Scroll += tbUpdateFrequency_Scroll;
            // 
            // lblUpdateFrequency
            // 
            lblUpdateFrequency.FlatStyle = FlatStyle.Flat;
            lblUpdateFrequency.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblUpdateFrequency.Location = new Point(436, 170);
            lblUpdateFrequency.Name = "lblUpdateFrequency";
            lblUpdateFrequency.Size = new Size(29, 41);
            lblUpdateFrequency.TabIndex = 22;
            lblUpdateFrequency.Text = "00";
            lblUpdateFrequency.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDCSBIOSImport
            // 
            lblDCSBIOSImport.FlatStyle = FlatStyle.Flat;
            lblDCSBIOSImport.Font = new Font("Segoe UI", 10F);
            lblDCSBIOSImport.Location = new Point(12, 212);
            lblDCSBIOSImport.Name = "lblDCSBIOSImport";
            lblDCSBIOSImport.Size = new Size(418, 29);
            lblDCSBIOSImport.TabIndex = 23;
            lblDCSBIOSImport.Text = "DCS-BIOS Import port (default 7778)";
            // 
            // txtDCSBIOSImportPort
            // 
            txtDCSBIOSImportPort.Font = new Font("Segoe UI", 11F);
            txtDCSBIOSImportPort.Location = new Point(436, 213);
            txtDCSBIOSImportPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            txtDCSBIOSImportPort.Name = "txtDCSBIOSImportPort";
            txtDCSBIOSImportPort.Size = new Size(227, 27);
            txtDCSBIOSImportPort.TabIndex = 24;
            txtDCSBIOSImportPort.Value = new decimal(new int[] { 7778, 0, 0, 0 });
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(675, 24);
            menuStrip1.TabIndex = 25;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportVariablesListToolStripMenuItem, importVariablesListToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // exportVariablesListToolStripMenuItem
            // 
            exportVariablesListToolStripMenuItem.Name = "exportVariablesListToolStripMenuItem";
            exportVariablesListToolStripMenuItem.Size = new Size(180, 22);
            exportVariablesListToolStripMenuItem.Text = "Export variables list";
            exportVariablesListToolStripMenuItem.Click += exportVariablesListToolStripMenuItem_Click;
            // 
            // importVariablesListToolStripMenuItem
            // 
            importVariablesListToolStripMenuItem.Name = "importVariablesListToolStripMenuItem";
            importVariablesListToolStripMenuItem.Size = new Size(180, 22);
            importVariablesListToolStripMenuItem.Text = "Import variables list";
            importVariablesListToolStripMenuItem.Click += importVariablesListToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { manualToolStripMenuItem, projectWebToolStripMenuItem, checkForUpdateToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // manualToolStripMenuItem
            // 
            manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            manualToolStripMenuItem.Size = new Size(144, 22);
            manualToolStripMenuItem.Text = "Manual";
            manualToolStripMenuItem.Click += manualToolStripMenuItem_Click;
            // 
            // projectWebToolStripMenuItem
            // 
            projectWebToolStripMenuItem.Name = "projectWebToolStripMenuItem";
            projectWebToolStripMenuItem.Size = new Size(144, 22);
            projectWebToolStripMenuItem.Text = "Project web";
            projectWebToolStripMenuItem.Click += projectWebToolStripMenuItem_Click;
            // 
            // checkForUpdateToolStripMenuItem
            // 
            checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            checkForUpdateToolStripMenuItem.Size = new Size(144, 22);
            checkForUpdateToolStripMenuItem.Text = "Latest release";
            checkForUpdateToolStripMenuItem.Click += checkForUpdateToolStripMenuItem_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(675, 438);
            Controls.Add(lblDCSBIOSImport);
            Controls.Add(txtDCSBIOSImportPort);
            Controls.Add(lblUpdateFrequency);
            Controls.Add(tbUpdateFrequency);
            Controls.Add(lblUpdatesLabel);
            Controls.Add(btnFilterDialog);
            Controls.Add(pbBuffer);
            Controls.Add(pbSentVars);
            Controls.Add(lblBiosInstance);
            Controls.Add(txtDCSBiosInstancePath);
            Controls.Add(btnBrowse);
            Controls.Add(btnStartStop);
            Controls.Add(lblBiosListenPort);
            Controls.Add(txtListenIp);
            Controls.Add(lblBiosListenIP);
            Controls.Add(txtBIOSListenPort);
            Controls.Add(groupBox1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "FormMain";
            Text = "DCS-BIOS MATRIC Middleware";
            Load += frmMain_Load;
            ((System.ComponentModel.ISupportInitialize)txtBIOSListenPort).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbUpdateFrequency).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtDCSBIOSImportPort).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown txtBIOSListenPort;
        private Label lblBiosListenIP;
        private ComboBox txtListenIp;
        private Label lblBiosListenPort;
        private Button btnStartStop;
        private FolderBrowserDialog dlgFindDCS;
        private Button btnBrowse;
        private ComboBox txtDCSBiosInstancePath;
        private Label lblBiosInstance;
        private ProgressBar pbSentVars;
        private ProgressBar pbBuffer;
        private Label label1;
        private Label label2;
        private GroupBox groupBox1;
        private Button btnFilterDialog;
        private Label lblUpdatesLabel;
        private TrackBar tbUpdateFrequency;
        private Label lblUpdateFrequency;
        private Label lblDCSBIOSImport;
        private NumericUpDown txtDCSBIOSImportPort;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exportVariablesListToolStripMenuItem;
        private ToolStripMenuItem importVariablesListToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem manualToolStripMenuItem;
        private ToolStripMenuItem checkForUpdateToolStripMenuItem;
        private ToolStripMenuItem projectWebToolStripMenuItem;
    }
}

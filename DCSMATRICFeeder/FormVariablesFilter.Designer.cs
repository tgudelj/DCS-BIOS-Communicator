namespace DCSMATRICFeeder {
    partial class FormVariablesFilter {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVariablesFilter));
            ddAircraft = new ComboBox();
            lblAircraft = new Label();
            txtFilter = new TextBox();
            label1 = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            btnResetVariables = new Button();
            SuspendLayout();
            // 
            // ddAircraft
            // 
            ddAircraft.Font = new Font("Segoe UI", 10F);
            ddAircraft.FormattingEnabled = true;
            ddAircraft.Location = new Point(198, 8);
            ddAircraft.Name = "ddAircraft";
            ddAircraft.Size = new Size(241, 25);
            ddAircraft.TabIndex = 0;
            ddAircraft.SelectedIndexChanged += ddAircraft_SelectedIndexChanged;
            // 
            // lblAircraft
            // 
            lblAircraft.Font = new Font("Segoe UI", 10F);
            lblAircraft.Location = new Point(12, 9);
            lblAircraft.Name = "lblAircraft";
            lblAircraft.Size = new Size(180, 22);
            lblAircraft.TabIndex = 14;
            lblAircraft.Text = "Aircraft name";
            lblAircraft.Click += lblAircraft_Click;
            // 
            // txtFilter
            // 
            txtFilter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtFilter.BorderStyle = BorderStyle.FixedSingle;
            txtFilter.Font = new Font("Segoe UI", 10F);
            txtFilter.Location = new Point(12, 74);
            txtFilter.Multiline = true;
            txtFilter.Name = "txtFilter";
            txtFilter.ScrollBars = ScrollBars.Vertical;
            txtFilter.Size = new Size(786, 377);
            txtFilter.TabIndex = 15;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(12, 49);
            label1.Name = "label1";
            label1.Size = new Size(427, 22);
            label1.TabIndex = 16;
            label1.Text = "Variables to export from DCS-BIOS to MATRIC";
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Font = new Font("Segoe UI", 10F);
            btnSave.Location = new Point(692, 457);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(96, 37);
            btnSave.TabIndex = 17;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Font = new Font("Segoe UI", 10F);
            btnCancel.Location = new Point(590, 457);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(96, 37);
            btnCancel.TabIndex = 18;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnResetVariables
            // 
            btnResetVariables.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnResetVariables.Font = new Font("Segoe UI", 10F);
            btnResetVariables.Location = new Point(590, 9);
            btnResetVariables.Name = "btnResetVariables";
            btnResetVariables.Size = new Size(198, 37);
            btnResetVariables.TabIndex = 19;
            btnResetVariables.Text = "Reset variables";
            btnResetVariables.UseVisualStyleBackColor = true;
            btnResetVariables.Click += btnResetVariables_Click;
            // 
            // FormVariablesFilter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 506);
            Controls.Add(btnResetVariables);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(label1);
            Controls.Add(txtFilter);
            Controls.Add(lblAircraft);
            Controls.Add(ddAircraft);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(816, 545);
            Name = "FormVariablesFilter";
            Text = "Variables Filter";
            Load += FormVariablesFilter_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox ddAircraft;
        private Label lblAircraft;
        private TextBox txtFilter;
        private Label label1;
        private Button btnSave;
        private Button btnCancel;
        private Button btnResetVariables;
    }
}
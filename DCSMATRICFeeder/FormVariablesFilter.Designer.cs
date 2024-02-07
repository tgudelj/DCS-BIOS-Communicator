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
            lblAvailable = new Label();
            lstAvailableVariables = new ListBox();
            lblCategory = new Label();
            ddCategory = new ComboBox();
            lstConfiguredVariables = new ListBox();
            btnAddSelected = new Button();
            btnRemoveSelected = new Button();
            btnAddAll = new Button();
            btnRemoveAll = new Button();
            SuspendLayout();
            // 
            // ddAircraft
            // 
            ddAircraft.DropDownStyle = ComboBoxStyle.DropDownList;
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
            txtFilter.Location = new Point(486, 8);
            txtFilter.Multiline = true;
            txtFilter.Name = "txtFilter";
            txtFilter.ScrollBars = ScrollBars.Vertical;
            txtFilter.Size = new Size(302, 52);
            txtFilter.TabIndex = 15;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 10F);
            label1.Location = new Point(474, 84);
            label1.Name = "label1";
            label1.Size = new Size(302, 22);
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
            // lblAvailable
            // 
            lblAvailable.Font = new Font("Segoe UI", 10F);
            lblAvailable.Location = new Point(12, 84);
            lblAvailable.Name = "lblAvailable";
            lblAvailable.Size = new Size(286, 22);
            lblAvailable.TabIndex = 20;
            lblAvailable.Text = "Available variables";
            // 
            // lstAvailableVariables
            // 
            lstAvailableVariables.FormattingEnabled = true;
            lstAvailableVariables.ItemHeight = 15;
            lstAvailableVariables.Location = new Point(14, 109);
            lstAvailableVariables.Name = "lstAvailableVariables";
            lstAvailableVariables.SelectionMode = SelectionMode.MultiExtended;
            lstAvailableVariables.Size = new Size(343, 334);
            lstAvailableVariables.TabIndex = 21;
            // 
            // lblCategory
            // 
            lblCategory.Font = new Font("Segoe UI", 10F);
            lblCategory.Location = new Point(12, 49);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(180, 22);
            lblCategory.TabIndex = 23;
            lblCategory.Text = "Category";
            lblCategory.Click += lblCategory_Click;
            // 
            // ddCategory
            // 
            ddCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            ddCategory.Font = new Font("Segoe UI", 10F);
            ddCategory.FormattingEnabled = true;
            ddCategory.Location = new Point(198, 49);
            ddCategory.Name = "ddCategory";
            ddCategory.Size = new Size(241, 25);
            ddCategory.TabIndex = 22;
            ddCategory.SelectedIndexChanged += ddCategory_SelectedIndexChanged;
            // 
            // lstConfiguredVariables
            // 
            lstConfiguredVariables.FormattingEnabled = true;
            lstConfiguredVariables.ItemHeight = 15;
            lstConfiguredVariables.Location = new Point(445, 109);
            lstConfiguredVariables.Name = "lstConfiguredVariables";
            lstConfiguredVariables.SelectionMode = SelectionMode.MultiExtended;
            lstConfiguredVariables.Size = new Size(343, 334);
            lstConfiguredVariables.TabIndex = 24;
            // 
            // btnAddSelected
            // 
            btnAddSelected.Font = new Font("Segoe UI", 12F);
            btnAddSelected.Location = new Point(375, 137);
            btnAddSelected.Name = "btnAddSelected";
            btnAddSelected.Size = new Size(46, 46);
            btnAddSelected.TabIndex = 25;
            btnAddSelected.Text = ">";
            btnAddSelected.UseVisualStyleBackColor = true;
            btnAddSelected.Click += btnAddSelected_Click;
            // 
            // btnRemoveSelected
            // 
            btnRemoveSelected.Font = new Font("Segoe UI", 12F);
            btnRemoveSelected.Location = new Point(375, 370);
            btnRemoveSelected.Name = "btnRemoveSelected";
            btnRemoveSelected.Size = new Size(46, 46);
            btnRemoveSelected.TabIndex = 26;
            btnRemoveSelected.Text = "<";
            btnRemoveSelected.UseVisualStyleBackColor = true;
            btnRemoveSelected.Click += btnRemoveSelected_Click;
            // 
            // btnAddAll
            // 
            btnAddAll.Font = new Font("Segoe UI", 12F);
            btnAddAll.Location = new Point(375, 193);
            btnAddAll.Name = "btnAddAll";
            btnAddAll.Size = new Size(46, 46);
            btnAddAll.TabIndex = 27;
            btnAddAll.Text = ">>";
            btnAddAll.UseVisualStyleBackColor = true;
            btnAddAll.Click += btnAddAll_Click;
            // 
            // btnRemoveAll
            // 
            btnRemoveAll.Font = new Font("Segoe UI", 12F);
            btnRemoveAll.Location = new Point(375, 318);
            btnRemoveAll.Name = "btnRemoveAll";
            btnRemoveAll.Size = new Size(46, 46);
            btnRemoveAll.TabIndex = 28;
            btnRemoveAll.Text = "<<";
            btnRemoveAll.UseVisualStyleBackColor = true;
            btnRemoveAll.Click += btnRemoveAll_Click;
            // 
            // FormVariablesFilter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 506);
            Controls.Add(btnRemoveAll);
            Controls.Add(btnAddAll);
            Controls.Add(btnRemoveSelected);
            Controls.Add(btnAddSelected);
            Controls.Add(lstConfiguredVariables);
            Controls.Add(lblCategory);
            Controls.Add(ddCategory);
            Controls.Add(lstAvailableVariables);
            Controls.Add(lblAvailable);
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
        private Label lblAvailable;
        private ListBox lstAvailableVariables;
        private Label lblCategory;
        private ComboBox ddCategory;
        private ListBox lstConfiguredVariables;
        private Button btnAddSelected;
        private Button btnRemoveSelected;
        private Button btnAddAll;
        private Button btnRemoveAll;
    }
}
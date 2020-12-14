namespace HotKeyMgr
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ofdApp = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtKeyName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblKeyComb = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtChar = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtExec = new System.Windows.Forms.TextBox();
            this.txtSK = new System.Windows.Forms.TextBox();
            this.chkExec = new System.Windows.Forms.CheckBox();
            this.chkSK = new System.Windows.Forms.CheckBox();
            this.chkWin = new System.Windows.Forms.CheckBox();
            this.chkShift = new System.Windows.Forms.CheckBox();
            this.chkALT = new System.Windows.Forms.CheckBox();
            this.chkCTRL = new System.Windows.Forms.CheckBox();
            this.dgvCommands = new System.Windows.Forms.DataGridView();
            this.fldShortcut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fldKeyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommands)).BeginInit();
            this.SuspendLayout();
            // 
            // ofdApp
            // 
            this.ofdApp.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.txtKeyName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblKeyComb);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.txtChar);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.txtExec);
            this.groupBox1.Controls.Add(this.txtSK);
            this.groupBox1.Controls.Add(this.chkExec);
            this.groupBox1.Controls.Add(this.chkSK);
            this.groupBox1.Controls.Add(this.chkWin);
            this.groupBox1.Controls.Add(this.chkShift);
            this.groupBox1.Controls.Add(this.chkALT);
            this.groupBox1.Controls.Add(this.chkCTRL);
            this.groupBox1.Location = new System.Drawing.Point(3, 222);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(424, 253);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 6.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(16, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(361, 12);
            this.label6.TabIndex = 32;
            this.label6.Text = "Use the add / remove to add, edit or delete records and Save to commit";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(211, 222);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(72, 23);
            this.btnDelete.TabIndex = 31;
            this.btnDelete.Text = "Remove";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.OnRemoveButtonClicked);
            // 
            // txtKeyName
            // 
            this.txtKeyName.Location = new System.Drawing.Point(12, 75);
            this.txtKeyName.Name = "txtKeyName";
            this.txtKeyName.Size = new System.Drawing.Size(403, 21);
            this.txtKeyName.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Key Name";
            // 
            // lblKeyComb
            // 
            this.lblKeyComb.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKeyComb.Location = new System.Drawing.Point(81, 51);
            this.lblKeyComb.Name = "lblKeyComb";
            this.lblKeyComb.Size = new System.Drawing.Size(327, 19);
            this.lblKeyComb.TabIndex = 29;
            this.lblKeyComb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(230, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "+";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(142, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "+";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "+";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(308, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "+";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(289, 222);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 23);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtChar
            // 
            this.txtChar.Location = new System.Drawing.Point(330, 17);
            this.txtChar.MaxLength = 1;
            this.txtChar.Name = "txtChar";
            this.txtChar.Size = new System.Drawing.Size(26, 21);
            this.txtChar.TabIndex = 5;
            this.txtChar.TextChanged += new System.EventHandler(this.txtChar_TextChanged);
            this.txtChar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChar_KeyPress);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(355, 222);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 222);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Enabled = false;
            this.btnBrowse.Location = new System.Drawing.Point(378, 172);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(37, 23);
            this.btnBrowse.TabIndex = 10;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtExec
            // 
            this.txtExec.Enabled = false;
            this.txtExec.Location = new System.Drawing.Point(12, 172);
            this.txtExec.Name = "txtExec";
            this.txtExec.Size = new System.Drawing.Size(360, 21);
            this.txtExec.TabIndex = 9;
            // 
            // txtSK
            // 
            this.txtSK.Enabled = false;
            this.txtSK.Location = new System.Drawing.Point(12, 122);
            this.txtSK.Name = "txtSK";
            this.txtSK.Size = new System.Drawing.Size(403, 21);
            this.txtSK.TabIndex = 7;
            // 
            // chkExec
            // 
            this.chkExec.AutoSize = true;
            this.chkExec.Location = new System.Drawing.Point(12, 149);
            this.chkExec.Name = "chkExec";
            this.chkExec.Size = new System.Drawing.Size(114, 17);
            this.chkExec.TabIndex = 8;
            this.chkExec.Text = "Run Application";
            this.chkExec.UseVisualStyleBackColor = true;
            this.chkExec.CheckedChanged += new System.EventHandler(this.chkExec_CheckedChanged);
            // 
            // chkSK
            // 
            this.chkSK.AutoSize = true;
            this.chkSK.Location = new System.Drawing.Point(12, 102);
            this.chkSK.Name = "chkSK";
            this.chkSK.Size = new System.Drawing.Size(87, 17);
            this.chkSK.TabIndex = 6;
            this.chkSK.Text = "Send Keys";
            this.chkSK.UseVisualStyleBackColor = true;
            this.chkSK.CheckedChanged += new System.EventHandler(this.chkSK_CheckedChanged);
            // 
            // chkWin
            // 
            this.chkWin.AutoSize = true;
            this.chkWin.Location = new System.Drawing.Point(252, 20);
            this.chkWin.Name = "chkWin";
            this.chkWin.Size = new System.Drawing.Size(50, 17);
            this.chkWin.TabIndex = 4;
            this.chkWin.Text = "WIN";
            this.chkWin.UseVisualStyleBackColor = true;
            this.chkWin.CheckedChanged += new System.EventHandler(this.MaskCheck_CheckedChanged);
            // 
            // chkShift
            // 
            this.chkShift.AutoSize = true;
            this.chkShift.Location = new System.Drawing.Point(164, 20);
            this.chkShift.Name = "chkShift";
            this.chkShift.Size = new System.Drawing.Size(60, 17);
            this.chkShift.TabIndex = 3;
            this.chkShift.Text = "SHIFT";
            this.chkShift.UseVisualStyleBackColor = true;
            this.chkShift.CheckedChanged += new System.EventHandler(this.MaskCheck_CheckedChanged);
            // 
            // chkALT
            // 
            this.chkALT.AutoSize = true;
            this.chkALT.Location = new System.Drawing.Point(96, 19);
            this.chkALT.Name = "chkALT";
            this.chkALT.Size = new System.Drawing.Size(47, 17);
            this.chkALT.TabIndex = 2;
            this.chkALT.Text = "ALT";
            this.chkALT.UseVisualStyleBackColor = true;
            this.chkALT.CheckedChanged += new System.EventHandler(this.MaskCheck_CheckedChanged);
            // 
            // chkCTRL
            // 
            this.chkCTRL.AutoSize = true;
            this.chkCTRL.Location = new System.Drawing.Point(12, 20);
            this.chkCTRL.Name = "chkCTRL";
            this.chkCTRL.Size = new System.Drawing.Size(56, 17);
            this.chkCTRL.TabIndex = 1;
            this.chkCTRL.Text = "CTRL";
            this.chkCTRL.UseVisualStyleBackColor = true;
            this.chkCTRL.CheckedChanged += new System.EventHandler(this.MaskCheck_CheckedChanged);
            // 
            // dgvCommands
            // 
            this.dgvCommands.AllowUserToAddRows = false;
            this.dgvCommands.AllowUserToDeleteRows = false;
            this.dgvCommands.AllowUserToOrderColumns = true;
            this.dgvCommands.AllowUserToResizeRows = false;
            this.dgvCommands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCommands.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCommands.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fldShortcut,
            this.fldKeyName});
            this.dgvCommands.Location = new System.Drawing.Point(3, 3);
            this.dgvCommands.Name = "dgvCommands";
            this.dgvCommands.ReadOnly = true;
            this.dgvCommands.RowHeadersVisible = false;
            this.dgvCommands.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCommands.Size = new System.Drawing.Size(424, 219);
            this.dgvCommands.TabIndex = 0;
            this.dgvCommands.DoubleClick += new System.EventHandler(this.dgvCommands_DoubleClick);
            this.dgvCommands.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvCommands_DataError);
            this.dgvCommands.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvCommands_KeyUp);
            // 
            // fldShortcut
            // 
            this.fldShortcut.HeaderText = "Shortcut";
            this.fldShortcut.Name = "fldShortcut";
            this.fldShortcut.ReadOnly = true;
            // 
            // fldKeyName
            // 
            this.fldKeyName.HeaderText = "Key Name";
            this.fldKeyName.Name = "fldKeyName";
            this.fldKeyName.ReadOnly = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 6.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(128, 151);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(248, 12);
            this.label7.TabIndex = 33;
            this.label7.Text = "right click on the below text box for more options";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 476);
            this.Controls.Add(this.dgvCommands);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.Resize += new System.EventHandler(this.Settings_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommands)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdApp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtChar;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtExec;
        private System.Windows.Forms.TextBox txtSK;
        private System.Windows.Forms.CheckBox chkExec;
        private System.Windows.Forms.CheckBox chkSK;
        private System.Windows.Forms.CheckBox chkWin;
        private System.Windows.Forms.CheckBox chkShift;
        private System.Windows.Forms.CheckBox chkALT;
        private System.Windows.Forms.CheckBox chkCTRL;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvCommands;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblKeyComb;
        private System.Windows.Forms.TextBox txtKeyName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn fldShortcut;
        private System.Windows.Forms.DataGridViewTextBoxColumn fldKeyName;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}
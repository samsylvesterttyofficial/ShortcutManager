namespace HotKeyMgr
{
    partial class ShortcutDisplay
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
            this.dgvShortCuts = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShortCuts)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvShortCuts
            // 
            this.dgvShortCuts.AllowUserToAddRows = false;
            this.dgvShortCuts.AllowUserToDeleteRows = false;
            this.dgvShortCuts.AllowUserToResizeRows = false;
            this.dgvShortCuts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShortCuts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShortCuts.Location = new System.Drawing.Point(0, 0);
            this.dgvShortCuts.Name = "dgvShortCuts";
            this.dgvShortCuts.ReadOnly = true;
            this.dgvShortCuts.RowHeadersVisible = false;
            this.dgvShortCuts.Size = new System.Drawing.Size(335, 198);
            this.dgvShortCuts.TabIndex = 1;
            // 
            // ShortcutDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 198);
            this.Controls.Add(this.dgvShortCuts);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ShortcutDisplay";
            this.Load += new System.EventHandler(this.ShortcutDisplay_Load);
            this.Deactivate += new System.EventHandler(ShortcutDisplay_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShortcutDisplay_FormClosing);
            this.Resize += new System.EventHandler(this.ShortcutDisplay_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShortCuts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvShortCuts;

    }
}
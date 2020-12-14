using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HotKeyMgr
{
    public partial class ShortcutDisplay : Form
    {

        KeyCommandManager manager;
        private string fileName;

        public ShortcutDisplay()
        {
            InitializeComponent();
        }

        public ShortcutDisplay(string fName)
        {
            InitializeComponent();
            manager = new KeyCommandManager(fName);
            fileName = fName;
            this.ShowInTaskbar = false;
            this.TopLevel = true;
        }

        public void ReloadData()
        {
            DataTable dtShortcut = new DataTable();

            dtShortcut.Columns.Add("HotKeyData", typeof(KeyCommand));
            dtShortcut.Columns.Add("Shortcut", typeof(string));
            dtShortcut.Columns.Add("KeyName", typeof(string));

            DataRow _dr = null;

            foreach (var cmdObject in manager.GetCollection())
            {
                _dr = dtShortcut.NewRow();
                _dr["HotKeyData"] = cmdObject;
                _dr["Shortcut"] = cmdObject.ToString();
                _dr["KeyName"] =  (cmdObject.SendKey.Trim() != "" ? cmdObject.SendKey.Trim() : cmdObject.KeyName);

                dtShortcut.Rows.Add(_dr);

            }

            dtShortcut.DefaultView.Sort = "Shortcut";

            dgvShortCuts.DataSource = dtShortcut.DefaultView.ToTable();

            dgvShortCuts.Columns[0].Visible = false;
            dgvShortCuts.Columns[1].Width = (int)(dgvShortCuts.Width * (25.0/ 100.0));
            dgvShortCuts.Columns[2].Width = (int)(dgvShortCuts.Width * (70.0/ 100.0));

            dgvShortCuts.Columns[1].HeaderText = "Shortcut";
            dgvShortCuts.Columns[2].HeaderText = "Key Name";

        }

        private void ShortcutDisplay_Load(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void ShortcutDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void ShortcutDisplay_Resize(object sender, EventArgs e)
        {
            try
            {
                dgvShortCuts.Columns[0].Visible = false;
                dgvShortCuts.Columns[1].Width = (int)(dgvShortCuts.Width * (25.0 / 100.0));
                dgvShortCuts.Columns[2].Width = (int)(dgvShortCuts.Width * (70.0 / 100.0));
                
                dgvShortCuts.Refresh();

            }
            catch (Exception)
            {
                
                throw;
            }
        }


        void ShortcutDisplay_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }


     


    }
}

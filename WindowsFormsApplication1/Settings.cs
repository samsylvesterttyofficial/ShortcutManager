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
    public partial class Settings : Form
    {
        private string fileName;
        KeyCommandManager manager;
        KeyCommand editObject;
        int editRowIndex;
        private bool _updated;
        private bool _editMode = false;

        public Settings()
        {
            InitializeComponent();
        }

        public Settings(string fName)
        {
            InitializeComponent();
            manager = new KeyCommandManager(fName);
            fileName = fName;

        }

        public bool UpdateSettings()
        {
            _updated = false;
            this.ShowDialog();
            return _updated;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            manager.SaveAll();
            _updated = true;
            this.Close();
        }

        private KeyCommand GetSaveObject()
        {
            KeyCommand command = new KeyCommand();

            command.CTRLMask = chkCTRL.Checked;
            command.ShiftMask = chkShift.Checked;
            command.ALTMask = chkALT.Checked;
            command.WINMask = chkWin.Checked;
            command.Key = (Keys)((int)txtChar.Text.ToCharArray()[0]);
            command.SendKey = chkSK.Checked ? txtSK.Text.Trim() : "";
            command.ExecString = chkExec.Checked ? txtExec.Text.Trim() : "";
            command.KeyName = txtKeyName.Text.Trim();

            return command;

        }

        private bool Validation()
        {

            if (txtKeyName.Text.Trim() == "")
            {
                MessageBox.Show("Empty Key Name", "Validation failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtChar.Focus();
                return false;
            }

            if (txtChar.Text.Trim() == "")
            {
                MessageBox.Show("Empty Character", "Validation failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtChar.Focus();
                return false;
            }
            if (!chkSK.Checked && !chkExec.Checked)
            {
                MessageBox.Show("Specify an action to be performed", "Validation failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (!chkSK.Checked) chkSK.Focus();
                else if (!chkExec.Checked) chkExec.Focus();
                return false;
            }
            if (chkSK.Checked && txtSK.Text.Trim() == "")
            {
                MessageBox.Show("Empty Send Keys", "Validation failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSK.Focus();
                return false;
            }
            if (chkExec.Checked && txtExec.Text.Trim() == "")
            {
                MessageBox.Show("Empty Application path", "Validation failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtExec.Focus();
                return false;
            }
            KeyCommand tmpObject = GetSaveObject();
            //if (!CheckDuplication(tmpObject))
            //{
            //    MessageBox.Show("Hotkey Registered Already", "Validation failed",
            //           MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    chkCTRL.Focus();
            //    return false;
            //}



            if (!CheckExistence(tmpObject.ToString(), false, _editMode)) // Hot key exists in deleted records
            {
                if (!tmpObject.CanRegister())
                {
                    MessageBox.Show("Error Registering Hotkey", "Validation failed",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                    chkCTRL.Focus();
                    return false;
                }


            }

            if (CheckExistence(txtKeyName.Text, true, _editMode))
            {
                MessageBox.Show("A Key name already exists.", "Validation failed",
                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                chkCTRL.Focus();
                return false;
            }

            return true;

        }

        private bool CheckDuplication(KeyCommand commandObject)
        {
            var query = from KeyCommand command in manager.KeyCommandCollection
                        where command.ToString() == commandObject.ToString()
                        select command;

            if (query.Count() > 0)
            {
                if (_editMode)
                    return (editObject.ToString() == commandObject.ToString());
                else
                    return false;
            }
            else
                return true;

        }

        private bool CheckExistence(string value, bool checkKeyName, bool editMode)
        {
            var query = from KeyCommand command in manager.GetCollection()
                        where command.ToString() == value
                        select command; ;

            if (checkKeyName)
            {
                query = from KeyCommand command in manager.GetCollection()
                        where command.KeyName.ToString().ToUpper() == value.ToUpper()
                        select command;
            }

            if (editMode && checkKeyName)
            {
                if (query.Count() > 0)
                {

                    List<KeyCommand> kc = query.ToList<KeyCommand>();
                    if (kc.Count > 1)
                        return true;
                    else
                    {
                        return false;
                        //return kc[0].ToString() != manager.KeyCommandCollection[editRowIndex].ToString();
                    }
                }
                else
                    return false;
            }
            else
            {
                return query.Count() > 0;
            }

        }

        private void chkSK_CheckedChanged(object sender, EventArgs e)
        {
            txtSK.Enabled = chkSK.Checked;
        }

        private void chkExec_CheckedChanged(object sender, EventArgs e)
        {
            txtExec.Enabled = btnBrowse.Enabled = chkExec.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtChar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray()[0];
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            ofdApp.FileName = "";
            ofdApp.Filter = "Application Files|*.exe|Batch Files|*.bat|All Files|*.*";
            ofdApp.Title = "Select an application to run";
            ofdApp.ShowDialog();
            if (ofdApp.FileName != "")
                txtExec.Text = ofdApp.FileName;
        }

        

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (Validation()) 
            {
                if (_editMode)
                {
                    int foundIndex = manager.KeyCommandCollection.FindIndex
                                    (
                                        delegate(KeyCommand keyCommand)
                                        {
                                            return keyCommand.KeyName == editObject.KeyName;
                                        }
                                    );

                    if (foundIndex >= 0)
                        manager.KeyCommandCollection[foundIndex] = GetSaveObject();
                }
                else
                    manager.KeyCommandCollection.Add(GetSaveObject());
                _editMode = false;
                _updated = true;
                BindData();
                AssignControls(new KeyCommand());
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            BindData();

            txtExec.ContextMenu = GetTextContextMenu();

        }

        private void BindData()
        {
            dgvCommands.DataSource = null;
            //_noRowAddHandling = true;
            dgvCommands.DataSource = (from KeyCommand kCommand in manager.KeyCommandCollection
                                      orderby kCommand.ToString()
                                      select kCommand).ToList<KeyCommand>();
            //_noRowAddHandling = false;
            FormatGrid();
        }

        private void MaskCheck_CheckedChanged(object sender, EventArgs e)
        {
            ConstructKeyCommandString();
        }

        private void ConstructKeyCommandString()
        {
            KeyCommand command = new KeyCommand();
            if (chkALT.Checked)
                command.ALTMask = true;
            if (chkCTRL.Checked)
                command.CTRLMask = true;
            if (chkShift.Checked)
                command.ShiftMask = true;
            if (chkWin.Checked)
                command.WINMask = true;

            if (txtChar.Text.Trim() != "")
                command.Key = (Keys)((int)txtChar.Text.ToCharArray()[0]);

            if (command.ToString().Trim() == "+")
                lblKeyComb.Text = "";
            else
                lblKeyComb.Text = command.ToString();

            if (lblKeyComb.Text.Trim().EndsWith("+"))
                lblKeyComb.Text = lblKeyComb.Text.Substring(0, lblKeyComb.Text.Length - 2).Trim();

            command = null;


        }

        private void txtChar_TextChanged(object sender, EventArgs e)
        {
            ConstructKeyCommandString();
        }

        private void dgvCommands_DoubleClick(object sender, EventArgs e)
        {
            if (dgvCommands.SelectedCells.Count == 0) return;
            editRowIndex = dgvCommands.SelectedCells[0].RowIndex;

            //if (editRowIndex == 0) return; // Header Row

            editObject = (KeyCommand)dgvCommands.Rows[dgvCommands.SelectedCells[0].RowIndex].DataBoundItem;

            AssignControls(editObject);

            _editMode = true;
        }

        private void AssignControls(KeyCommand commandObject)
        {
            chkALT.Checked = commandObject.ALTMask;
            chkCTRL.Checked = commandObject.CTRLMask;
            chkShift.Checked = commandObject.ShiftMask;
            chkWin.Checked = commandObject.WINMask;
            txtChar.Text = ((char)commandObject.Key).ToString();
            chkSK.Checked = commandObject.SendKey.Trim() != "";
            chkExec.Checked = commandObject.ExecString.Trim() != "";
            txtSK.Text = commandObject.SendKey.Trim();
            txtExec.Text = commandObject.ExecString.Trim();
            txtKeyName.Text = commandObject.KeyName.Trim();

        }

        private void dgvCommands_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveKeyCommand();
            }
            
        }

        private void RemoveKeyCommand()
        {
            if (dgvCommands.SelectedCells.Count > 0)
            {
                int rowIndex = dgvCommands.SelectedCells[0].RowIndex;
                KeyCommand command = (KeyCommand)dgvCommands.Rows[rowIndex].DataBoundItem;
                manager.KeyCommandCollection.Remove(command);
                BindData();
            }
        }

        private void dgvCommands_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private ContextMenu GetTextContextMenu()
        {
            ContextMenu menu = new ContextMenu();

            MenuItem mi = new MenuItem("Application");
            mi.MenuItems.Add(new MenuItem("Open Settings", new EventHandler(textMenuItemClicked)));
            mi.MenuItems[0].Tag = "OPEN_SETTINGS";
            mi.MenuItems.Add(new MenuItem("Display Shortcuts", new EventHandler(textMenuItemClicked)));
            mi.MenuItems[1].Tag = "DISPLAY_SHORTCUT";
            mi.MenuItems.Add(new MenuItem("Exit HotKeyMgr", new EventHandler(textMenuItemClicked)));
            mi.MenuItems[2].Tag = "EXIT_APP";

            menu.MenuItems.Add(mi);

            mi = new MenuItem("Lync");
            mi.MenuItems.Add(new MenuItem("Open Contact for IM", new EventHandler(textMenuItemClicked)));
            mi.MenuItems[0].Tag = "OPEN_LYNC_CONTACT";

            menu.MenuItems.Add(mi);

            mi = new MenuItem("Misc");
            mi.MenuItems.Add(new MenuItem("Calendar", new EventHandler(textMenuItemClicked)));
            mi.MenuItems[0].Tag = "OPEN_OUT_CALENDAR";
            menu.MenuItems.Add(mi);
            mi.MenuItems.Add(new MenuItem("Proxy (Clipboard)", new EventHandler(textMenuItemClicked)));
            mi.MenuItems[1].Tag = "OPEN_PROXY_CLIP";
            menu.MenuItems.Add(mi);
            mi.MenuItems.Add(new MenuItem("Proxy (Clipboard) - Incognito", new EventHandler(textMenuItemClicked)));
            mi.MenuItems[2].Tag = "OPEN_PROXY_CLIP_INCOGNITO";
            menu.MenuItems.Add(mi);
            mi.MenuItems.Add(new MenuItem("Proxy (URL)", new EventHandler(textMenuItemClicked)));
            mi.MenuItems[3].Tag = "OPEN_PROXY_URL";
            menu.MenuItems.Add(mi);
            mi.MenuItems.Add(new MenuItem("Proxy (URL) - Incognito", new EventHandler(textMenuItemClicked)));
            mi.MenuItems[4].Tag = "OPEN_PROXY_URL_INCOGNITO";
            menu.MenuItems.Add(mi);
            return menu;


        }

        protected void textMenuItemClicked(object sender, EventArgs e)
        {
            string selectedCtxtMenu = ((MenuItem)sender).Tag.ToString();
            bool selectText;
            string command = GetExecCommand(selectedCtxtMenu, out selectText);
            if (command.Trim() != "")
            {
                txtExec.Text = command;
                txtExec.Focus();
                if (selectText)
                {
                    if (txtExec.Text.LastIndexOf(":") > 0)
                    {
                        txtExec.SelectionStart = txtExec.Text.LastIndexOf(":") + 1;
                        txtExec.SelectionLength = txtExec.Text.Length - (txtExec.Text.LastIndexOf(":") - 1);
                    }
                }

            }
        }


        private string GetExecCommand(string tag, out bool selecText)
        {
            string execCommand = "";
            selecText = false;
            switch (tag.ToUpper())
            {
                case "OPEN_SETTINGS":
                    execCommand = "<!SETTINGS!>";
                    break;
                case "DISPLAY_SHORTCUT":
                    execCommand = "<!DISP_SHORT!>";
                    break;
                case "EXIT_APP":
                    execCommand = "<!EXIT!>";
                    break;
                case "OPEN_LYNC_CONTACT":
                    execCommand = "<!LYNC!>:CONTACT_NAME";
                    selecText = true;
                    break;
                case "OPEN_PROXY_CLIP":
                    execCommand = "<!PROXY!>:NORMAL:CLIP";
                    break;
                case "OPEN_PROXY_CLIP_INCOGNITO":
                    execCommand = "<!PROXY!>:INCOGNITO:CLIP";
                    break;
                case "OPEN_PROXY_URL":
                    execCommand = "<!PROXY!>:NORMAL:URL:<URL>";
                    selecText = true;
                    break;
                case "OPEN_PROXY_URL_INCOGNITO":
                    execCommand = "<!PROXY!>:INCOGNITO:URL:<URL>";
                    selecText = true;
                    break;
                case "OPEN_OUT_CALENDAR":
                    execCommand = "<!OUTLOOK!>:CAL";
                    break;
            }
            return execCommand;
        }

        private void Settings_Resize(object sender, EventArgs e)
        {

            //dgvCommands.Height = this.Height * (60 / 100);
            //groupBox1.Height = this.Height * (38 / 100);
        }

        private void FormatGrid()
        {

            dgvCommands.Columns[0].Width = Convert.ToInt32(dgvCommands.Width * 0.38);
            dgvCommands.Columns[1].Width = Convert.ToInt32(dgvCommands.Width * 0.57);

            dgvCommands.Columns[2].Visible = false;
            dgvCommands.Columns[3].Visible = false;
            dgvCommands.Columns[4].Visible = false;
            dgvCommands.Columns[5].Visible = false;
            dgvCommands.Columns[6].Visible = false;
            dgvCommands.Columns[7].Visible = false;
            dgvCommands.Columns[8].Visible = false;
            dgvCommands.Columns[9].Visible = false;



            foreach (DataGridViewRow dgvRow in dgvCommands.Rows)
            {

                KeyCommand keyCurrent = (KeyCommand)dgvRow.DataBoundItem;

                if (keyCurrent != null)
                {
                    dgvRow.Cells["fldShortcut"].Value = keyCurrent.ToString();
                    dgvRow.Cells["fldKeyName"].Value = keyCurrent.KeyName;
                }

            }




            //DataGridViewColumn dgvCol = new DataGridViewColumn();
            //dgvCol.Name = "fldShortcut";
            //dgvCol.DisplayIndex = 0;
            //dgvCol.HeaderText = "Shortcut";

            //dgvCommands.Columns.Add(dgvCol);

            //dgvCol = new DataGridViewColumn();
            //dgvCol.Name = "fldKeyName";
            //dgvCol.DisplayIndex = 1;
            //dgvCol.HeaderText = "Key Name";
            //dgvCommands.Columns.Add(dgvCol);


        }

        private void OnRemoveButtonClicked(object sender, EventArgs e)
        {
            RemoveKeyCommand();
        }


    }
}

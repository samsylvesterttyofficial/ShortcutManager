using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.ComponentModel;

namespace HotKeyMgr
{

    public static class Common
    {
        public static Hotkey BindHotKey(KeyCommand keyCommand)
        {
            Hotkey hotKey = new Hotkey(keyCommand.Key, keyCommand.ShiftMask, keyCommand.CTRLMask,
                    keyCommand.ALTMask, keyCommand.WINMask, keyCommand.SendKey, keyCommand.ExecString, keyCommand.KeyName);
            return hotKey;
        }
    }

    public class KeyCommand
    {

        //1<|>1<|>1<|>1<|>32<@>username¿TAB¿password¿TAB¿¿ENTER¿

        #region Declaration
        
        private bool _ctrlMask;
        private bool _altMask;
        private bool _winMask;
        private bool _shiftMask;
        private Keys _key;
        private string _keyName;
        private string _execString;
        private string _sendKey;

        public bool CTRLMask
        {
            get { return _ctrlMask; }
            set { _ctrlMask = value; }
        }
        public bool ALTMask
        {
            get { return _altMask; }
            set { _altMask = value; }
        }
        public bool WINMask
        {
            get { return _winMask; }
            set { _winMask = value; }
        }
        public bool ShiftMask
        {
            get { return _shiftMask; }
            set { _shiftMask = value; }
        }
        public Keys Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public string SendKey
        {
            get { return _sendKey; }
            set { _sendKey = value; }
        }

        public string ExecString
        {
            get { return _execString; }
            set { _execString = value; }
        }


        public string KeyName
        {
            get { return _keyName; }
            set { _keyName = value; }
        }
        #endregion

        public KeyCommand()
        {
            InitEmptyObject();
        }

        public void InitEmptyObject()
        {
            _ctrlMask = false;
            _altMask = false;
            _winMask = false;
            _shiftMask = false;
            _key = Keys.None;
            _sendKey = string.Empty;
            _execString = string.Empty;
            _keyName = string.Empty;
        }

        public KeyCommand(string keyCommandString)
        {
            try
            {
                string[] splitStrings = keyCommandString.Split(new string[] { "<@>" }, StringSplitOptions.None);
                string keyComb = splitStrings[0];
                _sendKey = splitStrings[1];
                _execString = splitStrings[2];
                if (splitStrings.Length == 4)
                    _keyName = splitStrings[3].ToString();
                else
                    _keyName = splitStrings[2].ToString();

                string[] keySep = keyComb.Split(new string[] { "<|>" }, StringSplitOptions.None);
                _ctrlMask = (keySep[0].ToString() == "1");
                _shiftMask = (keySep[1].ToString() == "1");
                _altMask = (keySep[2].ToString() == "1");
                _winMask = (keySep[3].ToString() == "1");
                _key = (Keys)Convert.ToInt16(keySep[4].ToString());
                

            }
            catch (Exception ex)
            {
                InitEmptyObject();
            }

        }

        public override string ToString()
        {
            string keysDesc = "";
            if (_ctrlMask)
                keysDesc = "CTRL";
            if (_altMask)
                keysDesc = keysDesc + (keysDesc.Trim() == "" ? "" : " + ") + "ALT";

            if (_shiftMask)
                keysDesc = keysDesc + (keysDesc.Trim() == "" ? "" : " + ") + "SHIFT";

            if (_winMask)
                keysDesc = keysDesc + (keysDesc.Trim() == "" ? "" : " + ") + "WIN";

            keysDesc = keysDesc + (keysDesc.Trim() == "" ? "" : " + ") + (((char)(int)_key)).ToString();

            return keysDesc;

        }

        public bool CanRegister()
        {
            Hotkey keyObject = Common.BindHotKey(this);
            bool canBeRegistered = keyObject.Register(new Label());
            keyObject.Unregister();
            return canBeRegistered;
        }

        public void Unregister()
        {
            Common.BindHotKey(this).Unregister();
        }

    }

    public class KeyCommandManager
    {
        #region Declaration
        List<KeyCommand> _keyCommandCollection = new List<KeyCommand>();
        private DataTable _keyCommandCollectionTable;
        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        public DataTable KeyCommandCollectionTable
        {
            get { return _keyCommandCollectionTable; }
            set { _keyCommandCollectionTable = value; }
        }

        public List<KeyCommand> KeyCommandCollection
        {
            get { return _keyCommandCollection; }
            set { _keyCommandCollection = value; }
        }

        #endregion
        public KeyCommandManager(string fileName)
        {
            _fileName = fileName;
            ReloadCollections();
        }

        private void ReloadCollections()
        {
            _keyCommandCollection = GetCollection();
            _keyCommandCollectionTable = GetCollectionTable();
        }
        public DataTable GetCollectionTable()
        {
            TextReader reader = new StreamReader(_fileName);

            string readLine = reader.ReadLine();

            DataTable _dt = new DataTable();

            _dt.Columns.Add("CTRL", typeof(string));
            _dt.Columns.Add("SHIFT", typeof(string));
            _dt.Columns.Add("ALT", typeof(string));
            _dt.Columns.Add("WIN", typeof(string));
            _dt.Columns.Add("KeyName", typeof(string));
            _dt.Columns.Add("Send", typeof(string));
            _dt.Columns.Add("App", typeof(string));

            DataRow _dr;

            while (readLine != null)
            {
                KeyCommand cmd = new KeyCommand(readLine);

                _dr = _dt.NewRow();
                _dr["CTRL"] = cmd.CTRLMask ? "YES" : "NO";
                _dr["SHIFT"] = cmd.CTRLMask ? "YES" : "NO";
                _dr["ALT"] = cmd.CTRLMask ? "YES" : "NO";
                _dr["WIN"] = cmd.CTRLMask ? "YES" : "NO";
                _dr["Send"] = cmd.SendKey;
                _dr["App"] = cmd.ExecString;
                _dr["KeyName"] = cmd.KeyName;
                _dt.Rows.Add(_dr);

                readLine = reader.ReadLine();

            }

            reader.Close();

            return _dt;

            

        }
        public List<KeyCommand> GetCollection()
        {
            TextReader reader = new StreamReader(_fileName);
            List<KeyCommand> cmdCollection = new List<KeyCommand>();

            string readLine = reader.ReadLine();


            while (readLine != null)
            {
                KeyCommand cmd = new KeyCommand(readLine);
                if (cmd.Key != Keys.None)
                    cmdCollection.Add(cmd);
                readLine = reader.ReadLine();

            }
            reader.Close();
            return cmdCollection;

        }
        public bool Save(KeyCommand command)
        {
            bool flag = true;
            TextWriter writer = new StreamWriter(_fileName, true);
            try
            {
                string commandString = "";

                commandString =
                        (command.CTRLMask ? "1" : "0") + "<|>" +
                        (command.ShiftMask ? "1" : "0") + "<|>" +
                        (command.ALTMask ? "1" : "0") + "<|>" +
                        (command.WINMask ? "1" : "0") + "<|>" +
                        ((int)command.Key).ToString() + "<@>" +
                        command.SendKey + "<@>" +
                        command.ExecString + "<@>" + 
                        command.KeyName;

                writer.WriteLine(commandString);

            }
            catch
            {
                flag = false;
            }
            finally
            {
                writer.Close();
            }
            return flag;
        }
        public bool SaveAll()
        {
            if (File.Exists(_fileName)) File.Delete(_fileName);
            StreamWriter writer = null;
            if (!File.Exists(_fileName))
            {
                writer = File.CreateText(_fileName);
                writer.Close();
            }
            foreach (KeyCommand command in _keyCommandCollection)
            {
                if (command != null)
                    Save(command);
            }
            ReloadCollections();
            return true;
        }
    }
    

}

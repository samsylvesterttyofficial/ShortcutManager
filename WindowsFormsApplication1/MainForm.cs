using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Timers;
using Microsoft.Win32;

namespace HotKeyMgr
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        ~MainForm()
        {
            commandList = null;
            hotKeyList = null;
            nfyMain.Icon = null;
            nfyMain = null;
        }


        Settings settingForm;


        List<KeyCommand> commandList;
        List<Hotkey> hotKeyList = new List<Hotkey>();
        List<string> menuCommands = new List<string>();

        ShortcutDisplay display;

        KeyCommandManager manager;

        private ContextMenu _CommandMenu;

        private bool canExit;
        private bool balloonExited;

        private static string logPath = Application.StartupPath + "\\HotKey.log";

        private string GetFileName()
        {
            return Application.StartupPath + "\\HotKeys.cnf";
        }

        private void InitEventLog()
        {
            if (!File.Exists(logPath))
            {
                StreamWriter writer = File.CreateText(logPath);
                writer.Close();
            }
        }
        
        private void InitIcon()
        {
            nfyMain.Icon = Properties.Resources.MAIN as Icon;
            nfyMain.Text = "Hot Key Manager";
            System.Windows.Forms.ContextMenu ctxMenu = new ContextMenu();
            MenuItem mi = new MenuItem("Configure HotKey...", new EventHandler(MenuClicked));
            ctxMenu.MenuItems.Add(mi);
            mi = new MenuItem("Display Shortcuts...", new EventHandler(MenuClicked));
            ctxMenu.MenuItems.Add(mi);
            mi = new MenuItem("Exit", new EventHandler(MenuClicked));
            ctxMenu.MenuItems.Add(mi);

            nfyMain.ContextMenu = ctxMenu;

            nfyMain.BalloonTipClicked += new EventHandler(nfyMain_BalloonTipClicked);
            nfyMain.DoubleClick += new EventHandler(nfyMain_DoubleClick);

        }

        void nfyMain_DoubleClick(object sender, EventArgs e)
        {
            OpenSettings();
        }

        private void ShowBalloonMessage(string message, string tag)
        {
            nfyMain.BalloonTipTitle = "Hot Key Manager";
            nfyMain.BalloonTipText = message;
            nfyMain.BalloonTipIcon = ToolTipIcon.Info;
            nfyMain.Tag = tag;
            nfyMain.ShowBalloonTip(3000);
            balloonExited = false;
            tmrBalloon.Interval = 3000;
            tmrBalloon.Enabled = true;
        }

        void nfyMain_BalloonTipClicked(object sender, EventArgs e)
        {

        }


        protected void MenuClicked(object sender, EventArgs e)
        {
            string menu = ((MenuItem)sender).Text;
            if (menu.ToUpper() == "EXIT")
                this.Close();
            else if (menu == "Configure HotKey...")
            {
                OpenSettings();
            }
            else if (menu == "Display Shortcuts...")
            {
                if (display.Visible) display.Hide();
                display.Show();
            }
        }

        private void OpenSettings()
        {
            if (settingForm == null)
            {
                settingForm = new Settings(GetFileName());
                if (settingForm.UpdateSettings())
                    ResetApplication();
                settingForm = null;
            }
            else
            {
                settingForm.BringToFront();
            }

        }

        private void ResetApplication()
        {
            try
            {
                foreach (Hotkey hotKey in hotKeyList)
                {
                    hotKey.Unregister();
                }
            }
            catch (Exception ex)
            {
                WriteEntry("ResetApplication:" + ex.Message);
            }
            ResetService();
        }

        private void WriteEntry(string message)
        {
            string formattedMessage = "[" + DateTime.Now.ToString() + "]" + message;

            TextWriter writer = null;
            try
            {
                writer = new StreamWriter(logPath, true);
                writer.WriteLine(formattedMessage);
            }
            catch
            {

            }
            finally
            {
                writer.Close();
            }


        }

        private void ResetService()
        {
            commandList = GetKeyCommands(GetFileName());
        }

        private void CreateDefaultEntries(string fileName)
        {

            WriteEntry("Creating default entries");

            List<string> commandExpressions = new List<string>();

            KeyCommandManager mgr = new KeyCommandManager(fileName);

            if (File.Exists(Application.StartupPath + "\\PS.EN.DE.exe"))
                commandExpressions.Add("1<|>1<|>1<|>0<|>90<@><@>" + Application.StartupPath + "\\PS.EN.DE.exe<!args!>c<@>Lentracs Decypher Clipboard");

            if (File.Exists(Application.StartupPath + "\\TrackInfo.exe"))
                commandExpressions.Add("1<|>1<|>1<|>0<|>84<@><@>" + Application.StartupPath + "\\TrackInfo.exe<@>Track Info Bug Tracking");

            commandExpressions.Add("0<|>0<|>0<|>1<|>83<@><@><!SETTINGS!><@>App Settings"); // App Settings
            commandExpressions.Add("1<|>1<|>1<|>0<|>68<@><@><!DISP_SHORT!><@>Show Shortcuts"); // Display Shortcuts

            foreach (string sObject in commandExpressions)
                mgr.Save(new KeyCommand(sObject));



        }

        private List<KeyCommand> GetKeyCommands(string fileName)
        {
            StreamWriter writer = null;
            if (!File.Exists(fileName))
            {
                writer = File.CreateText(fileName);
                writer.Close();
                CreateDefaultEntries(fileName);
            }

            hotKeyList = new List<Hotkey>();
            //hotKeyList.RemoveAll(PredicateDelegate);

            manager = new KeyCommandManager(fileName);

            WriteEntry("Binding Keys");

            if (manager.KeyCommandCollection.Count > 0)
            {
                foreach (KeyCommand command in manager.KeyCommandCollection)
                    MaintainHotKey(BindHotKey(command));
            }
            else
            {
                // New conf file creation
                KeyCommand newCommand;
                if (File.Exists(Application.StartupPath + "\\PS.EN.DE.exe"))
                {

                    //E:\Projects\PS.EN.DE\PS.EN.DE\bin\Debug\PS.EN.DE.exe<!args!>c noui
                    //new Hotkey(Keys.Z, true, true, true, false, "", Application.StartupPath + "\\PS.EN.DE.exe<!args!>c noui", "Lentracs Decypher");

                    WriteEntry("Creating Hot Key For Encryptor");
                    newCommand = new KeyCommand();
                    newCommand.KeyName = "Lentracs Decypher";
                    newCommand.Key = Keys.Z;
                    newCommand.CTRLMask = true;
                    newCommand.ALTMask = true;
                    newCommand.ShiftMask = true;
                    newCommand.WINMask = false;
                    newCommand.ExecString = Application.StartupPath + "\\PS.EN.DE.exe<!args!>c noui";
                    newCommand.SendKey = "";

                    manager.Save(newCommand);

                }
                if (File.Exists(Application.StartupPath + "\\TrackInfo.exe"))
                {

                    //E:\Projects\PS.EN.DE\PS.EN.DE\bin\Debug\PS.EN.DE.exe<!args!>c noui
                    //new Hotkey(Keys.Z, true, true, true, false, "", Application.StartupPath + "\\PS.EN.DE.exe<!args!>c noui", "Lentracs Decypher");

                    WriteEntry("Creating Hot Key For TrackInfo");
                    newCommand = new KeyCommand();
                    newCommand.KeyName = "TrackInfo";
                    newCommand.Key = Keys.T;
                    newCommand.CTRLMask = true;
                    newCommand.ALTMask = true;
                    newCommand.ShiftMask = true;
                    newCommand.WINMask = false;
                    newCommand.ExecString = Application.StartupPath + "\\TrackInfo.exe";
                    newCommand.SendKey = "";

                    manager.Save(newCommand);

                }
            }

            return manager.KeyCommandCollection;

            #region Old Commented




            //TextReader reader = new StreamReader(fileName);

            //List<KeyCommand> keyCommandList = new List<KeyCommand>();

            //string readLine = reader.ReadLine();
            //while (readLine != null)
            //{
            //    KeyCommand keyCommandObject = new KeyCommand(readLine);
            //    if (keyCommandObject.Key != Keys.None)
            //    {
            //        hotKeyList.Add(BindHotKey(keyCommandObject));
            //        keyCommandList.Add(keyCommandObject);
            //    }
            //    readLine = reader.ReadLine();
            //}
            //reader.Close();

            //return keyCommandList;

            #endregion
        }


        private void MaintainHotKey(Hotkey keyObject)
        {
            var query =
                from Hotkey key in hotKeyList
                where
                    key.Alt == keyObject.Alt &&
                    key.Control == keyObject.Control &&
                    key.Shift == keyObject.Shift &&
                    key.Windows == keyObject.Windows &&
                    key.KeyCode == keyObject.KeyCode
                select key;

            List<Hotkey> keyCollection = query.ToList<Hotkey>();

            if (keyCollection.Count > 0)
            {
                if (keyObject.SendKey[0].Trim() != "")
                    keyCollection[0].SendKey.Add(keyObject.SendKey[0]);
                if (keyObject.ExecCommand[0].Trim() != "")
                {
                    keyCollection[0].ExecCommand.Add(keyObject.ExecCommand[0]);
                    keyCollection[0].KeyName.Add(keyObject.KeyName[0]);
                }
            }
            else
            {
                hotKeyList.Add(keyObject);
            }

        }

        #region Old Commented Codes
        //private string GetCommand()
        //{
        //    bool ctrl, alt, win;
        //    Keys key;

        //    ctrl = GetAsyncKeyState(Keys.ControlKey);
        //    alt = GetAsyncKeyState(Keys.Alt);
        //    win = GetAsyncKeyState(Keys.LWin) || GetAsyncKeyState(Keys.RWin);

        //    var query = from KeyCommand commandObject in commandList
        //                where GetAsyncKeyState(commandObject.Key) &&
        //                    (!commandObject.CTRLMask || ctrl) &&
        //                    (!commandObject.ALTMask || alt) &&
        //                    (!commandObject.WINMask || win)
        //                select commandObject.ExecString;

        //    string commands = string.Join("[TAB]", query.ToArray());

        //    return commands;
        //}
        //private void SendKeys()
        //{
        //    string[] formattedCommand = GetFormattedCommad(GetCommand());
        //    foreach (string command in formattedCommand)
        //        System.Windows.Forms.SendKeys.Send(command);
        //}

        //private string[] GetFormattedCommad(string command)
        //{
        //    List<string> commandList = new List<string>();
        //    string[] splitCommands = command.Split(new string[] { "¿" }, StringSplitOptions.None);
        //    int counter = 0;
        //    foreach (string cmd in splitCommands)
        //    {
        //        if (counter % 2 == 1)
        //            commandList.Add(GetMaskString(cmd));
        //        else
        //            commandList.Add(cmd);
        //        counter++;
        //    }
        //    return commandList.ToArray();
        //}

        //private string GetMaskString(string mask)
        //{
        //    string maskString = "";
        //    switch (mask)
        //    {
        //        case "TAB":
        //            maskString = "{TAB}";
        //            break;
        //        case "ENTER":
        //            maskString = "{ENTER}";
        //            break;
        //    }
        //    return maskString;
        //}

        #endregion

        private bool PredicateDelegate(Hotkey emp)
        {
            return true;
        }
        private Hotkey BindHotKey(KeyCommand keyCommand)
        {
            Hotkey hotKey = Common.BindHotKey(keyCommand);
            hotKey.Register(new Label());
            hotKey.Pressed += new HandledEventHandler(HandleHotKeyPress);

            return hotKey;

        }

        protected void HandleHotKeyPress(object sender, HandledEventArgs e)
        {
            WriteEntry("Hot Key (" + sender.ToString() + ") Pressed");

            if (((Hotkey)sender).SendKey.Count == 1)
            {
                if (((Hotkey)sender).SendKey[0].Trim() != "")
                    System.Windows.Forms.SendKeys.Send(((Hotkey)sender).SendKey[0]);
            }
            else
                HandleSendKeys(((Hotkey)sender).SendKey);


            if (((Hotkey)sender).ExecCommand.Count == 1)
            {
                string execCommand = "";
                if (((Hotkey)sender).ExecCommand[0].Trim() != "")
                {
                    execCommand = ((Hotkey)sender).ExecCommand[0].Trim();
                    ExecuteCommand(execCommand);
                }
            }
            else
            {
                menuCommands = ((Hotkey)sender).ExecCommand;
                HandleExecCommands(((Hotkey)sender).KeyName);
            }
        }

        private void HandleSendKeys(List<string> stringValues)
        {

            string concatSendKeys = "";

            foreach (string keyString in stringValues)
                concatSendKeys += keyString;

            SendKeys.Send(concatSendKeys);

            ResetApplication();

        }

        private void HandleExecCommands(List<string> stringValues)
        {
            ContextMenu menu = GetContextMenu(stringValues, "EXEC_COMMANDS");
            if (menu.MenuItems.Count > 4)
            {
                menu.Show(this, new Point(Cursor.Position.X - 300, Cursor.Position.Y - 120));
            }
            else
                ExecuteCommand(menu.MenuItems[0].Text);

        }

        private void ExecuteCommand(string command)
        {
            string argument = "";
            string execCommand = "";
            switch (command.ToUpper())
            {
                case "<!SETTINGS!>":
                    OpenSettings();
                    break;
                case "<!EXIT!>":
                    this.Close();
                    break;
                case "<!DISP_SHORT!>":
                    if (display.Visible) display.Hide();
                    display.Show();
                    break;
                default:
                    if (command.ToUpper().StartsWith("<!LYNC!>"))
                        execCommand = GetLyncCommand(command);
                    else if (command.ToUpper().StartsWith("<!OUTLOOK!>"))
                        execCommand = GetOutlookCommand(command);
                    else if (command.ToUpper().StartsWith("<!PROXY!>"))
                        execCommand = GetProxyCommand(command);
                    else
                        execCommand = command;

                    if (execCommand.Trim() != "")
                        if (execCommand.Contains("<!args!>"))
                            ExecuteCommand(execCommand.Split(new string[] { "<!args!>" }, StringSplitOptions.None)[0],
                                                execCommand.Split(new string[] { "<!args!>" }, StringSplitOptions.None)[1]);
                        else
                            ExecuteCommand(execCommand, argument);
                    break;
            }
        }


        private string GetProxyCommand(string commandString)
        {
            string command = "";
            try
            {

                string[] argParts = commandString.Split(new string[] { ":" }, StringSplitOptions.None);

                command = new FileInfo(Application.ExecutablePath).DirectoryName + "\\OpenWebInProxy.exe<!args!> -B -PX ";

                

                if (argParts.Length > 1)
                {
                    if (argParts[1].ToUpper() == "INCOGNITO")
                        command += "-IN ";
                }

                if (argParts.Length > 2)
                {
                    if (argParts[2].ToUpper() == "URL")
                    {
                        if (argParts.Length > 3)
                            command += "-URL:" + argParts[3];
                    }
                    else if (argParts[2].ToUpper() == "CLIP")
                        command += "-CLIP";
                }              

            }
            catch
            {
            }
            return command;
        }

        private ContextMenu GetContextMenu(List<string> stringValues, string tagName)
        {

            ContextMenu menu = new ContextMenu();

            MenuItem mi = null;

            foreach (string value in stringValues)
            {
                if (value.Trim() != "")
                {
                    mi = new MenuItem(value, new EventHandler(onMenuItemClicked));
                    mi.Tag = tagName;
                    menu.MenuItems.Add(mi);
                }
            }

            mi = new MenuItem("-", new EventHandler(onMenuItemClicked));
            mi.Tag = tagName;
            menu.MenuItems.Add(mi);

            mi = new MenuItem("Execute All", new EventHandler(onMenuItemClicked));
            mi.Tag = tagName;
            menu.MenuItems.Add(mi);

            mi = new MenuItem("Cancel", new EventHandler(onMenuItemClicked));
            mi.Tag = "";
            menu.MenuItems.Add(mi);

            return menu;

        }


        protected void onMenuItemClicked(object sender, EventArgs e)
        {
            switch (((MenuItem)sender).Tag.ToString().ToUpper())
            {
                case "SEND_KEYS":

                    if (((MenuItem)sender).Text.ToUpper() == "EXECUTE ALL")
                    {
                        ContextMenu menu = (ContextMenu)((MenuItem)sender).Parent;
                        foreach (MenuItem mi in menu.MenuItems)
                        {
                            SendKeys.Send(mi.Text);
                        }
                    }
                    else
                    {
                        SendKeys.Send(((MenuItem)sender).Text);
                    }

                    break;
                case "EXEC_COMMANDS":

                    if (((MenuItem)sender).Text.ToUpper() == "EXECUTE ALL")
                    {
                        ContextMenu menu = (ContextMenu)((MenuItem)sender).Parent;
                        foreach (MenuItem mi in menu.MenuItems)
                        {
                            if (mi.Text != "Execute All" && mi.Text != "Cancel" && mi.Text != "-")
                                ExecuteCommand(menuCommands[mi.Index].ToString());
                        }
                    }
                    else if (((MenuItem)sender).Text.ToUpper() == "CANCEL")
                    {
                    }
                    else
                    {
                        ExecuteCommand(menuCommands[((MenuItem)sender).Index].ToString());
                    }
                    break;
            }
        }

        private string GetOutlookCommand(string command)
        {
            string outLookCommand = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Microsoft Office\Office12\OUTLOOK.exe";

            string[] commandArray = command.Split(new char[] { ':' });

            outLookCommand = outLookCommand + "<!args!>";
            if (command.Length > 1)
            {
                switch (commandArray[1].ToUpper())
                {
                    case "CAL":
                        outLookCommand = outLookCommand + "/select outlook:calendar";
                        break;
                }
            }
            else
            {
                outLookCommand = "";
            }

            return outLookCommand;

        }

        private string GetLyncInstallationPath()
        {
            string communicatorPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Microsoft Lync\communicator.exe";
            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(@"callto\shell\open\command");
            communicatorPath = regKey.GetValue("").ToString();
            communicatorPath = communicatorPath.Substring(communicatorPath.IndexOf('"'), communicatorPath.IndexOf('"', 2));
            communicatorPath = communicatorPath.Replace(((char)34).ToString(), "");
            return communicatorPath;
        }

        private string GetLyncCommand(string command)
        {
            string lyncCommand = GetLyncInstallationPath();
            //Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Microsoft Lync\communicator.exe";
            try
            {
                string[] commandArray = command.Split(new char[] { ':' });
                if (commandArray.Length > 1)
                {
                    lyncCommand = lyncCommand + "<!args!>";
                    for (int i = 1; i < commandArray.Length; i++)
                    {
                        lyncCommand = lyncCommand + "im:sip:" + commandArray[i] + "@isgn.com ";
                    }
                }

            }
            catch
            {
            }

            return lyncCommand;

        }

        private void ExecuteCommand(string command, string arguments)
        {

            try
            {
                //string tmpFilePath = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\temp.bat";
                //if (File.Exists(tmpFilePath)) File.Delete(tmpFilePath);
                //TextWriter writer = new StreamWriter(tmpFilePath);    
                //writer.WriteLine(command);
                //writer.Close();
                System.Diagnostics.Process.Start(command, arguments);
            }
            catch (Exception exp)
            {
                WriteEntry("Error executing command (" + command + ")\nDescription: " + exp.Message);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitIcon();
            InitEventLog();
            ResetService();
            InitShortcutDisplay();
            SetAsStartup();
            WriteEntry("Service Started");
            this.ShowInTaskbar = false;
            this.Hide();
            this.Width = 0;
            this.Height = 0;


        }


        private void SetAsStartup()
        {
            try
            {
                ShellLinks.Link.Update(Environment.SpecialFolder.Startup, Application.ExecutablePath, "HotKeyMgr", true);
            }
            catch (Exception exp)
            {
                WriteEntry("Error On Setting Startup. \nDescription: " + exp.Message);
            }
        }

        private void InitShortcutDisplay()
        {
            display = new ShortcutDisplay(GetFileName());
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.TaskManagerClosing:
                    WriteEntry("Hot key manager closed by Task Manager");
                    break;
                case CloseReason.UserClosing:
                    WriteEntry("Hot key manager closed by User");
                    break;
                case CloseReason.WindowsShutDown:
                    WriteEntry("Hot key manager is shutting down with windows");
                    break;
            }

            WriteEntry("Hot Key manager shutting down");
        }

        private void tmrBalloon_Tick(object sender, EventArgs e)
        {
            tmrBalloon.Enabled = false;
        }



    }

}

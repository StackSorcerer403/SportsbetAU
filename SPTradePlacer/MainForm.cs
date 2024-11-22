using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using BettingBot.Json;
using BettingBot.Controller;
using BettingBot.Constants;
using System.Net;
using WebSocketSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using TipsterSW.Constant;
using System.Security.Cryptography;

namespace BettingBot
{
    public delegate void onWriteStatusEvent(string status);
    public delegate void onWriteLogEvent(string filename, string strLog);
    public delegate void onProcNewTipEvent(JsonTip newTip);
    public delegate void onProcNewTradeTipEvent(JsonTip newTip);
    public delegate void onProcUpdateNetworkStatusEvent(bool isOnline);


    public partial class MainForm : Form
    {

        public event onProcUpdateNetworkStatusEvent onProcUpdateNetworkStatusEvent;
        public event onProcNewTipEvent onProcNewTipEvent;
        public event onProcNewTradeTipEvent onProcNewTradeTipEvent;
        public event onWriteLogEvent onWriteLog;
        public event onWriteStatusEvent onWriteStatus;
        private Thread threadBet = null;
        private Thread threadEventUpdater = null;
        private BookieCtrl m_betCtrl = BookieCtrl.instance;
        private List<JsonTip> _betList = new List<JsonTip>();
        private List<JsonTip> _tradeBetList = new List<JsonTip>();
        BindingList<HorseItem> _horseList = new BindingList<HorseItem>();
        SocketConnector m_socketConnector;
        private bool isExiting = false;

        public MainForm()
        {
            InitializeComponent();

            Setting.instance.loadSettingInfo();

            this.onWriteStatus += writeStatus;
            this.onWriteLog += LogToFile;
            this.onProcUpdateNetworkStatusEvent += procUpdateNetworkStatus;
            this.onProcNewTipEvent += procNewTip;
            this.onProcNewTradeTipEvent += procNewTradeTip;
            m_socketConnector = new SocketConnector(onWriteLog, onWriteStatus, onProcNewTipEvent, onProcNewTradeTipEvent);
            m_socketConnector.m_handlerProcUpdateNetworkStatus = onProcUpdateNetworkStatusEvent;
            LogMng.CreateInstance(writeStatus, LogToFile);
            OcrEngine.CreateInstance();
            GuiAutomation.CreateInstance();
            dataSourceBets.DataSource = m_socketConnector.m_placedBetList;
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            dataSourceEvents.DataSource = _horseList;
            //this.tblEvent.RowPrePaint += new DataGridViewRowPrePaintEventHandler(this.dataGridView1_RowPrePaint);
            foreach (DataGridViewColumn column in tblEvent.Columns)
            {
                column.Frozen = false;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            tblEvent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            SettingForm frm = new SettingForm();
            DialogResult res = frm.ShowDialog();
        }

        private void refreshControls(bool state)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    btnStart.Enabled = state;
                }));
                GlobalConstants.state = state ? State.Stop : State.Running;
            }
            catch (Exception e)
            {
            }
        }

        private bool canStart()
        {
            if (GlobalConstants.state == State.Running)
            {
                MessageBox.Show(this, "The bot has been started already!");
                return false;
            }
            if (string.IsNullOrEmpty(Setting.instance.betUser) ||
                string.IsNullOrEmpty(Setting.instance.betPassword))
            {
                MessageBox.Show(this, "Empty Login credientials.");
                return false;
            }



            return true;
        }

        //private string getMainLogTitle()
        //{
        //    string date = string.Format("[{0}][Main] ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //    return date;
        //}

        private void btnStart_Click(object sender1, EventArgs e1)
        {
            toolStripStatusBot.Text = "Starting...";
            if (!canStart())
                return;

            refreshControls(false);
            writeStatus("The bot has been started!");

            BookieCtrl.CreateInstance();
            
            BrowserCtrl.CloseOldBrowser();
            BrowserCtrl.CreateInstance();
            BrowserCtrl.instance.DoOpenPage("https://www.sportsbet.com.au");

            //m_socketConnector.startListening();
            threadBet = new Thread(threadBetFunc);
            threadBet.Start();
            //threadEventUpdater = new Thread(eventUpdaterThreadFunc);
            //threadEventUpdater.Start();
        }
        private void eventUpdaterThreadFunc()
        {
            int retryCounter = 2 * 10;
            List<RaceItem> tabRaceList = BookieCtrl.instance.GetRaceList(true);
            while (GlobalConstants.state == State.Running)
            {
                try
                {
                    List<RaceItem> betfairRaceList = BookieCtrl.instance.GetBfRaceList(true);
                    int foundCounter = BookieCtrl.instance.Fill365DirectLink(tabRaceList);
                    toolStripStatusBot.Text = $"Betfair :{betfairRaceList.Count} Sportbet:{tabRaceList.Count} Matched:{foundCounter}";
                    Thread.Sleep(1000 * 30);
                    if (--retryCounter <= 0)
                    {
                        retryCounter = 2 * 10;
                        tabRaceList = BookieCtrl.instance.GetRaceList(true);
                    }
                }
                catch
                {
                }
            }
        }

        public void threadBetFunc()
        {
            DateTime lastLoadingTime = DateTime.Now;
            Thread.Sleep(5000);            
            
            //if (BookieCtrl.instance.getBalance() == -1)
            //    BookieCtrl.instance.doLogin();

            this.BeginInvoke((Action)(() =>
            {
                dataSourceBets.DataSource = BookieCtrl.instance.PlacedBetList;
                dataSourceBets.ResetBindings(false);
            }));

            while (true)
            {
                try
                {
                    BookieCtrl.instance.doCheckBetslip(10);
                    List<RaceItem> nextRaceList = BookieCtrl.instance.getNextRaces(Setting.instance.beforeKickoff);
                    List<HorseItem> horseList = new List<HorseItem>();
                    foreach (RaceItem raceItem in nextRaceList)
                    {
                        List<HorseItem> horseListEX = BookieCtrl.instance.GetBfHorseList(raceItem.winMarketId);
                        //toolStripStatusThread.Text = $" horseListEX: {horseListEX.Count}";
                        if (horseListEX.Count == 0)
                        {
                            Thread.Sleep(200);
                            continue;
                        }
                        if (string.IsNullOrEmpty(raceItem.b365DirectLink))
                        {
                            Thread.Sleep(200);
                            continue;
                        }
                        List<HorseItem> horseListSB = BookieCtrl.instance.GetHorseList(raceItem.b365DirectLink);
                        //toolStripStatusThread.Text = $" horseListEX: {horseListEX.Count}";
                        //toolStripStatusThread.Text += $" horseListSB: {horseListSB.Count}";
                        if (horseListSB.Count == 0)
                        {
                            Thread.Sleep(200);
                            continue;
                        }

                        for (int i = 0; i < horseListEX.Count; i++)
                        {
                            HorseItem horseEx = horseListEX[i];
                            for (int j = 0; j < horseListSB.Count; j++)
                            {
                                HorseItem horseSb = horseListSB[j];
                                horseSb.raceName = raceItem.raceTitle;
                                horseSb.raceStart = raceItem.raceStart;
                                horseSb.type = raceItem.type;
                                horseSb.title = horseSb.title.Replace("'", "");
                                double dist = JaroWinklerDistance.distance(horseEx.title.Trim(), horseSb.title.Trim());
                                if (dist < 0.06)
                                {
                                    horseSb.title2 = horseEx.title;
                                    horseSb.backOdds = horseEx.backOdds;
                                    horseSb.layOdds = horseEx.layOdds;
                                    horseList.Add(horseSb);
                                    if(BookieCtrl.instance.startNewTrade(horseSb))
                                    {
                                        this.BeginInvoke((Action)(() =>
                                        {
                                            dataSourceBets.DataSource = BookieCtrl.instance.PlacedBetList;
                                            dataSourceBets.ResetBindings(false);
                                        }));
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    _horseList = new BindingList<HorseItem>(horseList);
                    this.BeginInvoke((Action)(() =>
                    {
                        dataSourceEvents.DataSource = _horseList;
                        dataSourceEvents.ResetBindings(false);
                    }));

                    
                }
                catch (Exception ex)
                {
                    writeStatus("Exception in threadBetFunc: " + ex.ToString());
                }
                Thread.Sleep(100);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            writeStatus("The bot has been stopped!");
            refreshControls(true);
            try
            {
                threadBet.Abort();
            }
            catch
            {
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (threadBet != null) {
                    threadBet.Abort(); 
                } else { 
                    Console.WriteLine("The thread is not initialized."); 
                }
            }
            catch
            {
            }
            this.Close();
        }

        private void writeStatus(string status)
        {
            try
            {
                string str = status;
                string curPath = Directory.GetCurrentDirectory();
                if (rtLog.InvokeRequired)
                    rtLog.Invoke(onWriteStatus, status);
                else
                {
                    string logText = ((string.IsNullOrEmpty(rtLog.Text) ? "" : "\r") + string.Format("[{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), status));
                    this.onWriteLog(curPath + "\\Log\\" + string.Format("log_{0}.txt", DateTime.Now.ToString("yyyy-MM-dd")), logText);
                    rtLog.AppendText(logText);
                    rtLog.ScrollToCaret();
                }

            }
            catch (Exception)
            {

            }
        }
        private void createLogFolders()
        {
            string curPath = Directory.GetCurrentDirectory();
            if (!Directory.Exists(curPath + "\\Log\\"))
                Directory.CreateDirectory(curPath + "\\Log\\");
            if (!Directory.Exists(curPath + "\\Bet\\"))
                Directory.CreateDirectory(curPath + "\\Bet\\");
        }


        private void LogToFile(string filename, string result)
        {
            try
            {
                string curPath = Directory.GetCurrentDirectory();
                if (string.IsNullOrEmpty(filename))
                    return;
                StreamWriter streamWriter = new StreamWriter((Stream)System.IO.File.Open(filename, FileMode.Append, FileAccess.Write, FileShare.Read), Encoding.UTF8);
                if (!string.IsNullOrEmpty(result))
                    streamWriter.WriteLine(result);
                streamWriter.Close();
            }
            catch (System.Exception ex)
            {
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExiting) { 
                return; 
            }

            DialogResult result = MessageBox.Show("Are you sure to exit program?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }

            isExiting = true;

            if (GlobalConstants.state == State.Running)
            {
                e.Cancel = false;
            }
            ExitProgram();
        }

        private void ExitProgram()
        {
            GlobalConstants.state = State.Stop;
            Setting.instance.saveSettingInfo();
            try
            {
                Application.Exit();
            }
            catch
            {

            }
        }

        private void procUpdateNetworkStatus(bool isOnline)
        {
            try
            {
                if (lblOnline.InvokeRequired)
                    rtLog.Invoke(onProcUpdateNetworkStatusEvent, isOnline);
                else
                {
                    if (isOnline)
                    {
                        lblOnline.Text = "Online";
                        lblOnline.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblOnline.Text = "Offline";
                        lblOnline.ForeColor = System.Drawing.Color.Red;
                    }
                }
                lblUsername.Text = Setting.instance.betUser;
            }
            catch (Exception ex)
            {
                this.writeStatus(ex.ToString());
            }
        }

        private void procNewTip(JsonTip betitem)
        {
            lock (_betList)
            {
                _betList.Add(betitem);
            }
        }

        private void procNewTradeTip(JsonTip betitem)
        {
            lock (_tradeBetList)
            {
                _tradeBetList.Add(betitem);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BookieCtrl.instance.checkHistory();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            createLogFolders();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string batchFilePath = @".\logout.bat";

            //    // Create a new process to run the batch file
            //    Process process = new Process();
            //    process.StartInfo.FileName = batchFilePath;
            //    process.StartInfo.UseShellExecute = false; // Optional: Use true if you need to run it in a shell
            //    process.StartInfo.RedirectStandardOutput = true;
            //    process.StartInfo.RedirectStandardError = true;
            //    process.Start();
            //}
            //catch
            //{
            //}

            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "powershell.exe";

                // Pass the path of the script to PowerShell as an argument
                process.StartInfo.Arguments = $"-ExecutionPolicy Bypass -File \"{"tscon.ps1"}\"";

                // Optional: Redirect output and error streams for debugging or logging purposes
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false; // Important to set to false when redirecting output
                process.StartInfo.CreateNoWindow = true;   // Optional: Don't create a window for the process
                process.Start();
            }
            catch
            {
            }
        }

        private void UpdateFile(JObject betWin)
        {
            try
            {
                string text = File.ReadAllText("aa.csv");
                text = text.Replace("\n\n", "\r\n");
                File.WriteAllText("aa.csv", text);

                string[] lines = File.ReadAllLines("aa.csv");
                string lastWorkId = "";
                for (int i = 0; i < lines.Length; i++)
                {
                    try
                    {
                        string line = lines[i];
                        if (string.IsNullOrEmpty(line)) continue;
                        string[] parts = line.Split(',');
                        string roundId = Utils.Between(parts[4], "(", ")");
                        if (lastWorkId == roundId) continue;
                        
                        if (betWin[roundId] == null) continue;
                        string turn_bet = betWin[roundId]["turn_bet"].ToString();
                        string turn_win = "0", turn_draw = "0", turn_cancel = "0";
                        if (betWin[roundId]["turn_win"] != null)
                            turn_win = betWin[roundId]["turn_win"].ToString();
                        if (betWin[roundId]["turn_draw"] != null)
                            turn_draw = betWin[roundId]["turn_draw"].ToString();
                        if (betWin[roundId]["turn_cancel"] != null)
                            turn_cancel = betWin[roundId]["turn_cancel"].ToString();
                        lines[i] = lines[i] + $",{turn_bet},{turn_win}, {turn_draw}, {turn_cancel}";
                        lastWorkId = roundId;
                        betWin.Remove(roundId);
                    }
                    catch(Exception ex)
                    {
                        writeStatus(ex.ToString());
                    }
                }
                File.WriteAllLines("aa.csv", lines);
            }
            catch(Exception ex)
            {
                writeStatus(ex.ToString());
            }
            
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            string dataToHash = "{\"username\":\"qa009winbat777\",\"requestKey\":638652964598832831}1e6b140c399a96d384b8a56a382a974c";

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(dataToHash));

                writeStatus(Convert.ToBase64String(hashBytes));
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }
    }
}

namespace BettingBot
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.rtLog = new System.Windows.Forms.RichTextBox();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblOnline = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tblEvent = new System.Windows.Forms.DataGridView();
            this.Start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Race = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seconds = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Horse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Horse2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Odd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Even = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataSourceEvents = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stake = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataSourceBets = new System.Windows.Forms.BindingSource(this.components);
            this.statusBot = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusBot = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusThread = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tblEvent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSourceEvents)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSourceBets)).BeginInit();
            this.statusBot.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtLog
            // 
            this.rtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtLog.Location = new System.Drawing.Point(3, 372);
            this.rtLog.Name = "rtLog";
            this.rtLog.Size = new System.Drawing.Size(768, 152);
            this.rtLog.TabIndex = 21;
            this.rtLog.Text = "";
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(12, 562);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(75, 23);
            this.btnSetting.TabIndex = 23;
            this.btnSetting.Text = "Setting";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(483, 562);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 23;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(583, 562);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 24;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(684, 562);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 25;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblOnline
            // 
            this.lblOnline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOnline.AutoSize = true;
            this.lblOnline.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOnline.ForeColor = System.Drawing.Color.Red;
            this.lblOnline.Location = new System.Drawing.Point(103, 567);
            this.lblOnline.Name = "lblOnline";
            this.lblOnline.Size = new System.Drawing.Size(44, 13);
            this.lblOnline.TabIndex = 34;
            this.lblOnline.Text = "Offline";
            // 
            // lblUsername
            // 
            this.lblUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(162, 567);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(55, 13);
            this.lblUsername.TabIndex = 37;
            this.lblUsername.Text = "Username";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(782, 553);
            this.tabControl1.TabIndex = 38;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tblEvent);
            this.tabPage1.Controls.Add(this.rtLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(774, 527);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Live Status";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tblEvent
            // 
            this.tblEvent.AutoGenerateColumns = false;
            this.tblEvent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblEvent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Start,
            this.Race,
            this.Seconds,
            this.Horse,
            this.Horse2,
            this.Odd,
            this.Even});
            this.tblEvent.DataSource = this.dataSourceEvents;
            this.tblEvent.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblEvent.Location = new System.Drawing.Point(3, 3);
            this.tblEvent.Margin = new System.Windows.Forms.Padding(10);
            this.tblEvent.Name = "tblEvent";
            this.tblEvent.Size = new System.Drawing.Size(768, 364);
            this.tblEvent.TabIndex = 28;
            // 
            // Start
            // 
            this.Start.DataPropertyName = "raceStart";
            this.Start.Frozen = true;
            this.Start.HeaderText = "Start";
            this.Start.Name = "Start";
            // 
            // Race
            // 
            this.Race.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Race.DataPropertyName = "raceName";
            this.Race.HeaderText = "Race";
            this.Race.Name = "Race";
            // 
            // Seconds
            // 
            this.Seconds.DataPropertyName = "beforeSeconds";
            this.Seconds.HeaderText = "Seconds";
            this.Seconds.Name = "Seconds";
            // 
            // Horse
            // 
            this.Horse.DataPropertyName = "title";
            this.Horse.HeaderText = "Horse";
            this.Horse.Name = "Horse";
            // 
            // Horse2
            // 
            this.Horse2.DataPropertyName = "title2";
            this.Horse2.HeaderText = "-";
            this.Horse2.Name = "Horse2";
            // 
            // Odd
            // 
            this.Odd.DataPropertyName = "odds";
            this.Odd.HeaderText = "Odds";
            this.Odd.Name = "Odd";
            // 
            // Even
            // 
            this.Even.DataPropertyName = "layOdds";
            this.Even.HeaderText = "BF Odds";
            this.Even.Name = "Even";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(774, 527);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Bet List";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.Stake,
            this.Percent});
            this.dataGridView1.DataSource = this.dataSourceBets;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(10);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(768, 364);
            this.dataGridView1.TabIndex = 29;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "raceStart";
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Start";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "raceName";
            this.dataGridViewTextBoxColumn2.HeaderText = "Race";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "title";
            this.dataGridViewTextBoxColumn4.HeaderText = "Horse";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "odds";
            this.dataGridViewTextBoxColumn5.HeaderText = "Odds";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "backOdds";
            this.dataGridViewTextBoxColumn6.HeaderText = "BF Odds";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // Stake
            // 
            this.Stake.DataPropertyName = "stake";
            this.Stake.HeaderText = "Stake";
            this.Stake.Name = "Stake";
            // 
            // Percent
            // 
            this.Percent.DataPropertyName = "percent";
            this.Percent.HeaderText = "Percent";
            this.Percent.Name = "Percent";
            // 
            // statusBot
            // 
            this.statusBot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusBot,
            this.toolStripStatusThread});
            this.statusBot.Location = new System.Drawing.Point(0, 596);
            this.statusBot.Name = "statusBot";
            this.statusBot.Size = new System.Drawing.Size(782, 22);
            this.statusBot.TabIndex = 40;
            this.statusBot.Text = "statusStrip1";
            // 
            // toolStripStatusBot
            // 
            this.toolStripStatusBot.Name = "toolStripStatusBot";
            this.toolStripStatusBot.Size = new System.Drawing.Size(48, 17);
            this.toolStripStatusBot.Text = "Waiting";
            // 
            // toolStripStatusThread
            // 
            this.toolStripStatusThread.Name = "toolStripStatusThread";
            this.toolStripStatusThread.Size = new System.Drawing.Size(48, 17);
            this.toolStripStatusThread.Text = "Waiting";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(305, 562);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 41;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 618);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusBot);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblOnline);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "SportsbetBot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tblEvent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSourceEvents)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSourceBets)).EndInit();
            this.statusBot.ResumeLayout(false);
            this.statusBot.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtLog;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblOnline;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.BindingSource dataSourceBets;
        private System.Windows.Forms.DataGridView tblEvent;
        private System.Windows.Forms.BindingSource dataSourceEvents;
        private System.Windows.Forms.StatusStrip statusBot;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusBot;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Start;
        private System.Windows.Forms.DataGridViewTextBoxColumn Race;
        private System.Windows.Forms.DataGridViewTextBoxColumn Seconds;
        private System.Windows.Forms.DataGridViewTextBoxColumn Horse;
        private System.Windows.Forms.DataGridViewTextBoxColumn Horse2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Odd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Even;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusThread;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stake;
        private System.Windows.Forms.DataGridViewTextBoxColumn Percent;
        private System.Windows.Forms.Button button1;
    }
}


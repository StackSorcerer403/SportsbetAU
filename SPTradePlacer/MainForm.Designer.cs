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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dataSourceEvents = new System.Windows.Forms.BindingSource(this.components);
            this.dataSourceBets = new System.Windows.Forms.BindingSource(this.components);
            this.rtLog = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBetPass = new System.Windows.Forms.TextBox();
            this.txtBetUser = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numSlipCount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAutoStaker = new System.Windows.Forms.CheckBox();
            this.chkAutoSlip = new System.Windows.Forms.CheckBox();
            this.numTotalReturn = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataSourceEvents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSourceBets)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSlipCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalReturn)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnStart.Font = new System.Drawing.Font("Cambria", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnStart.Location = new System.Drawing.Point(113, 561);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 23;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnStop.Font = new System.Drawing.Font("Cambria", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnStop.Location = new System.Drawing.Point(355, 562);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 24;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btnExit.Font = new System.Drawing.Font("Cambria", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnExit.Location = new System.Drawing.Point(578, 562);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 25;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // rtLog
            // 
            this.rtLog.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.rtLog.Font = new System.Drawing.Font("Cascadia Mono", 11.25F);
            this.rtLog.Location = new System.Drawing.Point(12, 305);
            this.rtLog.Name = "rtLog";
            this.rtLog.Size = new System.Drawing.Size(758, 239);
            this.rtLog.TabIndex = 42;
            this.rtLog.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBetPass);
            this.groupBox1.Controls.Add(this.txtBetUser);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Font = new System.Drawing.Font("Cambria", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(436, 287);
            this.groupBox1.TabIndex = 49;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Account Details";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 62;
            this.label1.Text = "Password:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtBetPass
            // 
            this.txtBetPass.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtBetPass.Location = new System.Drawing.Point(101, 87);
            this.txtBetPass.Name = "txtBetPass";
            this.txtBetPass.Size = new System.Drawing.Size(303, 25);
            this.txtBetPass.TabIndex = 61;
            // 
            // txtBetUser
            // 
            this.txtBetUser.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtBetUser.Location = new System.Drawing.Point(101, 36);
            this.txtBetUser.Name = "txtBetUser";
            this.txtBetUser.Size = new System.Drawing.Size(303, 25);
            this.txtBetUser.TabIndex = 55;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 17);
            this.label8.TabIndex = 54;
            this.label8.Text = "Account:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numSlipCount);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.chkAutoStaker);
            this.groupBox2.Controls.Add(this.chkAutoSlip);
            this.groupBox2.Controls.Add(this.numTotalReturn);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("Cambria", 11.25F);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Location = new System.Drawing.Point(460, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(310, 287);
            this.groupBox2.TabIndex = 50;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter";
            // 
            // numSlipCount
            // 
            this.numSlipCount.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.numSlipCount.Location = new System.Drawing.Point(118, 89);
            this.numSlipCount.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numSlipCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSlipCount.Name = "numSlipCount";
            this.numSlipCount.Size = new System.Drawing.Size(80, 25);
            this.numSlipCount.TabIndex = 69;
            this.numSlipCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 68;
            this.label2.Text = "Slip Count:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkAutoStaker
            // 
            this.chkAutoStaker.AutoSize = true;
            this.chkAutoStaker.Location = new System.Drawing.Point(25, 190);
            this.chkAutoStaker.Name = "chkAutoStaker";
            this.chkAutoStaker.Size = new System.Drawing.Size(100, 21);
            this.chkAutoStaker.TabIndex = 67;
            this.chkAutoStaker.Text = "Auto Staker";
            this.chkAutoStaker.UseVisualStyleBackColor = true;
            // 
            // chkAutoSlip
            // 
            this.chkAutoSlip.AutoSize = true;
            this.chkAutoSlip.Location = new System.Drawing.Point(25, 139);
            this.chkAutoSlip.Name = "chkAutoSlip";
            this.chkAutoSlip.Size = new System.Drawing.Size(130, 21);
            this.chkAutoSlip.TabIndex = 66;
            this.chkAutoSlip.Text = "Auto Adding Slip";
            this.chkAutoSlip.UseVisualStyleBackColor = true;
            // 
            // numTotalReturn
            // 
            this.numTotalReturn.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.numTotalReturn.Location = new System.Drawing.Point(118, 36);
            this.numTotalReturn.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numTotalReturn.Name = "numTotalReturn";
            this.numTotalReturn.Size = new System.Drawing.Size(80, 25);
            this.numTotalReturn.TabIndex = 65;
            this.numTotalReturn.Value = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 17);
            this.label3.TabIndex = 64;
            this.label3.Text = "Total Return:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(782, 596);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rtLog);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "SportsbetBot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSourceEvents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSourceBets)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSlipCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalReturn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.BindingSource dataSourceBets;
        private System.Windows.Forms.BindingSource dataSourceEvents;
        private System.Windows.Forms.RichTextBox rtLog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBetPass;
        private System.Windows.Forms.TextBox txtBetUser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numSlipCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkAutoStaker;
        private System.Windows.Forms.CheckBox chkAutoSlip;
        private System.Windows.Forms.NumericUpDown numTotalReturn;
        private System.Windows.Forms.Label label3;
    }
}


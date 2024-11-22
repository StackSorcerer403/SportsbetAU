namespace BettingBot
{
    partial class SettingForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBetPass = new System.Windows.Forms.TextBox();
            this.txtProxyPass = new System.Windows.Forms.TextBox();
            this.txtProxyUser = new System.Windows.Forms.TextBox();
            this.txtBetUser = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtProxy = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.setCurrency = new System.Windows.Forms.DomainUpDown();
            this.numFlatStake = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numTotalReturn = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.chkHarness = new System.Windows.Forms.CheckBox();
            this.chkDog = new System.Windows.Forms.CheckBox();
            this.chkHorse = new System.Windows.Forms.CheckBox();
            this.numBeforeKickoff = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numMinPercent = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numMaxPercent = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.numMinValue = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numMaxOdds = new System.Windows.Forms.NumericUpDown();
            this.numMinOdds = new System.Windows.Forms.NumericUpDown();
            this.numMaxValue = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkAutoSlip = new System.Windows.Forms.CheckBox();
            this.chkAutoStaker = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFlatStake)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBeforeKickoff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxOdds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinOdds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxValue)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(331, 390);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 23);
            this.btnCancel.TabIndex = 47;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(213, 390);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 23);
            this.btnOK.TabIndex = 46;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBetPass);
            this.groupBox1.Controls.Add(this.txtProxyPass);
            this.groupBox1.Controls.Add(this.txtProxyUser);
            this.groupBox1.Controls.Add(this.txtBetUser);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtProxy);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(595, 116);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Account Details";
            // 
            // txtBetPass
            // 
            this.txtBetPass.Location = new System.Drawing.Point(319, 28);
            this.txtBetPass.Name = "txtBetPass";
            this.txtBetPass.Size = new System.Drawing.Size(254, 20);
            this.txtBetPass.TabIndex = 61;
            // 
            // txtProxyPass
            // 
            this.txtProxyPass.Location = new System.Drawing.Point(438, 67);
            this.txtProxyPass.Name = "txtProxyPass";
            this.txtProxyPass.Size = new System.Drawing.Size(135, 20);
            this.txtProxyPass.TabIndex = 58;
            this.txtProxyPass.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // txtProxyUser
            // 
            this.txtProxyUser.Location = new System.Drawing.Point(300, 67);
            this.txtProxyUser.Name = "txtProxyUser";
            this.txtProxyUser.Size = new System.Drawing.Size(113, 20);
            this.txtProxyUser.TabIndex = 57;
            // 
            // txtBetUser
            // 
            this.txtBetUser.Location = new System.Drawing.Point(90, 28);
            this.txtBetUser.Name = "txtBetUser";
            this.txtBetUser.Size = new System.Drawing.Size(222, 20);
            this.txtBetUser.TabIndex = 55;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 54;
            this.label8.Text = "Account:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtProxy
            // 
            this.txtProxy.Location = new System.Drawing.Point(90, 67);
            this.txtProxy.Name = "txtProxy";
            this.txtProxy.Size = new System.Drawing.Size(179, 20);
            this.txtProxy.TabIndex = 53;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 52;
            this.label7.Text = "Proxy:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // setCurrency
            // 
            this.setCurrency.Items.Add("es-ES");
            this.setCurrency.Items.Add("en-GB");
            this.setCurrency.Location = new System.Drawing.Point(453, 41);
            this.setCurrency.Name = "setCurrency";
            this.setCurrency.Size = new System.Drawing.Size(104, 20);
            this.setCurrency.TabIndex = 60;
            this.setCurrency.Text = "Choose Culture";
            // 
            // numFlatStake
            // 
            this.numFlatStake.DecimalPlaces = 2;
            this.numFlatStake.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numFlatStake.Location = new System.Drawing.Point(313, 78);
            this.numFlatStake.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numFlatStake.Name = "numFlatStake";
            this.numFlatStake.Size = new System.Drawing.Size(80, 20);
            this.numFlatStake.TabIndex = 51;
            this.numFlatStake.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(230, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 50;
            this.label6.Text = "Stake:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkAutoStaker);
            this.groupBox2.Controls.Add(this.chkAutoSlip);
            this.groupBox2.Controls.Add(this.numTotalReturn);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.chkHarness);
            this.groupBox2.Controls.Add(this.chkDog);
            this.groupBox2.Controls.Add(this.chkHorse);
            this.groupBox2.Controls.Add(this.numBeforeKickoff);
            this.groupBox2.Controls.Add(this.setCurrency);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.numMinPercent);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.numMaxPercent);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.numMinValue);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.numFlatStake);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.numMaxOdds);
            this.groupBox2.Controls.Add(this.numMinOdds);
            this.groupBox2.Controls.Add(this.numMaxValue);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(595, 249);
            this.groupBox2.TabIndex = 49;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // numTotalReturn
            // 
            this.numTotalReturn.Location = new System.Drawing.Point(495, 78);
            this.numTotalReturn.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numTotalReturn.Name = "numTotalReturn";
            this.numTotalReturn.Size = new System.Drawing.Size(80, 20);
            this.numTotalReturn.TabIndex = 65;
            this.numTotalReturn.Value = new decimal(new int[] {
            8000,
            0,
            0,
            0});
            this.numTotalReturn.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(417, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 64;
            this.label2.Text = "Total Return:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // chkHarness
            // 
            this.chkHarness.AutoSize = true;
            this.chkHarness.Location = new System.Drawing.Point(196, 41);
            this.chkHarness.Name = "chkHarness";
            this.chkHarness.Size = new System.Drawing.Size(65, 17);
            this.chkHarness.TabIndex = 63;
            this.chkHarness.Text = "Harness";
            this.chkHarness.UseVisualStyleBackColor = true;
            // 
            // chkDog
            // 
            this.chkDog.AutoSize = true;
            this.chkDog.Location = new System.Drawing.Point(115, 41);
            this.chkDog.Name = "chkDog";
            this.chkDog.Size = new System.Drawing.Size(46, 17);
            this.chkDog.TabIndex = 62;
            this.chkDog.Text = "Dog";
            this.chkDog.UseVisualStyleBackColor = true;
            // 
            // chkHorse
            // 
            this.chkHorse.AutoSize = true;
            this.chkHorse.Location = new System.Drawing.Point(35, 41);
            this.chkHorse.Name = "chkHorse";
            this.chkHorse.Size = new System.Drawing.Size(54, 17);
            this.chkHorse.TabIndex = 61;
            this.chkHorse.Text = "Horse";
            this.chkHorse.UseVisualStyleBackColor = true;
            // 
            // numBeforeKickoff
            // 
            this.numBeforeKickoff.Location = new System.Drawing.Point(110, 78);
            this.numBeforeKickoff.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numBeforeKickoff.Name = "numBeforeKickoff";
            this.numBeforeKickoff.Size = new System.Drawing.Size(80, 20);
            this.numBeforeKickoff.TabIndex = 58;
            this.numBeforeKickoff.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numBeforeKickoff.ValueChanged += new System.EventHandler(this.numBeforeKickoff_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 13);
            this.label9.TabIndex = 57;
            this.label9.Text = "Before Kickoff:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numMinPercent
            // 
            this.numMinPercent.DecimalPlaces = 2;
            this.numMinPercent.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numMinPercent.Location = new System.Drawing.Point(110, 120);
            this.numMinPercent.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMinPercent.Name = "numMinPercent";
            this.numMinPercent.Size = new System.Drawing.Size(80, 20);
            this.numMinPercent.TabIndex = 56;
            this.numMinPercent.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 55;
            this.label10.Text = "Min Percent:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numMaxPercent
            // 
            this.numMaxPercent.Location = new System.Drawing.Point(313, 120);
            this.numMaxPercent.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMaxPercent.Name = "numMaxPercent";
            this.numMaxPercent.Size = new System.Drawing.Size(80, 20);
            this.numMaxPercent.TabIndex = 54;
            this.numMaxPercent.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(230, 123);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 53;
            this.label11.Text = "Max Percent:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numMinValue
            // 
            this.numMinValue.DecimalPlaces = 2;
            this.numMinValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numMinValue.Location = new System.Drawing.Point(110, 163);
            this.numMinValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMinValue.Name = "numMinValue";
            this.numMinValue.Size = new System.Drawing.Size(80, 20);
            this.numMinValue.TabIndex = 52;
            this.numMinValue.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 51;
            this.label5.Text = "Min Value:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // numMaxOdds
            // 
            this.numMaxOdds.DecimalPlaces = 2;
            this.numMaxOdds.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numMaxOdds.Location = new System.Drawing.Point(314, 208);
            this.numMaxOdds.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMaxOdds.Name = "numMaxOdds";
            this.numMaxOdds.Size = new System.Drawing.Size(80, 20);
            this.numMaxOdds.TabIndex = 50;
            this.numMaxOdds.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // numMinOdds
            // 
            this.numMinOdds.DecimalPlaces = 2;
            this.numMinOdds.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numMinOdds.Location = new System.Drawing.Point(110, 208);
            this.numMinOdds.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMinOdds.Name = "numMinOdds";
            this.numMinOdds.Size = new System.Drawing.Size(80, 20);
            this.numMinOdds.TabIndex = 49;
            this.numMinOdds.Value = new decimal(new int[] {
            12,
            0,
            0,
            65536});
            // 
            // numMaxValue
            // 
            this.numMaxValue.Location = new System.Drawing.Point(314, 163);
            this.numMaxValue.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMaxValue.Name = "numMaxValue";
            this.numMaxValue.Size = new System.Drawing.Size(80, 20);
            this.numMaxValue.TabIndex = 48;
            this.numMaxValue.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(230, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "Max Odds:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Max Value:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Min Odds:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // chkAutoSlip
            // 
            this.chkAutoSlip.AutoSize = true;
            this.chkAutoSlip.Location = new System.Drawing.Point(420, 123);
            this.chkAutoSlip.Name = "chkAutoSlip";
            this.chkAutoSlip.Size = new System.Drawing.Size(104, 17);
            this.chkAutoSlip.TabIndex = 66;
            this.chkAutoSlip.Text = "Auto Adding Slip";
            this.chkAutoSlip.UseVisualStyleBackColor = true;
            this.chkAutoSlip.CheckedChanged += new System.EventHandler(this.chkAutoSlip_CheckedChanged);
            // 
            // chkAutoStaker
            // 
            this.chkAutoStaker.AutoSize = true;
            this.chkAutoStaker.Location = new System.Drawing.Point(420, 165);
            this.chkAutoStaker.Name = "chkAutoStaker";
            this.chkAutoStaker.Size = new System.Drawing.Size(82, 17);
            this.chkAutoStaker.TabIndex = 67;
            this.chkAutoStaker.Text = "Auto Staker";
            this.chkAutoStaker.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 425);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFlatStake)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBeforeKickoff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxOdds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinOdds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numMaxOdds;
        private System.Windows.Forms.NumericUpDown numMinOdds;
        private System.Windows.Forms.NumericUpDown numMaxValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numMinValue;
        private System.Windows.Forms.NumericUpDown numFlatStake;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtProxy;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBetUser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtProxyPass;
        private System.Windows.Forms.TextBox txtProxyUser;
        private System.Windows.Forms.DomainUpDown setCurrency;
        private System.Windows.Forms.TextBox txtBetPass;
        private System.Windows.Forms.NumericUpDown numMinPercent;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numMaxPercent;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numBeforeKickoff;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkHarness;
        private System.Windows.Forms.CheckBox chkDog;
        private System.Windows.Forms.CheckBox chkHorse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numTotalReturn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkAutoStaker;
        private System.Windows.Forms.CheckBox chkAutoSlip;
    }
}
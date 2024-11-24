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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numSlipCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chkAutoStaker = new System.Windows.Forms.CheckBox();
            this.chkAutoSlip = new System.Windows.Forms.CheckBox();
            this.numTotalReturn = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSlipCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalReturn)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.HotPink;
            this.btnCancel.Font = new System.Drawing.Font("Cambria", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(331, 390);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 23);
            this.btnCancel.TabIndex = 47;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnOK.Font = new System.Drawing.Font("Cambria", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnOK.Location = new System.Drawing.Point(213, 390);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 23);
            this.btnOK.TabIndex = 46;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
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
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(595, 116);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Account Details";
            // 
            // txtBetPass
            // 
            this.txtBetPass.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtBetPass.Location = new System.Drawing.Point(319, 28);
            this.txtBetPass.Name = "txtBetPass";
            this.txtBetPass.Size = new System.Drawing.Size(254, 20);
            this.txtBetPass.TabIndex = 61;
            // 
            // txtProxyPass
            // 
            this.txtProxyPass.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtProxyPass.Location = new System.Drawing.Point(438, 67);
            this.txtProxyPass.Name = "txtProxyPass";
            this.txtProxyPass.Size = new System.Drawing.Size(135, 20);
            this.txtProxyPass.TabIndex = 58;
            this.txtProxyPass.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // txtProxyUser
            // 
            this.txtProxyUser.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtProxyUser.Location = new System.Drawing.Point(300, 67);
            this.txtProxyUser.Name = "txtProxyUser";
            this.txtProxyUser.Size = new System.Drawing.Size(113, 20);
            this.txtProxyUser.TabIndex = 57;
            // 
            // txtBetUser
            // 
            this.txtBetUser.BackColor = System.Drawing.SystemColors.ScrollBar;
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
            this.txtProxy.BackColor = System.Drawing.SystemColors.ScrollBar;
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
            this.setCurrency.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.setCurrency.Items.Add("es-ES");
            this.setCurrency.Items.Add("en-GB");
            this.setCurrency.Location = new System.Drawing.Point(469, 19);
            this.setCurrency.Name = "setCurrency";
            this.setCurrency.Size = new System.Drawing.Size(104, 20);
            this.setCurrency.TabIndex = 60;
            this.setCurrency.Text = "Choose Culture";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numSlipCount);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.chkAutoStaker);
            this.groupBox2.Controls.Add(this.chkAutoSlip);
            this.groupBox2.Controls.Add(this.numTotalReturn);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.setCurrency);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox2.Location = new System.Drawing.Point(12, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(595, 249);
            this.groupBox2.TabIndex = 49;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filter";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // numSlipCount
            // 
            this.numSlipCount.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.numSlipCount.Location = new System.Drawing.Point(96, 83);
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
            this.numSlipCount.Size = new System.Drawing.Size(80, 20);
            this.numSlipCount.TabIndex = 69;
            this.numSlipCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numSlipCount.ValueChanged += new System.EventHandler(this.numSlipCount_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "Slip Count:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkAutoStaker
            // 
            this.chkAutoStaker.AutoSize = true;
            this.chkAutoStaker.Location = new System.Drawing.Point(24, 159);
            this.chkAutoStaker.Name = "chkAutoStaker";
            this.chkAutoStaker.Size = new System.Drawing.Size(82, 17);
            this.chkAutoStaker.TabIndex = 67;
            this.chkAutoStaker.Text = "Auto Staker";
            this.chkAutoStaker.UseVisualStyleBackColor = true;
            // 
            // chkAutoSlip
            // 
            this.chkAutoSlip.AutoSize = true;
            this.chkAutoSlip.Location = new System.Drawing.Point(24, 121);
            this.chkAutoSlip.Name = "chkAutoSlip";
            this.chkAutoSlip.Size = new System.Drawing.Size(104, 17);
            this.chkAutoSlip.TabIndex = 66;
            this.chkAutoSlip.Text = "Auto Adding Slip";
            this.chkAutoSlip.UseVisualStyleBackColor = true;
            this.chkAutoSlip.CheckedChanged += new System.EventHandler(this.chkAutoSlip_CheckedChanged);
            // 
            // numTotalReturn
            // 
            this.numTotalReturn.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.numTotalReturn.Location = new System.Drawing.Point(96, 46);
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
            this.label2.Location = new System.Drawing.Point(21, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 64;
            this.label2.Text = "Total Return:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSlipCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalReturn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtProxy;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBetUser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtProxyPass;
        private System.Windows.Forms.TextBox txtProxyUser;
        private System.Windows.Forms.DomainUpDown setCurrency;
        private System.Windows.Forms.TextBox txtBetPass;
        private System.Windows.Forms.NumericUpDown numTotalReturn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkAutoStaker;
        private System.Windows.Forms.CheckBox chkAutoSlip;
        private System.Windows.Forms.NumericUpDown numSlipCount;
        private System.Windows.Forms.Label label1;
    }
}
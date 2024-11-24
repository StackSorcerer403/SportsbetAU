using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BettingBot
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            initValues();
        }

        private void initValues()
        {
            txtProxyUser.Text = Setting.instance.proxyUser;
            txtProxyPass.Text = Setting.instance.proxyPass;

            txtProxy.Text = Setting.instance.proxy;
            setCurrency.Text  = Setting.instance.currency;

            txtBetUser.Text = Setting.instance.betUser;
            txtBetPass.Text = Setting.instance.betPassword;          
            chkAutoSlip.Checked = Setting.instance.enableAutoSlip;
            chkAutoStaker.Checked = Setting.instance.enableAutoStaker;
            numTotalReturn.Value = (decimal)Setting.instance.totalReturn;
            numSlipCount.Value = (decimal)Setting.instance.slipCount;
            //numBeforeKickoff.Value = (decimal)Setting.instance.beforeKickoff;

        }

        private bool canSet()
        {

            if (string.IsNullOrEmpty(txtBetUser.Text) || string.IsNullOrEmpty(txtBetPass.Text) || string.IsNullOrEmpty(setCurrency.Text))
            {
                MessageBox.Show(this, "Please check if you put account details correctly!", "Alert");
                return false;
            }

            return true;
        }

        private void setValues()
        {
            Setting.instance.proxy = txtProxy.Text;
            Setting.instance.proxyUser = txtProxyUser.Text;
            Setting.instance.proxyPass = txtProxyPass.Text;

            Setting.instance.betUser = txtBetUser.Text;
            Setting.instance.betPassword = txtBetPass.Text;
            Setting.instance.currency    = setCurrency.Text;

            Setting.instance.totalReturn = (double)numTotalReturn.Value;
            Setting.instance.slipCount = (int)numSlipCount.Value;
            Setting.instance.enableAutoSlip = chkAutoSlip.Checked;
            Setting.instance.enableAutoStaker = chkAutoStaker.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!canSet())
                return;

            setValues();
            Setting.instance.saveSettingInfo();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numBeforeKickoff_ValueChanged(object sender, EventArgs e)
        {

        }

        private void chkAutoSlip_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void numSlipCount_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

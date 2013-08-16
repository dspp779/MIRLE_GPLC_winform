using MIRLE_GPLC.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MIRLE_GPLC.form
{
    public partial class PLCForm : Form
    {
        private ProjectData project;
        public PLC plc = null;

        public PLCForm(ProjectData p)
        {
            InitializeComponent();
            this.project = p;
        }

        public PLCForm(ProjectData project, PLC plc)
        {
            InitializeComponent();
            this.project = project;
            this.plc = plc;
        }

        private void PLCForm_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            if (plc != null)
            {
                this.Text = "設定PLC";
                textBox_name.Text = plc.alias;
                textBox_net_ID.Text = plc.netid.ToString();
                textBox_net_ip.Text = plc.ip;
                textBox_net_port.Text = plc.port.ToString();
            }
            else
            {
                this.Text = "新增PLC到專案";
            }
            label_project.Text = project.name;
        }

        private void PLCForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    long id = (plc != null) ? plc.id: -1;
                    plc = new PLC(id, int.Parse(textBox_net_ID.Text), textBox_net_ip.Text,
                        int.Parse(textBox_net_port.Text), textBox_name.Text, null);
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                    e.Cancel = true;
                }
            }
        }


    }
}

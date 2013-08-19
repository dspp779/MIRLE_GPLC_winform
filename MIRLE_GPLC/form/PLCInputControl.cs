using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MIRLE_GPLC.Model;

namespace MIRLE_GPLC.form
{
    public partial class PLCInputControl : UserControl
    {
        private ProjectData project;
        public PLC plc = null;

        public PLCInputControl()
        {
            InitializeComponent();
        }

        public void init(ProjectData p)
        {
            this.project = p;
            this.plc = null;
            // UI
            textBox_name.Text = "";
            textBox_net_ID.Text = "";
            textBox_net_ip.Text = "";
            label_project.Text = "新增PLC";
            this.Show();
        }

        public void init(ProjectData project, PLC plc)
        {
            this.project = project;
            this.plc = plc;

            // UI
            textBox_name.Text = plc.alias;
            textBox_net_ID.Text = plc.netid.ToString();
            textBox_net_ip.Text = plc.ip;
            textBox_net_port.Text = plc.port.ToString();
            label_project.Text = "設定PLC";
            this.Show();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (plc != null)
                {
                    plc = new PLC(plc.id, int.Parse(textBox_net_ID.Text), textBox_net_ip.Text,
                        int.Parse(textBox_net_port.Text), textBox_name.Text, null);
                    ModelUtil.inputPLC(plc, project.id);
                }
                else
                {
                    ModelUtil.insertPLC(int.Parse(textBox_net_ID.Text), textBox_net_ip.Text,
                        int.Parse(textBox_net_port.Text), textBox_name.Text, project.id);
                }
                this.Parent.Refresh();
            }
            catch (FormatException)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Parent.Refresh();
        }

    }
}

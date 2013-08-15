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
    public partial class DataFieldForm : Form
    {
        public Record record;

        public DataFieldForm()
        {
            InitializeComponent();
        }

        public DataFieldForm(Record record)
        {
            InitializeComponent();
            this.record = record;
        }

        private void DataFieldForm_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            if (record != null)
            {
                this.Text = "Modify data:" + record.alias;
                textBox_name.Text = record.alias;
                textBox_addr.Text = record.addr.ToString();
                textBox_length.Text = record.length.ToString();
                comboBox_format.Text = record.format;
            }
            else
            {
                this.Text = "Add data";
            }
        }

        private void DataFieldForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    record = new Record(record.PLC_ID, int.Parse(textBox_addr.Text),
                    int.Parse(textBox_length.Text), comboBox_format.Text, textBox_name.Text);
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

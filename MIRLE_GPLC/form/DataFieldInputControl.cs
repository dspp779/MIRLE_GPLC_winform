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
    public partial class DataFieldInputControl : UserControl
    {
        public long plcid = 0;
        public Record record = null;

        public DataFieldInputControl()
        {
            InitializeComponent();
        }

        public void init(long plcid)
        {
            this.plcid = plcid;
            this.record = null;
            // ui
            label_field.Text = "新增資料項";
            textBox_name.Tag = "資料項名稱";
            textBox_addr.Tag = "開始位址";
            textBox_length.Tag = "長度";
            comboBox_format.Text = "";
            this.Show();
        }

        public void init(long plcid, Record record)
        {
            this.plcid = plcid;
            this.record = record;
            // ui
            label_field.Text = "修改資料項";
            textBox_name.Text = record.alias;
            textBox_addr.Text = record.addr.ToString();
            textBox_length.Text = record.length.ToString();
            comboBox_format.Text = record.format;
            this.Show();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (record != null)
                {
                    record = new Record(record.id, int.Parse(textBox_addr.Text), int.Parse(textBox_length.Text),
                        comboBox_format.Text, textBox_name.Text, plcid);
                    ModelUtil.inputItem(record);
                }
                else
                {
                    ModelUtil.insertItem(int.Parse(textBox_addr.Text), int.Parse(textBox_length.Text),
                    comboBox_format.Text, textBox_name.Text, plcid);
                }
                this.Parent.Refresh();
            }
            catch (FormatException)
            {
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Parent.Refresh();
        }

    }
}

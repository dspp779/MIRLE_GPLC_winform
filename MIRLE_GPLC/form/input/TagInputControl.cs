using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MIRLE_GPLC.Model;
using MIRLE_GPLC.Security;

namespace MIRLE_GPLC.form
{
    public partial class TagInputControl : UserControl
    {
        public long plcid = 0;
        public Tag tag = null;

        public TagInputControl()
        {
            InitializeComponent();
        }

        public void init(long plcid)
        {
            this.plcid = plcid;
            this.tag = null;
            // ui
            label_field.Text = "新增資料項";
            textBox_name.Text = "";
            textBox_addr.Text = "";
            comboBox_type.Text = "WORD";
            comboBox_format.Text = "####";
            textBox_unit.Text = "";
            comboBox_scale_type.Text = "WORD";
            //show
            this.Show();
        }
        public void init(long plcid, Tag tag)
        {
            this.plcid = plcid;
            this.tag = tag;
            // get scaling info
                checkBox_scale_linear.Checked = false;
                comboBox_scale_type.Text = "WORD";
                textBox_raw_hi.Text = "";
                textBox_raw_lo.Text = "";
                textBox_scale_hi.Text = "";
                textBox_scale_lo.Text = "";
            // show
            this.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Parent.Refresh();
        }

        private void checkBox_scale_linear_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_scale_type.Enabled = textBox_raw_hi.Enabled = textBox_raw_lo.Enabled
                = textBox_scale_hi.Enabled = textBox_scale_lo.Enabled
                = checkBox_scale_linear.Checked;
        }

    }
}

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
            comboBox_type.Text = "";
            comboBox_format.Text = "";
            this.Show();
        }

        public void init(long plcid, Tag tag)
        {
            this.plcid = plcid;
            this.tag = tag;
            // ui
            label_field.Text = "修改資料項";
            textBox_name.Text = tag.alias;
            textBox_addr.Text = tag.addr.ToString();
            comboBox_type.SelectedText = tag.type.ToString();
            comboBox_format.SelectedText = tag.format;
            this.Show();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                // check authentication
                GPLC.Auth(GPLCAuthority.Administrator);

                if (tag != null)
                {
                    tag = new Tag(tag.id, textBox_name.Text, int.Parse(textBox_addr.Text), comboBox_type.Text,
                        comboBox_format.Text, textBox_unit.Text, plcid);
                    ModelUtil.inputTag(tag);
                }
                else
                {
                    ModelUtil.insertTag(textBox_name.Text, int.Parse(textBox_addr.Text), comboBox_type.Text,
                    comboBox_format.Text, textBox_unit.Text, plcid);
                }
                this.Parent.Refresh();
            }
            catch (FormatException)
            {
            }
            catch (UnauthorizedException ex)
            {
                MessageBox.Show(ex.Message, "Fatal Error");
                Application.Exit();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Parent.Refresh();
        }

        private void checkBox_scale_linear_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_type.Enabled = textBox_raw_hi.Enabled = textBox_raw_lo.Enabled
                = textBox_scale_hi.Enabled = textBox_scale_lo.Enabled
                = checkBox_scale_linear.Checked;
        }

    }
}

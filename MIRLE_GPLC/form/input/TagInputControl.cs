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
            Scaling s = ModelUtil.getScaling(tag.id);
            if (s != null)
            {
                comboBox_scale_type.Text = s.scale_type.ToString();
                textBox_raw_hi.Text = s.raw_hi.ToString();
                textBox_raw_lo.Text = s.raw_lo.ToString();
                textBox_scale_hi.Text = s.scale_hi.ToString();
                textBox_scale_lo.Text = s.scale_lo.ToString();
            }
            else
            {
                comboBox_scale_type.Text = "WORD";
                textBox_raw_hi.Text = "";
                textBox_raw_lo.Text = "";
                textBox_scale_hi.Text = "";
                textBox_scale_lo.Text = "";
            }
            // ui
            label_field.Text = "修改資料項";
            textBox_name.Text = tag.alias;
            textBox_addr.Text = tag.addr.ToString();
            comboBox_type.Text = tag.type.ToString();
            comboBox_format.Text = tag.format;
            textBox_unit.Text = tag.unit;
            // show
            this.Show();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                // check authentication
                GPLC.Auth(GPLCAuthority.Administrator);

                long id = (tag != null) ? tag.id : -1;
                Tag inputTag = new Tag(id, textBox_name.Text,
                        textBox_addr.Text, comboBox_type.Text,
                        comboBox_format.Text, textBox_unit.Text, plcid);

                if (checkBox_scale_linear.Checked)
                {
                    Scaling scale = new Scaling(comboBox_scale_type.Text,
                        textBox_raw_hi.Text, textBox_raw_lo.Text,
                        textBox_scale_hi.Text, textBox_scale_lo.Text);
                    id = id < 0 ? ModelUtil.inputTag(inputTag) : id;
                    ModelUtil.inputScaling(scale, id);
                }
                else
                {
                    ModelUtil.inputTag(inputTag);
                }
                this.Parent.Refresh();
            }
            catch (FormatException ex)
            {
                label_info.Text = ex.Message;
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
            comboBox_type.Enabled = comboBox_scale_type.Enabled
                = textBox_raw_hi.Enabled = textBox_raw_lo.Enabled
                = textBox_scale_hi.Enabled = textBox_scale_lo.Enabled
                = checkBox_scale_linear.Checked;
        }

    }
}

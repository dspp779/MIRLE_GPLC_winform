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
            textBox_length.Text = "";
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
            textBox_length.Text = tag.length.ToString();
            comboBox_format.Text = tag.format;
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
                    tag = new Tag(tag.id, int.Parse(textBox_addr.Text), int.Parse(textBox_length.Text),
                        comboBox_format.Text, textBox_name.Text, plcid);
                    ModelUtil.inputTag(tag);
                }
                else
                {
                    ModelUtil.insertTag(int.Parse(textBox_addr.Text), int.Parse(textBox_length.Text),
                    comboBox_format.Text, textBox_name.Text, plcid);
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

    }
}

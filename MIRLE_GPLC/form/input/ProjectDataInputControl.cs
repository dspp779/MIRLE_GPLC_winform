using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MIRLE_GPLC.Model;
using GMap.NET.WindowsForms;
using MIRLE_GPLC.form.marker;
using MIRLE_GPLC.Security;

namespace MIRLE_GPLC.form
{
    public partial class ProjectDataInputControl : UserControl
    {
        private GMapMarker marker;

        public ProjectDataInputControl()
        {
            InitializeComponent();
        }

        public void init(GMapMarker marker)
        {
            // check authentication
            GPLC.Auth(GPLCAuthority.Administrator);

            this.marker = marker;
            if (marker is ProjectMarker)
            {
                ProjectData p = (marker as ProjectMarker).ProjectData;
                label_project.Text = "設定專案";
                InputButton.Text = "Modify";
                setInput(p.name, p.addr, p.lat, p.lng);
                DeleteButton.Visible = true;
            }
            else
            {
                label_project.Text = "新增專案";
                setInput("", "", marker.Position.Lat, marker.Position.Lng);
                InputButton.Dock = DockStyle.Fill;
            }
            this.Show();
        }

        private void setInput(string name, string addr, double lat, double lng)
        {
            textBox_case_Name.Text = name;
            richTextBox_case_addr.Text = addr;
            textBox_latlng_lat.Text = lat.ToString();
            textBox_latlng_lng.Text = lng.ToString();
        }

        private void InputButton_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox_case_Name.Text.Trim();
                string addr = richTextBox_case_addr.Text.Trim();
                double lat = double.Parse(textBox_latlng_lat.Text.Trim());
                double lng = double.Parse(textBox_latlng_lng.Text.Trim());
                // check name
                if (name.Length < 4)
                {
                    throw new FormatException("Name長度必須超過4");
                }
                // check latlng
                if (lat > 90 || lat < -90 || lng > 180 || lat < -180)
                {
                    throw new FormatException("Latlng value out of bound");
                }

                // check authentication
                GPLC.Auth(GPLCAuthority.Administrator);

                // update database
                if (InputButton.Text.Equals("Modify"))
                {
                    long id = (marker as ProjectMarker).ProjectData.id;
                    ModelUtil.updateProject(id, name, addr, lat, lng);
                }
                else
                {
                    ModelUtil.insertProject(name, addr, lat, lng);
                }

                //refresh map
                GPLC.Refresh();
                this.Parent.Dispose();
            }
            catch (FormatException)
            {
                this.Parent.Dispose();
            }
            catch (UnauthorizedException ex)
            {
                MessageBox.Show(ex.Message, "Fatal Error");
                Application.Exit();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            // check authentication
            GPLC.Auth(GPLCAuthority.Administrator);

            ModelUtil.deleteProject((marker as ProjectMarker).ProjectData.id);
            //refresh map
            GPLC.Refresh();
            this.Parent.Dispose();
        }
    }
}

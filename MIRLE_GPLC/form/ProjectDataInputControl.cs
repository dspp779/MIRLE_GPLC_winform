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
            this.marker = marker;
            if (marker is ProjectMarker)
            {
                ProjectData p = (marker as ProjectMarker).ProjectData;
                label_project.Text = "設定專案";
                InputButton.Text = "Modify";
                setInput(p.name, p.addr, p.lat, p.lng);
            }
            else
            {
                label_project.Text = "新增專案";
                setInput("", "", marker.Position.Lat, marker.Position.Lng);
            }
            this.Show();
        }

        private void InputButton_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox_case_Name.Text;
                string addr = richTextBox_case_addr.Text;
                double lat = double.Parse(textBox_latlng_lat.Text);
                double lng = double.Parse(textBox_latlng_lng.Text);
                // check latlng
                if (lat > 90 || lat < -90 || lng > 180 || lat < -180)
                {
                    throw new FormatException("Latlng value out of bound");
                }

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
                Program.mainForm.loadProjects();
                this.Parent.Dispose();
            }
            catch (FormatException)
            {
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            if (marker is ProjectMarker)
            {
                ProjectData p = (marker as ProjectMarker).ProjectData;
                setInput(p.name, p.addr, p.lat, p.lng);
            }
            else
            {
                setInput("", "", marker.Position.Lat, marker.Position.Lng);
            }
        }

        private void setInput(string name, string addr, double lat, double lng)
        {
            textBox_case_Name.Text = name;
            richTextBox_case_addr.Text = addr;
            textBox_latlng_lat.Text = lat.ToString();
            textBox_latlng_lng.Text = lng.ToString();
        }
    }
}

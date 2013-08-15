using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace SuperContextMenu
{
    public partial class MainForm : Form
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }




        ContextMenuForButton m_popedContainerForButton;//a user control that is derievd from PopedContainer; it can contain any type of controls and you design it as if you design a form!!!
        PoperContainer m_poperContainerForButton;//the container... which displays previous user control as a context poped up menu

        ContextMenuForForm m_popedContainerForForm;
        PoperContainer m_poperContainerForForm;

        ContextMenuForLabel m_popedContainerForLabel;
        PoperContainer m_poperContainerForLabel;








        public MainForm()
        {
            InitializeComponent();


            m_popedContainerForButton = new ContextMenuForButton();
            m_poperContainerForButton = new PoperContainer(m_popedContainerForButton);

            m_popedContainerForForm = new ContextMenuForForm();
            m_poperContainerForForm = new PoperContainer(m_popedContainerForForm);

            m_popedContainerForLabel = new ContextMenuForLabel();
            m_poperContainerForLabel = new PoperContainer(m_popedContainerForLabel);

            // add the event handlers here for the changes to take effects...................................
            // NOTE: members must be public
            m_popedContainerForButton.buttonCut.Click += new System.EventHandler(this.buttonCut_Click);
            m_popedContainerForButton.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            m_popedContainerForButton.buttonPaste.Click += new System.EventHandler(this.buttonPaste_Click);
            m_popedContainerForButton.comboBoxBorder.SelectedIndexChanged += new System.EventHandler(this.comboBoxBorder_SelectedIndexChanged);
            m_popedContainerForButton.textBoxCaption.TextChanged += new EventHandler(textBoxCaption_TextChanged);
            m_popedContainerForButton.buttonColor.Click += new System.EventHandler(this.buttonColor_Click);

            m_popedContainerForForm.textBoxCaption.TextChanged += new EventHandler(this.ChangeFormTitle);
            m_popedContainerForForm.buttonColor.Click += new System.EventHandler(this.ChangeFormBKColor);

            m_popedContainerForLabel.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);

        }



                          













        //simulate editing an item, show the super context menu for it .......................
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                m_poperContainerForButton.Show(button1);
            }
        }


        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                m_poperContainerForForm.Show(this,e.Location);
            }

            base.OnMouseClick(e);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                m_poperContainerForLabel.Show(this.label1);
            }
        }












        //respond to the changes
        private void buttonCut_Click(object sender, EventArgs e)
        {
            m_poperContainerForButton.Close();//manually close it coz the user finished the command

            button1.Text = "I GOT IT, we should cut this";
        }


        private void buttonCopy_Click(object sender, EventArgs e)
        {
            m_poperContainerForButton.Close();//manually close it coz the user finished the command

            button1.Text = "ok, copy the text to the clib board";
        }

        private void buttonPaste_Click(object sender, EventArgs e)
        {
            m_poperContainerForButton.Close();//manually close it coz the user finished the command

            button1.Text = "ok, will paste later";
        }


        private void comboBoxBorder_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (m_popedContainerForButton.comboBoxBorder.SelectedIndex)
            {
                case 0: button1.FlatStyle = FlatStyle.Flat; break;
                case 1: button1.FlatStyle = FlatStyle.Popup; break;
                case 2: button1.FlatStyle = FlatStyle.Standard; break;
                case 3: button1.FlatStyle = FlatStyle.System; break;
            }
        }

        void textBoxCaption_TextChanged(object sender, EventArgs e)
        {
            button1.Text = m_popedContainerForButton.textBoxCaption.Text;
        }


        private void buttonColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.button1.ForeColor = colorDialog1.Color;
                m_popedContainerForButton.buttonColor.BackColor = colorDialog1.Color;
            }
        }

        void ChangeFormTitle(object sender, EventArgs e)
        {
            this.Text = m_popedContainerForForm.textBoxCaption.Text;
        }


        private void ChangeFormBKColor(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = colorDialog1.Color;
                m_popedContainerForForm.buttonColor.BackColor = colorDialog1.Color;
            }
        }



        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.label1.Font = new Font(this.label1.Font.FontFamily, m_popedContainerForLabel.trackBar1.Value);

        }






    }
}
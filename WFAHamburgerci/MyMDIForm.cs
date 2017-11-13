using System;
using System.Windows.Forms;

namespace WFAHamburgerci
{
    public partial class MyMDIForm : Form
    {
        public MyMDIForm()
        {
            InitializeComponent();
        }

        private void MyMDIForm_Load(object sender, EventArgs e)
        {

        }


        void ChildForm(Form _childForm)
        {
            this.Width = _childForm.Width + 22;
            this.Height = _childForm.Height + 68;

            bool durum = false;
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Text == _childForm.Text)
                {
                    durum = true;
                    frm.Activate();
                }
                else
                {
                    frm.Close();
                }
            }
            if (durum == false)
            {
                _childForm.MdiParent = this;
                _childForm.Show();
            }
        }

        private void siparişOluşturToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm(new Form1());
        }

        private void hamburgerEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm(new Form2());
        }

        private void extraMalzemeEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm(new Form3());
        }

        private void siparişBilgileriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildForm(new Form4());
        }

        
    }
}

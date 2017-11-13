using System;
using System.Windows.Forms;

namespace WFAHamburgerci
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.Ekstralar.Add(new Extra(txtAd.Text, nmrFiyat.Value));
            Fonksiyon.Temizle(this.Controls);
            MessageBox.Show("Başarıyla Eklendi.");
        }
    }
}

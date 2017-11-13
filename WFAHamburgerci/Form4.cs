using System;
using System.Windows.Forms;

namespace WFAHamburgerci
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            decimal ciro = 0;
            decimal exMalzemeGeliri = 0;
            int satisAdedi = 0;
            foreach (Siparis item in Form1.Siparisler)
            {
                ciro += item.ToplamTutar;

                foreach (Extra ex in item.ExtraMalzemesi)
                {
                    exMalzemeGeliri += ex.Fiyati;
                }

                satisAdedi += item.Adet;

                lstSiparisler.Items.Add(item);
            }

            lblSiparisSayisi.Text = lstSiparisler.Items.Count.ToString();
            lblCiro.Text = ciro.ToString("C2");
            lblExtraMalzemeGeliri.Text = exMalzemeGeliri.ToString("C2");
            lblSatilanUrunAdedi.Text = satisAdedi.ToString();
        }
    }
}

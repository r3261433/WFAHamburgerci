using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WFAHamburgerci
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static List<Siparis> Siparisler = new List<Siparis>();
        public static List<Siparis> MevcutSiparisler = new List<Siparis>();

        public static List<Menu> Menuler = new List<Menu>();
        public static List<Extra> Ekstralar = new List<Extra>();

        private void Form1_Load(object sender, EventArgs e)
        {
            FileHelper.MenuYarat();
            FileHelper.IlkMenuDegerleriniEkle();
            Menuler = FileHelper.MenuleriOku();

            foreach (Menu item in Menuler)
                cbMenuler.Items.Add(item);

            FileHelper.EkstraYarat();
            Ekstralar = FileHelper.EkstralariOku();

            foreach (Extra ex in Ekstralar)
                flpExtraMalzemeler.Controls.Add(new CheckBox() { Text = ex.ExtraAdi, Tag = ex });

            foreach (Siparis item in MevcutSiparisler)
                lstSiparisler.Items.Add(item);

            TutarHesapla();

            cbMenuler.SelectedIndex = 0;
        }

        private void btnSiparisEkle_Click(object sender, EventArgs e)
        {
            Siparis yeniSiparis = new Siparis();
            yeniSiparis.SeciliMenusu = (Menu)cbMenuler.SelectedItem;

            if (rbKucuk.Checked)
                yeniSiparis.Boyutu = Boyut.Kucuk;
            else if (rbOrta.Checked)
                yeniSiparis.Boyutu = Boyut.Orta;
            else
                yeniSiparis.Boyutu = Boyut.Buyuk; 

            yeniSiparis.ExtraMalzemesi = new List<Extra>();
            foreach (CheckBox item in flpExtraMalzemeler.Controls)
            {
                if (item.Checked)
                {
                    yeniSiparis.ExtraMalzemesi.Add((Extra)item.Tag);
                }
            } 

            yeniSiparis.Adet = Convert.ToInt32(nmrAdet.Value);
            yeniSiparis.Hesapla();
            MevcutSiparisler.Add(yeniSiparis);
            Siparisler.Add(yeniSiparis);
            lstSiparisler.Items.Add(yeniSiparis);

            TutarHesapla();

            Fonksiyon.Temizle(this.Controls);
        }

        private void btmSiparisTamamla_Click(object sender, EventArgs e)
        {
            decimal toplamTutar = TutarHesapla();

            DialogResult dr = MessageBox.Show("Toplam Sipariş Ücreti : " + toplamTutar.ToString("C2") + " Satın almayı tamamlamak istermisiniz?", "Sipariş Bilgisi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                var siparisList = lstSiparisler.Items.Cast<Siparis>().ToList();
                FileHelper.SiparisleriKaydet(siparisList);

                lstSiparisler.Items.Clear();
                MevcutSiparisler.Clear();
                TutarHesapla();
                MessageBox.Show("Sipariş Tamamlandı!");
            }
            else
            {
                MessageBox.Show("İptal Edildi!");
            }
        }

        private decimal TutarHesapla()
        {
            decimal toplamTutar = 0;
            for (int i = 0; i < lstSiparisler.Items.Count; i++)
            {
                Siparis gelen = (Siparis)lstSiparisler.Items[i];
                toplamTutar += gelen.ToplamTutar;
            }

            lblToplamTutar.Text = toplamTutar.ToString("C2");

            return toplamTutar;
        }
    }
}

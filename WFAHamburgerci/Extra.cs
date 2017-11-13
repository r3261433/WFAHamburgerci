namespace WFAHamburgerci
{
    public class Extra
    {
        public string ExtraAdi { get; set; }
        public decimal Fiyati { get; set; }

        public Extra(string _extraAdi, decimal _fiyati)
        {
            ExtraAdi = _extraAdi;
            Fiyati = _fiyati;
        }
    }
}

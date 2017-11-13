using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFAHamburgerci
{
    [Serializable]
    public class Menu
    {
        public string MenuAdi { get; set; }
        public decimal Fiyati { get; set; }

        public override string ToString()
        {
            return MenuAdi + " Menu";
        }

        public Menu()
        {
            MenuAdi = string.Empty;
            Fiyati = 0;
        }

        public Menu(string _menuAdi, decimal _fiyati)
        {
            MenuAdi = _menuAdi;
            Fiyati = _fiyati;
        }
    }
}

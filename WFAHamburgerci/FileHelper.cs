using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WFAHamburgerci
{
    public static class FileHelper
    {
        private static string KlasorYolu = @"C:\\WFAHamburgerci";
        private static string MenuYolu = Path.Combine(KlasorYolu, "Menuler.xml");
        private static string EkstraYolu = Path.Combine(KlasorYolu, "Ekstralar.xml");
        private static string SiparisYolu = Path.Combine(KlasorYolu, "Siparisler.xml");

        public static void KlasorYarat()
        {
            if (!Directory.Exists(KlasorYolu))
                Directory.CreateDirectory(KlasorYolu);
        }

        #region Menüler

        public static void MenuYarat()
        {
            KlasorYarat();

            if (!File.Exists(MenuYolu))
            {
                var file = File.Create(MenuYolu);
                file.Close();
            }
        } 

        public static void IlkMenuDegerleriniEkle()
        {
            MenuSchema menuler = new MenuSchema();
            List<MenuSchemaMenu> menuList = new List<MenuSchemaMenu>() {
                                                            new MenuSchemaMenu() { MenuAdi = "SteakHouse", Fiyati = 18.25M },
                                                            new MenuSchemaMenu() { MenuAdi = "McFish", Fiyati = 8M },
                                                            new MenuSchemaMenu() { MenuAdi = "Whooper", Fiyati = 10M },
                                                            new MenuSchemaMenu() { MenuAdi = "BigKing", Fiyati = 13.75M },
                                                            new MenuSchemaMenu() { MenuAdi = "Chicken Royal", Fiyati = 17M },
                                                            new MenuSchemaMenu() { MenuAdi = "King Chicken", Fiyati = 11M }
            };
             
            menuler.Menu = menuList.ToArray();

            //XElement menuler = new XElement("Menuler",
            //                                            new XElement("Menu",
            //                                                    new XElement("MenuAdi", "Steakhouse"),
            //                                                    new XElement("Fiyati", "18,25")
            //                                                        ),
            //                                            new XElement("Menu",
            //                                                    new XElement("MenuAdi", "McFish"),
            //                                                    new XElement("Fiyati", "8")
            //                                                        ),
            //                                            new XElement("Menu",
            //                                                    new XElement("MenuAdi", "Whooper"),
            //                                                    new XElement("Fiyati", "10")
            //                                                        ),
            //                                            new XElement("Menu",
            //                                                    new XElement("MenuAdi", "BigKing"),
            //                                                    new XElement("Fiyati", "13,75")
            //                                                        ),
            //                                            new XElement("Menu",
            //                                                    new XElement("MenuAdi", "Chicken Royal"),
            //                                                    new XElement("Fiyati", "17")
            //                                                        ),
            //                                            new XElement("Menu",
            //                                                    new XElement("MenuAdi", "King Chicken"),
            //                                                    new XElement("Fiyati", "11")
            //                                                        )
            //                                           );

            XmlSerializer xsSubmit = new XmlSerializer(typeof(MenuSchema));
            var xml = "";

            using (var sw = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sw))
                {
                    xsSubmit.Serialize(writer, menuler);
                    xml = sw.ToString();
                }
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            xmlDoc.Save(MenuYolu);
        }

        public static List<Menu> MenuleriOku()
        {
            if (File.Exists(MenuYolu))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(MenuYolu);

                XmlNode menulerNode = xml.ChildNodes[1];
                List<Menu> menuList = new List<Menu>();

                foreach (XmlNode menu in menulerNode)
                    menuList.Add(new Menu(menu["MenuAdi"].InnerText.Trim(), Convert.ToDecimal(menu["Fiyati"].InnerText.Trim())));

                return menuList;
            }

            return new List<Menu>();
        }

        public static void MenuEkle(Menu menuBilgisi)
        {
            MenuYarat();

            XElement menu = new XElement("Menu",
                                    new XElement("MenuAdi", menuBilgisi.MenuAdi),
                                    new XElement("Fiyati", menuBilgisi.Fiyati)
                                        );

            FileStream stream = File.Open(MenuYolu, FileMode.Open);
            XDocument xml = XDocument.Load(stream);

            stream.Close();

            xml.Root.Add(menu);
            xml.Save(MenuYolu);
        }

        #endregion Menüler

        #region Ekstralar

        public static void EkstraYarat()
        {
            KlasorYarat();

            if (!File.Exists(EkstraYolu))
            {
                var file = File.Create(EkstraYolu);
                file.Close();
            }

            XElement ekstralar = new XElement("Ekstralar",
                                            new XElement("Ekstra",
                                                    new XElement("EkstraAdi", "Hardal"),
                                                    new XElement("Fiyati", "0,50")
                                                        ),
                                            new XElement("Ekstra",
                                                    new XElement("EkstraAdi", "Ketçap"),
                                                    new XElement("Fiyati", "0,25")
                                                        ),
                                            new XElement("Ekstra",
                                                    new XElement("EkstraAdi", "Ranch"),
                                                    new XElement("Fiyati", "0,35")
                                                        ),
                                            new XElement("Ekstra",
                                                    new XElement("EkstraAdi", "BBQ"),
                                                    new XElement("Fiyati", "0,75")
                                                        ),
                                            new XElement("Ekstra",
                                                    new XElement("EkstraAdi", "Mayonez"),
                                                    new XElement("Fiyati", "0,1")
                                                        ),
                                            new XElement("Ekstra",
                                                    new XElement("EkstraAdi", "Soğan"),
                                                    new XElement("Fiyati", "1")
                                                        ),
                                            new XElement("Ekstra",
                                                    new XElement("EkstraAdi", "Buffalo"),
                                                    new XElement("Fiyati", "0.60")
                                                        )
                                           );

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(ekstralar.ToString());
            xml.Save(EkstraYolu);
        }

        public static List<Extra> EkstralariOku()
        {
            if (File.Exists(EkstraYolu))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(EkstraYolu);

                XmlNode ekstralarNode = xml.ChildNodes[0];
                List<Extra> ekstraList = new List<Extra>();

                foreach (XmlNode ekstra in ekstralarNode)
                    ekstraList.Add(new Extra(ekstra["EkstraAdi"].InnerText.Trim(), Convert.ToDecimal(ekstra["Fiyati"].InnerText.Trim())));

                return ekstraList;
            }

            return new List<Extra>();
        }

        #endregion Ekstralar

        #region Siparişler

        public static void SiparisleriKaydet(List<Siparis> siparisList)
        {
            KlasorYarat();

            if (!File.Exists(SiparisYolu))
            {
                var file = File.Create(SiparisYolu);
                file.Close();
            }

            XElement siparisNodeList = new XElement("Siparisler");

            foreach (Siparis siparis in siparisList)
            {
                XElement siparisNode = new XElement("Siparis",
                                               new XElement("MenuAdi", siparis.SeciliMenusu.MenuAdi),
                                               new XElement("Fiyati", siparis.SeciliMenusu.Fiyati),
                                               new XElement("Boyutu", siparis.Boyutu.ToString()),
                                               new XElement("Adedi", siparis.Adet.ToString()),
                                               new XElement("ToplamTutar", siparis.ToplamTutar.ToString())
                                                    );

                if (siparis.ExtraMalzemesi.Count > 0)
                {
                    XElement ekstralarNode = new XElement("Ekstralar");

                    foreach (Extra ekstra in siparis.ExtraMalzemesi)
                    {
                        XElement ekstraNode = new XElement("Ekstra",
                                                        new XElement("EkstraAdi", ekstra.ExtraAdi),
                                                        new XElement("Fiyati", ekstra.Fiyati)
                                                          );

                        ekstralarNode.Add(ekstraNode);
                    }

                    siparisNode.Add(ekstralarNode);
                }

                siparisNodeList.Add(siparisNode);
            }

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(siparisNodeList.ToString());
            xml.Save(SiparisYolu);
        }

        #endregion Siparişler
    }
}

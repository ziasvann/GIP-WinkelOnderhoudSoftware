using System;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Xml;

namespace GIP_WinkelProductenSysteem
{
    public partial class ChangeProd : Form
    {
        public ChangeProd()
        {
            InitializeComponent();
        }

        private void ChangeProd_Load(object sender, EventArgs e)
        {
            LoadProducten();
            werkCategorieënBij();
            werkNamenBij();
        }

        bool test = false;

        public bool newProd = false;
        public bool naamFout = false;
        public bool categorieFout = false;
        public bool aantalAanwezigFout = false;
        public bool aantalBestAanwezigFout = false;
        public bool prijsFout = false;
        public bool kortingFout = false;


        public string welkomMsg = "Welkom!\nIn dit venster kunt u producten aanmaken en wijzigen. Momenteel zijn er nog geen producten opgeslagen.\nBegin dus met een product aan te maken.";
        public string zekerMsg = "Ben u zeker?";
        public string abortMsg = "Geen probleem! Er is niets veranderd.";
        public string selectItemMsg = "Gelieve één item te selecteren.";
        public string foutenMsg = "Er zit een fout in de ingevoerde gegevens.";
        public string naamAanwezigMsg = "Deze naam is al gebruikt voor een ander product. Kies een andere naam.";

        //string filePath = @"D:\ZIAS\School\2021-2022\GIP\Projects\GIP\GIP-WinkelProductenSysteem\GIP-WinkelProductenSysteem\Producten.xml";
        string filePath = Application.LocalUserAppDataPath + @"\Producten.xml";

        string[] categorieën = new string[0];
        string[] namen = new string[0];


        //'huidigeNaam' wordt gebruikt om te controleren of de naam is gewijzigd na het aanpassen van een product.
        string huidigeNaam = "";

        private void LoadProducten()
        {
            if (!File.Exists(filePath))
            {
                MakeXmlProducten();
                MessageBox.Show(welkomMsg, "Welkom!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (File.ReadAllText(filePath) == "")
            {
                MakeXmlProducten();
                MessageBox.Show(welkomMsg, "Welkom!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else FillListView();
        }

        private void btnMaakProduct_Click(object sender, EventArgs e)
        {
            newProd = true;
            ClearTxb();
            pnlProductEigenschappen.Visible = true;
        }

        private void btnBevestigProducten_Click(object sender, EventArgs e)
        {
            ControleerTxb(txbNaam);
            ControleerTxb(txbCategorie);
            ControleerTxb(txbAantalAanwezig);
            ControleerTxb(txbAantalBestAanwezig);
            ControleerTxb(txbPrijs);
            ControleerTxb(txbKorting);

            if (GeenErrors())
            {
                pnlProductEigenschappen.Visible = false;

                if (newProd) MaakNieuwProduct();
                else if (!newProd) WijzigProduct();

                FillListView();
                newProd = false;
            }
            else
            {
                if (test)
                {
                    MessageBox.Show(aantalAanwezigXml(GetTxbData(txbCategorie)).ToString());
                }

                MessageBox.Show(foutenMsg);
            }
        }

        private void btnChangeProd_Click(object sender, EventArgs e)
        {
            FillTxb();
            newProd = false;
            huidigeNaam = GetTxbData(txbNaam);
        }

        private void btnDelProduct_Click(object sender, EventArgs e)
        {
            DelProduct();
            werkCategorieënBij();
        }

        public void MakeXmlProducten()
        {
            XmlWriter xml;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;

            xml = XmlWriter.Create(filePath, settings);

            xml.WriteStartDocument();
            xml.WriteStartElement("Producten");
            xml.WriteEndElement();
            xml.Close();
        }

        public void MaakNieuwProduct()
        {
            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(filePath, FileMode.Open);
            xmlDoc.Load(file);

            string prodNaam = GetTxbData(txbNaam);
            string prodCategorie = GetTxbData(txbCategorie);
            string prodAantal = GetTxbData(txbAantalAanwezig);
            string prodBestAantal = GetTxbData(txbAantalBestAanwezig);
            string prodPrijs = GetTxbData(txbPrijs);
            string prodKorting = GetTxbData(txbKorting);

            double prijsProd = double.Parse(prodPrijs);
            double kortingProd = double.Parse(prodKorting);
            string nieuwePrijsProd = kortingPrijs(prijsProd, kortingProd).ToString();

            XmlElement product = xmlDoc.CreateElement("Product");

            XmlElement naam = xmlDoc.CreateElement("Naam");
            XmlText naamText = xmlDoc.CreateTextNode(prodNaam);

            XmlElement categorie = xmlDoc.CreateElement("Categorie");
            XmlText categorieText = xmlDoc.CreateTextNode(prodCategorie);

            XmlElement aantal = xmlDoc.CreateElement("Aantal");
            XmlText aantalText = xmlDoc.CreateTextNode(prodAantal);

            XmlElement aantalBest = xmlDoc.CreateElement("Bestaantal");
            XmlText aantalBestText = xmlDoc.CreateTextNode(prodBestAantal);

            XmlElement prijs = xmlDoc.CreateElement("Prijs");
            XmlText prijsText = xmlDoc.CreateTextNode(prodPrijs);

            XmlElement korting = xmlDoc.CreateElement("Korting");
            XmlText kortingText = xmlDoc.CreateTextNode(prodKorting);

            XmlElement nieuwePrijs = xmlDoc.CreateElement("NieuwePrijs");
            XmlText nieuwePrijsText = xmlDoc.CreateTextNode(nieuwePrijsProd);

            naam.AppendChild(naamText);
            categorie.AppendChild(categorieText);
            aantal.AppendChild(aantalText);
            aantalBest.AppendChild(aantalBestText);
            prijs.AppendChild(prijsText);
            korting.AppendChild(kortingText);
            nieuwePrijs.AppendChild(nieuwePrijsText);

            product.AppendChild(naam);
            product.AppendChild(categorie);
            product.AppendChild(aantal);
            product.AppendChild(aantalBest);
            product.AppendChild(prijs);
            product.AppendChild(korting);
            product.AppendChild(nieuwePrijs);

            xmlDoc.DocumentElement.AppendChild(product);
            file.Close();
            xmlDoc.Save(filePath);

            werkCategorieënBij();

            if (test)
            {
                MessageBox.Show($"Prijs: {prodPrijs}\nKorting: {prodKorting}\nNieuwe prijs: {nieuwePrijsProd}");
            }
        }

        public void WijzigProduct()
        {
            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(filePath, FileMode.Open);
            xmlDoc.Load(file);

            string prodNaam = GetTxbData(txbNaam);
            string prodCategorie = GetTxbData(txbCategorie);
            string prodAantal = GetTxbData(txbAantalAanwezig);
            string prodBestAantal = GetTxbData(txbAantalBestAanwezig);
            string prodPrijs = GetTxbData(txbPrijs);
            string prodKorting = GetTxbData(txbKorting);

            double prijsProd = double.Parse(prodPrijs);
            double kortingProd = double.Parse(prodKorting);
            string nieuwePrijsProd = kortingPrijs(prijsProd, kortingProd).ToString();

            foreach (XmlNode xmlNode in xmlDoc.SelectNodes("/Producten/Product"))
            {
                if (xmlNode.SelectSingleNode("Naam").InnerText == prodNaam)
                {
                    xmlNode["Categorie"].InnerText = prodCategorie;
                    xmlNode["Aantal"].InnerText = prodAantal;
                    xmlNode["Bestaantal"].InnerText = prodBestAantal;
                    xmlNode["Prijs"].InnerText = prodPrijs;
                    xmlNode["Korting"].InnerText = prodKorting;
                    xmlNode["NieuwePrijs"].InnerText = nieuwePrijsProd;
                }
            }

            //Afsluiten van bestand
            file.Close();
            //Rechtstreeks schrijven naar bestandsLocatie!! Anders wordt alles opnieuw toegevoegd!
            xmlDoc.Save(filePath);
        }

        public void DelProduct()
        {
            if (lvProducten.SelectedItems.Count == 1)
            {
                DialogResult result = MessageBox.Show(zekerMsg, "Verwijderen?", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    int rowIndex = lvProducten.FocusedItem.Index + 1;

                    XmlDocument xmlDoc = new XmlDocument();
                    FileStream file = new FileStream(filePath, FileMode.Open);
                    xmlDoc.Load(file);

                    XmlNode xmlNode = xmlDoc.SelectSingleNode($"/Producten/Product[{rowIndex}]");
                    xmlNode.ParentNode.RemoveChild(xmlNode);

                    file.Close();
                    xmlDoc.Save(filePath);
                    FillListView();
                }
                else
                {
                    MessageBox.Show(abortMsg);
                    pnlProductEigenschappen.Visible = false;
                }
            }
            else
            {
                MessageBox.Show(selectItemMsg);
                pnlProductEigenschappen.Visible = false;
            }
        }

        public void FillListView()
        {
            lvProducten.Items.Clear();

            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(filePath, FileMode.Open);
            xmlDoc.Load(file);


            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("/Producten/Product");

            foreach (XmlNode xmlNode in xmlNodeList)
            {
                string prodNaam = xmlNode["Naam"].InnerText;
                string prodCategorie = xmlNode["Categorie"].InnerText;
                string prodAantal = xmlNode["Aantal"].InnerText;
                string prodBestAantal = xmlNode["Bestaantal"].InnerText;
                string prodPrijs = xmlNode["Prijs"].InnerText;
                string prodKorting = xmlNode["Korting"].InnerText;
                string prodNieuwePrijs = xmlNode["NieuwePrijs"].InnerText;

                ListViewItem lvi = new ListViewItem(prodNaam);

                lvi.SubItems.Add(prodCategorie);
                lvi.SubItems.Add(prodAantal);
                lvi.SubItems.Add(prodBestAantal);
                lvi.SubItems.Add(prodPrijs);
                lvi.SubItems.Add(prodKorting);
                lvi.SubItems.Add(prodNieuwePrijs);

                lvProducten.Items.Add(lvi);
            }

            file.Close();
        }

        public void FillTxb()
        {
            if (lvProducten.SelectedItems.Count == 1)
            {
                int rowIndex = lvProducten.FocusedItem.Index + 1;

                XmlDocument xmlDoc = new XmlDocument();
                FileStream file = new FileStream(filePath, FileMode.Open);
                xmlDoc.Load(file);

                XmlNode xmlNode = xmlDoc.SelectSingleNode($"/Producten/Product[{rowIndex}]");

                string prodNaam = xmlNode["Naam"].InnerText;
                string prodCategorie = xmlNode["Categorie"].InnerText;
                string prodAantal = xmlNode["Aantal"].InnerText;
                string prodBestAantal = xmlNode["Bestaantal"].InnerText;
                string prodPrijs = xmlNode["Prijs"].InnerText;
                string prodKorting = xmlNode["Korting"].InnerText;

                txbNaam.Text = prodNaam;
                txbNaam.Enabled = false;
                txbCategorie.Text = prodCategorie;
                txbCategorie.Enabled = false;    
                txbAantalAanwezig.Text = prodAantal;
                txbAantalBestAanwezig.Text = prodBestAantal;
                txbPrijs.Text = prodPrijs;
                txbKorting.Text = prodKorting;

                file.Close();

                pnlProductEigenschappen.Visible = true;
            }
            else
            {
                MessageBox.Show(selectItemMsg);
                pnlProductEigenschappen.Visible = false;
            }
        }
        public void ClearTxb()
        {
            txbNaam.Text = "";
            txbNaam.Enabled = true;
            txbCategorie.Text = "";
            txbCategorie.Enabled = true;
            txbAantalAanwezig.Text = "";
            txbAantalBestAanwezig.Text = "";
        }
        public string GetTxbData(TextBox txb)
        {
            string inhoud = "";
            try
            {
                inhoud = txb.Text;
            }
            catch (Exception ex)
            {
                errorProv.SetError(txb, ex.Message);
            }
            return inhoud;
        }

        public void ControleerTxb(TextBox txb)
        {
            string txbText = GetTxbData(txb);

            if (txb.Name == "txbNaam")
            {
                naamFout = true;

                if (!txbText.All(char.IsLetter) || string.IsNullOrEmpty(txbText)) errorProv.SetError(txb, foutenMsg);

                else if (naamAlAanwezig(txbText) && newProd) errorProv.SetError(txb, naamAanwezigMsg);
                else if (naamAlAanwezig(txbText) && !newProd && txbText != huidigeNaam) errorProv.SetError(txb, naamAanwezigMsg);
                
                else
                {
                    errorProv.SetError(txb, "");
                    naamFout = false;
                }
            }
            else if (txb.Name == "txbCategorie")
            {
                categorieFout = true;
                
                if (!txbText.All(char.IsLetter) || string.IsNullOrEmpty(txbText)) errorProv.SetError(txb, foutenMsg);
                
                else
                {
                    errorProv.SetError(txb, "");
                    categorieFout = false;
                }
            }
            else if (txb.Name == "txbAantalAanwezig")
            {
                aantalAanwezigFout = true;
                
                if (!txbText.All(char.IsNumber) || string.IsNullOrEmpty(txbText) || Int32.Parse(txbText) <= 0 || Int32.Parse(txbText) % 1 != 0) errorProv.SetError(txb, foutenMsg);

                else
                {
                    errorProv.SetError(txb, "");
                    aantalAanwezigFout = false;
                }
            }
            else if (txb.Name == "txbAantalBestAanwezig")
            {
                aantalBestAanwezigFout = true;

                if (!txbText.All(char.IsNumber) || string.IsNullOrEmpty(txbText) || Int32.Parse(txbText) <= 0 || Int32.Parse(txbText) % 1 != 0) errorProv.SetError(txb, foutenMsg);

                else
                {
                    errorProv.SetError(txb, "");
                    aantalBestAanwezigFout = false;
                }
            }
            else if(txb.Name == "txbPrijs")
            {
                prijsFout = true;

                if (!txbText.All(char.IsNumber) || string.IsNullOrEmpty(txbText) || Int32.Parse(txbText) <= 0 || Int32.Parse(txbText) % 1 != 0) errorProv.SetError(txb, foutenMsg);

                else
                {
                    errorProv.SetError(txb, "");
                    prijsFout = false;
                }
            }
            else if (txb.Name == "txbKorting")
            {
                kortingFout = true;

                if (!txbText.All(char.IsNumber) || string.IsNullOrEmpty(txbText) && Int32.Parse(txbText) <= 0 || Int32.Parse(txbText) % 1 != 0 || Int32.Parse(txbText) > 100) errorProv.SetError(txb, foutenMsg);

                else
                {
                    errorProv.SetError(txb, "");
                    kortingFout = false;
                }
            }

        }

        public bool naamAlAanwezig(string naam)
        {
            bool aanwezig = false;

            foreach(string n in namen)
            {
                if(n==naam) aanwezig = true;
            }
            if (aanwezig&&newProd) naamFout = true;

            return aanwezig;
        }

        public bool GeenErrors()
        {
            bool geenErrors = true;

            if (naamFout || categorieFout || aantalAanwezigFout || aantalBestAanwezigFout || prijsFout || kortingFout) geenErrors = false;

            return geenErrors;
        }

        public int aantalAanwezigXml(string teZoeken)
        {
            int aantalAanwezig = 0;

            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(filePath, FileMode.Open);
            xmlDoc.Load(file);

            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("/Producten/Product");

            foreach (XmlNode xmlNode in xmlNodeList)
            {
                string prodNaam = xmlNode["Naam"].InnerText;
                string prodCategorie = xmlNode["Categorie"].InnerText;
                string prodAantal = xmlNode["Aantal"].InnerText;
                string prodBestAantal = xmlNode["Bestaantal"].InnerText;

                if (prodNaam == teZoeken) aantalAanwezig++;
                if (prodCategorie == teZoeken) aantalAanwezig++;
                //prodAantal en prodBestAantal kunnen dezelfde zijn!!
                if (prodAantal == teZoeken) aantalAanwezig++;
                if (prodBestAantal == teZoeken) aantalAanwezig++;
            }

            file.Close();

            return aantalAanwezig;
        }

        //public string[,] zoekenInXml(string soortTeZoeken, string teZoeken)
        //{
        //    string[,] gevonden = new string[1, 4];
        //    int count = 0;

        //    XmlDocument xmlDoc = new XmlDocument();
        //    FileStream file = new FileStream(filePath, FileMode.Open);
        //    xmlDoc.Load(file);

        //    XmlNodeList xmlNodeList = xmlDoc.SelectNodes("/Producten/Product");

        //    if (soortTeZoeken == "Naam")
        //    {
        //        xmlNodeList = xmlDoc.SelectNodes("/Producten/Product/Naam");
        //    }
        //    else if (soortTeZoeken == "Categorie")
        //    {
        //        xmlNodeList = xmlDoc.SelectNodes("/Producten/Product/Categorie");
        //    }
        //    else if (soortTeZoeken == "Aantal")
        //    {
        //        xmlNodeList = xmlDoc.SelectNodes("/Producten/Product/Aantal");
        //    }
        //    else if (soortTeZoeken == "Bestaantal")
        //    {
        //        xmlNodeList = xmlDoc.SelectNodes("/Producten/Product/Bestaantal");
        //    }

        //    foreach (XmlNode xmlNode in xmlNodeList)
        //    {
        //        string prodNaam = xmlNode.ParentNode["Naam"].InnerText;
        //        string prodCategorie = xmlNode.ParentNode["Categorie"].InnerText;
        //        string prodAantal = xmlNode.ParentNode["Aantal"].InnerText;
        //        string prodBestAantal = xmlNode.ParentNode["Bestaantal"].InnerText;

        //        if (prodNaam == teZoeken && soortTeZoeken == "Naam")
        //        {
        //            gevonden[count, 0] = prodNaam;
        //            gevonden[count, 1] = prodCategorie;
        //            gevonden[count, 2] = prodAantal;
        //            gevonden[count, 3] = prodBestAantal;

        //        }
        //        else if (prodCategorie == teZoeken && soortTeZoeken == "Categorie")
        //        {
        //            gevonden[count, 0] = prodNaam;
        //            gevonden[count, 1] = prodCategorie;
        //            gevonden[count, 2] = prodAantal;
        //            gevonden[count, 3] = prodBestAantal;
        //        }
        //        else if (prodAantal == teZoeken && soortTeZoeken == "Aantal")
        //        {
        //            gevonden[count, 0] = prodNaam;
        //            gevonden[count, 1] = prodCategorie;
        //            gevonden[count, 2] = prodAantal;
        //            gevonden[count, 3] = prodBestAantal;
        //        }
        //        else if (prodBestAantal == teZoeken && soortTeZoeken == "Bestaantal")
        //        {
        //            gevonden[count, 0] = prodNaam;
        //            gevonden[count, 1] = prodCategorie;
        //            gevonden[count, 2] = prodAantal;
        //            gevonden[count, 3] = prodBestAantal;
        //        }

        //        count++;
        //        //Lengte van array "gevonden" aanpassen.
        //        string[,] temparr = new string[count + 1, 4];
        //        Array.Copy(gevonden, temparr, gevonden.Length);
        //        gevonden = temparr;
        //    }

        //    file.Close();

        //    return gevonden;
        //}

        public string[] wijzigCategorieën()
        {
            string[] categorieën = new string[0];

            int count = 0;

            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(filePath, FileMode.Open);
            xmlDoc.Load(file);

            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("/Producten/Product/Categorie");

            foreach(XmlNode node in xmlNodeList)
            {
                bool aanwezig = false;
                foreach(string n in categorieën)
                {
                    if (node.InnerText == n) aanwezig = true;
                }
                if (!aanwezig)
                {
                    Array.Resize(ref categorieën, categorieën.Length + 1);
                    categorieën[count] = node.InnerText;
                    count++;
                }

            }
            file.Close();

            return categorieën;
        }

        public void werkCategorieënBij()
        {
            Array.Resize(ref categorieën, wijzigCategorieën().Length);
            categorieën = wijzigCategorieën();
        }

        private void ChangeProd_DoubleClick(object sender, EventArgs e)
        {
            if (test)
            {
                string n = "";
                foreach (string x in categorieën)
                {
                    n += x + "\n";
                }
                MessageBox.Show(n);
            }
            
        }

        private void txbCategorie_TextChanged(object sender, EventArgs e)
        {
            autocompleteTxbCat();
        }

        public void autocompleteTxbCat()
       {
            AutoCompleteStringCollection autoSrc = new AutoCompleteStringCollection();
            autoSrc.AddRange(categorieën);

            txbCategorie.AutoCompleteMode = AutoCompleteMode.Suggest;
            txbCategorie.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txbCategorie.AutoCompleteCustomSource = autoSrc;

        }

        public string[] wijzigNamen()
        {
            //Deze array geeft als resultaat alle product namen uit de XML-database.


            string[] namen = new string[0];

            int count = 0;

            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(filePath, FileMode.Open);
            xmlDoc.Load(file);

            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("/Producten/Product/Naam");

            foreach (XmlNode node in xmlNodeList)
            {
                bool aanwezig = false;
                foreach (string n in namen)
                {
                    if (node.InnerText == n) aanwezig = true;
                }
                if (!aanwezig)
                {
                    Array.Resize(ref namen, namen.Length + 1);
                    namen[count] = node.InnerText;
                    count++;
                }
            }
            file.Close();

            return namen;
        }

        public void werkNamenBij()
        {
            //Dit werkt de array met namen bij zodat deze up-to-date is.

            //Lengte van de array +1.
            Array.Resize(ref namen, wijzigNamen().Length);
            //Werk namen bij met juiste array.
            namen = wijzigNamen();
        }

        public double kortingPrijs(double prijs, double korting)
        {

            //Korting op duidelijk manier schrijven:
            //Bv.: Korting 20% --> korting = 0,8
            double rekenbareKorting = 1 - (korting / 100);


            //Prijs berekenen
            double berekendePrijs = prijs * rekenbareKorting;


            //Prijs terugsturen in normaal scenario
            if (berekendePrijs >= 0)
            {
                return berekendePrijs;
            }
            
            //Foutafhandeling:
            //Als fout && testfase --> Zeg dat er een fout is en stuur '0' terug.
            else if (test)
            {
                MessageBox.Show("berekenPrijs: fout!!!!");
                return 0;
            }
            //Als fout --> Stuur '0' terug, zodat duidelijk is dat er een fout is!
            else
            {
                return 0;
            }
        }
    } 
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.IO;

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
            loadProducten();
        }
        
        bool newProd = false;
        bool naamFout = false;
        bool categorieFout = false;
        bool aantalAanwezigFout = false;
        bool aantalBestAanwezigFout = false;


        string welkomMsg = "Welkom!\nIn dit venster kunt u producten aanmaken en wijzigen. Momenteel zijn er nog geen producten opgeslagen.\nBegin dus met een product aan te maken.";
        string zekerMsg = "Ben u zeker?";
        string abortMsg = "Geen probleem! Er is niets veranderd.";
        string selectItemMsg = "Gelieve één item te selecteren.";
        string foutenMsg = "Er zit een fout in de ingevoerde gegevens.";
        string filePath = @"D:\ZIAS\School\2021-2022\GIP\Projects\GIP\GIP-WinkelProductenSysteem\GIP-WinkelProductenSysteem\Producten.xml";
        

        private void loadProducten()
        {
            if (!File.Exists(filePath))
            {
                makeXmlProducten();
                MessageBox.Show(welkomMsg, "Welkom!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (File.ReadAllText(filePath) == "")
            {
                makeXmlProducten();
                MessageBox.Show(welkomMsg, "Welkom!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else fillListView();
        }

        private void fillListView()
        {
            lvProducten.Items.Clear();

            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(filePath, FileMode.Open);
            xmlDoc.Load(file);


            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("/Producten/Product");

            foreach(XmlNode xmlNode in xmlNodeList)
            {
                string prodNaam = xmlNode["Naam"].InnerText;
                string prodCategorie = xmlNode["Categorie"].InnerText;
                string prodAantal = xmlNode["Aantal"].InnerText;
                string prodBestAantal = xmlNode["Bestaantal"].InnerText;

                ListViewItem lvi = new ListViewItem(prodNaam);

                lvi.SubItems.Add(prodCategorie);
                lvi.SubItems.Add(prodAantal);
                lvi.SubItems.Add(prodBestAantal);

                lvProducten.Items.Add(lvi);
            }

            file.Close();
        }

        private void makeXmlProducten()
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


        private void maakNieuwProduct()
        {
            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(filePath, FileMode.Open);
            xmlDoc.Load(file);

            string prodNaam = getTxbData(txbNaam);
            string prodCategorie = getTxbData(txbCategorie);
            string prodAantal = getTxbData(txbAantalAanwezig);
            string prodBestAantal = getTxbData(txbAantalBestAanwezig);

            XmlElement product = xmlDoc.CreateElement("Product");

            XmlElement naam = xmlDoc.CreateElement("Naam");
            XmlText naamText = xmlDoc.CreateTextNode(prodNaam);

            XmlElement categorie = xmlDoc.CreateElement("Categorie");
            XmlText categorieText = xmlDoc.CreateTextNode(prodCategorie);

            XmlElement aantal = xmlDoc.CreateElement("Aantal");
            XmlText aantalText = xmlDoc.CreateTextNode(prodAantal);

            XmlElement aantalBest = xmlDoc.CreateElement("Bestaantal");
            XmlText aantalBestText = xmlDoc.CreateTextNode(prodBestAantal);

            naam.AppendChild(naamText);
            categorie.AppendChild(categorieText);
            aantal.AppendChild(aantalText);
            aantalBest.AppendChild(aantalBestText);

            product.AppendChild(naam);
            product.AppendChild(categorie);
            product.AppendChild(aantal);
            product.AppendChild(aantalBest);

            xmlDoc.DocumentElement.AppendChild(product);
            file.Close();
            xmlDoc.Save(filePath);
        }
        private void wijzigProduct()
        {
            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(filePath, FileMode.Open);
            xmlDoc.Load(file);

            string prodNaam = getTxbData(txbNaam);
            string prodCategorie = getTxbData(txbCategorie);
            string prodAantal = getTxbData(txbAantalAanwezig);
            string prodBestAantal = getTxbData(txbAantalBestAanwezig);

            foreach(XmlNode xmlNode in xmlDoc.SelectNodes("/Producten/Product"))
            {
                if (xmlNode.SelectSingleNode("Naam").InnerText == prodNaam)
                {
                    xmlNode["Categorie"].InnerText = prodCategorie;
                    xmlNode["Aantal"].InnerText = prodAantal;
                    xmlNode["Bestaantal"].InnerText = prodBestAantal;
                }
            }

            //Afsluiten van bestand
            file.Close();
            //Rechtstreeks schrijven naar bestandsLocatie!! Anders wordt alles opnieuw toegevoegd!
            xmlDoc.Save(filePath);
        }
        
        private void delProduct()
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
                    fillListView();
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
        
        
        private void fillTxb()
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

                txbNaam.Text = prodNaam;
                txbNaam.Enabled = false;
                txbCategorie.Text = prodCategorie;
                txbAantalAanwezig.Text = prodAantal;
                txbAantalBestAanwezig.Text = prodBestAantal;

                file.Close();

                pnlProductEigenschappen.Visible = true;

            }
            else
            {
                MessageBox.Show(selectItemMsg);
                pnlProductEigenschappen.Visible = false;
            }
        }
        private void clearTxb()
        {
            txbNaam.Text = "";
            txbNaam.Enabled = true;
            txbCategorie.Text = "";
            txbAantalAanwezig.Text = "";
            txbAantalBestAanwezig.Text = "";
        }
        private string getTxbData(TextBox txb)
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

        private void controleerTxb(TextBox txb)
        {
            string txbText = getTxbData(txb);

            if(txb.Name == "txbNaam")
            {
                naamFout = true;
                if (txbText.All(char.IsLetter) && !string.IsNullOrEmpty(txbText)) naamFout = false;
                
                if (naamFout) errorProv.SetError(txb, foutenMsg);
                else errorProv.SetError(txb, "");
            }
            else if (txb.Name == "txbCategorie")
            {
                categorieFout = true;
                if (txbText.All(char.IsLetter) && !string.IsNullOrEmpty(txbText)) categorieFout = false;

                if (categorieFout) errorProv.SetError(txb, foutenMsg);
                else errorProv.SetError(txb, "");
            }
            else if (txb.Name == "txbAantalAanwezig")
            {
                aantalAanwezigFout = true;
                if (txbText.All(char.IsNumber) && !string.IsNullOrEmpty(txbText) && Int32.Parse(txbText) > 0 && Int32.Parse(txbText) % 1 == 0) aantalAanwezigFout = false;

                if (aantalAanwezigFout) errorProv.SetError(txb, foutenMsg);
                else errorProv.SetError(txb, "");
            }
            else if (txb.Name == "txbAantalBestAanwezig")
            {
                aantalBestAanwezigFout = true;
                if (txbText.All(char.IsNumber) && !string.IsNullOrEmpty(txbText) && Int32.Parse(txbText) > 0 && Int32.Parse(txbText) % 1 == 0) aantalBestAanwezigFout = false;

                if (aantalBestAanwezigFout) errorProv.SetError(txb, foutenMsg);
                else errorProv.SetError(txb, "");
            }

        }

        private int aantalAanwezigXml(string teZoeken)
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

        private string[,] zoekenInXml(string soortTeZoeken, string teZoeken)
        {
            string[,] gevonden = new string[1,4];
            int count = 0;

            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(filePath, FileMode.Open);
            xmlDoc.Load(file);

            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("/Producten/Product");

            if (soortTeZoeken == "Naam")
            {
                xmlNodeList = xmlDoc.SelectNodes("/Producten/Product/Naam");
            }
            else if (soortTeZoeken == "Categorie")
            {
                xmlNodeList = xmlDoc.SelectNodes("/Producten/Product/Categorie");
            }
            else if (soortTeZoeken == "Aantal")
            {
                xmlNodeList = xmlDoc.SelectNodes("/Producten/Product/Aantal");
            }
            else if (soortTeZoeken == "Bestaantal")
            {
                xmlNodeList = xmlDoc.SelectNodes("/Producten/Product/Bestaantal");
            }

            foreach (XmlNode xmlNode in xmlNodeList)
            {
                string prodNaam = xmlNode.ParentNode["Naam"].InnerText;
                string prodCategorie = xmlNode.ParentNode["Categorie"].InnerText;
                string prodAantal = xmlNode.ParentNode["Aantal"].InnerText;
                string prodBestAantal = xmlNode.ParentNode["Bestaantal"].InnerText;

                if (prodNaam == teZoeken && soortTeZoeken == "Naam")
                {
                    gevonden[count, 0] = prodNaam;
                    gevonden[count, 1] = prodCategorie;
                    gevonden[count, 2] = prodAantal;
                    gevonden[count, 3] = prodBestAantal;

                }
                else if (prodCategorie == teZoeken && soortTeZoeken == "Categorie")
                {
                    gevonden[count, 0] = prodNaam;
                    gevonden[count, 1] = prodCategorie;
                    gevonden[count, 2] = prodAantal;
                    gevonden[count, 3] = prodBestAantal;
                }
                else if (prodAantal == teZoeken && soortTeZoeken == "Aantal")
                {
                    gevonden[count, 0] = prodNaam;
                    gevonden[count, 1] = prodCategorie;
                    gevonden[count, 2] = prodAantal;
                    gevonden[count, 3] = prodBestAantal;
                }
                else if (prodBestAantal == teZoeken && soortTeZoeken == "Bestaantal")
                {
                    gevonden[count, 0] = prodNaam;
                    gevonden[count, 1] = prodCategorie;
                    gevonden[count, 2] = prodAantal;
                    gevonden[count, 3] = prodBestAantal;
                }

                count++;
                //Lengte van array "gevonden" aanpassen.
                string[,] temparr = new string[count + 1, 4];
                Array.Copy(gevonden, temparr, gevonden.Length);
                gevonden = temparr;
            }

            file.Close();

            return gevonden;
        }

        private bool geenErrors()
        {
            bool geenErrors = true;

            if (naamFout || categorieFout || aantalAanwezigFout || aantalBestAanwezigFout) geenErrors = false;

            return geenErrors;
        }

        private void btnMaakProduct_Click(object sender, EventArgs e)
        {
            newProd = true;
            clearTxb();
            pnlProductEigenschappen.Visible = true;
        }

        private void btnBevestigProducten_Click(object sender, EventArgs e)
        {
            controleerTxb(txbNaam);
            controleerTxb(txbCategorie);
            controleerTxb(txbAantalAanwezig);
            controleerTxb(txbAantalBestAanwezig);

            if (geenErrors())
            {
                pnlProductEigenschappen.Visible = false;

                if (newProd) maakNieuwProduct();
                else if (!newProd) wijzigProduct();

                fillListView();
                newProd = false;
            }
            else
            {
                MessageBox.Show(aantalAanwezigXml(getTxbData(txbCategorie)).ToString());

                MessageBox.Show(foutenMsg);
            }
            
        }

        private void tempactions()
        {
            string[,] temp = zoekenInXml("Categorie", "Fruit");
            string weergeven = "";

            for(int i = 0; i < temp.GetLength(0); i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    weergeven += temp[i, j];
                    weergeven += "\n";
                }
            }
            MessageBox.Show(weergeven);
        }
        private void btnChangeProd_Click(object sender, EventArgs e)
        {
            fillTxb();
            newProd = false;
        }

        private void btnDelProduct_Click(object sender, EventArgs e)
        {
            tempactions();
            delProduct();
        }
    }
}

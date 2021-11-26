using System.Linq;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace GIP_WinkelProductenSysteem
{
    class WriteInXml
    {
        public WriteInXml()
        {
        }
        ChangeProd changeProd;

        string FilePath;

        string zekerMsg = "Ben u zeker?";
        string abortMsg = "Geen probleem! Er is niets veranderd.";
        string selectItemMsg = "Gelieve één item te selecteren.";
        string foutenMsg = "Er zit een fout in de ingevoerde gegevens.";

        public WriteInXml(string filepath)
        {
            FilePath = filepath;
        }
        public void MakeXmlProducten()
        {
            XmlWriter xml;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;

            xml = XmlWriter.Create(FilePath, settings);

            xml.WriteStartDocument();
            xml.WriteStartElement("Producten");
            xml.WriteEndElement();
            xml.Close();
        }

        public void MaakNieuwProduct()
        {
            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(FilePath, FileMode.Open);
            xmlDoc.Load(file);

            string prodNaam = changeProd.GetTxbData(changeProd.txbNaam);
            string prodCategorie = changeProd.GetTxbData(changeProd.txbCategorie);
            string prodAantal = changeProd.GetTxbData(changeProd.txbAantalAanwezig);
            string prodBestAantal = changeProd.GetTxbData(changeProd.txbAantalBestAanwezig);

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
            xmlDoc.Save(FilePath);
        }

        public void WijzigProduct()
        {
            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(FilePath, FileMode.Open);
            xmlDoc.Load(file);

            string prodNaam = changeProd.GetTxbData(changeProd.txbNaam);
            string prodCategorie = changeProd.GetTxbData(changeProd.txbCategorie);
            string prodAantal = changeProd.GetTxbData(changeProd.txbAantalAanwezig);
            string prodBestAantal = changeProd.GetTxbData(changeProd.txbAantalBestAanwezig);

            foreach (XmlNode xmlNode in xmlDoc.SelectNodes("/Producten/Product"))
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
            xmlDoc.Save(FilePath);
        }

        public void DelProduct()
        {
            if (changeProd.lvProducten.SelectedItems.Count == 1)
            {
                DialogResult result = MessageBox.Show(zekerMsg, "Verwijderen?", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    int rowIndex = changeProd.lvProducten.FocusedItem.Index + 1;

                    XmlDocument xmlDoc = new XmlDocument();
                    FileStream file = new FileStream(FilePath, FileMode.Open);
                    xmlDoc.Load(file);

                    XmlNode xmlNode = xmlDoc.SelectSingleNode($"/Producten/Product[{rowIndex}]");
                    xmlNode.ParentNode.RemoveChild(xmlNode);

                    file.Close();
                    xmlDoc.Save(FilePath);
                    changeProd.FillListView();
                }
                else
                {
                    MessageBox.Show(abortMsg);
                    changeProd.pnlProductEigenschappen.Visible = false;
                }
            }
            else
            {
                MessageBox.Show(selectItemMsg);
                changeProd.pnlProductEigenschappen.Visible = false;
            }
        }
    }
}

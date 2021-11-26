using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace GIP_WinkelProductenSysteem
{
    class WriteInXml
    {
        private string FilePath;
        public WriteInXml()
        {
        }
        public WriteInXml(string filepath)
        {
            FilePath = filepath;
        }

        ChangeProd changeProd;
        EditChangeProdForm editChange;

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

            string prodNaam = editChange.GetTxbData(changeProd.txbNaam);
            string prodCategorie = editChange.GetTxbData(changeProd.txbCategorie);
            string prodAantal = editChange.GetTxbData(changeProd.txbAantalAanwezig);
            string prodBestAantal = editChange.GetTxbData(changeProd.txbAantalBestAanwezig);

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

            string prodNaam = EditChangeProdForm.GetTxbData(changeProd.txbNaam);
            string prodCategorie = EditChangeProdForm.GetTxbData(changeProd.txbCategorie);
            string prodAantal = EditChangeProdForm.GetTxbData(changeProd.txbAantalAanwezig);
            string prodBestAantal = EditChangeProdForm.GetTxbData(changeProd.txbAantalBestAanwezig);

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
                DialogResult result = MessageBox.Show(changeProd.zekerMsg, "Verwijderen?", MessageBoxButtons.YesNo);

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
                    editChange.FillListView();
                }
                else
                {
                    MessageBox.Show(changeProd.abortMsg);
                    changeProd.pnlProductEigenschappen.Visible = false;
                }
            }
            else
            {
                MessageBox.Show(changeProd.selectItemMsg);
                changeProd.pnlProductEigenschappen.Visible = false;
            }
        }
    }
}

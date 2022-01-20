using System;
using System.Linq;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace GIP_WinkelProductenSysteem
{
    class EditChangeProdForm
    {
        private string FilePath;
        public EditChangeProdForm()
        {
        }
        public EditChangeProdForm(string filepath)
        {
            FilePath = filepath;
        }

        ChangeProd changeProd;

        public void FillListView()
        {
            changeProd.lvProducten.Items.Clear();

            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(FilePath, FileMode.Open);
            xmlDoc.Load(file);


            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("/Producten/Product");

            foreach (XmlNode xmlNode in xmlNodeList)
            {
                string prodNaam = xmlNode["Naam"].InnerText;
                string prodCategorie = xmlNode["Categorie"].InnerText;
                string prodAantal = xmlNode["Aantal"].InnerText;
                string prodBestAantal = xmlNode["Bestaantal"].InnerText;

                ListViewItem lvi = new ListViewItem(prodNaam);

                lvi.SubItems.Add(prodCategorie);
                lvi.SubItems.Add(prodAantal);
                lvi.SubItems.Add(prodBestAantal);

                changeProd.lvProducten.Items.Add(lvi);
            }

            file.Close();
        }

        public void FillTxb()
        {
            if (changeProd.lvProducten.SelectedItems.Count == 1)
            {
                int rowIndex = changeProd.lvProducten.FocusedItem.Index + 1;

                XmlDocument xmlDoc = new XmlDocument();
                FileStream file = new FileStream(FilePath, FileMode.Open);
                xmlDoc.Load(file);

                XmlNode xmlNode = xmlDoc.SelectSingleNode($"/Producten/Product[{rowIndex}]");

                string prodNaam = xmlNode["Naam"].InnerText;
                string prodCategorie = xmlNode["Categorie"].InnerText;
                string prodAantal = xmlNode["Aantal"].InnerText;
                string prodBestAantal = xmlNode["Bestaantal"].InnerText;

                changeProd.txbNaam.Text = prodNaam;
                changeProd.txbNaam.Enabled = false;
                changeProd.txbCategorie.Text = prodCategorie;
                changeProd.txbAantalAanwezig.Text = prodAantal;
                changeProd.txbAantalBestAanwezig.Text = prodBestAantal;

                file.Close();

                changeProd.pnlProductEigenschappen.Visible = true;

            }
            else
            {
                MessageBox.Show(changeProd.selectItemMsg);
                changeProd.pnlProductEigenschappen.Visible = false;
            }
        }
        public void ClearTxb()
        {
            changeProd.txbNaam.Text = "";
            changeProd.txbNaam.Enabled = true;
            changeProd.txbCategorie.Text = "";
            changeProd.txbAantalAanwezig.Text = "";
            changeProd.txbAantalBestAanwezig.Text = "";
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
                changeProd.errorProv.SetError(txb, ex.Message);
            }
            return inhoud;
        }

        public void ControleerTxb(TextBox txb)
        {
            string txbText = GetTxbData(txb);

            if (txb.Name == "txbNaam")
            {
                changeProd.naamFout = true;
                if (txbText.All(char.IsLetter) && !string.IsNullOrEmpty(txbText)) changeProd.naamFout = false;

                if (changeProd.naamFout) changeProd.errorProv.SetError(txb, changeProd.foutenMsg);
                else changeProd.errorProv.SetError(txb, "");
            }
            else if (txb.Name == "txbCategorie")
            {
                changeProd.categorieFout = true;
                if (txbText.All(char.IsLetter) && !string.IsNullOrEmpty(txbText)) changeProd.categorieFout = false;

                if (changeProd.categorieFout) changeProd.errorProv.SetError(txb, changeProd.foutenMsg);
                else changeProd.errorProv.SetError(txb, "");
            }
            else if (txb.Name == "txbAantalAanwezig")
            {
                changeProd.aantalAanwezigFout = true;
                if (txbText.All(char.IsNumber) && !string.IsNullOrEmpty(txbText) && Int32.Parse(txbText) > 0 && Int32.Parse(txbText) % 1 == 0) changeProd.aantalAanwezigFout = false;

                if (changeProd.aantalAanwezigFout) changeProd.errorProv.SetError(txb, changeProd.foutenMsg);
                else changeProd.errorProv.SetError(txb, "");
            }
            else if (txb.Name == "txbAantalBestAanwezig")
            {
                changeProd.aantalBestAanwezigFout = true;
                if (txbText.All(char.IsNumber) && !string.IsNullOrEmpty(txbText) && Int32.Parse(txbText) > 0 && Int32.Parse(txbText) % 1 == 0) changeProd.aantalBestAanwezigFout = false;

                if (changeProd.aantalBestAanwezigFout) changeProd.errorProv.SetError(txb, changeProd.foutenMsg);
                else changeProd.errorProv.SetError(txb, "");
            }

        }

        public bool GeenErrors()
        {
            bool geenErrors = true;

            if (changeProd.naamFout || changeProd.categorieFout || changeProd.aantalAanwezigFout || changeProd.aantalBestAanwezigFout) geenErrors = false;

            return geenErrors;
        }

    }
}

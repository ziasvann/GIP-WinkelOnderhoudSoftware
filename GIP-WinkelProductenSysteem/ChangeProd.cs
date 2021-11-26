using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
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
            LoadProducten();
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


        private void LoadProducten()
        {
            WriteInXml writeInXml = new WriteInXml(filePath);
            if (!File.Exists(filePath))
            {
                writeInXml.MakeXmlProducten();
                MessageBox.Show(welkomMsg, "Welkom!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (File.ReadAllText(filePath) == "")
            {
                writeInXml.MakeXmlProducten();
                MessageBox.Show(welkomMsg, "Welkom!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else FillListView();
        }

        public void FillListView()
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
        
        private void FillTxb()
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
        private void ClearTxb()
        {
            txbNaam.Text = "";
            txbNaam.Enabled = true;
            txbCategorie.Text = "";
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

        private void ControleerTxb(TextBox txb)
        {
            string txbText = GetTxbData(txb);

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

        private bool GeenErrors()
        {
            bool geenErrors = true;

            if (naamFout || categorieFout || aantalAanwezigFout || aantalBestAanwezigFout) geenErrors = false;

            return geenErrors;
        }

        private void btnMaakProduct_Click(object sender, EventArgs e)
        {
            newProd = true;
            ClearTxb();
            pnlProductEigenschappen.Visible = true;
        }

        private void btnBevestigProducten_Click(object sender, EventArgs e)
        {
            WriteInXml writeInXml = new WriteInXml(filePath);

            ControleerTxb(txbNaam);
            ControleerTxb(txbCategorie);
            ControleerTxb(txbAantalAanwezig);
            ControleerTxb(txbAantalBestAanwezig);

            if (GeenErrors())
            {
                pnlProductEigenschappen.Visible = false;

                if (newProd) writeInXml.MaakNieuwProduct();
                else if (!newProd) writeInXml.WijzigProduct();

                FillListView();
                newProd = false;
            }
            else
            {
                GetFromXml getFromXml = new GetFromXml(filePath);
                MessageBox.Show(getFromXml.aantalAanwezigXml(GetTxbData(txbCategorie)).ToString());

                MessageBox.Show(foutenMsg);
            }
            
        }

        private void btnChangeProd_Click(object sender, EventArgs e)
        {
            FillTxb();
            newProd = false;
        }

        private void btnDelProduct_Click(object sender, EventArgs e)
        {
            WriteInXml writeInXml = new WriteInXml(filePath);
            writeInXml.DelProduct();
            // Tempactions();
        }

        /*private void Tempactions()
{
    GetFromXml getFromXml = new GetFromXml(filePath);
    string[,] temp = getFromXml.zoekenInXml("Categorie", "Fruit");
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
*/

    }
}

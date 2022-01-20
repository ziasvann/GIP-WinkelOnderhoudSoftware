using System;
using System.Windows.Forms;
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
        
        public bool newProd = false;
        public bool naamFout = false;
        public bool categorieFout = false;
        public bool aantalAanwezigFout = false;
        public bool aantalBestAanwezigFout = false;


        public string welkomMsg = "Welkom!\nIn dit venster kunt u producten aanmaken en wijzigen. Momenteel zijn er nog geen producten opgeslagen.\nBegin dus met een product aan te maken.";
        public string zekerMsg = "Ben u zeker?";
        public string abortMsg = "Geen probleem! Er is niets veranderd.";
        public string selectItemMsg = "Gelieve één item te selecteren.";
        public string foutenMsg = "Er zit een fout in de ingevoerde gegevens.";
        
        string filePath = @"D:\ZIAS\School\2021-2022\GIP\Projects\GIP\GIP-WinkelProductenSysteem\GIP-WinkelProductenSysteem\Producten.xml";


        private void LoadProducten()
        {
            EditChangeProdForm editChange = new EditChangeProdForm(filePath);

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
            else editChange.FillListView();
        }

        private void btnMaakProduct_Click(object sender, EventArgs e)
        {
            EditChangeProdForm editChange = new EditChangeProdForm(filePath);

            newProd = true;
            editChange.ClearTxb();
            pnlProductEigenschappen.Visible = true;
        }

        private void btnBevestigProducten_Click(object sender, EventArgs e)
        {
            EditChangeProdForm editChange = new EditChangeProdForm(filePath);

            WriteInXml writeInXml = new WriteInXml(filePath);

            editChange.ControleerTxb(txbNaam);
            editChange.ControleerTxb(txbCategorie);
            editChange.ControleerTxb(txbAantalAanwezig);
            editChange.ControleerTxb(txbAantalBestAanwezig);

            if (editChange.GeenErrors())
            {
                pnlProductEigenschappen.Visible = false;

                if (newProd) writeInXml.MaakNieuwProduct();
                else if (!newProd) writeInXml.WijzigProduct();

                editChange.FillListView();
                newProd = false;
            }
            else
            {
                GetFromXml getFromXml = new GetFromXml(filePath);
                MessageBox.Show(getFromXml.aantalAanwezigXml(editChange.GetTxbData(txbCategorie)).ToString());

                MessageBox.Show(foutenMsg);
            }
            
        }

        private void btnChangeProd_Click(object sender, EventArgs e)
        {
            EditChangeProdForm editChange = new EditChangeProdForm(filePath);
            editChange.FillTxb();
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

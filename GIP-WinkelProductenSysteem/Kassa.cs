using System;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Xml;

namespace GIP_WinkelProductenSysteem
{
    public partial class Kassa : Form
    {
        public Kassa()
        {
            InitializeComponent();
        }

        private void Kassa_Load(object sender, EventArgs e)
        {
            if (test)
            {
                string str = "";
                foreach(string product in productenArray())
                {
                    str += product+"\n";
                }
                MessageBox.Show(str);
            }
        }

        bool test = true;
        string filePath = Application.LocalUserAppDataPath + @"\Producten.xml";

        public string[] productenArray()
        {
            string[] producten = new string[0];

            //Het XML document wordt geopend.
            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(filePath, FileMode.Open);
            xmlDoc.Load(file);

            //De lijst XML node's met daarin de producten wordt geselecteerd.
            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("/Producten/Product");

            //De lijst XML node's met daarin de producten wordt geselecteerd.
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                //Vraag de eigenschappen van het huidige product op.
                string prodNaam = xmlNode["Naam"].InnerText;
                string prodPrijs = xmlNode["NieuwePrijs"].InnerText;
                string prodCategorie = xmlNode["Categorie"].InnerText;

                //Maak de lengte van de array + 1, voor het nieuwe productarray.
                Array.Resize(ref producten, producten.Length + 1);
                //Voeg het product toe aan de array.
                producten[producten.Length-1] = $"{prodNaam},{prodPrijs},{prodCategorie}";
            }

            //Sluit het bestand af en geef de array terug.
            file.Close();
            return producten;
        }
    }
}

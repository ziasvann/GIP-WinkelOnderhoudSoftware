using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

// ReSharper disable PossibleNullReferenceException

namespace GIP_WinkelProductenSysteem
{
    public partial class Kassa : Form
    {
        public Kassa()
        {
            InitializeComponent();
        }


        string welkomMsg = "Welkom!\nIn dit venster kunt u producten aanmaken en wijzigen. Momenteel zijn er nog geen producten opgeslagen.\nBegin dus met een product aan te maken.";
        string zekerMsg = "Ben u zeker?";
        string abortMsg = "Geen probleem! Er is niets veranderd.";
        string selectItemMsg = "Gelieve één item te selecteren.";
        string foutenMsg = "Er zit een fout in de ingevoerde gegevens.";
        string naamAanwezigMsg = "Deze naam is al gebruikt voor een ander product. Kies een andere naam.";


        bool naamFout = false;
        bool categorieFout = false;
        bool prijsFout = false;
        bool kortingFout = false;

        string totaalPrijs = "";



        private void Kassa_Load(object sender, EventArgs e)
        {
            if (test)
            {
                string str = "";
                foreach (string product in productenArray())
                {
                    str += product + "\n";
                }
                MessageBox.Show(str);
            }
        }

        bool test = true;
        string filePath = Application.LocalUserAppDataPath + @"\Producten.xml";
        string[,] productenKopen = new string[0, 0];

        string[] productenArray()
        {
            string[] producten = new string[0];

            //Het XML document wordt geopend.
            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(filePath, FileMode.Open);
            xmlDoc.Load(file);

            //De lijst XML node's met daarin de producten wordt geselecteerd.
            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("/Producten/Product");

            //De lijst XML node's met daarin de producten wordt één voor één doorlopen.
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                //Vraag de eigenschappen van het huidige product op.
                string prodNaam = xmlNode["Naam"].InnerText;
                string prodPrijs = xmlNode["NieuwePrijs"].InnerText;
                string prodCategorie = xmlNode["Categorie"].InnerText;

                //Maak de lengte van de array + 1, voor het nieuwe productarray.
                Array.Resize(ref producten, producten.Length + 1);

                prodPrijs = verander(prodPrijs, ',', ".");

                //Voeg het product toe aan de array.
                producten[producten.Length - 1] = $"{prodNaam},{prodPrijs},{prodCategorie}";
            }

            //Sluit het bestand af en geef de array terug.
            file.Close();
            return producten;
        }
        
        string verander(string getal, char oorspronkelijk, string nieuw)
        {
            string output = "";

            foreach (char n in getal)
            {
                if (n == oorspronkelijk)
                {
                    output += nieuw;
                }
                else output += n;
            }

            return output;
        }



        void voegProductToe(int productenArrayIndex)
        {
            string[] product = productenArray()[productenArrayIndex].Split(',');
            string productNaam = product[0];
            string productPrijs = product[1];
            string productCategorie = product[2];

            if (totaalPrijs != "")
            {
                totaalPrijs = Decimal.Add(Convert.ToDecimal(verander(productPrijs,'.',",")), Convert.ToDecimal(verander(totaalPrijs,'.',","))).ToString();
            }
            else
            {
                totaalPrijs = productPrijs;
            }


            ListViewItem lvItem = new ListViewItem(productNaam);
            lvItem.SubItems.Add(productPrijs);

            lvProducten.Items.Add(lvItem);

            lblTotPrijs.Text = verander(totaalPrijs,',',".");
        }

        
        private void btnVoegToe_Click(object sender, EventArgs e)
        {

            string productNaam = GetTxbData(txbHuidigProdNaam);

            voegProductToe(ZoekIndexInArray(productNaam));

        }

        int ZoekIndexInArray(string productNaam)
        {
            int index = -1;

            for (int i = 0; i < productenArray().Length; i++)
            {
                string[] product = productenArray()[i].Split(',');

                if (product[0] == productNaam)
                {
                    index = i;
                    return index;
                }
                else continue;

            }
                
            return index;
        }

        public void ControleerTxb(TextBox txb)
        {
            //Deze method dient om te controleren of de ingegeven tekst van de textbox een reële waarde is.

            //Eerst wordt de tekst van de textbox opgevraagd.
            string txbText = txb.Text;

            //Als de textbox txbNaam is wordt dit uitgevoerd.
            if (txb.Name == "txbHuidigProdNaam")
            {
                //Er wordt altijd vanuit gegaan dat er een fout is. Als alles juist is wordt er pas vanuit gegaan dat het juist is.
                naamFout = true;

                //Als de textbox niet enkel letters bevat of leeg is wordt er een fout aangegeven.
                if (!txbText.All(char.IsLetter) || string.IsNullOrEmpty(txbText)) errorProv.SetError(txb, foutenMsg);
                else
                {
                    //Als men er zeker van is dat er geen enkele fout is wordt de errorprovider leeg gemaakt en is er geen fout.
                    errorProv.SetError(txb, "");
                    naamFout = false;
                }
            }

            ////Als de textbox txbCategorie is wordt dit uitgevoerd.
            //else if (txb.Name == "txbCategorie")
            //{
            //    //Er wordt altijd vanuit gegaan dat er een fout is. Als alles juist is wordt er pas vanuit gegaan dat het juist is.
            //    categorieFout = true;

            //    //Als de textbox niet enkel letters bevat of leeg is wordt er een fout aangegeven.
            //    if (!txbText.All(char.IsLetter) || string.IsNullOrEmpty(txbText)) errorProv.SetError(txb, foutenMsg);

            //    else
            //    {
            //        //Als men er zeker van is dat er geen enkele fout is wordt de errorprovider leeg gemaakt en is er geen fout.
            //        errorProv.SetError(txb, "");
            //        categorieFout = false;
            //    }
            //}


            ////Als de textbox txbAantalAanwezig is wordt dit uitgevoerd.
            //else if (txb.Name == "txbAantalAanwezig")
            //{
            //    //Er wordt altijd vanuit gegaan dat er een fout is. Als alles juist is wordt er pas vanuit gegaan dat het juist is.
            //    aantalAanwezigFout = true;

            //    //Als de textbox niet enkel letters bevat of leeg is wordt er een fout aangegeven.
            //    if (!txbText.All(char.IsNumber) || string.IsNullOrEmpty(txbText) || Int32.Parse(txbText) <= 0 || Int32.Parse(txbText) % 1 != 0) errorProv.SetError(txb, foutenMsg);

            //    else
            //    {
            //        //Als men er zeker van is dat er geen enkele fout is wordt de errorprovider leeg gemaakt en is er geen fout.
            //        errorProv.SetError(txb, "");
            //        aantalAanwezigFout = false;
            //    }
            //}


            ////Als de textbox txbAantalBestAanwezig is wordt dit uitgevoerd.
            //else if (txb.Name == "txbAantalBestAanwezig")
            //{
            //    //Er wordt altijd vanuit gegaan dat er een fout is. Als alles juist is wordt er pas vanuit gegaan dat het juist is.
            //    aantalBestAanwezigFout = true;

            //    //Als de textbox niet enkel cijfers bevat of leeg is of het getal kleiner is dan 0 of een komma getal is dan wordt er een fout aangegeven.
            //    if (!txbText.All(char.IsNumber) || string.IsNullOrEmpty(txbText) || Int32.Parse(txbText) <= 0 || Int32.Parse(txbText) % 1 != 0) errorProv.SetError(txb, foutenMsg);

            //    else
            //    {
            //        //Als men er zeker van is dat er geen enkele fout is wordt de errorprovider leeg gemaakt en is er geen fout.
            //        errorProv.SetError(txb, "");
            //        aantalBestAanwezigFout = false;
            //    }
            //}


            ////Als de textbox txbPrijs is wordt dit uitgevoerd.
            //else if (txb.Name == "txbPrijs")
            //{
            //    //Er wordt altijd vanuit gegaan dat er een fout is. Als alles juist is wordt er pas vanuit gegaan dat het juist is.
            //    prijsFout = true;

            //    //Als de textbox niet enkel cijfers bevat of leeg is of het getal kleiner is dan 0 of een komma getal is dan wordt er een fout aangegeven.
            //    if (!txbText.All(char.IsNumber) || string.IsNullOrEmpty(txbText) || Int32.Parse(txbText) <= 0 || Int32.Parse(txbText) % 1 != 0) errorProv.SetError(txb, foutenMsg);

            //    else
            //    {
            //        //Als men er zeker van is dat er geen enkele fout is wordt de errorprovider leeg gemaakt en is er geen fout.
            //        errorProv.SetError(txb, "");
            //        prijsFout = false;
            //    }
            //}


            //Als de textbox txbKorting is wordt dit uitgevoerd.
            else if (txb.Name == "txbKorting")
            {
                //Er wordt altijd vanuit gegaan dat er een fout is. Als alles juist is wordt er pas vanuit gegaan dat het juist is.
                kortingFout = true;

                //Als de textbox niet enkel cijfers bevat of leeg is of het getal kleiner is dan 0 of een komma getal is of groter is dan 100 dan wordt er een fout aangegeven.
                if (!txbText.All(char.IsNumber) || string.IsNullOrEmpty(txbText) && Int32.Parse(txbText) <= 0 || Int32.Parse(txbText) % 1 != 0 || Int32.Parse(txbText) > 100) errorProv.SetError(txb, foutenMsg);

                else
                {
                    //Als men er zeker van is dat er geen enkele fout is wordt de errorprovider leeg gemaakt en is er geen fout.
                    errorProv.SetError(txb, "");
                    kortingFout = false;
                }
            }

        }

        string GetTxbData(TextBox txb)
        {
            //De string geeft de tekst van een opgevraagde textbox terug.

            //De string wordt aangemaakt.
            string inhoud = "";
            try
            {
                //Er wordt gezocht naar de textbox. Als die is gevonden wordt zijn tekstwaarde in de string inhoud geplaatst.
                inhoud = txb.Text;
            }
            catch (Exception ex)
            {
                //Als de textbox niet werd gevonden wordt er een fout gestuurd.
                errorProv.SetError(txb, ex.Message);
            }
            //De inhoud van de textbox wordt doorgestuurd.

            ControleerTxb(txb);
            if (txb.Name == "txbHuidigProdNaam")
            {
                if (!naamFout)
                {
                    return inhoud;
                }
                else
                {
                    return "Fout!";
                }
            }
            else
            {
                return "Textbox niet gevonden!";
            }
        }


        //string[,] appendArray(string[,] oudeArray)
        //{
        //    string[,] nieuweArray = new string[oudeArray.GetLength(0) + 1, 3];

        //    int c = 0;
        //    int cc = 0;
        //    foreach (string x in oudeArray)
        //    {
        //        nieuweArray[c, cc] = x;
        //        if (cc < 3) cc++;
        //        else
        //        {
        //            cc = 0;
        //            c++;
        //        }
        //    }

        //    return nieuweArray;
        //}

    }
}

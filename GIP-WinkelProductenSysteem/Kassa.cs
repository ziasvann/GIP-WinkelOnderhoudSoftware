using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
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

        private readonly string welkomMsg =
            "Welkom!\nIn dit venster kunt u producten aanmaken en wijzigen. Momenteel zijn er nog geen producten opgeslagen.\nBegin dus met een product aan te maken.";

        private readonly string zekerMsg = "Ben u zeker?";
        private readonly string abortMsg = "Geen probleem! Er is niets veranderd.";
        private readonly string selectItemMsg = "Gelieve één item te selecteren.";
        private readonly string foutenMsg = "Er zit een fout in de ingevoerde gegevens.";

        private readonly string naamAanwezigMsg =
            "Deze naam is al gebruikt voor een ander product. Kies een andere naam.";

        private bool naamFout = false;
        private readonly bool categorieFout = false;
        private readonly bool prijsFout = false;
        private bool kortingFout = false;
        private string totaalPrijs = "";

        private bool manueleprijs = false;


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

        private readonly bool test = true;
        private readonly string filePath = Application.LocalUserAppDataPath + @"\Producten.xml";
        private readonly string[,] productenKopen = new string[0, 0];

        string[] productenToegevoegd = new string[0];

        private string[] productenArray()
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

        private string verander(string getal, char oorspronkelijk, string nieuw)
        {
            string output = "";

            foreach (char n in getal)
            {
                if (n == oorspronkelijk)
                {
                    output += nieuw;
                }
                else
                {
                    output += n;
                }
            }

            return output;
        }

        private void voegProductToe(int productenArrayIndex, decimal prijs, decimal aantal)
        {
            string[] product = productenArray()[productenArrayIndex].Split(',');
            string productNaam = product[0];
            string productPrijs = "";
            
            string aantalPrijs = "";


            if (prijs == 0)
            {
                productPrijs = product[1];
                
                aantalPrijs = (Convert.ToDecimal(verander(productPrijs, '.',",")) * aantal).ToString();
            }
            else
            {
                productPrijs = prijs.ToString();
                aantalPrijs = (decimal.Parse(productPrijs) * aantal).ToString();
            }
            string productCategorie = product[2];

            if (totaalPrijs != "")
            {
                totaalPrijs = decimal.Add(Convert.ToDecimal(verander(aantalPrijs, '.', ",")),
                    Convert.ToDecimal(verander(totaalPrijs, '.', ","))).ToString();
            }
            else
            {
                totaalPrijs = aantalPrijs;
            }
            
            Array.Resize(ref productenToegevoegd, productenToegevoegd.Length + 1);
            productenToegevoegd[productenToegevoegd.Length-1] = productNaam;



            if (!alGekocht(productNaam))
            {
                ListViewItem lvItem = new ListViewItem(productNaam);
                lvItem.SubItems.Add(productPrijs);
                lvItem.SubItems.Add(aantal.ToString());
                lvProducten.Items.Add(lvItem);
            }
            else
            {
                int index = ZoekIndexInArray(productNaam, productenToegevoegd);
                ListViewItem lvItem = lvProducten.Items[index];
                int aantalGekocht = Convert.ToInt32(lvItem.SubItems[2].Text) + Convert.ToInt32(aantal);
                lvItem.SubItems[2].Text = aantalGekocht.ToString();
            }


            lblTotPrijs.Text = verander(totaalPrijs, ',', ".");
            pnlKorting.Visible = false;
            manueleprijs = false;
            txbHuidigProdNaam.Text = "";
            nmudAantal.Value = 1;
        }

        bool alGekocht(string productNaam)
        {
            bool val = false;

            foreach(ListViewItem product in lvProducten.Items)
            {
                if(product.Text == productNaam)
                {
                    val = true;
                }
            }

            return val;
        }

        private void btnVoegToe_Click(object sender, EventArgs e)
        {

            string productNaam = GetTxbData(txbHuidigProdNaam);
            decimal aantal = nmudAantal.Value;

            maakTxbsLeeg();

            voegProductToe(ZoekIndexInArray(productNaam, productenArray()), 0, aantal);

        }

        private int ZoekIndexInArray(string productNaam, string[] array)
        {
            int index = -1;

            for (int i = 0; i < array.Length; i++)
            {
                string[] product = array[i].Split(',');

                if (product[0] == productNaam)
                {
                    index = i;
                    return index;
                }
                else
                {
                    continue;
                }
            }

            return index;
        }

        private void maakTxbsLeeg()
        {
            txbHuidigProdNaam.Text = "";
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
                if (!txbText.All(char.IsLetter) || string.IsNullOrEmpty(txbText))
                {
                    errorProv.SetError(txb, foutenMsg);
                }
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
                if (!txbText.All(char.IsNumber) || string.IsNullOrEmpty(txbText) && int.Parse(txbText) <= 0 ||
                    int.Parse(txbText) % 1 != 0 || int.Parse(txbText) > 100)
                {
                    errorProv.SetError(txb, foutenMsg);
                }
                else
                {
                    //Als men er zeker van is dat er geen enkele fout is wordt de errorprovider leeg gemaakt en is er geen fout.
                    errorProv.SetError(txb, "");
                    kortingFout = false;
                }
            }

        }

        private string GetTxbData(TextBox txb)
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

        private string[] namenStrings()
        {
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
                    if (node.InnerText == n)
                    {
                        aanwezig = true;
                    }
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

        private void txbHuidigProdNaam_TextChanged(object sender, EventArgs e)
        {
            //Open op nieuwe thread voor betere prestaties
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                autocompleteTxbNaam();
            });
            autocompleteTxbNaam();

        }
        private void autocompleteTxbNaam()
        {
            AutoCompleteStringCollection autoSrc = new AutoCompleteStringCollection();
            autoSrc.AddRange(namenStrings());

            txbHuidigProdNaam.AutoCompleteMode = AutoCompleteMode.Suggest;
            txbHuidigProdNaam.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txbHuidigProdNaam.AutoCompleteCustomSource = autoSrc;
        }

        private void btnKorting_Click(object sender, EventArgs e)
        {
            pnlKorting.Visible = true;
        }
        
        private decimal kortingPrijs(decimal prijs, decimal korting)
        {

            //Korting op duidelijk manier schrijven:
            //Bv.: Korting 20% --> korting = 0,8
            decimal rekenbareKorting = 1 - (korting / 100);


            //Prijs berekenen
            decimal berekendePrijs = prijs * rekenbareKorting;


            //Prijs terugsturen in normaal scenario
            if (berekendePrijs >= 0)
            {
                return berekendePrijs;
            }

            //Foutafhandeling:
            //Als fout && testfase --> Zeg dat er een fout is en stuur '0' terug.
            else if (test)
            {
                MessageBox.Show("berekenPrijs: fout!");
                return 0;
            }
            //Als fout --> Stuur '0' terug, zodat duidelijk is dat er een fout is!
            else
            {
                return 0;
            }
        }

        private void btnManuelePrijs_Click(object sender, EventArgs e)
        {
            txbNieuwePrijs.Enabled = true;
            manueleprijs = true;
        }

        private void btnBevestigPrijs_Click(object sender, EventArgs e)
        {
            decimal prijs = 0;
            decimal aantal = nmudAantal.Value;
            string prodNaam = txbHuidigProdNaam.Text;
            int index = ZoekIndexInArray(prodNaam, productenArray());
            

            if (manueleprijs)
            {
                prijs = Convert.ToDecimal(verander(txbNieuwePrijs.Text,'.',","));
                txbNieuwePrijs.Enabled = false;

                voegProductToe(index, prijs, aantal);
            }
            else
            {
                string oudePrijsString = productenArray()[index].Split(',')[1];
                string kortingString = txbKorting.Text;

                decimal oudePrijs = Convert.ToDecimal(verander(oudePrijsString,'.',","));
                decimal korting = decimal.Parse(kortingString);
                
                prijs = kortingPrijs(oudePrijs, korting);

                voegProductToe(index, prijs, aantal);
            }
            pnlKorting.Visible = false;
            manueleprijs = false;
            txbHuidigProdNaam.Text = "";
            nmudAantal.Value = 1;
        }
    }
}

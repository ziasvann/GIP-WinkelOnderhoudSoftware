using System;
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

        private readonly string welkomMsg =
            "Welkom!\nIn dit venster kunt u producten aanmaken en wijzigen. Momenteel zijn er nog geen producten opgeslagen.\nBegin dus met een product aan te maken.";

        private readonly string zekerMsg = "Ben u zeker?";
        private readonly string abortMsg = "Geen probleem! Er is niets veranderd.";
        private readonly string selectItemMsg = "Gelieve één item te selecteren.";
        private readonly string foutenMsg = "Er zit een fout in de ingevoerde gegevens.";

        private readonly string naamAanwezigMsg =
            "Deze naam is al gebruikt voor een ander product. Kies een andere naam.";

        private bool naamFout = false;
        private bool categorieFout = false;
        private bool prijsFout = false;
        private bool kortingFout = false;
        string totaalPrijs = "";

        private bool manueleprijs = false;

        string[] winkelmandje = new string[0];

        private readonly bool test = false;
        private readonly string filePath = Application.LocalUserAppDataPath + @"\Producten.xml";
        private string[,] productenKopen = new string[0, 0];

        string[] productenToegevoegd = new string[0];


        void Kassa_Load(object sender, EventArgs e)
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

        void btnVoegToe_Click(object sender, EventArgs e)
        {
            string productNaam = GetTxbData(txbHuidigProdNaam);
            decimal aantal = nmudAantal.Value;

            maakTxbsLeeg();

            if (productAanwezig(productNaam))
            {
                voegProductToe(ZoekIndexInArray(productNaam, productenArray()), 0, aantal);
            }
            else
            {
                errorProv.SetError(txbHuidigProdNaam, "Product niet gevonden.");
            }
        }

        void txbHuidigProdNaam_TextChanged(object sender, EventArgs e)
        {
            autocompleteTxbNaam();
        }

        void btnKorting_Click(object sender, EventArgs e)
        {
            pnlKorting.Visible = true;
        }

        decimal kortingPrijs(decimal prijs, decimal korting)
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

        void btnManuelePrijs_Click(object sender, EventArgs e)
        {
            txbNieuwePrijs.Enabled = !txbNieuwePrijs.Enabled;
            txbKorting.Enabled = !txbKorting.Enabled;
            txbNieuwePrijs.Text = "";
            txbKorting.Text = "0";
            manueleprijs = !manueleprijs;
        }

        void btnBevestigPrijs_Click(object sender, EventArgs e)
        {
            decimal prijs = 0;
            decimal aantal = nmudAantal.Value;
            string prodNaam = GetTxbData(txbHuidigProdNaam);
            int index = ZoekIndexInArray(prodNaam, productenArray());


            if (manueleprijs)
            {
                try
                {
                    prijs = Convert.ToDecimal(verander(GetTxbData(txbNieuwePrijs), '.', ","));
                }
                catch (Exception)
                {
                    errorProv.SetError(txbNieuwePrijs, "De ingevulde prijs is ongeldig.");
                }
                txbNieuwePrijs.Enabled = !txbNieuwePrijs.Enabled;
                txbKorting.Enabled = !txbKorting.Enabled;

                voegProductToe(index, prijs, aantal);

                manueleprijs = false;
            }
            else
            {
                string oudePrijsString = productenArray()[index].Split(',')[1];
                string kortingString = GetTxbData(txbKorting);

                decimal oudePrijs = Convert.ToDecimal(verander(oudePrijsString, '.', ","));

                decimal korting = 0;

                if (!kortingFout)
                {
                    korting = decimal.Parse(kortingString);
                    prijs = kortingPrijs(oudePrijs, korting);

                    voegProductToe(index, prijs, aantal);

                    pnlKorting.Visible = false;
                    manueleprijs = false;
                    txbHuidigProdNaam.Text = "";
                    nmudAantal.Value = 1;
                }
                else
                {
                    MessageBox.Show("De ingevulde korting is ongeldig.");
                }
            }

        }

        void btnVerwijderProd_Click(object sender, EventArgs e)
        {
            if (lvProducten.SelectedItems.Count == 1)
            {
                int index = lvProducten.SelectedItems[0].Index;
                verwijderProd(index);
            }
            else
            {
                MessageBox.Show("Selecteer één product.");
            }

        }


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
                else
                {
                    output += n;
                }
            }

            return output;
        }

        void voegProductToe(int productenArrayIndex, decimal prijs, decimal aantal)
        {
            string[] product = productenArray()[productenArrayIndex].Split(',');
            string productNaam = product[0];
            string productPrijs = "";

            string aantalPrijs = "";

            //Prijs is 0 als er geen korting is.
            if (prijs == 0)
            {
                productPrijs = verander(product[1], ',', ".");
                aantalPrijs = (Convert.ToDecimal(verander(productPrijs, '.', ",")) * aantal).ToString();
            }
            //Als er wel korting is
            else
            {
                productPrijs = verander(prijs.ToString(), ',', ".");
                aantalPrijs = (decimal.Parse(productPrijs) * aantal).ToString();
            }


            //Als er nog geen enkel product is toegevoegd is de totaalprijs leeg
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
            productenToegevoegd[productenToegevoegd.Length - 1] = productNaam;

            //Product nog nooit gekocht
            if (!alGekocht(productNaam))
            {
                Array.Resize(ref winkelmandje, winkelmandje.Length + 1);
                winkelmandje[winkelmandje.Length - 1] = $"{productNaam},{productPrijs},{aantal}";

                herlaadListView();
            }
            //Product wel al eens gekocht
            else
            {
                int index = ZoekIndexInArray(productNaam, winkelmandje);
                int vorigAantal = Convert.ToInt32(winkelmandje[index].Split(',')[2]);
                int aantalGekocht = vorigAantal + Convert.ToInt32(aantal);

                winkelmandje[index] = $"{productNaam},{productPrijs},{aantalGekocht}";

                herlaadListView();
            }

            totaalPrijs = berekentotaalPrijs().ToString();
            lblTotPrijs.Text = totaalPrijs;

            pnlKorting.Visible = false;
            manueleprijs = false;
            txbHuidigProdNaam.Text = "";
            nmudAantal.Value = 1;
        }

        void herlaadListView()
        {
            lvProducten.Items.Clear();

            foreach (string product in winkelmandje)
            {
                bool toegevoegd = false;

                string[] eigenschappen = product.Split(',');

                string productNaam = eigenschappen[0];
                string productPrijs = eigenschappen[1];
                string aantal = eigenschappen[2];

                foreach (ListViewItem n in lvProducten.Items)
                {
                    //Als product al aanwezig in lvProducten moeten de eigenschappen van het product aangepast worden
                    if (n.Text == productNaam)
                    {
                        n.SubItems[1].Text = verander(productPrijs, ',', ".");
                        n.SubItems[2].Text = aantal;
                        toegevoegd = true;
                    }
                }
                //Als het product nog niet werd toegevoegd moet het worden toegevoegd
                if (!toegevoegd)
                {
                    ListViewItem lvItem = new ListViewItem(productNaam);
                    lvItem.SubItems.Add(verander(productPrijs, ',', "."));
                    lvItem.SubItems.Add(aantal);

                    lvProducten.Items.Add(lvItem);
                    toegevoegd = true;
                }
            }
        }

        bool alGekocht(string productNaam)
        {
            bool val = false;

            foreach (string product in winkelmandje)
            {
                string naam = product.Split(',')[0];
                if (naam == productNaam)
                {
                    val = true;
                }
            }

            return val;
        }

        bool productAanwezig(string productnaam)
        {
            bool val = false;

            foreach (string product in namenStrings())
            {
                if (product == productnaam) val = true;
            }

            return val;
        }

        int ZoekIndexInArray(string productNaam, string[] array)
        {
            int index = 0;

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

        void maakTxbsLeeg()
        {
            txbHuidigProdNaam.Text = "";
        }

        void ControleerTxb(TextBox txb)
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


            //Als de textbox txbKorting is wordt dit uitgevoerd.
            else if (txb.Name == "txbKorting")
            {
                //Tijdelijke bool om te kijken of er een fout is.
                bool tijdelijkFout = false;

                //Er wordt altijd vanuit gegaan dat er een fout is. Als alles juist is wordt er pas vanuit gegaan dat het juist is.
                kortingFout = true;

                //Als de textbox niet enkel letters bevat of leeg is wordt er een fout aangegeven.
                if (!txbText.All(char.IsNumber) || string.IsNullOrEmpty(txbText))
                {
                    errorProv.SetError(txb, foutenMsg);
                    tijdelijkFout = true;
                }
                //Wordt altijd uitgevoerd.
                else if (kortingFout)
                {
                    try
                    {
                        //Als getal kleiner is dan 0 moet er een foutmelding komen.
                        if (int.Parse(txbText) < 0)
                        {
                            errorProv.SetError(txb, "Getal mag niet negatief zijn.");
                            tijdelijkFout = true;
                        }
                        //Als getal een kommagetal is dan moet er een foutmelding komen.
                        if (int.Parse(txbText) % 1 != 0)
                        {
                            errorProv.SetError(txb, "Getal mag geen kommagetal zijn.");
                            tijdelijkFout = true;
                        }
                        if (int.Parse(txbText) >= 100)
                        {
                            errorProv.SetError(txb, "Getal moet kleiner dan 100 zijn.");
                            tijdelijkFout = true;
                        }
                    }
                    catch (Exception)
                    {
                        //Als er fout is met Int.Parse is er waarschijnlijk een letter in txbText. Er moet dus een foutmelding worden weergegeven.
                        errorProv.SetError(txb, "De inhoud van deze textbox moet een getal zijn.");
                        tijdelijkFout = true;
                    }
                }
                if (!tijdelijkFout)
                {
                    //Als men er zeker van is dat er geen enkele fout is wordt de errorprovider leeg gemaakt en is er geen fout.
                    errorProv.SetError(txb, "");
                    kortingFout = false;
                }
            }

            //Als de textbox txbNieuwePrijs is wordt dit uitgevoerd.
            else if (txb.Name == "txbNieuwePrijs")
            {
                //Tijdelijke bool om te kijken of er een fout is.
                bool tijdelijkFout = false;

                //Er wordt altijd vanuit gegaan dat er een fout is. Als alles juist is wordt er pas vanuit gegaan dat het juist is.
                prijsFout = true;

                //Als de textbox niet enkel cijfers bevat of leeg is wordt er een fout aangegeven.
                if (!double.TryParse(txbText, out double d) || string.IsNullOrEmpty(txbText))
                {
                    errorProv.SetError(txb, foutenMsg);
                    tijdelijkFout = true;
                }
                //Wordt altijd uitgevoerd.
                else if (prijsFout)
                {
                    try
                    {
                        //Als getal kleiner is dan 0 moet er een foutmelding komen.
                        if (double.Parse(txbText) <= 0)
                        {
                            errorProv.SetError(txb, "Getal moet groter dan 0 zijn.");
                            tijdelijkFout = true;
                        }
                    }
                    catch (Exception)
                    {
                        //Als er fout is met Int.Parse is er waarschijnlijk een letter in txbText. Er moet dus een foutmelding worden weergegeven.
                        errorProv.SetError(txb, "De inhoud van deze textbox moet een getal zijn.");
                        tijdelijkFout = true;
                    }
                }
                if (!tijdelijkFout)
                {
                    //Als men er zeker van is dat er geen enkele fout is wordt de errorprovider leeg gemaakt en is er geen fout.
                    errorProv.SetError(txb, "");
                    prijsFout = false;
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
            else if (txb.Name == "txbKorting")
            {
                if (!kortingFout)
                {
                    return inhoud;
                }
                else
                {
                    return "Fout!";
                }
            }
            else if (txb.Name == "txbNieuwePrijs")
            {
                if (!prijsFout)
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

        string[] namenStrings()
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

        }

        void autocompleteTxbNaam()
        {
            AutoCompleteStringCollection autoSrc = new AutoCompleteStringCollection();
            autoSrc.AddRange(namenStrings());

            txbHuidigProdNaam.AutoCompleteMode = AutoCompleteMode.Suggest;
            txbHuidigProdNaam.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txbHuidigProdNaam.AutoCompleteCustomSource = autoSrc;
        }

        void verwijderProd(int index)
        {
            string prijs = winkelmandje[index].Split(',')[1];
            double prijsProduct = Convert.ToDouble(verander(prijs, ',', "."));

            int aantal = Convert.ToInt32(winkelmandje[index].Split(',')[2]);

            winkelmandje[index] = "";
            for (int i = index; i < winkelmandje.Length - 1; i++)
            {
                winkelmandje[i] = winkelmandje[i + 1];
            }
            Array.Resize(ref winkelmandje, winkelmandje.Length - 1);

            herlaadListView();

            totaalPrijs = berekentotaalPrijs().ToString();
            lblTotPrijs.Text = totaalPrijs;
        }

        decimal berekentotaalPrijs()
        {
            decimal totaalPrijs = 0;
            foreach (ListViewItem lvItem in lvProducten.Items)
            {
                decimal productPrijs = Convert.ToDecimal(verander(lvItem.SubItems[1].Text, '.', ","));
                decimal aantal = Convert.ToDecimal(lvItem.SubItems[2].Text);

                decimal totaal = productPrijs * aantal;

                totaalPrijs += totaal;
            }

            return totaalPrijs;
        }

    }
}

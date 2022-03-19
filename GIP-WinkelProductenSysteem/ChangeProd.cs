using System;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Xml;

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
            werkCategorieënBij();
            werkNamenBij();
        }

        bool test = false;

        bool newProd = false;
        bool naamFout = false;
        bool categorieFout = false;
        bool aantalAanwezigFout = false;
        bool aantalBestAanwezigFout = false;
        bool prijsFout = false;
        bool kortingFout = false;


        string welkomMsg = "Welkom!\nIn dit venster kunt u producten aanmaken en wijzigen. Momenteel zijn er nog geen producten opgeslagen.\nBegin dus met een product aan te maken.";
        string zekerMsg = "Ben u zeker?";
        string abortMsg = "Geen probleem! Er is niets veranderd.";
        string selectItemMsg = "Gelieve één item te selecteren.";
        string foutenMsg = "Er zit een fout in de ingevoerde gegevens.";
        string naamAanwezigMsg = "Deze naam is al gebruikt voor een ander product. Kies een andere naam.";

        //string filePath = @"D:\ZIAS\School\2021-2022\GIP\Projects\GIP\GIP-WinkelProductenSysteem\GIP-WinkelProductenSysteem\Producten.xml";
        string filePath = Application.LocalUserAppDataPath + @"\Producten.xml";

        string[] categorieën = new string[0];
        string[] namen = new string[0];


        //'huidigeNaam' wordt gebruikt om te controleren of de naam is gewijzigd na het aanpassen van een product.
        string huidigeNaam = "";

        private void LoadProducten()
        {
            //Deze method wordt geactiveerd bij het laden van het programma, het laad alle nodige info om het programma te starten.
            
            //Maakt de XML-database aan als deze nog niet is gemaakt.
            if (!File.Exists(filePath))
            {
                MakeXmlProducten();
                MessageBox.Show(welkomMsg, "Welkom!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (File.ReadAllText(filePath) == "")
            {
                MakeXmlProducten();
                MessageBox.Show(welkomMsg, "Welkom!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //Als de XML-database volledig in orde is kunnen de producten geladen worden in de listview.
            else FillListView();
        }

        private void btnMaakProduct_Click(object sender, EventArgs e)
        {
            //Deze method wordt geactiveerd bij het klikken op btnMaakproduct.

            //Er moet een nieuw product worden gemaakt dus wordt newProd op 'true' gezet.
            newProd = true;
            //Alle textboxen worden leeggemaakt.
            ClearTxb();
            //De panel om een product aan te maken wordt zichtbaar gemaakt.
            pnlProductEigenschappen.Visible = true;
        }

        private void btnBevestigProducten_Click(object sender, EventArgs e)
        {
            //Deze method wordt geactiveerd bij het klikken op btnBevestigen.

            //Vooraleer alle textboxen worden ingelezen moeten ze worden gecontroleerd op fouten.
            ControleerTxb(txbNaam);
            ControleerTxb(txbCategorie);
            ControleerTxb(txbAantalAanwezig);
            ControleerTxb(txbAantalBestAanwezig);
            ControleerTxb(txbPrijs);
            ControleerTxb(txbKorting);

            //Pas wanneer er geen fouten zijn in de textboxen kan er worden verdergegaan.
            if (GeenErrors())
            {
                //De panel kan weer onzichtbaar worden, zodat er niet meteen een nieuw product kan worden gemaakt.
                pnlProductEigenschappen.Visible = false;

                //Als het een nieuw prduct is, wordt er een nieuw product aangemaakt. Als het dat niet is, wordt het huidige product gewijzigd.
                if (newProd) MaakNieuwProduct();
                else if (!newProd) WijzigProduct();

                //Om de wijzigeingen zichtbaar te maken wordt de listview opnieuw ingeladen.
                FillListView();

                //Er is nu sowieso geen nieuw product meer.
                newProd = false;
            }
            else
            {
                if (test)
                {
                    MessageBox.Show(aantalAanwezigXml(GetTxbData(txbCategorie)).ToString());
                }
                //Als er toch een fout zit in een van textboxen wordt dit weergegeven door een textbox.
                MessageBox.Show(foutenMsg);
            }
        }

        private void btnChangeProd_Click(object sender, EventArgs e)
        {
            //Deze method wordt geactiveerd bij het klikken op btnChangeProd.

            //Om een product te wijzigen worden eerst de oorspronkelijke waarden ingevoerd, zodat er makkelijk veranderingen kunnen plaatsvinden.
            FillTxb();
            //Er is geen nieuw product.
            newProd = false;
            //De naam van het product dat wordt gewijzigd wordt extra ingegeven. 
            huidigeNaam = GetTxbData(txbNaam);
        }

        private void btnDelProduct_Click(object sender, EventArgs e)
        {
            //Deze method wordt geactiveerd bij het klikken op btnDelProduct.

            //Het product moet worden verwijderd, daarna moeten de categorieën worden bijgewerkt.
            DelProduct();
            werkCategorieënBij();
        }

        void MakeXmlProducten()
        {
            //Deze method maakt een XML bestand met de standaard waarden. Op die manier kunnen er later producten in worden toegevoegd.

            //De XMLWriter wordt klaargezet en zijn settings worden ingegeven.
            XmlWriter xml;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;

            xml = XmlWriter.Create(filePath, settings);

            //Het bestand wordt aangemaakt en de standaardwaarden worden ingevoerd.
            xml.WriteStartDocument();
            xml.WriteStartElement("Producten");
            xml.WriteEndElement();
            xml.Close();
        }

        void MaakNieuwProduct()
        {
            //Deze method maakt een nieuw product en zet deze in de XML-database.

            //Er moet niet meer gecontroleerd worden of de waarden juist zijn, want dat werd al gedaan vooraleer deze method werd opgeroepen.


            //De XML writer wordt klaargezet met het XML doucment.
            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(filePath, FileMode.Open);
            xmlDoc.Load(file);


            //De eigenschappen van het product worden opgevraagd en opgeslagen.
            string prodNaam = GetTxbData(txbNaam);
            string prodCategorie = GetTxbData(txbCategorie);
            string prodAantal = GetTxbData(txbAantalAanwezig);
            string prodBestAantal = GetTxbData(txbAantalBestAanwezig);
            string prodPrijs = GetTxbData(txbPrijs);
            string prodKorting = GetTxbData(txbKorting);

            //De nieuwePrijs wordt berekent, op basis van de prijs en de korting.
            double prijsProd = double.Parse(prodPrijs);
            double kortingProd = double.Parse(prodKorting);
            string nieuwePrijsProd = kortingPrijs(prijsProd, kortingProd).ToString();

            //De elementen worden gemaakt.
            XmlElement product = xmlDoc.CreateElement("Product");

            XmlElement naam = xmlDoc.CreateElement("Naam");
            XmlText naamText = xmlDoc.CreateTextNode(prodNaam);

            XmlElement categorie = xmlDoc.CreateElement("Categorie");
            XmlText categorieText = xmlDoc.CreateTextNode(prodCategorie);

            XmlElement aantal = xmlDoc.CreateElement("Aantal");
            XmlText aantalText = xmlDoc.CreateTextNode(prodAantal);

            XmlElement aantalBest = xmlDoc.CreateElement("Bestaantal");
            XmlText aantalBestText = xmlDoc.CreateTextNode(prodBestAantal);

            XmlElement prijs = xmlDoc.CreateElement("Prijs");
            XmlText prijsText = xmlDoc.CreateTextNode(prodPrijs);

            XmlElement korting = xmlDoc.CreateElement("Korting");
            XmlText kortingText = xmlDoc.CreateTextNode(prodKorting);

            XmlElement nieuwePrijs = xmlDoc.CreateElement("NieuwePrijs");
            XmlText nieuwePrijsText = xmlDoc.CreateTextNode(nieuwePrijsProd);

            //De elementen worden toegewezen met hun overeenkomstige waarde.
            naam.AppendChild(naamText);
            categorie.AppendChild(categorieText);
            aantal.AppendChild(aantalText);
            aantalBest.AppendChild(aantalBestText);
            prijs.AppendChild(prijsText);
            korting.AppendChild(kortingText);
            nieuwePrijs.AppendChild(nieuwePrijsText);

            //De elementen worden toegevoegd aan een hoofdelement (product).
            product.AppendChild(naam);
            product.AppendChild(categorie);
            product.AppendChild(aantal);
            product.AppendChild(aantalBest);
            product.AppendChild(prijs);
            product.AppendChild(korting);
            product.AppendChild(nieuwePrijs);

            //Het hoofdelement wordt toegevoegd aan het XML document en het bestand wordt opgeslagen en gesloten.
            xmlDoc.DocumentElement.AppendChild(product);
            file.Close();
            xmlDoc.Save(filePath);

            //De categorieën worden bijgewerkt.
            werkCategorieënBij();

            if (test)
            {
                MessageBox.Show($"Prijs: {prodPrijs}\nKorting: {prodKorting}\nNieuwe prijs: {nieuwePrijsProd}");
            }
        }

        void WijzigProduct()
        {
            //Deze method wijzigt een bepaald product en geeft zijn nieuwe waarden.

            //Er moet niet meer gecontroleerd worden of de waarden juist zijn, want dat werd al gedaan vooraleer deze method werd opgeroepen.


            //De XML writer wordt klaargezet met het XML document.
            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(filePath, FileMode.Open);
            xmlDoc.Load(file);

            //De eigenschappen van het product worden opgevraagd en opgeslagen.
            string prodNaam = GetTxbData(txbNaam);
            string prodCategorie = GetTxbData(txbCategorie);
            string prodAantal = GetTxbData(txbAantalAanwezig);
            string prodBestAantal = GetTxbData(txbAantalBestAanwezig);
            string prodPrijs = GetTxbData(txbPrijs);
            string prodKorting = GetTxbData(txbKorting);

            //De nieuwePrijs wordt berekent, op basis van de prijs en de korting.
            double prijsProd = double.Parse(prodPrijs);
            double kortingProd = double.Parse(prodKorting);
            string nieuwePrijsProd = kortingPrijs(prijsProd, kortingProd).ToString();

            //Er wordt door het XML bestand gezocht naar het juiste product.
            foreach (XmlNode xmlNode in xmlDoc.SelectNodes("/Producten/Product"))
            {
                if (xmlNode.SelectSingleNode("Naam").InnerText == prodNaam)
                {
                    //Wanneer het juist product is gevonden worden de nieuwe waarden toegewezen.
                    xmlNode["Categorie"].InnerText = prodCategorie;
                    xmlNode["Aantal"].InnerText = prodAantal;
                    xmlNode["Bestaantal"].InnerText = prodBestAantal;
                    xmlNode["Prijs"].InnerText = prodPrijs;
                    xmlNode["Korting"].InnerText = prodKorting;
                    xmlNode["NieuwePrijs"].InnerText = nieuwePrijsProd;
                }
            }

            //Het XML document en het bestand wordt opgeslagen en gesloten.
            file.Close();
            xmlDoc.Save(filePath);
        }

        void DelProduct()
        {
            //Deze method verwijderd een bepaald product.


            //Er kan slechts één product tegelijkertijd worden verwijderd.
            if (lvProducten.SelectedItems.Count == 1)
            {
                //Er wordt gevraagd of men zeker is of u het product wilt verwijderen.

                DialogResult result = MessageBox.Show(zekerMsg, "Verwijderen?", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    //De rijindex van een XML document begint bij 1. Bij de listview is dat bij 0.
                    int rowIndex = lvProducten.FocusedItem.Index + 1;

                    //Het XML document wordt geopend.
                    XmlDocument xmlDoc = new XmlDocument();
                    FileStream file = new FileStream(filePath, FileMode.Open);
                    xmlDoc.Load(file);

                    //De XML node waarin het product zich bevindt wordt geselecteerd en vervolgens verwijderd.
                    XmlNode xmlNode = xmlDoc.SelectSingleNode($"/Producten/Product[{rowIndex}]");
                    xmlNode.ParentNode.RemoveChild(xmlNode);

                    //Het XML document en het bestand wordt opgeslagen en gesloten.
                    file.Close();
                    xmlDoc.Save(filePath);
                    FillListView();
                }
                else
                {
                    //Als men het product toch niet wilt verwijderen, wordt dit ook duidelijk gemaakt en wordt alles terug gezet.
                    MessageBox.Show(abortMsg);
                    pnlProductEigenschappen.Visible = false;
                }
            }
            else
            {
                //Als er niet één maar meerdere of nul producten werden geselecteerd wordt er een foutmelding gegeven en wordt alles terug gezet.
                MessageBox.Show(selectItemMsg);
                pnlProductEigenschappen.Visible = false;
            }
        }

        void FillListView()
        {
            //Deze method laad alle producten met hun eigenschappen in de ListView.

            //Vooraleer de producten worden geladen wordt de ListView leeg gemaakt.
            lvProducten.Items.Clear();

            //Het XML document wordt geopend.
            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(filePath, FileMode.Open);
            xmlDoc.Load(file);

            //De lijst XML node's met daarin de producten wordt geselecteerd.
            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("/Producten/Product");

            //De lijst met XML node's wordt doorlopen per node --> ieder product komt op die manier dus aan bod.
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                //De gegevens van het product worden opgevraagd.
                string prodNaam = xmlNode["Naam"].InnerText;
                string prodCategorie = xmlNode["Categorie"].InnerText;
                string prodAantal = xmlNode["Aantal"].InnerText;
                string prodBestAantal = xmlNode["Bestaantal"].InnerText;
                string prodPrijs = xmlNode["Prijs"].InnerText;
                string prodKorting = xmlNode["Korting"].InnerText;
                string prodNieuwePrijs = xmlNode["NieuwePrijs"].InnerText;


                //Het product wordt in een ListViewItem gezet, waarin er SubItems zjn voor de eigenschappen van het product.
                ListViewItem lvi = new ListViewItem(prodNaam);

                lvi.SubItems.Add(prodCategorie);
                lvi.SubItems.Add(prodAantal);
                lvi.SubItems.Add(prodBestAantal);
                lvi.SubItems.Add(prodPrijs);
                lvi.SubItems.Add(prodKorting);
                lvi.SubItems.Add(prodNieuwePrijs);

                //Het product wordt in het programma zichtbaar gemaakt door het in de ListView te zetten.
                lvProducten.Items.Add(lvi);
            }
            //Het XML document wordt gesloten.
            file.Close();
        }

        void FillTxb()
        {
            //Deze method zorgt ervoor dat alle eigenschappen van het geselecteerd product in zijn bijbehorende textbox komt.

            //Er mag slechts één product tegelijkertijd geselecteerd zijn.
            if (lvProducten.SelectedItems.Count == 1)
            {
                //De rijindex van een XML document begint bij 1. Bij de listview is dat bij 0.
                int rowIndex = lvProducten.FocusedItem.Index + 1;

                //Het XML document wordt geopend.
                XmlDocument xmlDoc = new XmlDocument();
                FileStream file = new FileStream(filePath, FileMode.Open);
                xmlDoc.Load(file);

                //De XML node waarin het product zich bevindt wordt geselecteerd.
                XmlNode xmlNode = xmlDoc.SelectSingleNode($"/Producten/Product[{rowIndex}]");

                //De eigenschappen van de XML node (het product dus) worden opgevraagd.
                string prodNaam = xmlNode["Naam"].InnerText;
                string prodCategorie = xmlNode["Categorie"].InnerText;
                string prodAantal = xmlNode["Aantal"].InnerText;
                string prodBestAantal = xmlNode["Bestaantal"].InnerText;
                string prodPrijs = xmlNode["Prijs"].InnerText;
                string prodKorting = xmlNode["Korting"].InnerText;

                //De opgevraagde eigenschappen worden in zijn bijbehorende textbox gesplaatst.
                //De wijzigbaar textboxen worden indien nodig onwijzigbaar gemaakt.
                txbNaam.Text = prodNaam;
                txbNaam.Enabled = false;
                txbCategorie.Text = prodCategorie;
                txbCategorie.Enabled = false;    
                txbAantalAanwezig.Text = prodAantal;
                txbAantalBestAanwezig.Text = prodBestAantal;
                txbPrijs.Text = prodPrijs;
                txbKorting.Text = prodKorting;
                
                //Het XML document wordt gesloten.
                file.Close();

                //De panel met daarin de net aangepaste textboxen wordt zichtbaar gemaakt.
                pnlProductEigenschappen.Visible = true;
            }
            else
            {
                //Als er niet één maar meerdere of nul producten werden geselecteerd wordt er een foutmelding gegeven en wordt alles terug gezet.
                MessageBox.Show(selectItemMsg);
                pnlProductEigenschappen.Visible = false;
            }
        }
        void ClearTxb()
        {
            //Deze method maakt alle textboxen leeg.

            //Iedere tekst van de textboxen wordt op "" (leeg) gezet.
            txbNaam.Text = "";
            txbNaam.Enabled = true;
            txbCategorie.Text = "";
            txbCategorie.Enabled = true;
            txbAantalAanwezig.Text = "";
            txbAantalBestAanwezig.Text = "";
            txbPrijs.Text = "" ;
            txbKorting.Text = "";
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
            return inhoud;
        }

        void ControleerTxb(TextBox txb)
        {
            //Deze method dient om te controleren of de ingegeven tekst van de textbox een reële waarde is.

            //Eerst wordt de tekst van de textbox opgevraagd.
            string txbText = GetTxbData(txb);

            //Als de textbox txbNaam is wordt dit uitgevoerd.
            if (txb.Name == "txbNaam")
            {
                //Er wordt altijd vanuit gegaan dat er een fout is. Als alles juist is wordt er pas vanuit gegaan dat het juist is.
                naamFout = true;

                //Als de textbox niet enkel letters bevat of leeg is wordt er een fout aangegeven.
                if (!txbText.All(char.IsLetter) || string.IsNullOrEmpty(txbText)) errorProv.SetError(txb, foutenMsg);

                //Als er nog geen fout was én de naam al eens gebruikt werd en het een nieuw product is, wordt er een fout gegeven (Het gaat dus over een nieuw product).
                else if (naamAlAanwezig(txbText) && newProd) errorProv.SetError(txb, naamAanwezigMsg);
                //Als er nog geen fout was én de naam al eens gebruikt werd maar al een oud product is, wordt er geen fout gegeven (Het product werd dus gewijzigd).
                else if (naamAlAanwezig(txbText) && !newProd && txbText != huidigeNaam) errorProv.SetError(txb, naamAanwezigMsg);
                
                else
                {
                    //Als men er zeker van is dat er geen enkele fout is wordt de errorprovider leeg gemaakt en is er geen fout.
                    errorProv.SetError(txb, "");
                    naamFout = false;
                }
            }

            //Als de textbox txbCategorie is wordt dit uitgevoerd.
            else if (txb.Name == "txbCategorie")
            {
                //Er wordt altijd vanuit gegaan dat er een fout is. Als alles juist is wordt er pas vanuit gegaan dat het juist is.
                categorieFout = true;
                
                //Als de textbox niet enkel letters bevat of leeg is wordt er een fout aangegeven.
                if (!txbText.All(char.IsLetter) || string.IsNullOrEmpty(txbText)) errorProv.SetError(txb, foutenMsg);
                
                else
                {
                    //Als men er zeker van is dat er geen enkele fout is wordt de errorprovider leeg gemaakt en is er geen fout.
                    errorProv.SetError(txb, "");
                    categorieFout = false;
                }
            }
            //Als de textbox txbAantalAanwezig is wordt dit uitgevoerd.
            else if (txb.Name == "txbAantalAanwezig")
            {
                //Er wordt altijd vanuit gegaan dat er een fout is. Als alles juist is wordt er pas vanuit gegaan dat het juist is.
                aantalAanwezigFout = true;
                
                //Als de textbox niet enkel letters bevat of leeg is wordt er een fout aangegeven.
                if (!txbText.All(char.IsNumber) || string.IsNullOrEmpty(txbText) || Int32.Parse(txbText) <= 0 || Int32.Parse(txbText) % 1 != 0) errorProv.SetError(txb, foutenMsg);

                else
                {
                    //Als men er zeker van is dat er geen enkele fout is wordt de errorprovider leeg gemaakt en is er geen fout.
                    errorProv.SetError(txb, "");
                    aantalAanwezigFout = false;
                }
            }
            //Als de textbox txbAantalBestAanwezig is wordt dit uitgevoerd.
            else if (txb.Name == "txbAantalBestAanwezig")
            {
                //Er wordt altijd vanuit gegaan dat er een fout is. Als alles juist is wordt er pas vanuit gegaan dat het juist is.
                aantalBestAanwezigFout = true;

                //Als de textbox niet enkel cijfers bevat of leeg is of het getal kleiner is dan 0 of een komma getal is dan wordt er een fout aangegeven.
                if (!txbText.All(char.IsNumber) || string.IsNullOrEmpty(txbText) || Int32.Parse(txbText) <= 0 || Int32.Parse(txbText) % 1 != 0) errorProv.SetError(txb, foutenMsg);

                else
                {
                    //Als men er zeker van is dat er geen enkele fout is wordt de errorprovider leeg gemaakt en is er geen fout.
                    errorProv.SetError(txb, "");
                    aantalBestAanwezigFout = false;
                }
            }
            //Als de textbox txbPrijs is wordt dit uitgevoerd.
            else if(txb.Name == "txbPrijs")
            {
                //Er wordt altijd vanuit gegaan dat er een fout is. Als alles juist is wordt er pas vanuit gegaan dat het juist is.
                prijsFout = true;

                //Als de textbox niet enkel cijfers bevat of leeg is of het getal kleiner is dan 0 of een komma getal is dan wordt er een fout aangegeven.
                if (!txbText.All(char.IsNumber) || string.IsNullOrEmpty(txbText) || Int32.Parse(txbText) <= 0 || Int32.Parse(txbText) % 1 != 0) errorProv.SetError(txb, foutenMsg);

                else
                {
                    //Als men er zeker van is dat er geen enkele fout is wordt de errorprovider leeg gemaakt en is er geen fout.
                    errorProv.SetError(txb, "");
                    prijsFout = false;
                }
            }
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

        bool naamAlAanwezig(string naam)
        {
            bool aanwezig = false;

            foreach(string n in namen)
            {
                if(n==naam) aanwezig = true;
            }
            if (aanwezig&&newProd) naamFout = true;

            return aanwezig;
        }

        bool GeenErrors()
        {
            bool geenErrors = true;

            if (naamFout || categorieFout || aantalAanwezigFout || aantalBestAanwezigFout || prijsFout || kortingFout) geenErrors = false;

            return geenErrors;
        }

        int aantalAanwezigXml(string teZoeken)
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

        string[] wijzigCategorieën()
        {
            string[] categorieën = new string[0];

            int count = 0;

            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(filePath, FileMode.Open);
            xmlDoc.Load(file);

            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("/Producten/Product/Categorie");

            foreach(XmlNode node in xmlNodeList)
            {
                bool aanwezig = false;
                foreach(string n in categorieën)
                {
                    if (node.InnerText == n) aanwezig = true;
                }
                if (!aanwezig)
                {
                    Array.Resize(ref categorieën, categorieën.Length + 1);
                    categorieën[count] = node.InnerText;
                    count++;
                }

            }
            file.Close();

            return categorieën;
        }

        void werkCategorieënBij()
        {
            Array.Resize(ref categorieën, wijzigCategorieën().Length);
            categorieën = wijzigCategorieën();
        }

        private void ChangeProd_DoubleClick(object sender, EventArgs e)
        {
            if (test)
            {
                string n = "";
                foreach (string x in categorieën)
                {
                    n += x + "\n";
                }
                MessageBox.Show(n);
            }
            
        }

        private void txbCategorie_TextChanged(object sender, EventArgs e)
        {
            autocompleteTxbCat();
        }

        void autocompleteTxbCat()
       {
            AutoCompleteStringCollection autoSrc = new AutoCompleteStringCollection();
            autoSrc.AddRange(categorieën);

            txbCategorie.AutoCompleteMode = AutoCompleteMode.Suggest;
            txbCategorie.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txbCategorie.AutoCompleteCustomSource = autoSrc;

        }

        string[] wijzigNamen()
        {
            //Deze array geeft als resultaat alle product namen uit de XML-database.


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
                    if (node.InnerText == n) aanwezig = true;
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

        void werkNamenBij()
        {
            //Dit werkt de array met namen bij zodat deze up-to-date is.

            //Lengte van de array +1.
            Array.Resize(ref namen, wijzigNamen().Length);
            //Werk namen bij met juiste array.
            namen = wijzigNamen();
        }

        double kortingPrijs(double prijs, double korting)
        {

            //Korting op duidelijk manier schrijven:
            //Bv.: Korting 20% --> korting = 0,8
            double rekenbareKorting = 1 - (korting / 100);


            //Prijs berekenen
            double berekendePrijs = prijs * rekenbareKorting;


            //Prijs terugsturen in normaal scenario
            if (berekendePrijs >= 0)
            {
                return berekendePrijs;
            }
            
            //Foutafhandeling:
            //Als fout && testfase --> Zeg dat er een fout is en stuur '0' terug.
            else if (test)
            {
                MessageBox.Show("berekenPrijs: fout!!!!");
                return 0;
            }
            //Als fout --> Stuur '0' terug, zodat duidelijk is dat er een fout is!
            else
            {
                return 0;
            }
        }
    } 
}
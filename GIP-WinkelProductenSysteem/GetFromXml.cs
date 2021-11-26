using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace GIP_WinkelProductenSysteem
{
    class GetFromXml
    {
        private string FilePath;

        public GetFromXml()
        {
        }
        public GetFromXml(string filepath)
        {
            FilePath = filepath;
        }

        public int aantalAanwezigXml(string teZoeken)
        {
            int aantalAanwezig = 0;

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

                if (prodNaam == teZoeken) aantalAanwezig++;
                if (prodCategorie == teZoeken) aantalAanwezig++;
                //prodAantal en prodBestAantal kunnen dezelfde zijn!!
                if (prodAantal == teZoeken) aantalAanwezig++;
                if (prodBestAantal == teZoeken) aantalAanwezig++;
            }

            file.Close();

            return aantalAanwezig;
        }

        public string[,] zoekenInXml(string soortTeZoeken, string teZoeken)
        {
            string[,] gevonden = new string[1, 4];
            int count = 0;

            XmlDocument xmlDoc = new XmlDocument();
            FileStream file = new FileStream(FilePath, FileMode.Open);
            xmlDoc.Load(file);

            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("/Producten/Product");

            if (soortTeZoeken == "Naam")
            {
                xmlNodeList = xmlDoc.SelectNodes("/Producten/Product/Naam");
            }
            else if (soortTeZoeken == "Categorie")
            {
                xmlNodeList = xmlDoc.SelectNodes("/Producten/Product/Categorie");
            }
            else if (soortTeZoeken == "Aantal")
            {
                xmlNodeList = xmlDoc.SelectNodes("/Producten/Product/Aantal");
            }
            else if (soortTeZoeken == "Bestaantal")
            {
                xmlNodeList = xmlDoc.SelectNodes("/Producten/Product/Bestaantal");
            }

            foreach (XmlNode xmlNode in xmlNodeList)
            {
                string prodNaam = xmlNode.ParentNode["Naam"].InnerText;
                string prodCategorie = xmlNode.ParentNode["Categorie"].InnerText;
                string prodAantal = xmlNode.ParentNode["Aantal"].InnerText;
                string prodBestAantal = xmlNode.ParentNode["Bestaantal"].InnerText;

                if (prodNaam == teZoeken && soortTeZoeken == "Naam")
                {
                    gevonden[count, 0] = prodNaam;
                    gevonden[count, 1] = prodCategorie;
                    gevonden[count, 2] = prodAantal;
                    gevonden[count, 3] = prodBestAantal;

                }
                else if (prodCategorie == teZoeken && soortTeZoeken == "Categorie")
                {
                    gevonden[count, 0] = prodNaam;
                    gevonden[count, 1] = prodCategorie;
                    gevonden[count, 2] = prodAantal;
                    gevonden[count, 3] = prodBestAantal;
                }
                else if (prodAantal == teZoeken && soortTeZoeken == "Aantal")
                {
                    gevonden[count, 0] = prodNaam;
                    gevonden[count, 1] = prodCategorie;
                    gevonden[count, 2] = prodAantal;
                    gevonden[count, 3] = prodBestAantal;
                }
                else if (prodBestAantal == teZoeken && soortTeZoeken == "Bestaantal")
                {
                    gevonden[count, 0] = prodNaam;
                    gevonden[count, 1] = prodCategorie;
                    gevonden[count, 2] = prodAantal;
                    gevonden[count, 3] = prodBestAantal;
                }

                count++;
                //Lengte van array "gevonden" aanpassen.
                string[,] temparr = new string[count + 1, 4];
                Array.Copy(gevonden, temparr, gevonden.Length);
                gevonden = temparr;
            }

            file.Close();

            return gevonden;
        }
    }
}

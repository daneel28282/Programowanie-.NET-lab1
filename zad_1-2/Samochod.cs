//ININ4(hybryda)_PR1.2 73980
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Samochod
    {
        public string marka;
        private string Marka
        {
            get { return marka; }
            set { marka = value; }
        }

        public string model;
        private string Model
        {
            get { return model; }
            set { model = value; }
        }

        public int iloscDrzwi;
        private int IloscDrzwi
        {
            get { return iloscDrzwi; }
            set { iloscDrzwi = value; }
        }

        public int pojemnoscSilnika;
        private int PojemnoscSilnika
        {
            get { return pojemnoscSilnika; }
            set { pojemnoscSilnika = value; }
        }

        public double srednieSpalanie;
        private double SrednieSpalanie
        {
            get { return srednieSpalanie; }
            set { srednieSpalanie = value; }
        }

        private static int iloscSamochodow = 0;

        public Samochod()
        {
            this.marka = "nieznana";
            this.model = "nieznany";
            this.iloscDrzwi = 0;
            this.pojemnoscSilnika = 0;
            this.srednieSpalanie = 0;

            iloscSamochodow++;
        }

        public Samochod(string marka, string model, int iloscDrzwi, int pojemnoscSilnika, double srednieSpalanie)
        {
            this.marka = marka;
            this.model = model;
            this.iloscDrzwi = iloscDrzwi;
            this.pojemnoscSilnika = pojemnoscSilnika;
            this.srednieSpalanie = srednieSpalanie;

            iloscSamochodow++;
        }


        private double ObliczSpalanie(double dlugoscTrasy)
        {
            return this.srednieSpalanie * dlugoscTrasy / 100.0;
        }
        public void ObliczSpalanieInfo(double dlugoscTrasy)
        {

            Console.WriteLine("obliczone spalanie Spalanie 100km ----- : " + this.srednieSpalanie * dlugoscTrasy / 100.0+" l/km");
        }

        private double ObliczKosztPrzejazdu(double dlugoscTrasy, double cenaPaliwa)
        {
            return this.ObliczSpalanie(dlugoscTrasy) * cenaPaliwa;
        }
        public void ObliczKosztPrzejazduInfp(double dlugoscTrasy, double cenaPaliwa)
        {
            Console.WriteLine("obliczony koszt przejazdu 100km cena paliwa: 6zł ----- : " + this.ObliczSpalanie(dlugoscTrasy) * cenaPaliwa +"zł");
        }

        public void WypiszInfo()
        {
            Console.WriteLine("marka: " + this.marka);
            Console.WriteLine("model: " + this.model);
            Console.WriteLine("Ilość Drzwi: " + this.IloscDrzwi);
            Console.WriteLine("Pojemność Silnika: " + this.pojemnoscSilnika);
            Console.WriteLine("Srednie Spalanie: " + this.srednieSpalanie);
        }

        public static void WypiszIloscSamochodow()
        {
            Console.WriteLine("iloscSamochodow:" + iloscSamochodow);
        }
    }
}
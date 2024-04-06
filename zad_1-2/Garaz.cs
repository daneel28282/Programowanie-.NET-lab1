//ININ4(hybryda)_PR1.2 73980
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Garaz
    {
        private string adres;
        public string Adres
        {
            get { return adres; }
            set { adres = value; }
        }

        private int pojemnosc;
        public int Pojemnosc
        {
            get { return pojemnosc; }
            set
            {
                pojemnosc = value;
                samochody = new Samochod[pojemnosc];
            }
        }

        private Samochod[] samochody;
        private int liczbaSamochodow = 0;

        public Garaz()
        {
            this.adres = "";
            this.pojemnosc = 0;
            samochody = null;
        }
        public Garaz(string adres_, int pojemnosc_)
        {
            this.adres = adres_;
            this.pojemnosc = pojemnosc_;
            samochody = new Samochod[this.pojemnosc];

        }

        public void WprowadzSamochod(Samochod samochod)
        {
            if (this.liczbaSamochodow == this.pojemnosc)
            {
                Console.WriteLine("BRAK MIEJSCA");
                Console.WriteLine();
            }
            else
            {

                samochody[this.liczbaSamochodow] = samochod;

                Console.WriteLine("------- WJAZD -------" + samochod.marka);
                Console.WriteLine();

                this.liczbaSamochodow++;
            }
        }
        //---------------
        public void WyprowadzSamochod()
        {
            if (this.liczbaSamochodow == 0)
            {
                Console.WriteLine("------- GARAŻ PUSTY -------");
                Console.WriteLine();
            }
            else
            {

                int miejsceWG = liczbaSamochodow - 1;

                Console.WriteLine("------- WYJAZD -------" + samochody[miejsceWG].marka);
                Console.WriteLine();

                Array.Clear(samochody, miejsceWG, 1);

                this.liczbaSamochodow--;
                

                //------------------list 
                //int indexToRemove = this.liczbaSamochodow;
                //var listaSamochodow = new List<Samochod>(samochody);
                //listaSamochodow.RemoveAt(0);
                //samochody = listaSamochodow.ToArray();
                //------------------list
                

            }
        }
        //---------------

        public void WypiszInfo()
        {
            Console.WriteLine("informacje o Garażu: ");
            Console.WriteLine("Adres: " + this.adres);
            Console.WriteLine("Pojemnosc: " + this.pojemnosc);
            Console.WriteLine("Ilosc Samochodów: " + this.liczbaSamochodow);
            Console.WriteLine();
            
            //---Sprawdzenie TABLICY
            Console.WriteLine("------- Garaż/Tablica Zawiera następujące Samochody/Elementy:");
            for (int i = samochody.GetLowerBound(0); i <= samochody.GetUpperBound(0); i++)
            {
                Console.WriteLine("   [{0,2}]: {1}", i, samochody[i] );
                if (samochody[i] != null)
                {
                    Console.WriteLine(samochody[i].marka);
                }
            }
            Console.WriteLine();
            //---Sprawdzenie TABLICY


            for (int i = 0; i < this.liczbaSamochodow; i++)
            {
                this.samochody[i].WypiszInfo();
                this.samochody[i].ObliczSpalanieInfo(100);
                this.samochody[i].ObliczKosztPrzejazduInfp(100, 6);
                Console.WriteLine();
            }
        }

    }
}
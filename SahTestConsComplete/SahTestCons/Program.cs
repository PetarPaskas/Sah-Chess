using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using Microsoft.VisualBasic.CompilerServices;

namespace SahTestCons
{
    public enum TipFigura
    {
        Top=1,
        Konj=2,
        Lovac=3,
        Kralj=4,
        Kica=5,
        Piun=6
    }
    public class Figura
    {
        private int xPos;
        private int yPos;
        private TipFigura tip;
        public int indikator { get; set; }  //Ovo ce biti indikator stanja, recimo ako je na 0, nikom nista, ako je na 1, onda ce indikator da indukuje na to da je recimo pijun odradio svoj "special move"
        public bool pozicija { get; set; } //Ovo ako je true = prijatelj, false = neprijatelj

        public int Xpos
        {
            get {return xPos; }
            set {xPos = value; }
        }

        public int Ypos
        {
            get {return yPos; }
            set { yPos = value; }
        }

        public string Tip
        {
            get { return tip.ToString(); }
        }

        public Figura(int xPos, int yPos, string tipFigure)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            tip = (TipFigura)Enum.Parse(typeof(TipFigura), tipFigure);
            this.indikator = 0;
            this.pozicija = true;
        }
        public Figura(int xPos, int yPos, string tipFigure, bool pozicija)
        {
            this.xPos = xPos;
            this.yPos = yPos;
            tip = (TipFigura)Enum.Parse(typeof(TipFigura), tipFigure);
            this.indikator = 0;
            this.pozicija = pozicija;
        }

    }
    class Program
    {
        public static void PomeriPrijatelj(List<Figura> lista, List<Figura> prijGroblje, bool[,] tabela, List<Figura> neprijatelj, List<Figura> groblje)
        {
            Console.WriteLine("Unesite koordinate figure koje zelite da pomerite, prvo X pa Y");



            bool konjina = true;
            int x = 0;
            int y = 0;
            int clan = 0;
            
            do
            {
                try
                {
                    Console.Write("x: ");
                    
                    x = int.Parse(Console.ReadLine());
                    Console.Write("\ny: ");
                    y = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Molimo unesite adekvatne brojeve figura");
                }
                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].Xpos == x && lista[i].Ypos == y)
                    {
                        konjina = false;
                        clan = i;
                        break;
                    }
                }
                if (konjina)
                    Console.WriteLine("Molimo izaberite adekvatnu figuricu");
            } while (konjina);
            /////////////////////////////////////////////

            konjina = true;



            do
            {
                do
                {
                    try
                    {
                        Console.WriteLine("Unesite koordinate gde zelite da pomerite figuru, prvo x pa y");
                        Console.WriteLine();
                        Console.Write("x: ");
                        x = int.Parse(Console.ReadLine());
                        Console.Write("\ny: ");
                        y = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Molimo unesite brojeve");
                    }

                    if (MinMax(x) && MinMax(y))
                        break;
                    else
                        Console.WriteLine("Molimo unesite ispravne brojeve od 0 do 7");



                }
                while (true);


                ProveraFigura(ref x, ref y, lista, prijGroblje, ref konjina, clan, neprijatelj, groblje);
            }
            while (konjina);


            tabela[lista[clan].Ypos, lista[clan].Xpos] = false;
            lista[clan].Xpos = x;
            lista[clan].Ypos = y;
            //////////////////////
            if ((string)lista[clan].Tip == "Piun" && prijGroblje.Count>0)
            {
                if ((lista[clan].Ypos == 0 && lista[clan].pozicija == true) || (lista[clan].Ypos == 7 && lista[clan].pozicija == false))
                {
                    bool? provera3 = null;
                    Console.WriteLine("Izaberite figuru iz groblja kojom zelite da zamenite pijuna:");
                    foreach (Figura fig in prijGroblje)
                    {
                        Console.WriteLine(fig.Tip);
                    }
                    Console.WriteLine();
                    while (provera3 == null)
                    {
                        Console.Write("Unos:");
                        string unos = Console.ReadLine();
                        foreach (Figura fig in prijGroblje)
                        {
                            if ((string)fig.Tip.ToLower() == unos.ToLower())
                            {
                                provera3 = true;
                                fig.Xpos = lista[clan].Xpos;
                                fig.Ypos = lista[clan].Ypos;

                                Figura kanta = lista[clan];
                               lista[clan] = fig;

                                prijGroblje.Remove(fig);
                                prijGroblje.Add(kanta);

                                break;
                            }
                        }
                        if (provera3 == null)
                        {
                            Console.WriteLine("Nisto dobro uneli podatak, pokusajte ponovo");
                        }
                    }


                }
            }
          
            //////////////////////
            tabela[lista[clan].Ypos, lista[clan].Xpos] = true;
           
        }

        public static bool MinMax(int x)
        {
            if (x > 7 || x < 0)
                return false;
            else
                return true;
        }

        public static void ProveraFigura(ref int x, ref int y, List<Figura> figure, List<Figura> prijGroblje,ref bool provera, int clan, List<Figura> neprijatelj, List<Figura> groblje)
        {
            bool top = false;
            bool? jedi = null;
            bool test = true;
           for(int i = 0; i<figure.Count; i++)
            {
                if (i != clan && x == figure[i].Xpos && y == figure[i].Ypos)
                {
                    test = false;
                    provera = true;
                    Console.WriteLine("Molimo unesite adekvatan unos, tu se nalazi vec vasa figura");
                }
               // 
            }
            while (test) {
                switch (figure[clan].Tip)
                {
                    case "Top":
                        bool postoji = false;
                        if ((y == figure[clan].Ypos && (x > figure[clan].Xpos || x < figure[clan].Xpos)) || (x == figure[clan].Xpos && (y > figure[clan].Ypos || y < figure[clan].Ypos)))
                        {
                            if (((x <= 7) && (x != figure[clan].Xpos)) && figure[clan].Ypos == y) //levo desno
                            {
                                if (x > figure[clan].Xpos) //Za ako idemo u desno
                                {
                                    for (int i = figure[clan].Xpos; i <= x; i++)
                                    {
                                        for (int j = 0; j < figure.Count; j++)
                                        {
                                            if (j != clan && figure[j].Ypos == y && figure[j].Xpos == i)
                                            {
                                                postoji = true;
                                                Console.WriteLine("Tvoja figura desno smeta");
                                            }

                                        }
                                    }
                                }
                                if (x < figure[clan].Xpos)   //Za levo
                                {
                                    for (int i = figure[clan].Xpos; i >= x; i--)
                                    {
                                        for (int j = 0; j < figure.Count; j++)
                                        {
                                            if (j != clan && figure[j].Xpos == i && figure[j].Ypos == y)
                                            {
                                                postoji = true;
                                                Console.WriteLine("Tvoja figura levo smeta");

                                            }

                                        }
                                    }
                                }


                            }
                            else
                            if (x == figure[clan].Xpos && ((y <= 7) && (y != figure[clan].Ypos))) //gore dole
                            {
                                if (y > figure[clan].Ypos) //dole
                                {
                                    for (int i = figure[clan].Ypos + 1; i <= y; i++)
                                        for (int j = 0; j < figure.Count; j++)
                                        {
                                            if (j != clan && figure[j].Ypos == i && figure[j].Xpos == x)
                                            {
                                                postoji = true;
                                                Console.WriteLine("Tvoja figura ispod smeta");
                                            }
                                        }
                                }
                                else
                                    if (y < figure[clan].Ypos) //gore
                                {
                                    for (int i = figure[clan].Ypos; i >= y; i--)
                                        for (int j = 0; j < figure.Count; j++)
                                        {
                                            if (j != clan && figure[j].Xpos == x && figure[j].Ypos == i)
                                            {
                                                postoji = true;
                                                Console.WriteLine("Tvoja figura iznad smeta");
                                            }

                                        }
                                }
                            }

                            if (postoji)
                            {
                                test = false;
                                provera = true;
                                Console.WriteLine("Pokusaj ponovo");
                            }

                            else
                            {
                                provera = false;
                                test = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Molimo unesite adekvatan pomeraj za Topa");
                            test = false;
                        }

                        break;


                    case "Konj":

                        if (figure[clan].Ypos - 2 == y && figure[clan].Xpos + 1 == x)
                        {
                            test = false;
                            provera = false;
                        }
                        else
                           if (figure[clan].Ypos - 2 == y && figure[clan].Xpos - 1 == x)
                        {
                            test = false;
                            provera = false;
                        }
                        else
                           if (figure[clan].Ypos + 2 == y && figure[clan].Xpos + 1 == x)
                        {
                            test = false;
                            provera = false;
                        }
                        if (figure[clan].Ypos + 2 == y && figure[clan].Xpos - 1 == x)
                        {
                            test = false;
                            provera = false;
                        }
                        else
                            if (figure[clan].Xpos + 2 == x && figure[clan].Ypos + 1 == y)
                        {
                            test = false;
                            provera = false;
                        }
                        else
                            if (figure[clan].Xpos + 2 == x && figure[clan].Ypos - 1 == y)
                        {
                            test = false;
                            provera = false;
                        }

                        else
                            if (figure[clan].Xpos - 2 == x && figure[clan].Ypos + 1 == y)
                        {
                            test = false;
                            provera = false;
                        }
                        else
                            if (figure[clan].Xpos - 2 == x && figure[clan].Ypos - 1 == y)
                        {
                            test = false;
                            provera = false;
                        }
                        else
                        {
                            test = false;
                            Console.WriteLine("Molimo unesite adekvatan pomeraj konja");
                        }
                        break;

                    case "Lovac":
                        bool? lovim = null;
                        if ((x == figure[clan].Xpos && y != figure[clan].Ypos) || (y == figure[clan].Ypos && x != figure[clan].Xpos)) //Kod kraljice, ovo ce biti provera da li je top pomeraj
                        {
                            lovim = false;
                           
                        }
                        else
                        {

                            if ((((x - figure[clan].Xpos) > 0) && (figure[clan].Ypos - y)>0) && ((x - figure[clan].Xpos) == (figure[clan].Ypos - y))) //gore desno
                            {
                                lovim = true;
                                for (int i = 1; figure[clan].Xpos + i <= 7 && figure[clan].Ypos - i >= 0; i++)
                                    if (figure[clan].Xpos + i == x && figure[clan].Ypos - i == y)
                                    {
                                        int k = figure[clan].Xpos + 1;
                                        int g = figure[clan].Ypos - 1;
                                        while (k <= x && g >= y)
                                        {
                                            for (int a = 0; a < figure.Count; a++)
                                            {
                                                if (a != clan && figure[a].Xpos == k && figure[a].Ypos == g)
                                                {
                                                    lovim = false;
                                                }
                                            }
                                            k++;
                                            g--;
                                        }
                                    }
                            }
                            else
                           if ( (((x - figure[clan].Xpos)>0) && ((y - figure[clan].Ypos)>0)) && ((x - figure[clan].Xpos) == (y - figure[clan].Ypos))) //dole desno
                            {
                                lovim = true;

                                for (int i = 1; figure[clan].Xpos + i <= 7 && figure[clan].Ypos + i <= 7; i++)
                                {
                                    if (figure[clan].Xpos + i == x && figure[clan].Ypos + i == y)
                                    {
                                        int k = figure[clan].Xpos + 1;
                                        int g = figure[clan].Ypos + 1;
                                        while (k <= x && g <= y)
                                        {
                                            for (int a = 0; a < figure.Count; a++)
                                            {
                                                if (a != clan && figure[a].Xpos == k && figure[a].Ypos == g)
                                                {
                                                    lovim = false;
                                                }
                                            }
                                            k++;
                                            g++;
                                        }



                                    }
                                }

                            }
                            else
                         if ( ( ((figure[clan].Xpos - x)>0) && ((figure[clan].Ypos - y)>0) )  && ((figure[clan].Xpos - x) == (figure[clan].Ypos - y))) //levo gore
                            {
                                lovim = true;
                                /* for (int i = 1; figure[clan].Xpos - i >= 0 && figure[clan].Ypos - i >= 0; i++)
                                 {
                                     if (figure[clan].Xpos - i == x && figure[clan].Ypos - i == y)
                                     {
                                         int k = figure[clan].Xpos - 1;
                                         int g = figure[clan].Ypos - 1;
                                         while (k >= x && g <= y)
                                         {
                                             for (int a = 0; a < figure.Count; a++)
                                             {
                                                 if (a != clan && figure[a].Xpos == k && figure[a].Ypos == g)
                                                 {
                                                     lovim = false;
                                                 }
                                             }
                                             k--;
                                             g--;
                                         }
                                     }
                                 }*/
                                int k = figure[clan].Xpos - 1;
                                int g = figure[clan].Ypos - 1;
                                do
                                {
                                    bool samotu = false;
                                    for (int i = 0; i < figure.Count; i++)
                                    {
                                        if (i != clan && figure[i].Xpos == k && figure[i].Ypos == g)
                                        {
                                            samotu = true;
                                            lovim = false;
                                            break;
                                        }
                                    }
                                    if (samotu)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        k--;
                                        g--;
                                    }
                                } while (k != x && g != y);
                            }
                            else
                            {
                                if (( ((figure[clan].Xpos - x)>0) && ((y - figure[clan].Ypos)>0) ) && ((figure[clan].Xpos - x) == (y - figure[clan].Ypos))) //levo dole
                                {
                                    lovim = true;
                                    for (int i = 1; figure[clan].Xpos - i >= 0 && figure[clan].Ypos + i <= 7; i++)
                                        if (figure[clan].Xpos - i == x && figure[clan].Ypos + i == y)
                                        {
                                            int k = figure[clan].Xpos - 1;
                                            int g = figure[clan].Ypos + 1;
                                            while (k <= x && g <= y)
                                            {
                                                for (int a = 0; a < figure.Count; a++)
                                                {
                                                    if (a != clan && figure[a].Xpos == k && figure[a].Ypos == g)
                                                    {
                                                        lovim = false;
                                                    }
                                                }
                                                k--;
                                                g++;
                                            }
                                        }
                                }
                                else
                                {
                                    
                                    Console.WriteLine("Molimo unesite adekvatan pomeraj za lovca");
                                    test = false;
                                }

                            }
                        }
                        if (lovim == true)
                        {
                            provera = false;
                            test = false;
                        }
                        else
                        {
                            Console.WriteLine("Greska!");
                            test = false;
                        }
                        break;

                    case "Kralj":
                        if (x > (figure[clan].Xpos + 1) || x < (figure[clan].Xpos - 1))
                        {
                            test = false;
                            provera = true;
                            Console.WriteLine("Molimo unesite adekvatna polja za Kralja");
                        }

                        else
                                if (y > (figure[clan].Ypos + 1) || y < (figure[clan].Ypos - 1))
                        {
                            Console.WriteLine("Molimo unesite adekvatna polja za Kralja");
                            test = false;
                            provera = true;
                        }

                       
                        break;

                    case "Kica":
                        bool? lovim2 = null;
                        if ((x == figure[clan].Xpos && y != figure[clan].Ypos) || (y == figure[clan].Ypos && x != figure[clan].Xpos)) //Kod kraljice, ovo ce biti provera da li je top pomeraj
                        {

                            top = true;
                            bool postoji2 = false;
                            if ((y == figure[clan].Ypos && (x > figure[clan].Xpos || x < figure[clan].Xpos)) || (x == figure[clan].Xpos && (y > figure[clan].Ypos || y < figure[clan].Ypos)))
                            {
                                if (((x <= 7) && (x != figure[clan].Xpos)) && figure[clan].Ypos == y) //levo desno
                                {
                                    if (x > figure[clan].Xpos) //Za ako idemo u desno
                                    {
                                        for (int i = figure[clan].Xpos; i <= x; i++)
                                        {
                                            for (int j = 0; j < figure.Count; j++)
                                            {
                                                if (j != clan && figure[j].Ypos == y && figure[j].Xpos == i)
                                                {
                                                    postoji2 = true;
                                                   // Console.WriteLine("Tvoja figura desno smeta");
                                                }

                                            }
                                        }
                                    }
                                    if (x < figure[clan].Xpos)   //Za levo
                                    {
                                        for (int i = figure[clan].Xpos; i >= x; i--)
                                        {
                                            for (int j = 0; j < figure.Count; j++)
                                            {
                                                if (j != clan && figure[j].Xpos == i && figure[j].Ypos == y)
                                                {
                                                    postoji2 = true;
                                                   // Console.WriteLine("Tvoja figura levo smeta");

                                                }

                                            }
                                        }
                                    }


                                }
                                else
                                if (x == figure[clan].Xpos && ((y <= 7) && (y != figure[clan].Ypos))) //gore dole
                                {
                                    if (y > figure[clan].Ypos) //dole
                                    {
                                        for (int i = figure[clan].Ypos + 1; i <= y; i++)
                                            for (int j = 0; j < figure.Count; j++)
                                            {
                                                if (j != clan && figure[j].Ypos == i && figure[j].Xpos == x)
                                                {
                                                    postoji2 = true;
                                                  //  Console.WriteLine("Tvoja figura ispod smeta");
                                                }
                                            }
                                    }
                                    else
                                        if (y < figure[clan].Ypos) //gore
                                    {
                                        for (int i = figure[clan].Ypos; i >= y; i--)
                                            for (int j = 0; j < figure.Count; j++)
                                            {
                                                if (j != clan && figure[j].Xpos == x && figure[j].Ypos == i)
                                                {
                                                    postoji2 = true; //Ovde
                                                  //  Console.WriteLine("Tvoja figura iznad smeta");
                                                }

                                            }
                                    }
                                }

                                if (postoji2)
                                {
                                    test = false;
                                    provera = true;
                                    Console.WriteLine("Pokusaj ponovo");
                                }
                                else
                                {
                                    provera = false;

                                    test = false;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Molimo unesite adekvatan pomeraj za Kraljicu");
                                test = false;
                            }


                        }
                        else
                        {
                           
                                if ((((x - figure[clan].Xpos) > 0) && (figure[clan].Ypos - y) > 0) && ((x - figure[clan].Xpos) == (figure[clan].Ypos - y))) //gore desno
                            {
                                lovim2 = true;
                                    for (int i = 1; figure[clan].Xpos + i <= 7 && figure[clan].Ypos - i >= 0; i++)
                                        if (figure[clan].Xpos + i == x && figure[clan].Ypos - i == y)
                                        {
                                            int k = figure[clan].Xpos + 1;
                                            int g = figure[clan].Ypos - 1;
                                            while (k <= x && g >= y)
                                            {
                                                for (int a = 0; a < figure.Count; a++)
                                                {
                                                    if (a != clan && figure[a].Xpos == k && figure[a].Ypos == g)
                                                    {
                                                        lovim2 = false;
                                                    }
                                                }
                                                k++;
                                                g--;
                                            }
                                        }
                                }
                                else
                             if ((((x - figure[clan].Xpos) > 0) && ((y - figure[clan].Ypos) > 0)) && ((x - figure[clan].Xpos) == (y - figure[clan].Ypos))) //dole desno
                            {
                                lovim2 = true;

                                for (int i = 1; figure[clan].Xpos + i <= 7 && figure[clan].Ypos + i <= 7; i++)
                                    {
                                        if (figure[clan].Xpos + i == x && figure[clan].Ypos + i == y)
                                        {
                                            int k = figure[clan].Xpos + 1;
                                            int g = figure[clan].Ypos + 1;
                                            while (k <= x && g <= y)
                                            {
                                                for (int a = 0; a < figure.Count; a++)
                                                {
                                                    if (a != clan && figure[a].Xpos == k && figure[a].Ypos == g)
                                                    {
                                                        lovim2 = false;
                                                    }
                                                }
                                                k++;
                                                g++;
                                            }



                                        }
                                    }

                                }
                                else
                            if ((((figure[clan].Xpos - x) > 0) && ((figure[clan].Ypos - y) > 0)) && ((figure[clan].Xpos - x) == (figure[clan].Ypos - y))) //levo gore
                            {
                                lovim2 = true;
                                /* for (int i = 1; figure[clan].Xpos - i >= 0 && figure[clan].Ypos - i >= 0; i++)
                                     {
                                         if (figure[clan].Xpos - i == x && figure[clan].Ypos - i == y)
                                         {
                                             int k = figure[clan].Xpos - 1;
                                             int g = figure[clan].Ypos - 1;
                                             while (k >= x && g <= y)
                                             {
                                                 for (int a = 0; a < figure.Count; a++)
                                                 {
                                                     if (a != clan && figure[a].Xpos == k && figure[a].Ypos == g)
                                                     {
                                                         lovim2 = false;
                                                     }
                                                 }
                                                 k--;
                                                 g--;
                                             }
                                         }
                                 }*/


                                int k = figure[clan].Xpos - 1;
                                int g = figure[clan].Ypos - 1;
                                do
                                {
                                    bool samotu = false;
                                    for(int i = 0; i<figure.Count;i++)
                                    {
                                        if(i!=clan && figure[i].Xpos == k && figure[i].Ypos==g)
                                        {
                                            samotu = true;
                                            lovim2 = false;
                                            break;
                                        }
                                    }
                                    if(samotu)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        k--;
                                        g--;
                                    }
                                } while (k != x && g != y);

                            }
                                else
                                {
                                    if ((((figure[clan].Xpos - x) > 0) && ((y - figure[clan].Ypos) > 0)) && ((figure[clan].Xpos - x) == (y - figure[clan].Ypos))) //levo dole
                                     {
                                    lovim2 = true;
                                     for (int i = 1; figure[clan].Xpos - i >= 0 && figure[clan].Ypos + i <= 7; i++)
                                         if (figure[clan].Xpos - i == x && figure[clan].Ypos + i == y)
                                         {
                                             int k = figure[clan].Xpos - 1;
                                             int g = figure[clan].Ypos + 1;
                                             while (k <= x && g <= y)
                                             {
                                                 for (int a = 0; a < figure.Count; a++)
                                                 {
                                                     if (a != clan && figure[a].Xpos == k && figure[a].Ypos == g)
                                                     {
                                                         lovim2 = false;
                                                     }
                                                 }
                                                 k--;
                                                 g++;
                                             }
                                         }

                                  
                                }
                                else
                                    {
                                        Console.WriteLine("Molimo unesite adekvatan pomeraj za kraljicu");
                                        test = false;
                                    }

                                }
                            
                           
                        }

                        if (lovim2 == true)
                        {
                            provera = false;
                            test = false;
                        }
                        else
                        {
                            Console.WriteLine("Kraljica tu ne moze da prodje");
                            test = false;
                        }
                        break;

                       

                    case "Piun":
                        bool aktiviran = false;
                        if (y == ( figure[clan].Ypos -2) && x == figure[clan].Xpos && figure[clan].pozicija == true && figure[clan].indikator == 0)
                        {
                            bool neprijatno = false;
                            for(int i = 0; i<neprijatelj.Count;i++)
                            {
                                if ((neprijatelj[i].Ypos == (figure[clan].Ypos - 1) && neprijatelj[i].Xpos == figure[clan].Xpos) || (neprijatelj[i].Ypos == (figure[clan].Ypos - 2) && neprijatelj[i].Xpos == figure[clan].Xpos))
                                    neprijatno = true;
                            }
                            for(int i = 0; i<figure.Count;i++)
                            {
                                if (i != clan && figure[clan].Xpos == figure[i].Xpos && figure[i].Ypos == (figure[clan].Ypos - 1))
                                    neprijatno = true;
                            }
                            if(!neprijatno)
                            {
                                test = false;
                                aktiviran = true;
                                provera = false;
                                figure[clan].indikator = 1;
                            }
                            else
                            {
                                aktiviran = false;
                                Console.WriteLine("Smeta figura ispred");
                            }
                            
                        }
                        else
                            if (y == (figure[clan].Ypos - 1) && x == figure[clan].Xpos && figure[clan].pozicija == true )
                        {
                            bool neprijatno=false;
                            for (int i = 0; i<neprijatelj.Count; i++)
                            {
                                if (neprijatelj[i].Xpos == figure[clan].Xpos && neprijatelj[i].Ypos == (figure[clan].Ypos - 1))
                                    neprijatno = true;
                            }
                            if(!neprijatno)
                            {
                                test = false;
                                provera = false;
                                aktiviran = true;
                                figure[clan].indikator = 1;
                            }
                            else
                            {
                                aktiviran = false;
                                Console.WriteLine("Figura ispred smeta");
                            }
                            
                        }
                        else
                            if( y ==(figure[clan].Ypos+1) && x == figure[clan].Xpos && figure[clan].pozicija == false )
                        {
                            bool neprijatno = false;
                            for(int i = 0; i<neprijatelj.Count;i++)
                            {
                                if (neprijatelj[i].Xpos == figure[clan].Xpos && neprijatelj[i].Ypos == (figure[clan].Ypos + 1))
                                    neprijatno = true;
                            }
                            if(!neprijatno)
                            {
                                test = false;
                                aktiviran = true;
                                provera = false;
                                figure[clan].indikator = 1;
                            }
                            else
                            {
                                aktiviran = false;
                                Console.WriteLine("Figura ispod smeta");
                            }
                            
                        }
                        else
                         if(y == (2 + figure[clan].Ypos) && x == figure[clan].Xpos && figure[clan].pozicija == false && figure[clan].indikator == 0 )
                         {

                            bool neprijatno = false;
                            for (int i = 0; i < neprijatelj.Count; i++)
                            {
                                if (((neprijatelj[i].Ypos == (figure[clan].Ypos + 1) && neprijatelj[i].Xpos == figure[clan].Xpos)) || ((neprijatelj[i].Ypos == (figure[clan].Ypos + 2) && neprijatelj[i].Xpos == figure[clan].Xpos)))
                                    neprijatno = true;
                            }
                            for (int i = 0; i < figure.Count; i++)
                            {
                                if (i != clan && figure[clan].Xpos == figure[i].Xpos && figure[i].Ypos == (figure[clan].Ypos + 1))
                                    neprijatno = true;
                            }
                            if(!neprijatno)
                            {
                                test = false;
                                aktiviran = true;
                                provera = false;
                                figure[clan].indikator = 1;
                            }
                            else
                            {
                                aktiviran = false;
                                Console.WriteLine("Figura ispod smeta");
                            }
                            
                         } 
                        else
                        {
                            if (x == (figure[clan].Xpos + 1) && y == (figure[clan].Ypos - 1) && figure[clan].pozicija == true)
                            {
                                for (int i = 0; i < neprijatelj.Count; i++)
                                {
                                    if (neprijatelj[i].Xpos == x && neprijatelj[i].Ypos == y)
                                    {
                                        aktiviran = true;
                                        figure[clan].indikator = 1;
                                        groblje.Add(neprijatelj[i]);
                                        neprijatelj.Remove(neprijatelj[i]);
                                        provera = false;
                                        test = false;
                                        break;
                                    }
                                }

                            }
                            else
                            if (x == (figure[clan].Xpos - 1) && y == (figure[clan].Ypos - 1) && figure[clan].pozicija == true)
                            {
                                for (int i = 0; i < neprijatelj.Count; i++)
                                {
                                    if (neprijatelj[i].Xpos == x && neprijatelj[i].Ypos == y)
                                    {
                                        aktiviran = true;
                                        figure[clan].indikator = 1;
                                        groblje.Add(neprijatelj[i]);
                                        neprijatelj.Remove(neprijatelj[i]);
                                        provera = false;
                                        test = false;
                                        break;

                                    }
                                }
                            }
                            else
                            if (x == (figure[clan].Xpos + 1) && y == (figure[clan].Ypos + 1) && figure[clan].pozicija == false)
                            {
                                for (int i = 0; i < neprijatelj.Count; i++)
                                {
                                    if (neprijatelj[i].Xpos == x && neprijatelj[i].Ypos == y)
                                    {
                                        aktiviran = true;
                                        groblje.Add(neprijatelj[i]);
                                        neprijatelj.Remove(neprijatelj[i]);
                                        figure[clan].indikator = 1;
                                        provera = false;
                                        test = false;
                                        break;
                                    }
                                }
                            }
                            else
                            if (x == (figure[clan].Xpos - 1) && y == (figure[clan].Ypos + 1) && figure[clan].pozicija == false)
                            {
                                for(int i = 0; i<neprijatelj.Count;i++)
                                {
                                    if(neprijatelj[i].Xpos == x && neprijatelj[i].Ypos == y)
                                    {
                                        aktiviran = true;
                                        groblje.Add(neprijatelj[i]);
                                        neprijatelj.Remove(neprijatelj[i]);
                                        figure[clan].indikator = 1;
                                        provera = false;
                                        test = false;
                                        break;
                                    }
                                }
                            } 
                            else
                            {
                                provera = true;
                                test = false;
                                aktiviran = true;
                                Console.WriteLine("Molimo unesite adekvatan unos za pijuna");
                            }

                        }
                        if(!aktiviran)
                        {
                            test = false;
                            provera = true;
                            Console.WriteLine("Molimo unesite adekvatan unos za pijuna");
                        }

                        break;
                }
            }
            
            if (!provera)
                while(jedi==null)
                {

                    switch(figure[clan].Tip)
                    {
                        case "Top":
                            bool postoji = false;
                            if (x > figure[clan].Xpos && y == figure[clan].Ypos) //Za desno
                            {
                                int promX = figure[clan].Xpos + 1;
                                while (promX <= x)
                                {
                                    for (int i = 0; i < neprijatelj.Count; i++)
                                    {
                                        if (neprijatelj[i].Xpos == promX && neprijatelj[i].Ypos == y)
                                        {
                                            groblje.Add(neprijatelj[i]);
                                            neprijatelj.Remove(neprijatelj[i]);
                                            postoji = true;
                                            x = promX;
                                            break;
                                        }
                                    }
                                        if (postoji)
                                        {
                                            jedi = true;
                                            break;
                                        }
                                        else
                                            promX++;
                                    
                                }
                            }
                            else
                                if (x < figure[clan].Xpos && y == figure[clan].Ypos) //Za levo
                            {

                                int promX = figure[clan].Xpos - 1;
                                while (promX >= x)
                                {
                                    for (int i = 0; i < neprijatelj.Count; i++)
                                    {
                                        if (neprijatelj[i].Xpos == promX && neprijatelj[i].Ypos == y)
                                        {
                                            groblje.Add(neprijatelj[i]);
                                            neprijatelj.Remove(neprijatelj[i]);
                                            postoji = true;
                                            x = promX;
                                            break;
                                        }
                                    }
                                        if (postoji)
                                        {
                                            jedi = true;
                                            break;
                                        }
                                        else
                                            promX--;
                                    
                                }


                            }
                            else
                            if (x == figure[clan].Xpos && y > figure[clan].Ypos) //dole
                            {
                                int promY = figure[clan].Ypos + 1;
                                while (promY <= y)
                                {
                                    for (int i = 0; i < neprijatelj.Count; i++)
                                    {
                                        if (neprijatelj[i].Ypos == promY && neprijatelj[i].Xpos == x)
                                        {
                                            groblje.Add(neprijatelj[i]);
                                            neprijatelj.Remove(neprijatelj[i]);
                                            postoji = true;
                                            y = promY;
                                            break;
                                        }
                                    }
                                        if (postoji)
                                        {
                                            jedi = true;
                                            break;
                                        }
                                            
                                        else
                                            promY++;
                                    
                                }
                            }
                            else
                            if (x == figure[clan].Xpos && y < figure[clan].Ypos)//gore
                            {
                                int promY = figure[clan].Ypos - 1;
                                while (promY >= y)
                                {
                                    for (int i = 0; i < neprijatelj.Count; i++)
                                    {
                                        if (neprijatelj[i].Ypos == promY && neprijatelj[i].Xpos == x)
                                        {
                                            groblje.Add(neprijatelj[i]);
                                            neprijatelj.Remove(neprijatelj[i]); 
                                            postoji = true;
                                            y = promY;
                                            break;
                                        }
                                    }
                                        if (postoji)
                                        {
                                            jedi = true;
                                            break;
                                        }
                                            
                                        else
                                            promY--;
                                   
                                }
                            }
                            else
                                jedi = false;
                                break;
                        case "Konj": 
                            foreach(Figura fig in neprijatelj)
                            {
                                if(fig.Xpos == x && fig.Ypos == y)
                                {
                                    groblje.Add(fig);
                                    neprijatelj.Remove(fig);
                                    jedi = true;
                                    break;
                                }
                                else
                                {
                                    jedi = false;
                                }    
                            }
                                break;
                        case "Lovac":

                            bool postojii = false;
                           
                            if (x > figure[clan].Xpos && y > figure[clan].Ypos) //desno dole
                            {
                                int posX = figure[clan].Xpos + 1;
                                int posY = figure[clan].Ypos + 1;
                                while (posX <= x && posY <= y)
                                {
                                    for (int i = 0; i < neprijatelj.Count; i++)
                                    {
                                        if (neprijatelj[i].Xpos == posX && neprijatelj[i].Ypos == posY)
                                        {
                                            groblje.Add(neprijatelj[i]);
                                            neprijatelj.Remove(neprijatelj[i]);
                                            x = posX;
                                            y = posY;
                                            postojii = true;
                                            break;
                                        }
                                    }
                                    if (postojii)
                                    {
                                        jedi = true;
                                        break;
                                    }
                                    else
                                    {
                                        posY++;
                                        posX++;
                                    }


                                }

                            }
                            else
                                if (x > figure[clan].Xpos && y < figure[clan].Ypos) //desno gore
                            {

                                int posX = figure[clan].Xpos + 1;
                                int posY = figure[clan].Ypos - 1;
                                while (posX <= x && posY >= y)
                                {
                                    for (int i = 0; i < neprijatelj.Count; i++)
                                    {
                                        if (neprijatelj[i].Xpos == posX && neprijatelj[i].Ypos == posY)
                                        {
                                            groblje.Add(neprijatelj[i]);
                                            neprijatelj.Remove(neprijatelj[i]);
                                            x = posX;
                                            y = posY;
                                            postojii = true;
                                            break;
                                        }
                                    }
                                    if (postojii)
                                    {
                                        jedi = true;
                                        break;
                                    }
                                    else
                                    {
                                        posY--;
                                        posX++;
                                    }


                                }

                            }
                            else
                                if (x < figure[clan].Xpos && y > figure[clan].Ypos) //levo dole
                            {
                                int posX = figure[clan].Xpos - 1;
                                int posY = figure[clan].Ypos + 1;
                                while (posX >= x && posY <= y)
                                {
                                    for (int i = 0; i < neprijatelj.Count; i++)
                                    {
                                        if (neprijatelj[i].Xpos == posX && neprijatelj[i].Ypos == posY)
                                        {
                                            groblje.Add(neprijatelj[i]);
                                            neprijatelj.Remove(neprijatelj[i]);
                                            x = posX;
                                            y = posY;
                                            postojii = true;
                                            break;
                                        }
                                    }
                                    if (postojii)
                                    {
                                        jedi = true;
                                        break;
                                    }
                                    else
                                    {
                                        posY++;
                                        posX--;
                                    }


                                }
                            }
                            else
                            if (x < figure[clan].Xpos && y < figure[clan].Ypos) //levo gore
                            {
                                int posX = figure[clan].Xpos - 1;
                                int posY = figure[clan].Ypos - 1;
                                while (posX >= x && posY >= y)
                                {
                                    for (int i = 0; i < neprijatelj.Count; i++)
                                    {
                                        if (neprijatelj[i].Xpos == posX && neprijatelj[i].Ypos == posY)
                                        {
                                            groblje.Add(neprijatelj[i]);
                                            neprijatelj.Remove(neprijatelj[i]);
                                            x = posX;
                                            y = posY;
                                            postojii = true;
                                            break;
                                        }
                                    }
                                    if (postojii)
                                    {
                                        jedi = true;
                                        break;
                                    }
                                    else
                                    {
                                        posY--;
                                        posX--;
                                    }


                                }
                            }
                            else
                            {
                                jedi = false;
                            }

                            if (postojii)
                            {
                                jedi = true;
                            }
                            else
                                jedi = false;


                    
                    break;

                        case "Kralj":
                            jedi = false;
                                    for(int i = 0; i<neprijatelj.Count;i++)
                            {
                                if(neprijatelj[i].Xpos == x && neprijatelj[i].Ypos==y)
                                {
                                    groblje.Add(neprijatelj[i]);
                                    neprijatelj.Remove(neprijatelj[i]);
                                    jedi = true;
                                    break;
                                }    
                            }
                            
                                break;
                        case "Kica":
                            
                            if(top)
                            {
                                bool postoji2 = false;
                                if (x > figure[clan].Xpos && y == figure[clan].Ypos) //Za desno
                                {
                                    int promX = figure[clan].Xpos + 1;
                                    while (promX <= x)
                                    {
                                        for (int i = 0; i < neprijatelj.Count; i++)
                                        {
                                            if (neprijatelj[i].Xpos == promX && neprijatelj[i].Ypos == y)
                                            {
                                                groblje.Add(neprijatelj[i]);
                                                neprijatelj.Remove(neprijatelj[i]);
                                                postoji2 = true;
                                                x = promX;
                                                break;
                                            }
                                        }
                                        if (postoji2)
                                        {
                                            jedi = true;
                                            break;
                                        }
                                        else
                                            promX++;

                                    }
                                }
                                else
                                    if (x < figure[clan].Xpos && y == figure[clan].Ypos) //Za levo
                                {

                                    int promX = figure[clan].Xpos - 1;
                                    while (promX >= x)
                                    {
                                        for (int i = 0; i < neprijatelj.Count; i++)
                                        {
                                            if (neprijatelj[i].Xpos == promX && neprijatelj[i].Ypos == y)
                                            {
                                                groblje.Add(neprijatelj[i]);
                                                neprijatelj.Remove(neprijatelj[i]);
                                                postoji2 = true;
                                                x = promX;
                                                break;
                                            }
                                        }
                                        if (postoji2)
                                        {
                                            jedi = true;
                                            break;
                                        }
                                        else
                                            promX--;

                                    }


                                }
                                else
                                if (x == figure[clan].Xpos && y > figure[clan].Ypos) //dole
                                {
                                    int promY = figure[clan].Ypos + 1;
                                    while (promY <= y)
                                    {
                                        for (int i = 0; i < neprijatelj.Count; i++)
                                        {
                                            if (neprijatelj[i].Ypos == promY && neprijatelj[i].Xpos == x)
                                            {
                                                groblje.Add(neprijatelj[i]);
                                                neprijatelj.Remove(neprijatelj[i]);
                                                postoji2 = true;
                                                y = promY;
                                                break;
                                            }
                                        }
                                        if (postoji2)
                                        {
                                            jedi = true;
                                            break;
                                        }

                                        else
                                            promY++;

                                    }
                                }
                                else
                                if (x == figure[clan].Xpos && y < figure[clan].Ypos)//gore
                                {
                                    int promY = figure[clan].Ypos - 1;
                                    while (promY >= y)
                                    {
                                        for (int i = 0; i < neprijatelj.Count; i++)
                                        {
                                            if (neprijatelj[i].Ypos == promY && neprijatelj[i].Xpos == x)
                                            {
                                                groblje.Add(neprijatelj[i]);
                                                neprijatelj.Remove(neprijatelj[i]);
                                                postoji2 = true;
                                                y = promY;
                                                break;
                                            }
                                        }
                                        if (postoji2)
                                        {
                                            jedi = true;
                                            break;
                                        }

                                        else
                                            promY--;

                                    }
                                }
                                if (postoji2)
                                {
                                    jedi = true;
                                }
                                else
                                    jedi = false;

                            }
                            else
                            {

                                bool postojii2 = false;
                                if (x > figure[clan].Xpos && y > figure[clan].Ypos) //desno dole
                                {
                                    int posX = figure[clan].Xpos + 1;
                                    int posY = figure[clan].Ypos + 1;
                                    while (posX <= x && posY <= y)
                                    {
                                        for (int i = 0; i < neprijatelj.Count; i++)
                                        {
                                            if (neprijatelj[i].Xpos == posX && neprijatelj[i].Ypos == posY)
                                            {
                                                groblje.Add(neprijatelj[i]);
                                                neprijatelj.Remove(neprijatelj[i]);
                                                x = posX;
                                                y = posY;
                                                postojii2 = true;
                                                break;
                                            }
                                        }
                                        if (postojii2)
                                        {
                                            jedi = true;
                                            break;
                                        }
                                        else
                                        {
                                            posY++;
                                            posX++;
                                        }


                                    }

                                }
                                else
                                    if (x > figure[clan].Xpos && y < figure[clan].Ypos) //desno gore
                                {

                                    int posX = figure[clan].Xpos + 1;
                                    int posY = figure[clan].Ypos - 1;
                                    while (posX <= x && posY >= y)
                                    {
                                        for (int i = 0; i < neprijatelj.Count; i++)
                                        {
                                            if (neprijatelj[i].Xpos == posX && neprijatelj[i].Ypos == posY)
                                            {
                                                groblje.Add(neprijatelj[i]);
                                                neprijatelj.Remove(neprijatelj[i]);
                                                x = posX;
                                                y = posY;
                                                postojii2 = true;
                                                break;
                                            }
                                        }
                                        if (postojii2)
                                        {
                                            jedi = true;
                                            break;
                                        }
                                        else
                                        {
                                            posY--;
                                            posX++;
                                        }


                                    }

                                }
                                else
                                    if (x < figure[clan].Xpos && y > figure[clan].Ypos) //levo dole
                                {
                                    int posX = figure[clan].Xpos - 1;
                                    int posY = figure[clan].Ypos + 1;
                                    while (posX >= x && posY <= y)
                                    {
                                        for (int i = 0; i < neprijatelj.Count; i++)
                                        {
                                            if (neprijatelj[i].Xpos == posX && neprijatelj[i].Ypos == posY)
                                            {
                                                groblje.Add(neprijatelj[i]);
                                                neprijatelj.Remove(neprijatelj[i]);
                                                x = posX;
                                                y = posY;
                                                postojii2 = true;
                                                break;
                                            }
                                        }
                                        if (postojii2)
                                        {
                                            jedi = true;
                                            break;
                                        }
                                        else
                                        {
                                            posY++;
                                            posX--;
                                        }


                                    }
                                }
                                else
                                if (x < figure[clan].Xpos && y < figure[clan].Ypos) //levo gore
                                {
                                    int posX = figure[clan].Xpos - 1;
                                    int posY = figure[clan].Ypos - 1;
                                    while (posX >= x && posY >= y)
                                    {
                                        for (int i = 0; i < neprijatelj.Count; i++)
                                        {
                                            if (neprijatelj[i].Xpos == posX && neprijatelj[i].Ypos == posY)
                                            {
                                                groblje.Add(neprijatelj[i]);
                                                neprijatelj.Remove(neprijatelj[i]);
                                                x = posX;
                                                y = posY;
                                                postojii2 = true;
                                                break;
                                            }
                                        }
                                        if (postojii2)
                                        {
                                            jedi = true;
                                            break;
                                        }
                                        else
                                        {
                                            posY--;
                                            posX--;
                                        }


                                    }
                                }
                                else
                                {
                                    jedi = false;
                                }

                                if (postojii2)
                                {
                                    jedi = true;
                                }
                                else
                                    jedi = false;


                            }
                                break;
                        case "Piun":
                            jedi = false;
                            break;
                        
                    }

                } 

        }

        public static void IspisTabele(bool[,] tabela, List<Figura> prijatelj, List<Figura> neprijatelj)
        {
            Console.Clear();
            Console.Write("                         ");
            for (int i = 0; i <= 7; i++)
                Console.Write("     " + i);
            Console.WriteLine();
            for (int i = 0; i<=7; i++)
            {
                Console.Write("                         ");
                Console.Write(i + " ");
                for (int j=0;j<=7;j++)
                {
                    switch(tabela[i,j])
                    {
                        case true:
                            int k = 0;
                            while(k<prijatelj.Count)
                            {
                                if(prijatelj[k].Xpos==j && prijatelj[k].Ypos==i)
                                {
                                   
                                   
                                    Console.Write("|"+ prijatelj[k].Tip.ToUpper() + " ");
                                }
                                ++k;
                            }
                            k = 0;
                            while (k < neprijatelj.Count)
                            {
                                if (neprijatelj[k].Xpos == j && neprijatelj[k].Ypos == i)
                                {
                                                                      
                                    Console.Write("|"+neprijatelj[k].Tip + " ");
                                }
                                ++k;
                            }

                            break;
                        case false:
                                                        
                            Console.Write("|  .  ");
                            break;

                    }
                   
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            
        }
        public static void PrijatGen(List<Figura> Prijatelj)
        {
            
              for (int i = 0; i < 8; i++)
              Prijatelj.Add(new Figura(i, 6, "Piun"));
            Prijatelj.Add(new Figura(0, 7, "Top")); //0 7
            Prijatelj.Add(new Figura(1, 7, "Konj"));
            Prijatelj.Add(new Figura(2, 7, "Lovac"));
            Prijatelj.Add(new Figura(3, 7, "Kica"));    
            Prijatelj.Add(new Figura(4, 7, "Kralj"));   
            Prijatelj.Add(new Figura(5, 7, "Lovac"));
            Prijatelj.Add(new Figura(6, 7, "Konj"));
            Prijatelj.Add(new Figura(7, 7, "Top"));
        }
        public static void NepriGen(List<Figura> Neprijatelj)
        {
           for (int i = 0; i < 8; i++)
                Neprijatelj.Add(new Figura(i, 1, "Piun",false));
            Neprijatelj.Add(new Figura(0, 0, "Top", false));
            Neprijatelj.Add(new Figura(1, 0, "Konj", false));
            Neprijatelj.Add(new Figura(2, 0, "Lovac", false));
            Neprijatelj.Add(new Figura(4, 0, "Kica", false)); //4 0
            Neprijatelj.Add(new Figura(3, 0, "Kralj", false)); //3 0
            Neprijatelj.Add(new Figura(5, 0, "Lovac", false));
            Neprijatelj.Add(new Figura(6, 0, "Konj", false));
            Neprijatelj.Add(new Figura(7, 0, "Top", false));
        }


        static void Main(string[] args)
        {          
            bool[,] Tabla = new bool[8, 8];
             for(int i = 0; i<=7; i++)
                 for(int j = 0; j<=7; j++)
                 {
                     Tabla[i, j] = false;
                 }

             List<Figura> Prijatelj = new List<Figura>();
             List<Figura> Neprijatelj = new List<Figura>();

            List<Figura> GrobljePrijatelj = new List<Figura>();
            List<Figura> GrobljeNeprijatelj = new List<Figura>();

            PrijatGen(Prijatelj);
            NepriGen(Neprijatelj);
            foreach (Figura figPri in Prijatelj)
            {
                Tabla[figPri.Ypos, figPri.Xpos] = true;
            }

            foreach (Figura figNep in Neprijatelj)
            {
                Tabla[figNep.Ypos, figNep.Xpos] = true;
            }
            
         

           


            bool provera = true;
            do
            {
                IspisTabele(Tabla, Prijatelj, Neprijatelj);

                foreach (Figura fig in GrobljeNeprijatelj)
                {
                    if (fig.Tip == "Kralj")
                    {
                        Console.WriteLine("Cestitano, Prijatelj je pobedio!!");
                        provera = false;
                        break;
                    }

                }
                foreach (Figura fig in GrobljePrijatelj)
                {
                    if (fig.Tip == "Kralj")
                    {
                        Console.WriteLine("Cestitamo, Neprijatelj je pobedio!");
                        provera = false;
                        break;
                    }
                }
                Console.WriteLine("Sada je prijatelj na potezu.");
                PomeriPrijatelj(Prijatelj, GrobljePrijatelj, Tabla, Neprijatelj, GrobljeNeprijatelj); //(Koga_pomerano, Groblje_Koga_pomeramo ,Tabla, Koga_jedemo, Groblje_Koga_jedeno)
                IspisTabele(Tabla, Prijatelj, Neprijatelj);
                Console.WriteLine("Sada je neprijatelj na potezu.");
                PomeriPrijatelj(Neprijatelj, GrobljeNeprijatelj, Tabla, Prijatelj, GrobljePrijatelj);

            } while (provera);



        }
    }
}

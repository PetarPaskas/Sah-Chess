using System;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using Microsoft.VisualBasic.CompilerServices;






















/*
 * 
 *  
                        else
 * 
 * 
 * */





















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
        public static void PomeriPrijatelj(List<Figura> lista, bool[,] tabela, List<Figura> neprijatelj, List<Figura> groblje)
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
                if(konjina)
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


                 ProveraFigura(ref x, ref y, lista, ref konjina, clan, neprijatelj, groblje);
            }
            while (konjina);
           

            tabela[lista[clan].Ypos, lista[clan].Xpos] = false;
            lista[clan].Xpos = x;
            lista[clan].Ypos = y;
            tabela[lista[clan].Ypos, lista[clan].Xpos] = true;
           
        }

        public static bool MinMax(int x)
        {
            if (x > 7 || x < 0)
                return false;
            else
                return true;
        }

        public static void ProveraFigura(ref int x, ref int y, List<Figura> figure, ref bool provera, int clan, List<Figura> neprijatelj, List<Figura> groblje)
        {
            bool pomeren = false;
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
                            Console.WriteLine("Molimo unesite adekvatan pomeraj za lovca");
                        }
                        else
                        {
                            if (x > figure[clan].Xpos && y < figure[clan].Ypos)
                            {
                                for (int i = 1; figure[clan].Xpos + i <= 7 && figure[clan].Ypos - i >= 0; i++) //gore desno
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
                              if (x > figure[clan].Xpos && y > figure[clan].Ypos) //dole desno
                            {

                                for (int i = 1; figure[clan].Xpos + i <= 7 && figure[clan].Ypos + i <= 7; i++)
                                {
                                    if (figure[clan].Xpos + i == x && figure[clan].Ypos + i == y) //fakticni provera da li kombo moze ili ne
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
                                        //Ubacis onda da je lovim(kombo) true i ako je true, onda ide od xpos do ypos i gleda ako ima nekog tvog becara iz liste


                                    }
                                }

                            }
                            else
                            if (x < figure[clan].Xpos && y < figure[clan].Ypos) //levo gore
                            {
                                for (int i = 1; figure[clan].Xpos - i >= 0 && figure[clan].Ypos - i >= 0; i++)
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
                                }
                            }
                            else
                            {
                                if (x < figure[clan].Xpos && y > figure[clan].Ypos) //levo dole
                                {
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
                            Console.WriteLine("Smeta Vam vasa figura");
                            test = false;
                        }
                        break;

                    case "Kralj":
                        if (x > (figure[clan].Xpos + 1) || x < (figure[clan].Xpos - 1))
                        {
                            test = false;
                            Console.WriteLine("Molimo unesite adekvatna polja za Kralja");
                        }

                        else
                                if (y > (figure[clan].Ypos + 1) || y < (figure[clan].Ypos - 1))
                        {
                            Console.WriteLine("Molimo unesite adekvatna polja za Kralja");
                            test = false;
                        }

                        else
                        {

                            for (int i = 0; i < figure.Count; i++) //Ovo ne znam stace mi kad svakako imam onu proveru da li je neka tvoja figura na tvom mestu, ali ajd kao
                            {
                                if (i != clan && figure[i].Xpos == x && figure[i].Ypos == y)
                                {
                                    Console.WriteLine("Greska, vasa figura se vec nalazi na tom mestu");
                                    test = false;
                                }
                                provera = false;
                                test = false;
                            }
                        }
                        break;

                    case "Kica": //Ovo na kraju ispisujes kad implementiras "jedenje"

                        break;

                    case "Piun":
                        bool aktiviran = false;
                        if (y == ( figure[clan].Ypos -2) && x == figure[clan].Xpos && figure[clan].pozicija == true && figure[clan].indikator == 0)
                        {
                            test = false;
                            aktiviran = true;
                            provera = false;
                            figure[clan].indikator = 1;
                        }
                        else
                            if (y == (figure[clan].Ypos - 1) && x == figure[clan].Xpos && figure[clan].pozicija == true )
                        {
                            test = false;
                            provera = false;
                            aktiviran = true;
                            figure[clan].indikator = 1;
                        }
                        else
                            if( y ==(figure[clan].Ypos+1) && x == figure[clan].Xpos && figure[clan].pozicija == false )
                        {
                            test = false;
                            aktiviran = true;
                            provera = false;
                            figure[clan].indikator = 1;
                        }
                        else
                            if(y == (2 + figure[clan].Ypos) && x == figure[clan].Xpos && figure[clan].pozicija == false && figure[clan].indikator == 0 )
                             {
                            test = false;
                            aktiviran = true;
                            provera = false;
                            figure[clan].indikator = 1;
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
                                int posX = figure[clan].Xpos+1;
                                int posY = figure[clan].Ypos+1;
                                while (posX <= x && posY <= y)
                                {
                                    for(int i = 0;i<neprijatelj.Count;i++)
                                    {
                                        if(neprijatelj[i].Xpos == posX && neprijatelj[i].Ypos==posY)
                                        {
                                            groblje.Add(neprijatelj[i]);
                                            neprijatelj.Remove(neprijatelj[i]);
                                            x = posX;
                                            y = posY;
                                            postoji = true;
                                            break;
                                        }
                                    }
                                    if (postojii)
                                    {
                                        jedi = true;
                                        break;
                                    }
                                    else {
                                        posY++;
                                        posX++;
                                    }
                                    

                                }

                            }
                                    else
                                if(x> figure[clan].Xpos && y < figure[clan].Ypos) //desno gore
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
                                            postoji = true;
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
                                if(x<figure[clan].Xpos && y > figure[clan].Ypos) //levo dole
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
                                            postoji = true;
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
                            if(x<figure[clan].Xpos && y < figure[clan].Ypos) //levo gore
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
                                            postoji = true;
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
                            break;
                        case "Kralj":
                                break;
                        case "Kica":
                                break;
                        case "Piun":
                            jedi = true;
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

            IspisTabele(Tabla,Prijatelj,Neprijatelj);
            PomeriPrijatelj(Prijatelj, Tabla, Neprijatelj, GrobljeNeprijatelj);  //(Koga_pomerano, Tabla, Koga_jedemo, Groblje_Koga_jedeno)

            IspisTabele(Tabla, Prijatelj, Neprijatelj);
            PomeriPrijatelj(Prijatelj, Tabla, Neprijatelj, GrobljeNeprijatelj);

            IspisTabele(Tabla, Prijatelj, Neprijatelj);
            PomeriPrijatelj(Prijatelj, Tabla, Neprijatelj, GrobljeNeprijatelj);

            IspisTabele(Tabla, Prijatelj, Neprijatelj);
            PomeriPrijatelj(Prijatelj, Tabla, Neprijatelj, GrobljeNeprijatelj);

            IspisTabele(Tabla, Prijatelj, Neprijatelj);
            PomeriPrijatelj(Prijatelj, Tabla, Neprijatelj, GrobljeNeprijatelj);
            IspisTabele(Tabla, Prijatelj, Neprijatelj);




        }
    }
}

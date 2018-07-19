using System;
using System.Collections.Generic;
using System.Threading;

namespace DrunkPeopleTest
{
    /*
     * Ristrutturare il seguente programma seguendo il paradigma ad oggetti e utilizzando gli opportuni design pattern.
     * 
     * In particolare, il metodo BringDrunkToHome dovrà diventare una classe FACTORY chiamata Pub con un metodo CreateDrunk.
     * Il metodo restituisce un'istanza di IDrunk.
     * L'osteria va realizzata come SINGLETON.
     * 
     * La classe Drunk ha un metodo GoHome(), dentro il quale c'è la logica che sposta la persona ubriaca verso casa.
     * Le coordinate dell'osteria (Pub) sono sempre (20, 0), quelle della casa (20, 20).
     * I due tipi di Drunk (WineDrunk e BeerDrunk) vanno realizzati col pattern TEMPLATE.
     * Questo perché la parte iniziale e finale sono le stesse. Quello che varia è la parte centrale, il modo in cui
     * l'ubriaco fa il suo percorso sgangherato (quindi questa parte sarà implementata nelle due classi derivate).
     *   
     * I parametri bool isDrunk e muchDrunk decidono di quanto si sposta l'ubriaco:
     * - se non è ubriaco, fa dritto dall'osteria a casa;
     * - se è poco ubriaco, riesce a fare parecchi passi nella stessa direzione;
     * - se è molto ubriaco, fa un zig zag molto stretto perché barcolla di più.
     * Questi due parametri devono diventare un insieme di tre piccole classi,
     * accomunate da un'interfaccia IDrunkLevel, da settare in una proprietà della classe Drunk con il pattern STRATEGY.
     * L'interfaccia ha un metodo IsDrunk() che restituisce un bool, e un metodo CalculateMaxSteps().
     *    
     * Il metodo GoHome() dei Drunk funziona quindi così:
     * - chiedo all'IDrunkLevel IsDrunk().
     * - se no, vado dritto a casa perché sono sobrio.
     * - altrimenti, faccio il percorso sgangherato in base al mio tipo (è la parte abstract del metodo TEMPLATE)
     * - alla fine, arrivato all'ultima riga, faccio i passi necessari per arrivare dritto a casa.
     * 
     * LittleDrunkLevel e MuchDrunkLevel restituiscono true nel metodo IsDrunk()
     * NullDrunkLevel è un NullObject: IsDrunk() restituisce false e CalculateMaxSteps() restituisce 0.
     * Implementarlo come SINGLETON.
     * 
     * In ultimo, estrarre la funzionalità di stampa in console della posizione.
     * Deve esserci una classe statica con metodi:
     * - MoveLeft
     * - MoveRight
     * - MoveDown
     * - MoveLeftDown
     * Tutti prendono in input il numero di passi da percorrere,
     * aspettano un numero random di millisecondi (il Thread.Sleep(r.Next(1, 10) * 25))
     * e si muovono della direzione giusta.
     */
    class Program
    {
        static void Main(string[] args)
        {
            BringDrunkToHome(true, "beer", false);

            Console.Read();
        }

        static void BringDrunkToHome(bool isDrunk, string type, bool muchDrunk)
        {
            Console.CursorLeft = 20;
            Console.CursorTop = 0;

            Console.Write("0");
            Console.CursorLeft--;

            if (!isDrunk)
            {
                while (Console.CursorTop < 20)
                {
                    Console.Write("0");
                    Console.CursorLeft--;
                    Console.CursorTop++;
                }
            }
            else if (type == "wine")
            {
                var r = new Random();

                while (Console.CursorTop < 20)
                {
                    // non è molto ubriaco
                    var stepsRight = r.Next(1, !muchDrunk ? 10 : 5);
                    //movimento verso destra
                    for (int i = 0; i < stepsRight; i++)
                    {
                        Thread.Sleep(r.Next(1, 10) * 25);
                        Console.CursorLeft++;
                        Console.Write("0");
                        Console.CursorLeft--;
                    }

                    var stepsDownLeft = r.Next(1, !muchDrunk ? 12 : 6);

                    //movimento in diagonale verso il basso a sinistra
                    for (int i = 0; i < stepsDownLeft; i++)
                    {
                        Thread.Sleep(r.Next(1, 10) * 25);
                        Console.CursorTop++;
                        Console.CursorLeft--;
                        Console.Write("0");
                        Console.CursorLeft--;
                    }
                    // end
                }


                // ultima riga torna a casa dritto
                if (Console.CursorLeft < 20)
                {
                    while (Console.CursorLeft < 20)
                    {
                        Thread.Sleep(r.Next(1, 10) * 25);
                        Console.Write("0");
                    }
                }
                else
                {
                    while (Console.CursorLeft > 20)
                    {
                        Thread.Sleep(r.Next(1, 10) * 25);
                        Console.Write("0");
                        Console.CursorLeft--;
                        Console.CursorLeft--;
                    }
                }

                //arrivato a casa
                Console.Write("X");
                Console.CursorLeft--;
            }
            else if (type == "beer")
            {
                var r = new Random();

                while (Console.CursorTop < 20)
                {
                    Thread.Sleep(500);

                    var stepsRight = r.Next(1, !muchDrunk ? 14 : 7);
                    for (int i = 0; i < stepsRight; i++)
                    {
                        Thread.Sleep(r.Next(1, 10) * 25);
                        Console.CursorLeft++;
                        Console.Write("0");
                        Console.CursorLeft--;
                    }

                    var stepsDown = r.Next(1, !muchDrunk ? 8 : 4);
                    for (int i = 0; i < stepsDown; i++)
                    {
                        Thread.Sleep(r.Next(1, 10) * 25);
                        Console.CursorTop++;
                        Console.Write("0");
                        Console.CursorLeft--;
                    }

                    var stepsLeft = r.Next(1, muchDrunk ? 14 : 7);
                    for (int i = 0; i < stepsLeft; i++)
                    {
                        Thread.Sleep(r.Next(1, 10) * 25);
                        Console.CursorLeft--;
                        Console.Write("0");
                        Console.CursorLeft--;
                    }

                    stepsDown = r.Next(1, muchDrunk ? 10 : 5);
                    for (int i = 0; i < stepsDown; i++)
                    {
                        Thread.Sleep(r.Next(1, 10) * 25);
                        Console.CursorTop++;
                        Console.Write("0");
                        Console.CursorLeft--;
                    }
                }
                //ultima riga torna a casa dritto
                if (Console.CursorLeft < 20)
                {
                    while (Console.CursorLeft < 10)
                    {
                        Console.Write("0");
                    }
                }
                else
                {
                    while (Console.CursorLeft > 20)
                    {
                        Thread.Sleep(r.Next(1, 10) * 25);
                        Console.Write("0");
                        Console.CursorLeft--;
                        Console.CursorLeft--;
                    }
                }


                //arrivato a casa
                Console.Write("X");
                Console.CursorLeft--;
            }

            else
                throw new Exception("unknown type!");
        }

    }

    interface IDrunk
    {
        void GoHome();
    }

    abstract class Drunk : IDrunk
    {
        public IDrunkLevel drunkLevel { get; set; }
        
        void GoHome()
        {

        }

        void IDrunk.GoHome()
        {
            throw new NotImplementedException();
        }
    }

    class WineDrunk : Drunk
    {
        // implementazione dei metodi astratti di GoHome();
    }

    class BeerDrunk : Drunk
    {
        // implementazione dei metodi astratti di GoHome();
    }

    interface IDrunkLevel
    {
        bool IsDrunk();
        List<int> CalculateMaxSteps();
    }

    class IsNotDrunk : IDrunkLevel
    {
        public bool IsDrunk()
        {
            return false;
        }

        public List<int> CalculateMaxSteps()
        {
            throw new NotImplementedException();            
        }
    }

    class IsLessDrunk : IDrunkLevel
    {
        bool IDrunkLevel.IsDrunk()
        {
            return true;
        }

        public List<int> CalculateMaxSteps()
        {
            var r = new Random();
            return new List<int>
            {
                r.Next(1, 12),
                r.Next(1, 8),
            };
        }
    }

    class IsMuchDrunk : IDrunkLevel
    {
        public bool IsDrunk()
        {
            return true;
        }

        public List<int> CalculateMaxSteps()
        {
            var r = new Random();
            return new List<int>
            {
                r.Next(1, 7),
                r.Next(1, 5),
            };
        }
    }

    class Pub
    {
        // classe factory
        private static IDrunk CreateDrunk(string type)
        {
            IDrunk dk;

            switch (type)
            {
                case "WineDrunk":
                    dk = new WineDrunk();
                    break;
                case "BeerDrunk":
                    dk = new BeerDrunk();
                    break;
                default:
                    throw new ArgumentException("Drunk type not found");
            }

            return dk;
        }

        #region Singleton
        private Pub() { }

        static Pub()
        {
            Instance = new Pub();
        }

        public static Pub Instance { get; }
        #endregion
    }
}

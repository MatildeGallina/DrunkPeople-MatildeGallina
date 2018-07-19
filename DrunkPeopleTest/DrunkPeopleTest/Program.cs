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
        }
    }

    public interface IDrunk
    {
        void GoHome();
    }
    
    abstract class Drunk : IDrunk
    {
        public IDrunkLevel drunkLevel
        {
            get { return _DrunkLevel; }
            set { _DrunkLevel = value ?? NullDrunkLevel.Instance; }
        }
        private IDrunkLevel _DrunkLevel;
        
        public void GoHome()
        {
            StartFromPub();

            if (!drunkLevel.IsDrunk())
            {
                while (Console.CursorTop < 20)
                {
                    Console.Write("0");
                    Console.CursorLeft--;
                    Console.CursorTop++;
                }
            };

            var r = new Random();

            CalculateStreet(r);

            LastLineStree(r);

            ArrivedHome();
            
        }

        private void StartFromPub()
        {
            Console.CursorLeft = 20;
            Console.CursorTop = 0;

            Console.Write("0");
            Console.CursorLeft--;
        }

        private void LastLineStree(Random r)
        {
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
        }

        private void ArrivedHome()
        {
            Console.Write("X");
            Console.CursorLeft--;
        }

        internal abstract void CalculateStreet(Random r);
    }

    class WineDrunk : Drunk
    {
        internal override void CalculateStreet(Random r)
        {
            while (Console.CursorTop < 20)
            {
                var stepsRight = drunkLevel.CalculateMoreSteps();
                for (int i = 0; i < stepsRight; i++)
                {
                    Thread.Sleep(r.Next(1, 10) * 25);
                    Console.CursorLeft++;
                    Console.Write("0");
                    Console.CursorLeft--;
                }

                var stepsDownLeft = drunkLevel.CalculateLessSteps();
                for (int i = 0; i < stepsDownLeft; i++)
                {
                    Thread.Sleep(r.Next(1, 10) * 25);
                    Console.CursorTop++;
                    Console.CursorLeft--;
                    Console.Write("0");
                    Console.CursorLeft--;
                }
            }
        }
    }

    class BeerDrunk : Drunk
    {
        internal override void CalculateStreet(Random r)
        {
            while (Console.CursorTop < 20)
            {
                Thread.Sleep(500);

                var stepsRight = drunkLevel.CalculateMoreSteps();
                for (int i = 0; i < stepsRight; i++)
                {
                    Thread.Sleep(r.Next(1, 10) * 25);
                    Console.CursorLeft++;
                    Console.Write("0");
                    Console.CursorLeft--;
                }

                var stepsDown = drunkLevel.CalculateLessSteps();
                for (int i = 0; i < stepsDown; i++)
                {
                    Thread.Sleep(r.Next(1, 10) * 25);
                    Console.CursorTop++;
                    Console.Write("0");
                    Console.CursorLeft--;
                }

                var stepsLeft = drunkLevel.CalculateMoreSteps();
                for (int i = 0; i < stepsLeft; i++)
                {
                    Thread.Sleep(r.Next(1, 10) * 25);
                    Console.CursorLeft--;
                    Console.Write("0");
                    Console.CursorLeft--;
                }

                stepsDown = drunkLevel.CalculateLessSteps();
                for (int i = 0; i < stepsDown; i++)
                {
                    Thread.Sleep(r.Next(1, 10) * 25);
                    Console.CursorTop++;
                    Console.Write("0");
                    Console.CursorLeft--;
                }
            }
        }
    }

    interface IDrunkLevel
    {
        bool IsDrunk();
        int CalculateMoreSteps();
        int CalculateLessSteps();
    }

    class IsNotDrunk : IDrunkLevel
    {
        public bool IsDrunk()
        {
            return false;
        }

        public int CalculateMoreSteps()
        {
            return 1;
        }

        public int CalculateLessSteps()
        {
            return 1;
        }
    }

    class IsLessDrunk : IDrunkLevel
    {
        bool IDrunkLevel.IsDrunk()
        {
            return true;
        }

        public int CalculateMoreSteps()
        {
            var r = new Random();
            return r.Next(1, 12);
        }

        public int CalculateLessSteps()
        {
            var r = new Random();
            return r.Next(1, 8);
        }
    }

    class IsMuchDrunk : IDrunkLevel
    {
        public bool IsDrunk()
        {
            return true;
        }

        public int CalculateMoreSteps()
        {
            var r = new Random();
            return r.Next(1, 7);
        }

        public int CalculateLessSteps()
        {
            var r = new Random();
            return r.Next(1, 5);
        }
    }

    class NullDrunkLevel : IDrunkLevel
    {
        static NullDrunkLevel()
        {
            Instance = new NullDrunkLevel();
        }

        public static NullDrunkLevel Instance { get; }

        private NullDrunkLevel() { }

        public bool IsDrunk()
        {
            return false;
        }

        public int CalculateLessSteps()
        {
            return 0;
        }

        public int CalculateMoreSteps()
        {
            return 1;
        }
    }

    public class Pub
    {
        public static IDrunk CreateDrunk(string type)
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
        
        internal Pub() { }

        static Pub()
        {
            Instance = new Pub();
        }

        public static Pub Instance { get; }
    }
}

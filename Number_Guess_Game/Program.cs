using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks; 

namespace Number_Guess_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool Unique;
            int number;
            string numberStr;

            Random rnd = new Random();

            int[] tab = new int[6];
            int[] tab2 = new int[6];
            for (int i = 0; i < tab.Length; i++)
            {
                do
                {
                    Unique = true;
                    number = rnd.Next(47);
                    foreach (int value in tab)
                    {
                        if (value == number)
                        {
                            Unique = false;
                        }
                    }
                } while (Unique == false);
                if (Unique)
                {
                    tab[i] = number;
                }
            }

            //if you want to see what numbers were generated uncomment this loop
            //
            //foreach(int value in tab)
            //{
            //    Console.WriteLine(value);
            //}
            
            Console.WriteLine("Podaj swoje 6 liczb (0-46): ");

            for (int i = 0; i < tab2.Length; i++)
            {
                do
                {   
                    Unique = true;
                    numberStr = Console.ReadLine();
                    number = int.Parse(numberStr);
                    foreach (int value in tab2)
                    {
                        if (value == number)
                        {
                            Unique = false;
                            Console.WriteLine("Ta liczba już jest");
                        }
                    }
                } while (Unique == false);
                if (Unique)
                {
                    tab2[i] = number;
                }
            }


            int match = 0;

            foreach (int value2 in tab2)
            {
                foreach (int value1 in tab)
                {
                    if (value1 == value2)
                    {
                        match++;
                    }
                }
            }

            Console.WriteLine("Tyle liczb trafileś: " + match);
            Console.ReadKey();

            //Random rnd = new Random();

            //int number = rnd.Next(11);
            //int digit2;
            //do
            //{
            //    Console.WriteLine("Podaj losowa liczbę: ");
            //    string digit = Console.ReadLine();
            //    digit2 = int.Parse(digit);
            //    if (digit2 > number)
            //    {
            //        Console.WriteLine("Liczba jest większa od poprawnej");
            //    } else if (digit2 < number)
            //    {
            //        Console.WriteLine("Liczba jest mniejsza od poprawnej");
            //    }


            //} while (digit2 != number);

            //Console.WriteLine("Gratulacje zgadłeś!!!");
            //Console.ReadKey();
        }
    }
}

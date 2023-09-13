using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lists
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //tests

            //List<int> list = new List<int>() { 10, 20, 30 };
            //list.Add(1);
            //list.Add(2);
            //list.Add(3);
            //list.Insert(3, 150);

            //foreach (int number in list) 
            //{
            //    Console.Write(number+" ");
            //}

            //Console.WriteLine();

            //Console.WriteLine(list[3]);

            //Console.WriteLine(list.Find(x => x == 10));

            //Console.WriteLine(list.Count);



            //number guess with all wrong digits showed if user wins

            //List<int> wrong = new List<int>();

            //int userDigit;

            //Random rnd = new Random();

            //int digit = rnd.Next(10);

            //Console.WriteLine(digit);
            //do
            //{
            //    Console.WriteLine("Podaj swoją liczbę: ");

            //    bool good = int.TryParse(Console.ReadLine(), out userDigit);

            //    if (good)
            //    {
            //        if (userDigit != digit)
            //        {
            //            Console.WriteLine("Zła liczba");
            //            if (!wrong.Contains(userDigit))
            //            {
            //                wrong.Add(userDigit);
            //            }

            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("To nie liczba");
            //    }
            //} while (userDigit != digit);

            //Console.WriteLine("Gratulację, wszystkie niepoprawne liczby: ");
            //foreach (int value in wrong)
            //{ 
            //    Console.Write(value + " ");
            //}

            //list with random content, user choose size, min value and max value, list is sorted

            List<int> RandomNumbers = new List<int>();

            int size;
            int min;
            int max;
            int digit;
            bool done = false;
            Random rnd = new Random();

            do
            {
                Console.WriteLine("Podaj z ilu elementów ma się składać lista: ");

                bool sizeGood = int.TryParse(Console.ReadLine(), out size);

                if (sizeGood)
                {
                    Console.WriteLine("Podaj mininmalną liczbę zakresu: ");

                    bool minGood = int.TryParse(Console.ReadLine(), out min);

                    if (minGood)
                    {
                        Console.WriteLine("Podaj maksymalną liczbę zakresu: ");

                        bool maxGood = int.TryParse(Console.ReadLine(), out max);

                        if (maxGood)
                        {
                            for (int i = 0; i < size; i++)
                            {
                                bool added;
                                do
                                {
                                    added = false;
                                    digit = rnd.Next(min, max);

                                    if (!RandomNumbers.Contains(digit))
                                    {
                                        RandomNumbers.Add(digit);
                                        added = true;
                                    }

                                } while (!added);
                            }
                            done = true;
                        }
                        else
                        {
                            Console.WriteLine("To nie liczba");
                        }
                    }
                    else
                    {
                        Console.WriteLine("To nie liczba");
                    }
                }
                else
                {
                    Console.WriteLine("To nie liczba");
                }
            } while (!done);

            RandomNumbers.Sort();

            Console.WriteLine("Twoje liczby to: ");

            foreach(int value in RandomNumbers)
            {
                Console.Write(value + " ");
            }

            Console.ReadKey();
        }
    }
}
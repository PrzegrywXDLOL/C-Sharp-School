using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Digit l1 = new Digit(5);
            //l1.Change(15);
            //int number = l1.GetNumber();
            //l1.Print();

            //Digit l2 = new Digit(10);

            //List<Digit> list = new List<Digit>();

            //list.Add(new Digit(10));
            //list.Add(new Digit(20));
            //list.Add(new Digit(30));

            //list[1].Change(16);

            //foreach(var item in list)
            //{
            //    item.Print();
            //}

            //method #1
            //var temp = list.Find(x => x.GetNumber() == 16);
            //list.Remove(temp);

            //method #2
            //var index = list.FindIndex(x => x.GetNumber() == 16);
            //if (index != -1)
            //  list.RemoveAt(index);

            //list with random 10 digits in range from 1 to 100 where even digits are removed
            
            List<Digit> list = new List<Digit>();

            Random rnd = new Random();

            bool done = false;
            int random;;

            for (int i = 0; i < 10; i++)
            {
                random = rnd.Next(1, 100);
                list.Add(new Digit(random));
            }

            do
            {
                var index = list.FindIndex(x => x.whatIsNumberEven(x.GetNumber()) == true);
                if (index != -1)
                    list.RemoveAt(index);
                else
                    done = true;
            }while (!done);

            foreach (var item in list)
            {
                item.PrintNumber();
            }

                Console.ReadKey();
        }
    }
}

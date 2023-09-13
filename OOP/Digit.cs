using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    internal class Digit
    {
        public int number;
        public bool even;

        public Digit() { }

        public Digit(int Number)
        {
            number = Number;
            even = whatIsNumberEven(Number);
        }

        //public Digit(int number, bool even)
        //{
        //    this.number = number;
        //    this.even = even;
        //}

        public bool whatIsNumberEven(int Number)
        {
            if (Number % 2 == 0)
                return true;
            
            return false;
        }

        public void Change(int Number)
        {
            number = Number;
            even = whatIsNumberEven(Number);
        }

        public int GetNumber()
        {
            return number;
        }


        public void Print()
        {
            Console.WriteLine(number);
            if (even)
                Console.WriteLine("parzyste");
            else
                Console.WriteLine("nieparzyste");
        }

        public void PrintNumber()
        {
            Console.WriteLine(number);
        }
    }
}

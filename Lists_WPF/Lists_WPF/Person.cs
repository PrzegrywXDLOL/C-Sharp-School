using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lists_WPF
{
    internal class Person
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public Person() { }

        public Person(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
    }
}

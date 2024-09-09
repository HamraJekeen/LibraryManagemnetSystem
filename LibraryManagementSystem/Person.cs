using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    public abstract class Person
    {
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }

        public Person(string name, DateTime dateofBirth, string address)
        {
            Name = name;
            DOB = dateofBirth;
            Address = address;
        }

        public abstract override string ToString();
    }
}

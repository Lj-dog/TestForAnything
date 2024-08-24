using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppDictionary.Models
{
    internal abstract class Person
    {
        public Sex Sex { get; set; }

        public int Age { get; set; }
    }

    public enum Sex
    {
        MALE,
        FEMALE,
    }
}
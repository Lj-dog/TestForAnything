using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMVVMTest.Models
{
    class People
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }

        public byte Age { get; set; }
        public DateTime BirthDay { get; set; }
    }
}

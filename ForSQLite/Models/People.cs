using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForSQLite.Models
{
    public class People
    {
        [Column(IsPrimary = true, IsIdentity = true)]
        public int Guid { get; set; }

        public string Name { get; set; }

        public uint Age { get; set; }

        public uint PhoneNum { get; set; }
    }
}
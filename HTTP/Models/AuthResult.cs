using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTP.Models
{
    public class AuthResult
    {
        public bool Authenticated { get; set; }

        public string User { get; set; }
    }
}

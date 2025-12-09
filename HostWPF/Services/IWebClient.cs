using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostWPF.Services
{
    internal interface IWebClient
    {
        Task<string> GetStringAsync(string url);
    }
}

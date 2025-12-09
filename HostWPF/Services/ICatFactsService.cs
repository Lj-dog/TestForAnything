using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostWPF.Services
{
    public interface ICatFactsService
    {
        public Task<IEnumerable<string>> GetCatFactsAsync(int count = 1);
    }
}

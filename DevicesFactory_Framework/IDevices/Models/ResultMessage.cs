using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesFactory_Framework.IDevices.Models
{
    public class ResultMessage
    {
        public ResultMessage(string target, byte[] data)
        {
            Target = target;
            ReceiveData = data;
        }

        public string Target { get; set; }

        public byte[] ReceiveData { get; set; }


    }
}

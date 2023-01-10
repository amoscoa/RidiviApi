using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPSWebApi.Models
{
    public class CambiaClave
    {
        public string idCliente { get; set; }
        public string clientKey { get; set; }
        public string signature { get; set; }
    }
}

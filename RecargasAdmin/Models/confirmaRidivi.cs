using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RidiviApi.Models
{
    public class confirmaRidivi
    {
        public string status { get; set; }
        public int code { get; set; }
        public bool error { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }
}

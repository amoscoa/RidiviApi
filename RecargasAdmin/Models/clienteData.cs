using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RidiviApi.Models
{
    public class cuentaData
    {
        public string NomPropietario { get; set; }
        public double SaldoDisponible { get; set; }
        public string CodigoMoneda { get; set; }
        public string idNumber { get; set; }
    }

    public class clienteData
    {
        public string error { get; set; }
        public cuentaData account { get; set; }
        public string message { get; set; }

    }

}

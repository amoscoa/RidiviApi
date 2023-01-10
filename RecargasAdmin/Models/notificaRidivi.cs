using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RidiviApi.Models
{
    public class notificaRidivi
    {
        public string customerIdNumber { get; set; }
        public int customerTypeIdNumber { get; set; }
        public int numMov { get; set; }
        public string sinpeReference { get; set; }
        public string approvalDate { get; set; }
        public string transactionType { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public entityData origin { get; set; }
        public entityData destination { get; set; }
        public string connection { get; set; }
        public string user { get; set; }
    }

    public class entityData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string iban { get; set; }
        public string entityCode { get; set; }
        public string phone { get; set; }
    }
}

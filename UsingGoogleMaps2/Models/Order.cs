using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsingGoogleMaps2.Models
{
    public class Order
    {

        public string TransactionID { get; set; }
        public string SAmountPaid { get; set; }
        public string DeviceID { get; set; }
        public string PayerEmail { get; set; }

        public string Item { get; set; }
        
    }
}
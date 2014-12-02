using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsingGoogleMaps2.Models
{
    public class DealDetails
    {

        public int DealId { get; set; }
        public string PubName { get; set; }
        public string PubAddress { get; set; }
        public byte[] DealImage { get; set; }

        public string StringImage { get; set; }
        public string Description { get; set; }
        public System.DateTime PublicationDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        
        

    }
}
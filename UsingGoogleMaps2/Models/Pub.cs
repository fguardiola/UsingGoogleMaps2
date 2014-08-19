using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsingGoogleMaps2.Models
{

    public enum PostCodes
    {
        D1,D2,D3,D4,D5,D6,D7,D8,D9,D10,D11,D12,D13,D14,D15,D16,D17,D18,D20,D21,D22,D24
    }
    public class Pub
    {
   
    public int Id { get; set; }
    public string Address { get; set; }

    public PostCodes PostCode { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }

    public double Longitude { get; set; }
    public string Info { get; set; }

   public ICollection<EEVA> EEVAs { get; set; }
    
    }
}
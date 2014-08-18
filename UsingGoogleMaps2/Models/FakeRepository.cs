using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsingGoogleMaps2.Models
{
   public class FakeRepository:IRepository
    {
        
       
       public static List<Pub> pubFakeTable = new List<Pub>()
       {
           new Pub{ Id=1, Name="Howl at the moon", Address="7/8 Lower Mount Street, Dublin 2", PostCode=PostCodes.D2, Info="9-5, M-F", Latitude=53.339426, Longitude=-6.245727 },
           new Pub{ Id=2, Name="Dicey's Garden",Address="21-25 Harcourt St, Dublin 2", PostCode=PostCodes.D2, Info="9-1,2-5, M-F", Latitude=53.335983, Longitude=-6.263532 },
           new Pub{ Id=3, Name="O'Reilly's", Address="George's Quay, Dublin 2", PostCode=PostCodes.D2,Info="9-7, M-F", Latitude=53.347260, Longitude=-6.254101 },
           new Pub{ Id=4, Name="Darkey kelly's",Address="19 Fishamble St, Dublin 8", PostCode=PostCodes.D8, Info="10-6, M-F", Latitude=53.344067, Longitude=-6.270058 }
           
       };
    

       public IEnumerable<Pub> ShowPubsOnMap(PostCodes postcode)
       {
           return pubFakeTable.Where(Entries => Entries.PostCode == postcode);
       }

       public IEnumerable<Pub> ShowAllPubsOnMap()
       {
           return pubFakeTable.Select(Entries => Entries);
       }
    }
}
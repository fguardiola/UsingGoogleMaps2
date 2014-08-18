using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsingGoogleMaps2.Models
{
    public class Repository:IRepository
    {
        public IEnumerable<Pub> ShowPubsOnMap(PostCodes postcode)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Pub> ShowAllPubsOnMap()
        {
            throw new NotImplementedException();
        }
    }
}
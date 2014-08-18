using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingGoogleMaps2.Models
{
   public interface IRepository
    {
       IEnumerable<Pub> ShowPubsOnMap(PostCodes postcode);
       IEnumerable<Pub> ShowAllPubsOnMap();
    }
}

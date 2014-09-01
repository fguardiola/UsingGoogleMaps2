using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingGoogleMaps2.Models
{
   public interface IRepository
    {
       //IEnumerable<PubTest> ShowPubsOnMap(PostCodes postcode);
       //IEnumerable<PubTest> ShowAllPubsOnMap();
       IEnumerable<Pub> ListMyPubs();
       Pub Manage(int id = 0);
       int GetCompanyId(string companyName);
       string UserName { get; set; }
       void RegisterPub(Pub pub);
       void CreateDeal(Deal deal);
       SearchResults SearchResults(SearchPreferences searchPreferences);
       
    }
}

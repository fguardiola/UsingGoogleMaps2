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
       //void CreateDeal(Deal deal);
       void CreateDealWithImage(TemporayDeal deal);
       SearchResults SearchResults(SearchPreferences searchPreferences);
       void EnterDublinAreaCoordinates(DublinAreasCoordinate areaCoordinates);
       DealDetails Details(int id = 0);
       DealPubOwnwerDetails DetailsPubOwners(int i = 0);

       byte[] GetImageIfExistBinaryFormat(int dealId);
       IEnumerable<Deal> ManageDeals(int pubId);
       IEnumerable<TransactionInfoSimple> CurrentTransactions(int id);

       Deal Delete(int id = 0);
       bool DeleteConfirmed(int id); 
      
       
    }
}

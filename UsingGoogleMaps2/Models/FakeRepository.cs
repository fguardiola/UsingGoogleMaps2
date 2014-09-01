using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsingGoogleMaps2.Models
{
   public class FakeRepository:IRepository
    {
       public string UserName { get; set; }
       
       public static List<PubTest> pubFakeTable = new List<PubTest>()
       {
           new PubTest{ Id=1, Name="Howl at the moon", Address="7/8 Lower Mount Street, Dublin 2", PostCode=PostCodes.D2, Info="9-5, M-F", Latitude=53.339426, Longitude=-6.245727 },
           new PubTest{ Id=2, Name="Dicey's Garden",Address="21-25 Harcourt St, Dublin 2", PostCode=PostCodes.D2, Info="9-1,2-5, M-F", Latitude=53.335983, Longitude=-6.263532 },
           new PubTest{ Id=3, Name="O'Reilly's", Address="George's Quay, Dublin 2", PostCode=PostCodes.D2,Info="9-7, M-F", Latitude=53.347260, Longitude=-6.254101 },
           new PubTest{ Id=4, Name="Darkey kelly's",Address="19 Fishamble St, Dublin 8", PostCode=PostCodes.D8, Info="10-6, M-F", Latitude=53.344067, Longitude=-6.270058 }
           
       };
    

       //public IEnumerable<PubTest> ShowPubsOnMap(PostCodes postcode)
       //{
       //    return pubFakeTable.Where(Entries => Entries.PostCode == postcode);
       //}

       //public IEnumerable<PubTest> ShowAllPubsOnMap()
       //{
       //    return pubFakeTable.Select(Entries => Entries);
       //}
       public void RegisterPub(Pub pub)
       {
           throw new NotImplementedException();
       }

       public IEnumerable<Pub> ListMyPubs()
       {
           throw new NotImplementedException();
       }

       public int GetCompanyId(string companyName)
       {
           throw new NotImplementedException();

       }




       public Pub Manage(int id = 0)
       {
           throw new NotImplementedException();
       }


       public void CreateDeal(Deal deal)
       {
           throw new NotImplementedException();
       }


       public SearchResults SearchResults(SearchPreferences searchPreferences)
       {
           throw new NotImplementedException();
       }

       IEnumerable<Pub> IRepository.ListMyPubs()
       {
           throw new NotImplementedException();
       }

       Pub IRepository.Manage(int id)
       {
           throw new NotImplementedException();
       }

       int IRepository.GetCompanyId(string companyName)
       {
           throw new NotImplementedException();
       }

       string IRepository.UserName
       {
           get
           {
               throw new NotImplementedException();
           }
           set
           {
               throw new NotImplementedException();
           }
       }

       void IRepository.RegisterPub(Pub pub)
       {
           throw new NotImplementedException();
       }

       void IRepository.CreateDeal(Deal deal)
       {
           throw new NotImplementedException();
       }

       SearchResults IRepository.SearchResults(SearchPreferences searchPreferences)
       {
           throw new NotImplementedException();
       }
    }
}
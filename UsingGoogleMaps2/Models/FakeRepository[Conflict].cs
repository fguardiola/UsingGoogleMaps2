using System;
using System.Collections.Generic;
using System.Linq;



namespace UsingGoogleMaps2.Models
{
   public class FakeRepository:IRepository
    {
       public string UserName  { get{return "For testing"; } set{ } }
       const string userName = "Fernando's company";


       public static List<Company> companyFakeTable = new List<Company>()
       { 
            new Company{Id=1,Name=userName,ContactPhone="0831521643",ContactEmail="fernando.guardiola.888@gmail.com"}
       
       };
       
       public static List<Pub> pubFakeTable = new List<Pub>()
       {
          new Pub{ Id=1,FK_Company=1, Name="The Auld Dubliner", Address="24-25 Temple Bar Dublin",Category="Pub",Area="Temple Bar",LatLng="(53.3456173, -6.261971499999959)",DistanceTillAreaCenter=50,ContactEmail="fernando.guardiola.888@gmail.com",NegotiatedPercentage=4 },
          new Pub{ Id=2,FK_Company=1, Name="The Temple Bar Pub", Address="47-48 Temple Bar, Dublin 2",Category="Pub",Area="Temple Bar",LatLng="(53.3454965, -6.264241900000002)",DistanceTillAreaCenter=53.8,ContactEmail="fernando.guardiola.888@gmail.com",NegotiatedPercentage=5 }
        };

       public static List<Deal> dealFakeTable = new List<Deal>() 
       {
        new Deal{Id=1,FK_Pub=1,Description="Any 3 pints 12 euro",Price=12,PublicationDate=new DateTime(2014, 9, 3, 16, 5, 7, 123),StartDate=new DateTime(2014, 9, 3, 16, 5, 7, 123),EndDate=new DateTime(2014, 10, 15, 16, 5, 7, 123),DayOfWeeK="Every Thursday",VouchersForSale=20,VouchersSold=0, Pub=pubFakeTable.ElementAt(0)},
        new Deal{Id=1,FK_Pub=1,Description="Any 4 pints 15 euro",Price=15,PublicationDate=new DateTime(2014, 9, 3, 16, 5, 7, 123),StartDate=new DateTime(2014, 9, 3, 16, 5, 7, 123),EndDate=new DateTime(2014, 10, 15, 16, 5, 7, 123),DayOfWeeK="Every Thursday",VouchersForSale=25,VouchersSold=0,Pub=pubFakeTable.ElementAt(0)},
        new Deal{Id=1,FK_Pub=1,Description="Any 5 pints 20 euro",Price=20,PublicationDate=new DateTime(2014, 9, 3, 16, 5, 7, 123),StartDate=new DateTime(2014, 9, 3, 16, 5, 7, 123),EndDate=new DateTime(2014, 10, 15, 16, 5, 7, 123),DayOfWeeK="Every Thursday",VouchersForSale=30,VouchersSold=0,Pub=pubFakeTable.ElementAt(0)}
       
       };

       public static List<DealEEVA> dealEEVAFakeTable = new List<DealEEVA>() {
           new DealEEVA { Id = 1, FK_Deal = 1, Attribute = "DealImage",Value="image.jpg" } };

       public static List<Image> imageFakeTable = new List<Image>() 
       {
         new Image{ BinaryImage=new byte []{255,0,255}, Id=1, Name="image.jpg"}
       };

       public static List<DublinAreasCoordinate> dublinAreaCoordinatesFakeTable = new List<DublinAreasCoordinate>() 
       {
         
       };

      

        public IEnumerable<Pub> ListMyPubs()
       {
           int companyId = GetCompanyId(userName);
           return pubFakeTable.Where(pub => pub.FK_Company == companyId).ToList();//All the sentries of the database sorted by DateAdded*/
       }

        public Pub Manage(int id = 0)
       {
           Pub pub = pubFakeTable.Find(p=>p.Id==id);
           return pub;
       }
         public int GetCompanyId(string companyName)
       {
           int companyId;
           Company company = companyFakeTable.FirstOrDefault(comp => comp.Name == companyName);
           companyId = company.Id;
           return companyId;

       }
       public void RegisterPub(Pub pub)
       {
           Company company = companyFakeTable.FirstOrDefault(comp => comp.Name == userName);
           pub.FK_Company = GetCompanyId(userName);
           pub.Company = company;
           pubFakeTable.Add(pub);
           
       }

    
       //public void CreateDeal(Deal deal)
       //{
       //    throw new NotImplementedException();
       //}

       public void CreateDealWithImage(TemporayDeal temporayDeal)
       {
          
           // Create a real deal from the temporary Deal
           Deal dealToStore = new Deal();
           Pub pub = pubFakeTable.FirstOrDefault(pb => pb.Id == temporayDeal.FK_Pub);
           dealToStore.Pub = pub;
           dealToStore.FK_Pub = temporayDeal.FK_Pub;
           dealToStore.DayOfWeeK = temporayDeal.DayOfWeeK;
           dealToStore.Description = temporayDeal.Description;
           dealToStore.EndDate = temporayDeal.EndDate;
           dealToStore.StartDate = temporayDeal.StartDate;
           dealToStore.Price = temporayDeal.Price;
           dealToStore.PublicationDate = DateTime.Now;
           dealToStore.VouchersForSale = temporayDeal.VouchersForSale;
           //store in faketable
           dealFakeTable.Add(dealToStore);
          
       }
       public SearchResults SearchResults(SearchPreferences searchPreferences)
       {
           //Search algorithm
           var priceDecimal = SearchPreferences.PriceMaxToDecimal(searchPreferences.PriceMax);
           var DealsArea = dealFakeTable.Where(deal => deal.Pub.Area == searchPreferences.Area);
           var DealsAreaPrice = DealsArea.Where(deal => deal.Price <= priceDecimal);
           var DealsAreaPriceTime = DealsAreaPrice.Where(deal => deal.EndDate >= DateTime.Now);



           SearchResults searchresults = new SearchResults();
           foreach (var deal in DealsAreaPriceTime)
           {

               SearchResult searchResult = new SearchResult();
               //looking for related EEVAs
               var RelatedEEVAs = dealEEVAFakeTable.Where(eeva => eeva.FK_Deal == deal.Id);
               //look for DealImage if esxist
               var imageEEVA = RelatedEEVAs.FirstOrDefault(eeva => eeva.Attribute == "DealImage");
               if (imageEEVA != null)
               {
                   string imageName = imageEEVA.Value; //tke name if exists the image
                   var image = imageFakeTable.FirstOrDefault(img => img.Name == imageName);
                   if (image != null) searchResult.DealImage = image.BinaryImage;
               }
               searchResult.PubAddress = deal.Pub.Address;
               searchResult.PubName = deal.Pub.Name;
               searchResult.Description = deal.Description;
               searchResult.LatLng = deal.Pub.LatLng;
               //set content string for google maps api
               searchResult.SetContentString();
               // Add deal data to list of results
               searchresults.AddResult(searchResult);
           }

           return searchresults;
       }
         public void EnterDublinAreaCoordinates(DublinAreasCoordinate areaCoordinates)
       {
           dublinAreaCoordinatesFakeTable.Add(areaCoordinates);
          
       }
      
       

       //string IRepository.UserName
       //{
       //    get
       //    {
       //        throw new NotImplementedException();
       //    }
       //    set
       //    {
       //        throw new NotImplementedException();
       //    }
       //}


         public DealDetails Details(int id = 0)
         {
             throw new System.NotImplementedException();
         }


         public byte[] GetImageIfExistBinaryFormat(int dealId)
         {
             throw new System.NotImplementedException();
         }







         public IEnumerable<Deal> ManageDeals(int pubId)
         {
             throw new NotImplementedException();
         }
    }
}
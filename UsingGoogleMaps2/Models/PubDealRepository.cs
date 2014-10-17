using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity.Validation;
namespace UsingGoogleMaps2.Models
{
    public class PubDealRepository : IRepository
    {

        public string UserName { get; set; }
        private PubDealsDBEntities db = new PubDealsDBEntities();
        private PubDealsImagesEntities db2 = new PubDealsImagesEntities();
        //take company Id giving current user name
        public int GetCompanyId(string companyName)
        {
            int companyId;
            Company company = db.Companies.FirstOrDefault(comp => comp.Name == companyName);
            companyId = company.Id;
            return companyId;

        }


        public void RegisterPub(Pub pub)
        {
            //calculate distance from pub to the area's centre
            var areaCentre=db.DublinAreasCoordinates.FirstOrDefault(ent=>ent.Area==pub.Area);
            //careful with format=> stored like a string= "(lat,long)"
            var LatLongAreaCentre=areaCentre.LatLng;
            //take lat long from string
            char[] delimiterChars = {',','(',')' };
            string[] LatLongAreaCentreSplit = LatLongAreaCentre.Split(delimiterChars);
            double latCentre = Convert.ToDouble(LatLongAreaCentreSplit[1]);
            double longCentre =Convert.ToDouble( LatLongAreaCentreSplit[2]);
            string[] LatLongPubSplit = pub.LatLng.Split(delimiterChars);
            double latPub = Convert.ToDouble(LatLongPubSplit[1]);
            double longPub = Convert.ToDouble(LatLongPubSplit[2]);
            //calculate distance from pub to the area's centre
            var distance = DistanceAlgorithm.DistanceBetweenPlaces(longPub, latPub, longCentre, latCentre);
            pub.DistanceTillAreaCenter = distance;
            Company company = db.Companies.FirstOrDefault(comp => comp.Name == UserName);
            pub.FK_Company = GetCompanyId(UserName);
            pub.Company = company;
            db.Pubs.Add(pub);
            db.SaveChanges();
        }
        public IEnumerable<Pub> ListMyPubs()
        {

            int companyId = GetCompanyId(UserName);
            return db.Pubs.Where(pub => pub.FK_Company == companyId).ToList();//All the sentries of the database sorted by DateAdded*/

        }

        public Pub Manage(int id = 0)
        {
            Pub pub = db.Pubs.Find(id);
            return pub;
        }
        //public void CreateDeal(Deal deal)
        //{
        //    Pub pub = db.Pubs.FirstOrDefault(pb => pb.Id == deal.FK_Pub);
        //    deal.Pub = pub;
        //    deal.PublicationDate = DateTime.Now;
        //    db.Deals.Add(deal);
        //    db.SaveChanges();
        //}

        // Search in db deals that match the user preferences and create list of objects with necesary info to show on map the pub's with deals and deals's info
        public SearchResults SearchResults(SearchPreferences searchPreferences)
        {
            
            //Search algorithm
            var priceDecimal = SearchPreferences.PriceMaxToDecimal(searchPreferences.PriceMax);
            var DealsArea = db.Deals.Where(deal => deal.Pub.Area == searchPreferences.Area);
            var DealsAreaPrice = DealsArea.Where(deal => deal.Price <= priceDecimal);
            var DealsAreaPriceTime = DealsAreaPrice.Where(deal => deal.EndDate >= DateTime.Now);
           
          

            SearchResults searchresults = new SearchResults();
            foreach (var deal in DealsAreaPriceTime)
            {
                
                SearchResult searchResult = new SearchResult();

                var image = GetImageIfExistBinaryFormat(deal.Id);
                if (image != null) searchResult.DealImage = image;
               
                searchResult.PubAddress = deal.Pub.Address;
                searchResult.PubName = deal.Pub.Name;
                searchResult.Description = deal.Description;
                searchResult.LatLng = deal.Pub.LatLng;
                // to pass to deatils view
                searchResult.DealId = deal.Id;
                //set content string for google maps api
                searchResult.SetContentString();
              
                // Add deal data to list of results
                 searchresults.AddResult(searchResult);
            }

            return searchresults;
           
        }


        public void CreateDealWithImage(TemporayDeal temporayDeal)
        {
           
            HttpPostedFileBase file = temporayDeal.File;
            bool hasTocreateEEVA=false;
            string nameImageToStore = "";
            if (file != null)
            {
                // unique name

                hasTocreateEEVA=true;
                string nameImage = System.DateTime.Now.ToString("ddMMyyhhmmss");
                nameImage = nameImage + System.IO.Path.GetFileName(file.FileName);



                // save the image path path to the database or you can send image
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                    Image im = new Image();
                    im.BinaryImage = array;
                    im.Name = nameImage;
                    nameImageToStore = nameImage;
                    db2.Images.Add(im);
                    db2.SaveChanges();
                    

                }
            }
            // Create a real deal from the temporary Deal
            Deal dealToStore = new Deal();
            Pub pub = db.Pubs.FirstOrDefault(pb => pb.Id == temporayDeal.FK_Pub);
            dealToStore.Pub = pub;
            dealToStore.FK_Pub=temporayDeal.FK_Pub;
            dealToStore.DayOfWeeK=temporayDeal.DayOfWeeK;
            dealToStore.Description=temporayDeal.Description;
            dealToStore.EndDate=temporayDeal.EndDate;
            dealToStore.StartDate = temporayDeal.StartDate;
            dealToStore.Price=temporayDeal.Price;
            dealToStore.PublicationDate = DateTime.Now;
            dealToStore.VouchersForSale = temporayDeal.VouchersForSale;
            //save in database
            db.Deals.Add(dealToStore);

            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                var x=3;//??
                throw;
            }
            
           
          
            
            if (hasTocreateEEVA)
            {
                Deal dealToAgregateEEVA = db.Deals.FirstOrDefault(dl => dl.Id == dealToStore.Id);
                //create new EEVA for the current deal
                DealEEVA dealEEVA = new DealEEVA();
                dealEEVA.Attribute = "DealImage";
                dealEEVA.Value = nameImageToStore;
                dealEEVA.FK_Deal = dealToAgregateEEVA.Id;
                //not sure
                dealEEVA.Deal = dealToAgregateEEVA;
                db.DealEEVAs.Add(dealEEVA);
                db.SaveChanges();
            }



        }


        public void EnterDublinAreaCoordinates(DublinAreasCoordinate areaCoordinates)
        {
            db.DublinAreasCoordinates.Add(areaCoordinates);
            db.SaveChanges();
        }


        
        public DealDetails Details(int id = 0)
        {
            DealDetails dealDetails = new DealDetails();
            Deal deal = db.Deals.FirstOrDefault(d => d.Id == id);

            Pub pub = db.Pubs.FirstOrDefault(p => p.Id == deal.FK_Pub);
            Company company = db.Companies.FirstOrDefault(c => c.Id == pub.FK_Company);
            //look for image related
            byte[] image = GetImageIfExistBinaryFormat(id);
            // set dealDetails properties
            if (image != null) 
            {
                dealDetails.DealImage = image;
                var base64 = Convert.ToBase64String(image);
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                dealDetails.StringImage = imgSrc;
            }

            dealDetails.DealId = deal.Id;
            dealDetails.Description = deal.Description;
            dealDetails.EndDate = deal.EndDate;
            dealDetails.PubAddress = pub.Address;
            dealDetails.PublicationDate = deal.PublicationDate;
            dealDetails.PubName = company.Name;

            return dealDetails;
        }

        public byte[] GetImageIfExistBinaryFormat(int dealId)
        {
            byte[] binaryImage = null;    
        //looking for related EEVAs
            var RelatedEEVAs = db.DealEEVAs.Where(eeva => eeva.FK_Deal == dealId);
            //look for DealImage if esxist
            var imageEEVA = RelatedEEVAs.FirstOrDefault(eeva => eeva.Attribute == "DealImage");
            if (imageEEVA != null)
            {
                string imageName = imageEEVA.Value; //take name if exists the image
                var image = db2.Images.FirstOrDefault(img => img.Name == imageName);
                if (image != null)
                {
                    binaryImage = image.BinaryImage;
                    var base64 = Convert.ToBase64String(binaryImage);
                    var imgSrc = String.Format("data:image/gif;base64,{0}", base64);

                }
            }


            
            return binaryImage;
                
               
        }


        public System.Collections.Generic.IEnumerable<Deal> ManageDeals(int pubId)
        {
            var myDeals = db.Deals.Where(d=>d.FK_Pub==pubId);
            return myDeals.OrderByDescending(entries => entries.PublicationDate).Take(20).ToList();//20 last  entries of the database sorted by DateAdded*/
        }
    }
}
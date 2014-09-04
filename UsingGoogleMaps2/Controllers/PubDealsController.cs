using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsingGoogleMaps2.Models;

namespace UsingGoogleMaps2.Controllers
{
    public class PubDealsController : Controller
    {

        private PubDealsDBEntities db = new PubDealsDBEntities();
        private IRepository _repository;
        //
        // GET: /ListUpdDel/
        public PubDealsController()
        {
            _repository = new PubDealRepository();

        }
        public PubDealsController(IRepository repository)
        {

            _repository = repository;
        }

        //public ActionResult Search()
        //{

        //    ViewBag.Areas = new SelectList(SearchPreferences.Areas);
        //    ViewBag.MaxPrices = new SelectList(SearchPreferences.MaxPrices);
        //    ViewBag.PropertyTypes = new SelectList(SearchPreferences.PropertyTypes);
        //    return View();
        //}


       [Authorize]
        public ActionResult RegisterPub()
        {
           _repository.UserName= User.Identity.Name;/*Comment for testing with fakerepository*/ 
            ViewBag.Area = new SelectList(SearchPreferences.Areas);
            ViewBag.Category = new SelectList(SearchPreferences.PropertyTypes);
            return View();
        }

        [HttpPost]
        public ActionResult RegisterPub(Pub pub)
        {
            var latLng = pub.LatLng;
              
            
            
            if (ModelState.IsValid)
                {
                    _repository.UserName = User.Identity.Name;/*Comment for testing*/
                    _repository.RegisterPub(pub);

                    return RedirectToAction("ThankYouRegisterPub", "PubDeals");

                }

               
                return View();
            }

        public ActionResult ThankYouRegisterPub()
        {
            return View();
        }

        public ActionResult ThankYouPostNewDeal()
        {
            return View();
        }

        [Authorize]
        public ActionResult MyPubDeals()
        {
          
            return View();
        }
         [Authorize]
        public ActionResult ManagePubs()// list myPubs to select one to interapt with
        {
             _repository.UserName = User.Identity.Name;/*Comment for testing with fakerepository*/

            var myPubs = _repository.ListMyPubs();

            return View(myPubs);
          
        }
         public ActionResult Manage(int id = 0)
         {
             Pub pub = _repository.Manage(id);

             ViewBag.PubId = pub.Id;
             if (pub == null)
             {
                 return HttpNotFound();
             }

             
             return View();
         }

         public ActionResult CreateNewDeal(int id=0)
         {
             ViewBag.DayOfWeeK = new SelectList(SearchPreferences.DayOfWeek);
             ViewBag.PubId =id;
             
             return View();
         }
         [HttpPost]
         public ActionResult CreateNewDeal(Deal deal)
         {
             
            if (ModelState.IsValid)
             {
                 //_repository.UserName = User.Identity.Name;/*Comment for testing*/
                 _repository.CreateDeal(deal);

                 return RedirectToAction("ThankYouPostNewDeal", "PubDeals");

             }


             return View();
         }
         public ActionResult CreateNewDealWithPicture(int id = 0)
         {
             ViewBag.DayOfWeeK = new SelectList(SearchPreferences.DayOfWeek);
             ViewBag.PubId = id;
             return View();
         }
         [HttpPost]
         public ActionResult CreateNewDealWithPicture(TemporayDeal deal)
         {

             if (ModelState.IsValid)
             {
                 //_repository.UserName = User.Identity.Name;/*Comment for testing*/
                 _repository.CreateDealWithImage(deal);

                 return RedirectToAction("ThankYouPostNewDeal", "PubDeals");

             }


             return View(deal);
         }
         public ActionResult SearchDeal()
         {
             ViewBag.Area = new SelectList(SearchPreferences.Areas);
             ViewBag.PropertyType = new SelectList(SearchPreferences.PropertyTypes);
             ViewBag.PriceMax = new SelectList(SearchPreferences.MaxPrices);
             return View();
         }
        [HttpPost] 
        public ActionResult SearchDeal(SearchPreferences searchPreferences)
         {
             if (ModelState.IsValid)
             {
               
                 var results = _repository.SearchResults(searchPreferences);

                //to avoid losing data in case you refresh the browser on next page. Using Tempdata you can lose them it
                 Session["PubsToShowOnMap"] = results;
                 //TempData["results"] = results;
                 
                 return RedirectToAction("ShowResultsOnMap", "PubDeals");

             }


             return View();
         }
        public ActionResult ShowResultsOnMap( )
        {
            //object results = TempData["results"];
            //SearchResults resultBack = (SearchResults)results;
            object results = Session["PubsToShowOnMap"];
            SearchResults resultBack =(SearchResults)Session["PubsToShowOnMap"];
            List<SearchResult> list = resultBack.GetSearchListSearchResult;
            return View(list);
        }

       
        public ActionResult UpdatePubInformation()
        {

            return View();
        }

     
        //
        // GET: /Maps/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadImage()
        {
            return View();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UsingGoogleMaps2.Models;
/*papal try*/
using System.Net;
using System.Text;
using System.IO;


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



        [Authorize]
        public ActionResult RegisterPub()
        {

            if (_repository.UserName != "For testing")
                _repository.UserName = User.Identity.Name;
            ViewBag.Area = new SelectList(SearchPreferences.Areas);
            ViewBag.Category = new SelectList(SearchPreferences.PropertyTypes);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterPub(Pub pub)
        {

            if (ModelState.IsValid)
            {
                if (_repository.UserName != "For testing")
                    _repository.UserName = User.Identity.Name;
                _repository.RegisterPub(pub);

                return RedirectToAction("ThankYouRegisterPub", "PubDeals");

            }
            if (_repository.UserName != "For testing")
                _repository.UserName = User.Identity.Name;
            ViewBag.Area = new SelectList(SearchPreferences.Areas);
            ViewBag.Category = new SelectList(SearchPreferences.PropertyTypes);


            return View(pub);
        }

        public ActionResult ThankYouRegisterPub()
        {
           
            return View();
        }

        public ActionResult ThankYouPostNewDeal(int id = 0)
        {
            ViewBag.PubId = id;
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
            if (_repository.UserName != "For testing")
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

      
        public ActionResult CreateNewDealWithPicture(int id = 0)
        {
            ViewBag.DayOfWeeK = new SelectList(SearchPreferences.DayOfWeek);
            ViewBag.PubId = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewDealWithPicture(TemporayDeal deal)
        {

            if (ModelState.IsValid)
            {
                //_repository.UserName = User.Identity.Name;/*Comment for testing*/
                _repository.CreateDealWithImage(deal);

                return RedirectToAction("ThankYouPostNewDeal", "PubDeals", new { id = deal.FK_Pub });

            }

            ViewBag.DayOfWeeK = new SelectList(SearchPreferences.DayOfWeek);
            return View(deal);
        }
        public ActionResult SearchDeal()
        {
            ViewBag.Area = new SelectList(SearchPreferences.Areas);
            ViewBag.PropertyType = new SelectList(SearchPreferences.PropertyTypes);
            ViewBag.PriceMax = new SelectList(SearchPreferences.MaxPrices);
            ViewBag.MaxDistance = new SelectList(SearchPreferences.MaxDistances);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult ShowResultsOnMap()
        {
            //object results = TempData["results"];
            //SearchResults resultBack = (SearchResults)results;
            if (_repository.UserName != "For testing")
            {
                object results = Session["PubsToShowOnMap"];
                SearchResults resultBack = (SearchResults)Session["PubsToShowOnMap"];
                List<SearchResult> list = resultBack.GetSearchListSearchResult;
                return View(list);
            }
            else return View(new List<SearchResult>());
        }


        //public ActionResult UpdatePubInformation()
        //{

        //    return View();
        //}


        [Authorize]
        public ActionResult EnterLatLongDUblinCoordinatesTable()
        {
            if (_repository.UserName != "For testing")
                _repository.UserName = User.Identity.Name;/*Comment for testing with fakerepository*/
            ViewBag.Area = new SelectList(SearchPreferences.Areas);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnterLatLongDUblinCoordinatesTable(DublinAreasCoordinate areaCoordinates)
        {

            if (ModelState.IsValid)
            {
                if (_repository.UserName != "For testing")
                    _repository.UserName = User.Identity.Name;/*Comment for testing*/
                _repository.EnterDublinAreaCoordinates(areaCoordinates);

                return RedirectToAction("ThankYouRegisterPub", "PubDeals");

            }
            if (_repository.UserName != "For testing")
                _repository.UserName = User.Identity.Name;/*Comment for testing with fakerepository*/
            ViewBag.Area = new SelectList(SearchPreferences.Areas);

            return View(areaCoordinates);
        }

       
       //For general users
        public ActionResult Details(int id = 0)
        {

            DealDetails dealDetails = _repository.Details(id);

            //if (dealDetails == null)
            //{
            //    return HttpNotFound();
            //}

            return View(dealDetails);
        }
        //For Pub's owners 
        [Authorize]
        public ActionResult DealPubOwnerDetails(int id = 0)
        {

            DealDetails dealDetails = _repository.Details(id);

            //if (dealDetails == null)
            //{
            //    return HttpNotFound();
            //}

            return View(dealDetails);
        }
        public ActionResult CurrentTransactions(int id=0)
        {
            var transactions = _repository.CurrentTransactions(id);
            return View(transactions);
        }
       

        public ActionResult ManageDeals(int id = 0)
        {
            ViewBag.PubId = id;
            var lastDeals = _repository.ManageDeals(id);

            //if (dealDetails == null)
            //{
            //    return HttpNotFound();
            //}

            return View(lastDeals);
        }

        [Authorize]
        public ActionResult Delete(int id = 0)
        {
            Deal entry = _repository.Delete(id);

            if (entry == null)
            {
                return HttpNotFound();
            }
            return View(entry);
        }


        // POST: /ListUpdDel/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var pubId = 0;
            var dealTemp = db.Deals.FirstOrDefault(deal => deal.Id == id);
            if (dealTemp != null)
            {
                pubId = dealTemp.Pub.Id;
            }
            bool entryFound = _repository.DeleteConfirmed(id);
            if (entryFound)
            {
                ViewBag.pubId = pubId;
                return RedirectToAction("ThankYouDelete", new { id = pubId });
            }
            else return RedirectToAction("ManageDeals", new { id =pubId });

        }
        public ActionResult ThankYouDelete(int id=0)
        {
            ViewBag.pubId = id;
            return View();
        }

        
       
    }
}

  
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

                return RedirectToAction("ThankYouPostNewDeal", "PubDeals");

            }

            ViewBag.DayOfWeeK = new SelectList(SearchPreferences.DayOfWeek);
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
        public ActionResult Details2(int id = 0)
        {

            DealDetails dealDetails = _repository.Details(id);

            //if (dealDetails == null)
            //{
            //    return HttpNotFound();
            //}

            return View(dealDetails);
        }

        public ActionResult ManageDeals(int id = 0)
        {

            var lastDeals = _repository.ManageDeals(id);

            //if (dealDetails == null)
            //{
            //    return HttpNotFound();
            //}

            return View(lastDeals);
        }
/**************************************************************PayPal manage responses***********************************************/
        
        public ActionResult IPN()
        {

            var order = new Order(); // this is something I have defined in order to save the order in the database
            //DealEEVA order=new DealEEVA();
            // Receive IPN request from PayPal and parse all the variables returned
            var formVals = new Dictionary<string, string>();
            formVals.Add("cmd", "_notify-synch"); //notify-synch_notify-validate
            formVals.Add("at", "this is a long token found in Buyers account"); // this has to be adjusted
            formVals.Add("tx", Request["tx"]);

            // if you want to use the PayPal sandbox change this from false to true
            string response = GetPayPalResponse(formVals, true);

            if (response.Contains("SUCCESS"))
            {
                string transactionID = GetPDTValue(response, "txn_id"); // txn_id //d
                string sAmountPaid = GetPDTValue(response, "mc_gross"); // d
                string deviceID = GetPDTValue(response, "custom"); // d
                string payerEmail = GetPDTValue(response, "payer_email"); // d
                string Item = GetPDTValue(response, "item_name");
                //added
                int dealId = Convert.ToInt32(GetPDTValue(response, "item_number"));
                Deal dealToCompare = db.Deals.FirstOrDefault(d => d.Id == dealId);
                var amount = dealToCompare.Price;
                DealEEVA transactionExist = new DealEEVA();

                //validate the order
                Decimal amountPaid = 0;
                Decimal.TryParse(sAmountPaid, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out amountPaid);
                //retrieve DealEEVA "transactions"
                if (dealToCompare != null)//deal exist
                {
                    var dealEEVAs = db.DealEEVAs.Where(d => d.FK_Deal == dealToCompare.Id);
                    transactionExist = dealEEVAs.FirstOrDefault(d => d.Attribute == "Transaction");

                    //else transactionExist = null;


                    if (amountPaid == amount)  // you might want to have a bigger than or equal to sign here!
                    {
                        if (transactionExist != null && transactionExist.Value == transactionID)
                        {

                            //if we are here, the user must have already used the transaction ID for an account
                            //you might want to show the details of the order, but do not upgrade it!
                        }
                        else
                        {
                            //if the transactionID is not found in the database, add it
                            //and save info in db
                            DealEEVA dEEVa = new DealEEVA();
                            dEEVa.FK_Deal = dealToCompare.Id;
                            dEEVa.Attribute = "Transaction";
                            dEEVa.Value = transactionID;
                            //Add one to the vouchers sold
                            dealToCompare.VouchersSold += 1;
                            //save 
                            db.DealEEVAs.Add(dEEVa);
                             db.SaveChanges();

                        }
                        // take the information returned and store this into a subscription table
                        // this is where you would update your database with the details of the tran

                        //return View();
                    }
                    else
                    {
                        // let fail - this is the IPN so there is no viewer
                        // you may want to log something here
                        DealEEVA dEEVaError = new DealEEVA();
                        DealEEVA dEEVa = new DealEEVA();
                        dEEVaError.FK_Deal = dealToCompare.Id;
                        dEEVa.FK_Deal = dealToCompare.Id;
                        dEEVa.Attribute = "Transaction";
                        dEEVa.Value = transactionID;

                        //another EEva ErrorEventArgs related CreateNewDealWithPicture transaction
                        dEEVaError.Attribute = transactionID;
                        dEEVaError.Value = "Error ammount. Ammount Paid=" + amountPaid + ". Ammount to pay= " + dealToCompare.Price;


                        // since the user did not pay the right amount, we still want to log that for future reference.

                        db.DealEEVAs.Add(dEEVa);
                        db.DealEEVAs.Add(dEEVaError);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                //error
            }
           
            return View();
        }

        string GetPayPalResponse(Dictionary<string, string> formVals, bool useSandbox)
        {

            string paypalUrl = useSandbox ? "https://www.sandbox.paypal.com/cgi-bin/webscr"
                : "https://www.paypal.com/cgi-bin/webscr";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(paypalUrl);

            // Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            byte[] param = Request.BinaryRead(Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);

            StringBuilder sb = new StringBuilder();
            sb.Append(strRequest);

            foreach (string key in formVals.Keys)
            {
                sb.AppendFormat("&{0}={1}", key, formVals[key]);
            }
            strRequest += sb.ToString();
            req.ContentLength = strRequest.Length;

            //for proxy
            //WebProxy proxy = new WebProxy(new Uri("http://urlort#");
            //req.Proxy = proxy;
            //Send the request to PayPal and get the response
            string response = "";
            using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII))
            {

                streamOut.Write(strRequest);
                streamOut.Close();
                using (StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    response = streamIn.ReadToEnd();
                }
            }

            return response;
        }
        string GetPDTValue(string pdt, string key)
        {

            string[] keys = pdt.Split('\n');
            string thisVal = "";
            string thisKey = "";
            foreach (string s in keys)
            {
                string[] bits = s.Split('=');
                if (bits.Length > 1)
                {
                    thisVal = bits[1];
                    thisKey = bits[0];
                    if (thisKey.Equals(key, StringComparison.InvariantCultureIgnoreCase))
                        break;
                }
            }
            return thisVal;

        }
    }
}

  
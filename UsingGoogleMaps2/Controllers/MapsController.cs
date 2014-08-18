using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UsingGoogleMaps2.Models;

namespace UsingGoogleMaps2.Controllers
{
    public class MapsController : Controller
    {
        
       private IRepository _repository;
        //
        // GET: /ListUpdDel/
        public MapsController()
        {
            _repository = new FakeRepository();

        }
        public MapsController(IRepository repository)
        {

            _repository = repository;
        }

        public ActionResult Search()
        {

            ViewBag.Areas = new SelectList(SearchPreferences.Areas);
            ViewBag.MaxPrices = new SelectList(SearchPreferences.MaxPrices);
            ViewBag.PropertyTypes = new SelectList(SearchPreferences.PropertyTypes);
            return View();
        }

       
        
        
        public ActionResult ShowAllPubsOnMap()
        {


            var PubEntries = _repository.ShowAllPubsOnMap();
            return View(PubEntries);
           

        }

        public ActionResult ShowPubsOnMap(AreaToSearsh arreaToSearch)
        {
            var PubEntries = _repository.ShowPubsOnMap(arreaToSearch.Postcode);
            return View(PubEntries);
        }

       

        

        
          
       
        //
        // GET: /Maps/

        public ActionResult Index()
        {
            return View();
        }

    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UsingGoogleMaps2.Models;
using System.Linq;
using UsingGoogleMaps2.Controllers;
//using System.Data.Entity.Validation;

using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeleteUpdateItemsDb;
using DeleteUpdateItemsDb.Models;
using DeleteUpdateItemsDb.Controllers;
using System.Linq;
using System.Web.Mvc;


namespace UsingGoogleMaps2.Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]

        public void Get_Id_Known_User_Return_ID_Pubs_from_companyFakeTable()
        {

            var repository = new FakeRepository();
            var actual = repository.GetCompanyId("Fernando's company");
            var expected = 1;//ID
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void List_Pubs_Known_User_Return_2_Pubs_from_pubFakeTable()
        {
            
            var repository=new FakeRepository();
            var result = repository.ListMyPubs();
            var expected = 2;
            Assert.AreEqual(expected, result.Count());
        }

        [TestMethod]
        public void Pass_especific_PubId_Return_Pub_from_pubFakeTable_That_Matches()
        {

            var repository = new FakeRepository();
            var result = repository.Manage(1);
            var nameExpected = "The Auld Dubliner";
            var actualName = result.Name;
            Assert.AreEqual(nameExpected, actualName);
        }
        [TestMethod]
        public void Register_new_Pub_For_Especific_Company()
        {

            var repository = new FakeRepository();
            Pub newPub = new Pub { Id = 3, Name = "The new Pub", Address = "50 Temple Bar, Dublin 2", Category = "Pub", Area = "Temple Bar", LatLng = "(53.345661, -6.264200)", DistanceTillAreaCenter = 53.8, ContactEmail = "fernando.guardiola.888@gmail.com", NegotiatedPercentage = 5 };
            repository.RegisterPub(newPub);
            var actual = FakeRepository.pubFakeTable.Count();
            var expected = 3;//number of pubs after adding newPub
            Assert.AreEqual(expected, actual);
            FakeRepository.pubFakeTable.RemoveAt(2);//delete pub added
        }

        [TestMethod]
        public void Create_Deal_for_specific_Pub_without_Image()
        {

            var repository = new FakeRepository();

            TemporayDeal tpD = new TemporayDeal {File=null,FK_Pub = 1, Description = "Any 6 pints 22 euro", Price = 22, PublicationDate = new DateTime(2014, 9, 3, 16, 5, 7, 123), StartDate = new DateTime(2014, 9, 3, 16, 5, 7, 123), EndDate = new DateTime(2014, 10, 15, 16, 5, 7, 123), DayOfWeeK = "Every Thursday", VouchersForSale = 35 };
            repository.CreateDealWithImage(tpD);
            // a new deal has been added witout creating a dealEEVA
            var actual = FakeRepository.dealFakeTable.Count();
            var expected = 4;//number of deals after adding new Deal
            Assert.AreEqual(expected, actual);
            FakeRepository.dealFakeTable.RemoveAt(3);//delete deal added
        }
        [TestMethod]
        public void Set_Search_preferences_Return_Deals_to_show_on_map_that_matches()
        {

            var repository = new FakeRepository();
            SearchPreferences sP=new SearchPreferences{ Area="Temple Bar", PriceMax="No max", PropertyType="Pub"};// It has to return three results
            var results=repository.SearchResults(sP);// It has to return three results
           
            var actual = results.GetSearchListSearchResult.Count();
            var expected = 3;//number of deals that match the search preferences
            Assert.AreEqual(expected, actual);
            
        }
        [TestMethod]
        public void Store_new_dublin_area_Coordinates()
        {

            var repository = new FakeRepository();
            DublinAreasCoordinate DAC = new DublinAreasCoordinate() { Id = 1, Area = "Temple Bar", LatLng = "(53.3450903, -6.263803199999984)" };
            repository.EnterDublinAreaCoordinates(DAC);

            var actual = FakeRepository.dublinAreaCoordinatesFakeTable.Count();
            var expected = 1;//Enter dublin area coordinates in fake table
            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void Test_Distance_Algorithm_passing_same_LatLng_Result_0_meters()
        {
            double lat1 = 53.3450903;
            double lat2=53.3450903; 
            double lng1=-6.263803199999984;
            double lng2=-6.263803199999984;
            double actual = DistanceAlgorithm.DistanceBetweenPlaces(lng1, lat1, lng2, lat2);
            var expected = 0.0;
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void Render_ThankYouPostNewDeal_View()
        {
            // Arrange
            PubDealsController controller = new PubDealsController();
            // Act
            ViewResult result = controller.ThankYouUpdate() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Render_ThankYouRegisterPub_View()
        {
            // Arrange
            JobEmergencyController controller = new JobEmergencyController();
            // Act
            ViewResult result = controller.ThankYouUpdate() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        
    }
}

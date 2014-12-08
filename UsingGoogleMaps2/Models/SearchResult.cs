using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsingGoogleMaps2.Models
{
    public class SearchResult
    {
        public string InfoWindowContentString{ get; set; }
        public string PubName { get; set; }
        public string PubAddress { get; set; }
        public byte[] DealImage { get; set; }
        public string Description { get; set; }
        public string LatLng { get; set; }
        //Deal id to pass to details view
        public int DealId{get; set; }

        public void SetContentString ()
        {
            string imgString="";
            string pubNameString="";
            string pubAddressString="";
            string descriptionString="";
            string linkToDeatils = "<a href=\"/PubDeals/Details/" + DealId + "\">Details</a>";
           
           
            if (DealImage!=null)
            {
                var base64 = Convert.ToBase64String(DealImage);
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                //string str = "\"How to add doublequotes\""
                imgString = "<style>IMG.MaxSized {max-width: 100px;max-height: 100px</style><div><img class=\"MaxSized\" src=\"" + imgSrc + "\" /></div>";
            }
            
            
            if (!String.IsNullOrEmpty(PubName)) 
            {
                pubNameString = "<h2 class=\"infoWindow\">" + PubName + "</h2>";
            }
            if (!String.IsNullOrEmpty(PubAddress))
            {
                pubAddressString = "<h3 class=\"infoWindow\">" + PubAddress + "</h3>";
            }
            if (!String.IsNullOrEmpty(Description))
            {
                descriptionString = "<h5 class=\"infoWindow\">" + Description + "</h5>";
            }

            
            InfoWindowContentString=imgString + "<div>" + pubNameString + pubAddressString + descriptionString +linkToDeatils+ "</div>";
            
        }

    
    }
}
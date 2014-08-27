using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace UsingGoogleMaps2.Models
{
    
    

    
    public class Register
    {
        [Required]
        [Display(Name = "Pub's name")]
        public string PubName { get; set; }
        [Required]
        [Display(Name = "Pub's category")]
        public string PubCategory { get; set; }
        [Required]
        [Display(Name = "Pub's area")]
        public string PubArea { get; set; }
        [Required]
        public string LatLng { get; set; }
        [Required]
        [Display(Name = "Pub's address")]
        public string PubAddress { get; set; }
         [Required]
        [Display(Name = "Pub's distance till area center")]
        public double DistanceTillAreaCenter { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Pub's contact email")]
        public string ContactEmail { get; set; }

        
    }
}
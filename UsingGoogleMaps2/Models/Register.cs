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
        [Display(Name = "Company's name")]
        public string CompanyName { get; set; }
        [Required]
        [Display(Name = "Company's county")]
        public string CompanyCounty { get; set; }
        [Required]
        public string LatLng { get; set; }
        [Required]
        [Display(Name = "Company's address")]
        public string CompanyAddress { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Company's contact phone")]
        public string CompanyPhone { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Company's contact email")]
        public string CompanyEmail { get; set; }

        
    }
}
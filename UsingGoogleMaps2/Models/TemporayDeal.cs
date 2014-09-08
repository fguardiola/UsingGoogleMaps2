using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UsingGoogleMaps2.Models
{
    // Class to allow to upload a picture (EEVA of the deal) during the process of posting a new deal
    public class TemporayDeal
    {
        [Display(Name = "Image to upload")]
        public HttpPostedFileBase File { get; set; }
        [Required]
        
        public int FK_Pub { get; set; }
        
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Price")]
        [Required]
        public decimal Price { get; set; }

        public System.DateTime PublicationDate { get; set; }
        [Required]
        [DateCustomValidation(ErrorMessage = "Date selected must be on or after today")]//Custom validation
        [DataType(DataType.Date)]
        [Display(Name = "Valid from")]
        public System.DateTime StartDate { get; set; }
        [Required]
        [DateCustomValidation(ErrorMessage = "Date selected must be on or after today")]//Custom validation
        [DataType(DataType.Date)]
        [Display(Name = "Valid Until")]
        public System.DateTime EndDate { get; set; }
        [Display(Name = "Day of the week")]
        public string DayOfWeeK { get; set; }
        [Required]
        [Display(Name = "Number of vouchers to sell")]
        public int VouchersForSale { get; set; }
    
    
    }
}
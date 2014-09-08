using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace UsingGoogleMaps2.Models
{
    public class AreaCoordinatesEntity
    {
      
        [Required]
        public string Area { get; set; }
        [Required]
        public string LatLng { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
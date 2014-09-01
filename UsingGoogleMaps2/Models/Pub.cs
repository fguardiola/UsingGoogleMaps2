//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;
namespace UsingGoogleMaps2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pub
    {
        public Pub()
        {
            this.Deals = new HashSet<Deal>();
            this.PubEEVAs = new HashSet<PubEEVA>();
        }
    
        public int Id { get; set; }
        public int FK_Company { get; set; }
        [Required]
        [StringLength(128, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Area { get; set; }
        [Required]
        public string LatLng { get; set; }
        public double DistanceTillAreaCenter { get; set; }
        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; }
        public Nullable<double> NegotiatedPercentage { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual ICollection<Deal> Deals { get; set; }
        public virtual ICollection<PubEEVA> PubEEVAs { get; set; }
    }
}

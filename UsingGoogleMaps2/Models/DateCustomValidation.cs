using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace UsingGoogleMaps2.Models
{
    public class DateCustomValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var dateEntered = (DateTime)value;
            var today = DateTime.Today;

            if (dateEntered >= today) return true;
            else return false;
           
        }

    }



}
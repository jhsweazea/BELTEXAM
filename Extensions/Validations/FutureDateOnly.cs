using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace BELTEXAM.Extensions
{
    public class FutureDateOnly : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime todayDate = Convert.ToDateTime(value);
            return todayDate >= DateTime.Now;
        }
    }
}
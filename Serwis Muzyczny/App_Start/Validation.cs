using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Serwis_Muzyczny.App_Start
{
    public class Validation
    {
        public class FutureDateAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                    return value != null && (DateTime)value <= DateTime.Now.Date;             
            }
        }
    }
}
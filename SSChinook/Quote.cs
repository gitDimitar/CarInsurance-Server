using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarInsurance
{

    public class Quote
    {

        [AutoIncrement]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int NoClaimsBonus { get; set; }
        public string LicenceType { get; set; }
        public int PenaltyPoints { get; set; }
        public Car Car { get; set; }
        public double ThirdPartyCover { get; set; }
        public double MonthlyThirdPartyCover { get; set; }
        public double FullyCompCover { get; set; }
        public double MonthlyFullyCompCover { get; set; }
  
    }

}
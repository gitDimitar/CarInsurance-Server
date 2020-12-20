using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace CarInsurance
{

    /*
    [Route("/Quote", Verbs = "GET")]
    public class QuoteListRequest { }
    */

    [Route("/Quote", Verbs = "POST")]
    public class PostQuoteRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int NoClaimsBonus { get; set; }
        public string LicenceType { get; set; }
        public int PenaltyPoints { get; set; }
        public string CarReg { get; set; }
    }

    [Route("/Quote", Verbs = "PUT")]
    public class PutQuoteRequest
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int NoClaimsBonus { get; set; }
        public string LicenceType { get; set; }
        public int PenaltyPoints { get; set; }
        public string CarReg { get; set; }
        public double ThirdPartyCover { get; set; }
        public double MonthlyThirdPartyCover { get; set; }
        public double FullyCompCover { get; set; }
        public double MonthlyFullyCompCover { get; set; }
    }

    [Route("/Quote/Id/{ID}", Verbs = "GET, DELETE")]
    public class QuoteIDRequest
    {
        public int ID { get; set; }
    }

    [Route("/Quote/Name/{Name}", Verbs = "GET")]
    public class QuoteByNameRequest
    {
        public string Name { get; set; }
    }

}
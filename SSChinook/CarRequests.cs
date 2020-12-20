using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace CarInsurance
{

    [Route("/Car", Verbs = "GET")]
    public class CarListRequest { }

    [Route("/Car", Verbs = "POST, PUT")]
    public class CarRequest
    {
        public string ID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public double EngineSize { get; set; }
    }

    [Route("/Car/Id/{ID}", Verbs = "GET, DELETE")]
    public class CarByIDRequest 
    {
        public string ID { get; set; }
    }

    [Route("/Car/Make", Verbs = "GET")]
    public class CarMakesRequest { }

    [Route("/Car/{Make}", Verbs = "GET")]
    public class CarModelsRequest 
    { 
        public string Make { get; set; }
    }

    [Route("/Car/{Make}/{Model}", Verbs = "GET")]
    public class CarEngineRequest 
    { 
        public string Make { get; set; }
        public string Model { get; set; }
    }

}
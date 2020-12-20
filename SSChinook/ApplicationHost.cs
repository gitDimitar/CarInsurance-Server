using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using System.Data;
using ServiceStack.OrmLite;
using Funq;
using ServiceStack.Data;
using ServiceStack.OrmLite.Sqlite;
using System.Diagnostics;



namespace CarInsurance
{

    public class ApplicationHost : AppHostHttpListenerBase
    {
        
        public ApplicationHost() 
            : base("CarInsurance", typeof(ApplicationHost).Assembly)
        {}

        public static string SqliteFileDb = "~/Insurance.sqlite".MapHostAbsolutePath();

        public override void Configure(Funq.Container container)
        {
            ServiceStack.OrmLite.OrmLiteConfig.DialectProvider  = new SqliteOrmLiteDialectProvider();
     
            container.Register<IDbConnectionFactory>
            (new OrmLiteConnectionFactory(SqliteFileDb, SqliteDialect.Provider));
            
            using (var Db = container.Resolve<IDbConnectionFactory>().Open())
            {
                Db.DropAndCreateTable<Car>();
                List<Car> cars = new List<Car>();
                cars.Add(new Car(){ Id="01MN6483", Make="AUDI", Model="A4", EngineSize=1.9 });
                cars.Add(new Car(){ Id="03MN6079", Make="VAUXHALL", Model="VECTRA", EngineSize=2.0 });
                cars.Add(new Car(){ Id="07DL543", Make="BMW", Model="320D", EngineSize=2.0 });
                cars.Add(new Car(){ Id="10D54932", Make="FORD", Model="MONDEO", EngineSize=1.9 });
                cars.Add(new Car(){ Id="141LH142", Make="VOLKSWAGEN", Model="GOLD", EngineSize=1.6 });
                cars.Add(new Car(){ Id="142LH82", Make="VOLKSWAGEN", Model="JETTA", EngineSize=1.9 });
                cars.Add(new Car(){ Id="97CN3214", Make="SEAT", Model="IBIZA", EngineSize=1.0 });
                cars.Add(new Car(){ Id="08C6492", Make="SEAT", Model="LEON", EngineSize=1.9 });
                cars.Add(new Car(){ Id="10MN593", Make="VAUXHALL", Model="INSIGNIA", EngineSize=2.0 });
                cars.Add(new Car(){ Id="06MH535", Make="TOYOTA", Model="AVENSIS", EngineSize=2.0 });
                cars.Add(new Car(){ Id="05MH4525", Make="SEAT", Model="LEON", EngineSize=1.4 });
                Db.InsertAll<Car>(cars);

                Db.DropAndCreateTable<Quote>();
                //Db.CreateTable<Quote>(overwrite: false);
                Quote testQuote1 = new Quote() {
                    FirstName = "Joe",
                    LastName = "Bloggs",
                    Age = 25,
                    NoClaimsBonus = 0,
                    LicenceType = "Provisional",
                    PenaltyPoints = 0,
                    Car = cars[0],
                    ThirdPartyCover = 1000.0,
                    MonthlyThirdPartyCover = 0.0,
                    FullyCompCover = 0.0,
                    MonthlyFullyCompCover = 0.0
                };
                Db.Insert<Quote>(testQuote1);

                Quote testQuote2 = new Quote() {
                    FirstName = "Jane",
                    LastName = "Doe",
                    Age = 30,
                    NoClaimsBonus = 3,
                    LicenceType = "Full",
                    PenaltyPoints = 0,
                    Car = cars[3],
                    ThirdPartyCover = 400.0,
                    MonthlyThirdPartyCover = 0.0,
                    FullyCompCover = 0.0,
                    MonthlyFullyCompCover = 0.0
                };
                Db.Insert<Quote>(testQuote2);

                Debug.WriteLine("Database Loaded...");
            }
        }

    }

}
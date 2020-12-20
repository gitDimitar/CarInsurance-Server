using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using System.Diagnostics;

namespace CarInsurance
{
    
    public class QuoteService : Service
    {

        /*
        public List<Quote> Get(QuoteListRequest request)
        {
            QuoteDao DAO = new QuoteDao(Db);
            return (DAO.GetAllQuotes());
        }
        */
 
        public Quote Post(PostQuoteRequest request)
        {
            CarDao CAR_DAO = new CarDao(Db);
            var car = CAR_DAO.GetCarByID(request.CarReg);

            var quote = new Quote()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                NoClaimsBonus = request.NoClaimsBonus,
                LicenceType = request.LicenceType,
                PenaltyPoints = request.PenaltyPoints,
                Car = car
            };

            quote.ThirdPartyCover = GetThirdPartyQuote(quote);
            quote.MonthlyThirdPartyCover = GetMonthlyThirdPartyQuote(quote);
            quote.FullyCompCover = GetFullyCompQuote(quote);
            quote.MonthlyFullyCompCover = GetMonthlyFullyCompQuote(quote);

            QuoteDao DAO = new QuoteDao(Db);
            return (DAO.AddQuote(quote));
        }
 
        public Quote Put(PutQuoteRequest request)
        {
            CarDao CAR_DAO = new CarDao(Db);
            Car car = CAR_DAO.GetCarByID(request.CarReg);

            var quote = new Quote()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                NoClaimsBonus = request.NoClaimsBonus,
                LicenceType = request.LicenceType,
                PenaltyPoints = request.PenaltyPoints,
                Car = car,
                ThirdPartyCover = request.ThirdPartyCover,
                MonthlyThirdPartyCover = request.MonthlyThirdPartyCover,
                FullyCompCover = request.FullyCompCover,
                MonthlyFullyCompCover = request.MonthlyFullyCompCover 
            };

            QuoteDao DAO = new QuoteDao(Db);
            return (DAO.UpdateQuote(quote));
        }

        public Quote Get(QuoteIDRequest request)
        {
            QuoteDao DAO = new QuoteDao(Db); 
            return (DAO.GetQuote(request.ID));
        }

        public void Delete(QuoteIDRequest request)
        {
            QuoteDao DAO = new QuoteDao(Db);
            DAO.DeleteQuote(request.ID);
        }

        public List<Quote> Get(QuoteByNameRequest request)
        {
            QuoteDao DAO = new QuoteDao(Db);
            return (DAO.GetQuote(request.Name));
        }



        #region Calculations
        public double GetThirdPartyQuote(Quote quote) 
        {
            double Total = 0.0;

            int Age = quote.Age;
            if (Age >= 18) 
            {
                //Set basic cover by the users age
                if (Age > 60)
                {
                    Total = 2000.0;
                }
                else if (Age > 30) {
                    Total = 1000.0;
                }
                else 
                {
                    Total = 3000.0;
                }

                //Factor in the users licence type
                string LicenceType = quote.LicenceType;
                if (LicenceType.ToUpper().Equals("FULL")) 
                {
                    Total *= 0.9;
                }

                //Factor in the users penalty points
                int PenaltyPoints = quote.PenaltyPoints;
                if (PenaltyPoints > 0)
                {
                    for (int i = 0; i < PenaltyPoints; i++) 
                    {
                        Total *= 1.05;
                    }
                }
                else 
                {
                    //Factor in the users no claims bonus
                    int NoClaimsBonus = quote.NoClaimsBonus;
                    if (NoClaimsBonus > 0)
                    {
                        for (int i = 0; i < NoClaimsBonus; i++) 
                        {
                            Total *= 0.9;
                        }    
                    }
                }

                Car car = quote.Car;
                if (car != null)
                {
                    //Factor in the cars engine size
                    double EngineSize = car.EngineSize;
                    if (EngineSize > 1.6) 
                    {
                        Total *= 0.1;
                    }
                    else if (EngineSize > 1.2)
                    {
                        Total *= 0.05;
                    }


                    //Factor in the cars age
                    string Reg = car.Id;
                    Reg = Reg.Substring(0, 2);

                    int Year = int.Parse(Reg);
                    if (Year < 10 || Year > 20)
                    {
                        Total *= 0.1;
                    }
                }
            }

            return (Total);
        }

        public double GetMonthlyThirdPartyQuote(Quote quote) 
        {
            double Total = GetThirdPartyQuote(quote);
            return ((Total / 12) * 0.05);
        }

        public double GetFullyCompQuote(Quote quote) 
        {
            double Total = GetThirdPartyQuote(quote);
            return (Total * 1.5);
        }
        
        public double GetMonthlyFullyCompQuote(Quote quote) 
        {
            double Total = GetFullyCompQuote(quote);
            return ((Total / 12) * 0.05);
        }
        #endregion
    }

}
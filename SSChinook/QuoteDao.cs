using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Data;
using ServiceStack;
using ServiceStack.OrmLite;
using System.Diagnostics;

namespace CarInsurance
{

    public class QuoteDao : Service
    {

        System.Data.IDbConnection DB;

        public QuoteDao(System.Data.IDbConnection quotes)
        {
            DB = quotes;
        }

        public Quote AddQuote(Quote quote)
        {
            int ID = (int) DB.Insert<Quote>(quote, selectIdentity: true);
            return (GetQuote(ID));
        }

        public List<Quote> GetAllQuotes()
        {
            return (DB.Select<Quote>());
        }

        public Quote GetQuote(int ID)
        {
            return (Db.SingleById<Quote>(ID));
        }

        public List<Quote> GetQuote(string Name)
        {
            Debug.WriteLine(Name);
            return (Db.Select<Quote>("SELECT * FROM quote WHERE UPPER(FirstName || ' ' || LastName) LIKE UPPER('" + Name + "%')"));
        }

        public Quote UpdateQuote(Quote quote)
        {
            DB.Update<Quote>(quote);
            return (quote);
        }

        public int DeleteQuote(int ID)
        {
            return (DB.DeleteById<Quote>(ID));
        }

    }

}
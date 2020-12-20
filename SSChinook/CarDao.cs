using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack.Data;
using ServiceStack;
using ServiceStack.OrmLite;

namespace CarInsurance
{

    public class CarDao : Service
    {

        System.Data.IDbConnection DB;

        public CarDao(System.Data.IDbConnection cars)
        {
            DB = cars;
        }

        public int AddCar(Car car)
        {
            return ((int) DB.Insert<Car>(car, selectIdentity: true));
        }

        public List<Car> GetAllCars()
        {
            return (DB.Select<Car>());
        }

        public Car GetCarByID(string ID)
        {
            if (ID != null) 
            {
                return (Db.SingleById<Car>(ID.ToUpper()));
            }

            return (null);
        }

        public List<string> GetCarMakes()
        {
            return (Db.Select<string>("SELECT DISTINCT(Make) FROM car"));
        }

        public List<string> GetCarModels(string Make)
        {
            return (Db.Select<string>("SELECT DISTINCT(Model) FROM car WHERE UPPER(Make)=UPPER('" + Make + "')"));
        }

        public List<double> GetCarEngineSizes(string Make, string Model)
        {
            return (Db.Select<double>("SELECT DISTINCT(EngineSize) FROM car WHERE UPPER(Make)=UPPER('" + Make + "') AND UPPER(Model)=UPPER('" + Model + "')"));
        }

        public Car UpdateCar(Car car)
        {
            DB.Update<Car>(car);
            return (car);
        }

        public int DeleteCar(string ID)
        {
            return (DB.DeleteById<Car>(ID));
        }

    }

}
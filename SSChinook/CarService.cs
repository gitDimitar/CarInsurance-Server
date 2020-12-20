using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;

namespace CarInsurance
{
    
    public class CarService : Service
    {

        public List<Car> Get(CarListRequest request)
        {
            CarDao DAO = new CarDao(Db);
            return (DAO.GetAllCars());
        }

        public int Post(CarRequest request)
        {
            var car = new Car()
            {
                Id = request.ID,
                Make = request.Make,
                Model = request.Model,
                EngineSize = request.EngineSize
            };

            CarDao DAO = new CarDao(Db);
            return (DAO.AddCar(car));
        }
 
        public Car Put(CarRequest request)
        {
            var car = new Car()
            {
                Id = request.ID,
                Make = request.Make,
                Model = request.Model,
                EngineSize = request.EngineSize
            };

            CarDao DAO = new CarDao(Db);
            return (DAO.UpdateCar(car));
        }

        public Car Get(CarByIDRequest request)
        {
            CarDao DAO = new CarDao(Db); 
            return (DAO.GetCarByID(request.ID));
        }

        public void Delete(CarByIDRequest request)
        {
            CarDao DAO = new CarDao(Db);
            DAO.DeleteCar(request.ID);
        }

        public List<string> Get(CarMakesRequest request)
        {
            CarDao DAO = new CarDao(Db);
            return (DAO.GetCarMakes());
        }

        public List<string> Get(CarModelsRequest request)
        {
            CarDao DAO = new CarDao(Db);
            return (DAO.GetCarModels(request.Make));
        }

        public List<double> Get(CarEngineRequest request)
        {
            CarDao DAO = new CarDao(Db);
            return (DAO.GetCarEngineSizes(request.Make, request.Model));
        }        

    }

}
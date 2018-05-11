using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    partial class Parking
    {
        public static Parking Instance = new Parking();


        private Parking()
        {
            Cars = new List<Car>();
            Transactions = new List<Transaction>();
        }

        public List<Car> Cars { get;  set; }
        public List<Transaction> Transactions { get;  set; }
        public decimal Balance { get; set; }

        public void AddCar(Car car)
        {
            if (Settings.Parking.Cars.Count >= Settings.ParkingSpace)
            {
                throw new NotEnoughSpaceException("No space in the parking lot");
            }
           
            Cars.Add(car);
            
        }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }

        public void AddMoney(decimal amount)
        {
            Balance += amount;
        }

        public bool AddMoney(int carId, decimal amount)
        {
            Car car = Cars.First(c => c.Id == carId);
            if (car == null)
            {
                return false;
            }
            car.AddMoney(amount);
            return true;
        }
        public void DeleteCar(int carId)
        {
            Car car = Cars.First(c => c.Id == carId);
            Cars.Remove(car);
        }
        public decimal GetCarBalance(int id)
        {
            Car car = Cars.First(c => c.Id == id);
            return car.Balance;
        }

        public bool IdExists(int id)
        {
            Car car = Cars.FirstOrDefault(c => c.Id == id);
            if (car == null)
            {
                return false;
            }
            return true;
        }
    }
}

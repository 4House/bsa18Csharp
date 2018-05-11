using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Parking
{
    static class Settings
    {
        
        private static Timer takeMoney;

        private static Timer writeToFile;

        private static int seconds;

        private static readonly decimal fine;

        public static Parking Parking;


        static Settings()
        {
            fine = 0.6m;
            seconds = 5;
            Parking = Parking.Instance;
            int minute = 60;
            ParkingSpace = 23;
            writeToFile = new Timer(TransactionsToFile, null, 1000, minute * 1000);
            takeMoney = new Timer(Timeout, null, 0, seconds * 1000);

        }
        static Dictionary<CarType, decimal> prices = new Dictionary<CarType, decimal>()
        {
            {CarType.Motorcycle, 5},
            { CarType.Passenger, 12},
            { CarType.Truck, 35},
            { CarType.Bus, 40}
        };

        public static int ParkingSpace { get; set; }

        private static void Timeout(object obj)
        {
            foreach (Car car in Parking.Cars)
            {
                decimal price = prices[car.Type];
                if (car.Balance - price < 0)
                {
                    price *= fine;
                }
                car.PayMoney(price);
                Parking.AddMoney(price);
                Parking.AddTransaction(new Transaction(car.Id, price));
            }
        }
        private static void TransactionsToFile(object obj)
        {
            {
                var lastMinuteTransactins = Parking.Transactions.
                    Where(t => DateTime.Now - t.TransactionTime < new TimeSpan(0, 1, 0));
                using (StreamWriter sw = new StreamWriter("Transactions.log", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine("Date: {0}", DateTime.Now);
                    decimal sum = 0;
                    foreach (var transaction in lastMinuteTransactins)
                    {
                        sum += transaction.Withdraw;
                    }
                    sw.WriteLine("Overall: {0:0.00}", sum);
                }
            }
        }

    }
}

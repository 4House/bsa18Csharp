using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{


    class Menu
    {
        public static bool Exit { get;set; }    

        public static void Start()
        {
            Console.WriteLine("1. Add car");
            Console.WriteLine("2. Increase car balance");
            Console.WriteLine("3. Delete car");
            Console.WriteLine("4. Output transaction history");
            Console.WriteLine("5. Show parking profit");
            Console.WriteLine("6. Show last minute profit");
            Console.WriteLine("7. show Transactions.log");
            Console.WriteLine("8. Show car balance");
            Console.WriteLine("9. Amount of free space");
            Console.WriteLine("10. Number of cars total");
            Console.WriteLine("11. Exit");
            int input = Int32.Parse(Console.ReadLine());
            switch (input)
            {
                case 1:
                    addCar();
                    break;
                case 2:
                    addMoney();
                    break;
                case 3:
                    removeCar();
                    break;
                case 4:
                    showLastMinuteTransactions();
                    break;
                case 5:
                    showParkingBalance();
                    break;
                case 6:
                    showLastMinuteEarnings();
                    break;
                case 7:
                    outputTransactions();
                    break;
                case 8:
                    showCarBalance();
                    break;
                case 9:
                    showFreeSpaces();
                    break;
                case 10:
                    showNumberOfCars();
                    break;
                case 11:
                    exit();
                    break;
                default:
                    throw new WrongCarTypeException("Unknown command");
            }
        }


        private static void removeCar()
        {
            Console.WriteLine("Car Id: ");
            int id = Int32.Parse(Console.ReadLine());
            if (Settings.Parking.GetCarBalance(id) < 0)
            {
                throw new NotEnoughMoneyException("You dont have enough money");
            }

            Settings.Parking.DeleteCar(id);
            Console.WriteLine("Removed");

        }

        private static void showLastMinuteTransactions()
        {
            var lastMinuteTransactins =
                Settings.Parking.Transactions.Where<Transaction>(t =>
                    DateTime.Now - t.TransactionTime < new TimeSpan(0, 1, 0));
            foreach (var transaction in lastMinuteTransactins)
            {
                Console.WriteLine(transaction);
            }
        }

        private static void exit()
        {
            Exit = true;
        }

        private static void showFreeSpaces()
        {
            Console.WriteLine("{0} ", Settings.ParkingSpace - Settings.Parking.Cars.Count);
        }

        private static void showCarBalance()
        {
            Console.WriteLine("Car Id: ");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Balance: {0}", Settings.Parking.GetCarBalance(id));
        }

        private static void outputTransactions()
        {
            using (StreamReader sr = new StreamReader("Transactions.log", System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }


        private static void addCar()
        {
            Console.WriteLine("enter Car Id: ");
            int id = Int32.Parse(Console.ReadLine());
            if (Settings.Parking.IdExists(id))
            {
                throw new WrongCommandException("Id already exists");
            }

            Console.WriteLine("Please specify (Passenger/Truck/Bus/Motorcycle): ");
            string type = Console.ReadLine();
            CarType carType;

            switch (type.ToLower())
            {
                case "passenger":
                    carType = CarType.Passenger;
                    break;
                case "truck":
                    carType = CarType.Truck;
                    break;
                case "bus":
                    carType = CarType.Bus;
                    break;
                case "motorcycle":
                    carType = CarType.Motorcycle;
                    break;
                default:
                    throw new WrongCarTypeException("Invalid input");
            }

            Console.WriteLine("Enter car balance: ");
            decimal balance = Decimal.Parse(Console.ReadLine());
            Settings.Parking.AddCar(new Car(id, carType, balance));
        }
        private static void showLastMinuteEarnings()
        {
            var lastMinuteTransactions =
                Settings.Parking.Transactions.Where(t =>
                    DateTime.Now - t.TransactionTime < new TimeSpan(0, 1, 0));
            decimal sum = 0;
            foreach (var transaction in lastMinuteTransactions)
            {
                sum += transaction.Withdraw;
            }

            Console.WriteLine("{0}", sum);
        }
        private static void addMoney()
        {
            Console.WriteLine("Car Id: ");
            int id = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Amount: ");
            decimal amount = Decimal.Parse(Console.ReadLine());
            Settings.Parking.AddMoney(id, amount);
            Console.WriteLine("Added");

        }

        private static void showNumberOfCars() => Console.WriteLine("{0}", Settings.Parking.Cars.Count);

        private static void showParkingBalance() => Console.WriteLine("{0}", Settings.Parking.Balance);
    }
}
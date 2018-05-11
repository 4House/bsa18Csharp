using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Car
    {
        public int Id { get; private set; }

        public readonly CarType Type;

        public decimal Balance { get; private set; }

        public Car(int id, CarType type, decimal balance = 0)
        {
            Id = id;
            Type = type;
            Balance = balance;
        }

        public decimal AddMoney(decimal value)
        {
            Balance += value;
            return Balance;
        }

        public decimal PayMoney(decimal value)
        {
            Balance -= value;
            return Balance;
        }

    }
}


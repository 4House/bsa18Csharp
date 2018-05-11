using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class WrongCarTypeException : Exception
    {
        public WrongCarTypeException()
        {
        }

        public WrongCarTypeException(string message) : base(message)
        {
        }

    }
}

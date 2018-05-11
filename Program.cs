using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking
{
    class Program
    {
        static void Main(string[] args)
        {
            while (Menu.Exit == false)
            {
                try
                {
                    Menu.Start();
                }

                catch (Exception ex)
                {
                    Console.WriteLine("{0}", ex.Message);
                }

            }
        }
    }
}

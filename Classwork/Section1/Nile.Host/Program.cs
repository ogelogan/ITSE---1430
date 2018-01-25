using System;

namespace Nile.Host
{
    class Program
    {
        static void Main( string[] args )
        {

        }

        static void PlayingWithPrimitives ()
        {
            //Primitive
            decimal unitPrice = 10.5m;

            //real declaration
            System.Decimal unitPrice2 = 10.5m;

            //current time
            DateTime now = DateTime.Now;

            System.Collections.ArrayList items;
        }
        static void PlayingWithVariables ()
        {

            int hours;
            double rate = .85;

            double poof, hot, cold;
            double minute = 83.125;
            string firstName, lastName;

            firstName = "Bob";
            lastName = "miller";

            firstName = lastName = "Sue";

            double ceiling = Math.Ceiling(rate);
            firstName = "hello";
            double floor = ceiling;
        }
    }
}

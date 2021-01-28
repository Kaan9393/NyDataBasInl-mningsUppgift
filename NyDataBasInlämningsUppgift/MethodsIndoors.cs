using NyDataBasInlämningsUppgift.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NyDataBasInlämningsUppgift
{
    class MethodsIndoors
    {
        public static void IndoorsAverageTempSearchDate()
        {
            Console.WriteLine("---------------------------------------------------");

            Console.Write("Enter a date with format 'YYYY-MM-DD' : ");
            try
            {

                DateTime inputDate = DateTime.Parse(Console.ReadLine());
                var start = inputDate.Date;
                var slut = start + new TimeSpan(1, 0, 0, 0);
                using (var weatherData = new MercuryContext())
                {
                    var vtemp = weatherData.TempData
                        .Where(i => i.Plats == "Inne" && i.Datum >= start && i.Datum < slut)
                        .Average(i => i.Temp);

                    Console.WriteLine($"Date: {inputDate.ToShortDateString()} AverageTemperature: {Math.Round(vtemp, 2)}");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("*** There is no data for this date");
            }

            Console.WriteLine("---------------------------------------------------");
        }       //Medeltemperatur för valt datum INNE

        public static void IndoorsAverageTempEachDay()
        {
            Console.WriteLine("---------------------------------------------------");

            using (var weatherData = new MercuryContext())
            {
                var vtemp = weatherData.TempData
                    .Where(i => i.Plats == "Inne")
                    .GroupBy(i => i.Datum.Date)
                    .Select(i => new
                    {
                        average = i.Average(i => i.Temp),   //annonym datatype
                        date = i.Key,
                    })
                    .OrderByDescending(i => i.average);


                foreach (var item in vtemp)
                {
                    Console.WriteLine($"Date:{item.date} AverageTemp:{Math.Round(item.average, 2)}");
                }
            }
            Console.WriteLine("---------------------------------------------------");

        }       //Sortering varmt till kallt, medeltemp per dag

        public static void IndoorsHumidity()
        {
            Console.WriteLine("---------------------------------------------------");

            using (var weatherData = new MercuryContext())
            {
                var vtemp = weatherData.TempData
                    .Where(i => i.Plats == "Inne")
                    .GroupBy(i => i.Datum.Date)
                    .Select(i => new
                    {
                        average = i.Average(i => i.Luftfuktighet),
                        date = i.Key
                    })
                    .OrderBy(i => i.average);

                foreach (var item in vtemp)
                {
                    Console.WriteLine($"Date:{item.date}, Humidity: {Math.Round(item.average, 2)} ");
                }
            }
            Console.WriteLine("---------------------------------------------------");

        }       //Sortering torrt till fuktigt, medelluftfuktighet per dag

        public static void IndoorsMold()
        {
            Console.WriteLine("---------------------------------------------------");

            using (var weatherData = new MercuryContext())
            {
                var vtemp = weatherData.TempData
                    .Where(i => i.Plats == "Inne")
                    .GroupBy(i => i.Datum.Date)
                    .Select(i => new
                    {
                        moldRisk = ((i.Average(i => i.Luftfuktighet) - 78) * (i.Average(i => i.Temp) / 15) / 0.22),
                        date = i.Key

                    })
                    .OrderBy(i => i.moldRisk);

                foreach (var item in vtemp)
                {
                    Console.WriteLine($"Date:{item.date}, MoldRisk: {Math.Round(item.moldRisk, 2)} %");
                }
                Console.WriteLine();
                Console.WriteLine("There is no risk for mold indoors");

            }
            Console.WriteLine("---------------------------------------------------");

        }       //Sortering mint till stört risk för mögel
    }
}

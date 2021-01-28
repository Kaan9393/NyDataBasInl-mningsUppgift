using NyDataBasInlämningsUppgift.Models.Entities;
using System;
using System.Linq;

namespace NyDataBasInlämningsUppgift
{
    class MethodsOutdoor
    {
        public static void OutdoorAverageTempSearchDate()
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
                        .Where(i => i.Plats == "Ute" && i.Datum >= start && i.Datum < slut)
                        .Average(i => i.Temp);

                    Console.WriteLine(inputDate.ToShortDateString() + " " + Math.Round(vtemp, 2));
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"*** There is no data for this date");
            }
            Console.WriteLine("---------------------------------------------------");

        }       //Medeltemperatur för valt datum

        public static void OutdoorAverageTempEachDay()
        {
            Console.WriteLine("---------------------------------------------------");

            using (var weatherData = new MercuryContext())
            {
                var vtemp = weatherData.TempData
                    .Where(i => i.Plats == "Ute")
                    .GroupBy(i => i.Datum.Date)
                    .Select(i => new
                    {
                        average = i.Average(i => i.Temp),   //annonym datatype
                        date = i.Key,
                    })
                    .OrderByDescending(i => i.average);



                foreach (var item in vtemp)
                {
                    Console.WriteLine($"{item.date}, {Math.Round(item.average, 2)}");
                }
            }
            Console.WriteLine("---------------------------------------------------");

        }       //Sortering varmt till kallt, medeltemp per dag

        public static void OutdoorHumidity()
        {
            Console.WriteLine("---------------------------------------------------");

            using (var weatherData = new MercuryContext())
            {
                var vtemp = weatherData.TempData
                    .Where(i => i.Plats == "Ute")
                    .GroupBy(i => i.Datum.Date)
                    .Select(i => new
                    {
                        average = i.Average(i => i.Luftfuktighet),
                        date = i.Key
                    })
                    .OrderBy(i => i.average);

                foreach (var item in vtemp)
                {
                    Console.WriteLine($"{item.date}, Humidity: {Math.Round(item.average, 2)} ");
                }
            }
            Console.WriteLine("---------------------------------------------------");

        }       //Sortering torrt till fuktigt, medelluftfuktighet per dag

        public static void OutdoorMold()
        {
            //MedelTemperaturPerDag och medelfuktighetperdag är mögelrisk
            Console.WriteLine("---------------------------------------------------");

            using (var weatherData = new MercuryContext())
            {

                var vtemp = weatherData.TempData
                    .Where(i => i.Plats == "Ute")
                    .GroupBy(i => i.Datum.Date)
                    .Select(i => new
                    {
                        moldrisk = ((i.Average(i => i.Luftfuktighet) - 78) * (i.Average(i => i.Temp) / 15) / 0.22),
                        date = i.Key

                    })
                    .OrderBy(i => i.moldrisk);

                foreach (var item in vtemp)
                {
                    Console.WriteLine($"{item.date}, MoldRisk: {Math.Round(item.moldrisk, 2)} %");
                }
            }
            Console.WriteLine("---------------------------------------------------");
        }       //Sortering mint till stört risk för mögel

        public static void MeteorologicalFall()
        {

            Console.WriteLine("---------------------------------------------------");

            using (var weatherData = new MercuryContext())
            {

                var vtemp = weatherData.TempData
                    .Where(i => i.Plats == "Ute" && (i.Datum.Month >= 08 && i.Datum.Month <= 12))
                    .GroupBy(i => i.Datum.Date)
                    .Select(g => new
                    {
                        date = g.Key,
                        avg = g.Average(i => i.Temp)
                    })
                    .OrderBy(i => i.date)
                    .ToList();

                bool below10 = false;
                int dayCount = 0;
                int i = 0;

                for (; i < vtemp.Count(); i++)
                {

                    below10 = vtemp[i].avg < 10.0;

                    if (below10)
                        dayCount++;
                    else
                        dayCount = 0;

                    if (dayCount == 5)
                    {
                        i -= 4;
                        break;
                    }
                }
                Console.WriteLine($"Fall: {vtemp[i].date}. Avg: {Math.Round(vtemp[i].avg, 2)}");
                Console.WriteLine();
                Console.WriteLine();

                foreach (var item in vtemp)
                {
                    Console.WriteLine($"Date: {item.date}. Avg: {Math.Round(item.avg, 2)}");
                }
            }
            Console.WriteLine("---------------------------------------------------");

        }   //Datum för meteorologisk Höst

        public static void MeteorologicalWinter()
        {

            Console.WriteLine("---------------------------------------------------");

            using (var weatherData = new MercuryContext())
            {

                var vtemp = weatherData.TempData
                    .Where(i => i.Plats == "Ute" && (i.Datum.Month >= 01 && i.Datum.Month <= 03))
                    .GroupBy(i => i.Datum.Date)
                    .Select(g => new
                    {
                        date = g.Key,
                        avg = g.Average(i => i.Temp)
                    })
                    .OrderBy(i => i.date)
                    .ToList();

                bool below0 = false;
                int dayCount = 0;
                int i = 0;

                for (; i < vtemp.Count(); i++)
                {

                    below0 = vtemp[i].avg < 0;

                    if (below0)
                        dayCount++;
                    else
                        dayCount = 0;

                    if (dayCount == 5)
                    {
                        i -= 4;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("There is no meteorological Winter");
                    }

                }
                Console.WriteLine();
                Console.WriteLine();

                //Denna loopen är för att se vilka värden som kommer ut. Eftersom det bara kommer ut ett datum så finns det ingen meteorologisk vinter.
                foreach (var item in vtemp)
                {
                    Console.WriteLine($"Datum: {item.date}. Avg: {Math.Round(item.avg, 2)}");
                }

                Console.WriteLine("This date shows that we don't get any meteorological Winter, because there is no more dates: ");
            }
            Console.WriteLine("---------------------------------------------------");

        }   //Datum för meteorologisk Vinter
    }
}

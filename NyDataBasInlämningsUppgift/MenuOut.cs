using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NyDataBasInlämningsUppgift
{
    class MenuOut
    {
        //MENU - Outdoor
        public static void MenuOutside()
        {
            Console.WriteLine();
            Console.WriteLine("1: Average temperature for selected date ");
            Console.WriteLine("2: Sort average temperature, warm to cold ");
            Console.WriteLine("3: Sort average humidity, dry to humidy ");
            Console.WriteLine("4: Sort risk for mold, lower to higher ");
            Console.WriteLine("5: Date for meteorological fall ");
            Console.WriteLine("6: Date for meteorological winter ");
            Console.WriteLine();
            ConsoleKey inputOutdoors = Console.ReadKey().Key;
            switch (inputOutdoors)
            {
                case ConsoleKey.D1:
                    MethodsOutdoor.OutdoorAverageTempSearchDate();
                    break;
                case ConsoleKey.D2:
                    MethodsOutdoor.OutdoorAverageTempEachDay();
                    break;
                case ConsoleKey.D3:
                    MethodsOutdoor.OutdoorHumidity();
                    break;
                case ConsoleKey.D4:
                    MethodsOutdoor.OutdoorMold();
                    break;
                case ConsoleKey.D5:
                    MethodsOutdoor.MeteorologicalFall();
                    break;
                case ConsoleKey.D6:
                    MethodsOutdoor.MeteorologicalWinter();
                    break;
                default:
                    break;
            }
        }
    }
}

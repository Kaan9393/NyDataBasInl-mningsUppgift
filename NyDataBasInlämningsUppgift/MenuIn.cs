using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NyDataBasInlämningsUppgift
{
    class MenuIn
    {
        //MENU - Indoors
        public static void MenuIndoors()
        {
            Console.WriteLine();
            Console.WriteLine("1: Average temperature for selected date ");
            Console.WriteLine("2: Sort average temperature, warm to cold ");
            Console.WriteLine("3: Sort average humidity, dry to humidy ");
            Console.WriteLine("4: Sort risk for mold, lower to higher ");
            Console.WriteLine();
            ConsoleKey inputIndoors = Console.ReadKey().Key;
            switch (inputIndoors)
            {
                case ConsoleKey.D1:
                    MethodsIndoors.IndoorsAverageTempSearchDate();
                    break;
                case ConsoleKey.D2:
                    MethodsIndoors.IndoorsAverageTempEachDay();
                    break;
                case ConsoleKey.D3:
                    MethodsIndoors.IndoorsHumidity();
                    break;
                case ConsoleKey.D4:
                    MethodsIndoors.IndoorsMold();
                    break;
                default:
                    break;
            }
        }
    }
}

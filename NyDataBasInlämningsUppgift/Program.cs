using NyDataBasInlämningsUppgift.Models.Entities;
using System;
using System.Linq;

namespace NyDataBasInlämningsUppgift
{
    class Program
    {
        static void Main(string[] args)
        { 
            //MENU

            do
            {
                Console.WriteLine("Forecast for weatherdata: \nOutdoor: O \nIndoors: I");      

                ConsoleKey inputLocation = Console.ReadKey().Key;

                switch (inputLocation)      
                {
                    case ConsoleKey.O:
                        MenuOut.MenuOutside();      
                        break;
                    case ConsoleKey.I:
                        MenuIn.MenuIndoors();
                        break;
                    default:
                        break;
                }
            } while (true);

        }
    }
}



//Det här är den koden jag använde för att hämta in datan från Microsoft SQL Server:

//Scaffold-DbContext "Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = Väderdatan; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir "Models/Entities" -Context "MercuryContext" -Force

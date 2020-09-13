using System;
using System.Threading.Tasks;

namespace TempFilesKiller
{
    class Program
    {
        static void Main()
        {
            try
            {
                Console.Write("Do you want to run the program? (Y/N or any other character): ");
                string userResponse = Console.ReadLine().ToLower();
                DirectoryHandler directoryHandler = new DirectoryHandler("C:/Users/highl/AppData/Local/Temp");

                while (userResponse == "y")
                {
                    directoryHandler.TryToDeleteFilesAndSubDirectories();
                    Console.Write("Do you want to run the program again? (Y/N or any other character): ");
                    userResponse = Console.ReadLine().ToLower();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something bad happened while I was trying to read/delete files! Error: {e.Message}");
            }
        }
    }
}
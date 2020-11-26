using System;

namespace TempFilesKiller
{
    class Program
    {
        static void Main()
        {
            try
            {
                DirectoryHandler directoryHandler;
                string userResponse;
                
                Console.Write("Do you want to run the program? (Y/N or any other character): ");
                userResponse = Console.ReadLine().ToLower();

                while (userResponse == "y")
                {
                    directoryHandler = new DirectoryHandler("C:/Users/highl/AppData/Local/Temp");
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
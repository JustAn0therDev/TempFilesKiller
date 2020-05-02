using System;

namespace TempFilesKiller
{
    class Program
    {
        static void Main()
        {
            try
            {
                Console.Write("Do you want to run the program? (Y/N): ");
                string userResponse = Console.ReadLine().ToUpper();

                while (userResponse != "N")
                {
                    using DirectoryHandler directoryHandler = new DirectoryHandler();
                    Console.Write("Do you want do see which and how many files are in the temp folder? (Y/N): ");
                    userResponse = Console.ReadLine().ToUpper();

                    if (userResponse == "Y")
                        directoryHandler.ShowAllDirectoriesAndFilesInTheTempFolder();

                    Console.Write("Do you want to delete all files and directories of the temp folder? (Y/N): ");
                    userResponse = Console.ReadLine().ToUpper();

                    if (userResponse == "Y")
                        directoryHandler.DeleteAllDirectoriesAndFiles();
                    else
                        Console.WriteLine("Ok, I won't do anything.");

                    Console.Write("Do you want to run the program again? (Y/N): ");
                    userResponse = Console.ReadLine().ToUpper();
                }
            }
            catch (Exception e)
            {
                Utils.TreatExceptionMessage($"Something bad happened while I was trying to read or delete the files! Error: {e.Message}");
            }
        }
    }
}

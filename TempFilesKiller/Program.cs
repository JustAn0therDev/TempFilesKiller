using System;

namespace TempFilesKiller
{
    class Program
    {
        static void Main()
        {
            try
            {
                using DirectoryHandler directoryHandler = new DirectoryHandler();
                Console.Write("Do you want do see which and how many files are in the temp folder? (Y/N): ");
                string userResponse = Console.ReadLine();

                if (userResponse.ToUpper() == "Y")
                    directoryHandler.ShowAllDirectoriesAndFilesInTheTempFolder();

                Console.Write("Do you want to delete all files and directories of the temp folder? (Y/N): ");
                userResponse = Console.ReadLine();

                if (userResponse.ToUpper() == "Y")
                    directoryHandler.DeleteAllDirectoriesAndFiles();
                else
                    Console.WriteLine("Ok, I won't do anything.");

                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Utils.TreatConsoleMessageForException($"Something bad happened while I was trying to read or delete the files! Error: {e.Message}");
            }
        }
    }
}

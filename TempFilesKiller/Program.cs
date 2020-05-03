﻿using System;

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
                    directoryHandler.DeleteAllDirectoriesAndFiles();

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

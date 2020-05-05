﻿using System;
using System.IO;

namespace TempFilesKiller
{
    class DirectoryHandler : IDisposable
    {
        private const string TEMP_DIRECTORY_PATH = "C:/Users/highl/AppData/Local/Temp";
        private const bool IS_RECURSIVE = true;

        public string[] ArrayOfDirectories { get; set; }
        public string[] ArrayOfFilesOutSideSubDirectories { get; set; }
        public DirectoryInfo MainTempDirectory { get; set; }

        public DirectoryHandler()
        {
            ArrayOfDirectories = Directory.GetDirectories(TEMP_DIRECTORY_PATH);
            ArrayOfFilesOutSideSubDirectories = Directory.GetFiles(TEMP_DIRECTORY_PATH);
            MainTempDirectory = new DirectoryInfo(TEMP_DIRECTORY_PATH);
        }

        public void DeleteAllDirectoriesAndFiles()
        {
            DeleteAllFilesInMainTempDirectory();

            Console.WriteLine("Accessing directories...");

            if (ArrayOfDirectories.Length > 0)
                for (int i = 0; i < ArrayOfDirectories.Length; i++)
                {
                    Console.WriteLine($"Accessing diretory: {ArrayOfDirectories[i]}...");
                    DirectoryInfo currentDirectory = new DirectoryInfo(ArrayOfDirectories[i]);

                    FileInfo[] filesInCurrentDirectory = currentDirectory.GetFiles();

                    if (filesInCurrentDirectory.Length > 0)
                    {
                        for (int j = 0; j < filesInCurrentDirectory.Length; j++)
                        {
                            try
                            {
                                filesInCurrentDirectory[j].Delete();
                            }
                            catch (Exception ex)
                            {
                                Utils.TreatExceptionMessage($"Couldn't delete the file because of the following error: {ex.Message}");
                            }
                        }
                    }

                    if (currentDirectory.GetFiles().Length == 0)
                    {
                        DeleteAllSubDirectoriesInDirectory(ArrayOfDirectories[i]);
                    }
                    else
                        Utils.TreatConditionalMessage($"Can't delete directory '{currentDirectory.Name}' because it's not empty!");

                    Console.WriteLine();
                }
            else
                Utils.TreatConditionalMessage("There are no directories in the temp folder to delete!");
        }

        private void DeleteAllFilesInMainTempDirectory()
        {
            FileInfo[] filesOutsideSubDirectories = MainTempDirectory.GetFiles();

            Console.WriteLine($"Attempting to delete files outside the sub-directories...");
            if (ArrayOfFilesOutSideSubDirectories.Length > 0)
            {
                for (int i = 0; i < ArrayOfFilesOutSideSubDirectories.Length; i++)
                {
                    try
                    {
                        filesOutsideSubDirectories[i].Delete();
                        Utils.TreatSuccessMessage($"File '{filesOutsideSubDirectories[i].Name}' deleted!");
                    }
                    catch (Exception ex)
                    {
                        Utils.TreatExceptionMessage($"Couldn't delete the file because of the following error: {ex.Message}");
                    }
                }
            }
            else
                Utils.TreatConditionalMessage("There are no files in the temp folder to delete!");

            Console.WriteLine();
        }

        private void DeleteAllSubDirectoriesInDirectory(string currentDirectoryName)
        {
            try
            {
                Console.WriteLine($"Attempting to delete directory: {currentDirectoryName}");
                Directory.Delete(currentDirectoryName, IS_RECURSIVE);
                Utils.TreatSuccessMessage($"Directory: '{currentDirectoryName} deleted!'");
            }
            catch (Exception ex)
            {
                Utils.TreatExceptionMessage($"Couldn't delete the current directory because of the following error: {ex.Message}");
            }
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}

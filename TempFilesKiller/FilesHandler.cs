using System;
using System.IO;

namespace TempFilesKiller
{
    internal static class FilesHandler
    {
        internal static void DeleteAllFilesInMainTempDirectory(DirectoryInfo mainTempDirectory, string[] arrayOfFilenamesOutsideSubDirectories)
        {
            FileInfo[] filesOutsideSubDirectories = mainTempDirectory.GetFiles();

            if (arrayOfFilenamesOutsideSubDirectories.Length > 0)
            {
                for (int i = 0; i < arrayOfFilenamesOutsideSubDirectories.Length; i++)
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
        internal static void TryToDeleteFilesInDirectory(DirectoryInfo currentDirectory)
        {
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
        }
    }
}

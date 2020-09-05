using System;
using System.IO;

namespace TempFilesKiller
{
    internal static class FilesHandler
    {
        /// <summary>
        /// Tries to delete all files in the provided directories and files in the same directory level.
        /// </summary>
        /// <param name="mainTempDirectory"></param>
        /// <param name="arrayOfFilenamesOutsideSubDirectories"></param>
        internal static void TryToDeleteAllFilesInMainTempDirectory(DirectoryInfo mainTempDirectory)
        {
            FileInfo[] filesOutsideSubDirectories = mainTempDirectory.GetFiles();

            if (filesOutsideSubDirectories.Length > 0)
            {
                for (int i = 0; i < filesOutsideSubDirectories.Length; i++)
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

        /// <summary>
        /// Tries to delete files in a directory, before deleting the directory itself.
        /// .NET won't allow a directory with one or more files to be deleted. 
        /// </summary>
        /// <param name="currentDirectory">The current directory</param>
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

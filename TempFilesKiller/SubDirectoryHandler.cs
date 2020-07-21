using System;
using System.IO;

namespace TempFilesKiller
{
    internal static class SubDirectoryHandler
    {
        internal static void DeleteAllSubDirectoriesInDirectory(string currentDirectoryName)
        {
            try
            {
                bool isRecursive = true;
                Directory.Delete(currentDirectoryName, isRecursive);
                Utils.TreatSuccessMessage($"Directory: '{currentDirectoryName} deleted!'");
            }
            catch (Exception ex)
            {
                Utils.TreatExceptionMessage($"Couldn't delete the current directory because of the following error: {ex.Message}");
            }
        }
    }
}

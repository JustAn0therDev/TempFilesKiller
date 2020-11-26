using System.IO;
using System.Threading.Tasks;
using System;

namespace TempFilesKiller
{
    internal struct DirectoryHandler
    {
        private string _path { get; set; }

        /// <summary>
        /// Receives a path argument on which it will operate.
        /// </summary>
        /// <param name="path"></param>
        public DirectoryHandler(string path) => _path = path;

        /// <summary>
        /// Tries to delete files and sub directories inside the path given to its constructor.
        /// </summary>
        public void TryToDeleteFilesAndSubDirectories()
        {
            TryDeleteFiles(Directory.GetFiles(_path));
            TryDeleteSubDirectories(Directory.GetDirectories(_path));
        }

        /// <summary>
        /// Tries to delete the top-level files inside the given directory.
        /// </summary>
        private void TryDeleteFiles(string[] filePaths)
        {
            Parallel.ForEach(filePaths, filePath =>
            {
                try
                {
                    File.Delete(filePath);
                    Console.WriteLine($"Successfully deleted file: {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Could not delete the file because of the following error: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Tries to delete the sub-directories inside the given directory.
        /// </summary>
        private void TryDeleteSubDirectories(string[] fileSubDirectories)
        {
            Parallel.ForEach(fileSubDirectories, subDirectory =>
            {
                try
                {
                    Directory.Delete(path: subDirectory, recursive: true);
                    Console.WriteLine($"Successfully deleted directory: {subDirectory}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Could not delete the directory because of the following error: {ex.Message}");
                }
            });
        }
    }
}
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TempFilesKiller
{
    internal class DirectoryHandler
    {
        private string _path { get; set; }

        /// <summary>
        /// Receives a path argument on which it will operate.
        /// </summary>
        /// <param name="path"></param>
        public DirectoryHandler(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Tries to delete files and sub directories inside the path given to this class' constructor.
        /// </summary>
        public void TryToDeleteFilesAndSubDirectories()
        {
            TryDeleteFiles(Directory.GetFiles(_path));
            TryDeleteSubDirectories(Directory.GetDirectories(_path));
        }

        /// <summary>
        /// Tries to delete the top-level files inside the given directory.
        /// </summary>
        private void TryDeleteFiles(string[] files)
        {
            Parallel.ForEach(files, file =>
            {
                try
                {
                    File.Delete(file);
                    Console.WriteLine($"Successfully deleted file: {file}");
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
        private void TryDeleteSubDirectories(string[] directories)
        {
            Parallel.ForEach(directories, directory =>
            {
                try
                {
                    Directory.Delete(directory, true);
                    Console.WriteLine($"Successfully deleted directory: {directory}");
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
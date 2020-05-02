using System;
using System.Collections.Generic;
using System.IO;

namespace TempFilesKiller
{
    class Program
    {
        private const string TEMP_DIRECTORY_PATH = "C:/Users/highl/AppData/Local/Temp";

        static void Main()
        {
            try
            {
                Console.Write("Do you want do see which and how many files are in the temp folder? (Y/N): ");
                string userResponse = Console.ReadLine();
                string[] arrayOfDirectories = Directory.GetDirectories(TEMP_DIRECTORY_PATH);
                string[] arrayOfFiles = Directory.GetFiles(TEMP_DIRECTORY_PATH);

                if (userResponse.ToUpper() == "Y")
                {
                    Console.WriteLine("List of Directories: ");
                    for (int i = 0; i < arrayOfDirectories.Length; i++)
                    {
                        Console.WriteLine(arrayOfDirectories[i]);
                    }

                    Console.WriteLine();
                    Console.WriteLine("------");
                    Console.WriteLine();

                    Console.WriteLine("List of Files: ");
                    for (int i = 0; i < arrayOfFiles.Length; i++)
                    {
                        Console.WriteLine(arrayOfFiles[i]);
                    }
                }

                Console.Write("Do you want to delete all files and directories of the temp folder? (Y/N): ");
                userResponse = Console.ReadLine();

                if (userResponse.ToUpper() == "Y")
                {
                    Console.WriteLine("Deleting directories...");
                    for (int i = 0; i < arrayOfDirectories.Length; i++)
                    {
                        //TODO: Implementar metodo recursivo para pegar todos os subdiretorios e excluir seus arquivos junto com eles.
                        DirectoryInfo directoryInfo = new DirectoryInfo(arrayOfDirectories[i]);

                        FileInfo[] files = directoryInfo.GetFiles();

                        Console.WriteLine($"Deleting files for directory: {directoryInfo.FullName}");
                        for (int j = 0; j < files.Length; j++)
                        {
                            try
                            {
                                files[i].Delete();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Couldn't delete the file because of the following error: {ex.Message}");
                            }
                        }

                        Console.WriteLine($"Deleting directory: {directoryInfo.FullName}");
                        Directory.Delete(arrayOfDirectories[i]);
                    }
                }
                else
                {
                    Console.WriteLine("Ok, I won't do anything.");
                }

                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something bad happened while I was trying to read or delete the files! Error: {e.Message}");
            }
        }
    }
}

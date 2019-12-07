using System;
using System.IO;

namespace AlarmClockWPFClient.Tools
{
    internal static class FileFolderHelper
    {
        private static readonly string AppDataPath =
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        internal static readonly string AppFolderPath =
            Path.Combine(AppDataPath, "APZRPKMA");

        internal static readonly string StorageFilePath =
            Path.Combine(AppFolderPath, "Storage.cskma");

        internal static readonly string ExceptionLogFilePath =
            Path.Combine(AppFolderPath, "ExceptionsLog.txt");

        internal static bool CreateFolderAndCheckFileExistance(string filePath)
        {
            var file = new FileInfo(filePath);
            return file.CreateFolderAndCheckFileExistance();
        }

        internal static bool CreateFolderAndCheckFileExistance(this FileInfo file)
        {
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            return file.Exists;
        }
    }
}

using AlarmClockWPFClient.Tools.Managers;
using System;
using System.IO;

namespace AlarmClockWPFClient.Tools
{
    internal static class Logger
    {
        internal static void SaveIntoFile(Exception ex, string filePath)
        {
            try
            {
                var file = new FileInfo(filePath);
                file.CreateFolderAndCheckFileExistance();

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------");
                    writer.WriteLine("Date and Time : " + DateTime.Now);
                    if (StationManager.CurrentUser != null)
                        writer.WriteLine($"Current user:{Environment.NewLine}" +
                                         $"\tFirst name: {StationManager.CurrentUser.FirstName}{Environment.NewLine}" +
                                         $"\tLast name: {StationManager.CurrentUser.LastName}{Environment.NewLine}" +
                                         $"\tLogin: {StationManager.CurrentUser.Login}{Environment.NewLine}");
                    writer.WriteLine(Environment.NewLine);

                    while (ex != null)
                    {
                        writer.WriteLine(ex.GetType().FullName);
                        writer.WriteLine("Message : " + ex.Message);
                        writer.WriteLine("StackTrace : " + ex.StackTrace);

                        ex = ex.InnerException;
                    }
                }
                
            }
            catch (Exception e)
            {
                SaveIntoFile(e, FileFolderHelper.ExceptionLogFilePath);
                throw new Exception($"Failed to log data to file {filePath}", ex);
            }
        }

    }
}

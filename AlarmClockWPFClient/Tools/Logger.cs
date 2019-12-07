using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmClockWPFClient.Tools
{
    internal static class Logger
    {
        internal static void SaveIntoFile(Exception ex, string filePath)
        {
            try
            {
                var file = new FileInfo(filePath);
                if (file.CreateFolderAndCheckFileExistance())
                {
                    file.Delete();
                }

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("-----------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
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
                throw new Exception($"Failed to log data to file {filePath}", ex);
            }
        }

    }
}

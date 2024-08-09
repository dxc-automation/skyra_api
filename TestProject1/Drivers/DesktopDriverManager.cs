using System.Text;
using System.Diagnostics;
using static System.Collections.Specialized.BitVector32;
using demo.Data;

namespace demo.Drivers
{
    public class DesktopDriverManager
    {
        //    private static WindowsDriver<WindowsElement> desktopDriver;
        private static Process process;



        public static void RunDesktopDriverService()
        {
            process = new Process();
            process.StartInfo.FileName = FilePaths.winAppDriver;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.StandardOutputEncoding = Encoding.Unicode;
            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();

            Console.WriteLine("Desktop driver service has been started successfully");
        }

        public static void StopDesktopDriverService()
        {
            if (process != null)
            {
                process.Close();
                process.Dispose();
                Console.WriteLine("Desktop driver service has been stopped successfully");
            }
        }


    }
}

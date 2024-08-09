using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace demo.Data
{
    public class FilePaths
    {
        //***   Returns build project directory path
        private static string GetProjectBuildPath()
        {
            return Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        }


        public static string projectRootPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private static string location = Assembly.GetExecutingAssembly().Location;
        public static string dir = Path.GetDirectoryName(location);

        public static string file = Path.Combine(dir + "\\Resources\\logo.png");
        public static string envFile = Path.Combine(projectRootPath, @"environment.txt");
        public static string token = Path.Combine(dir);

        public static string reportFolder = Path.Combine(projectRootPath, @"Report\");
        public string reportFile = Path.Combine(projectRootPath, @"Report\TestReport.html");
        public string reportConfig = Path.Combine(projectRootPath, @"Resources\extent-config.xml");
        public string reportZip = Path.Combine(projectRootPath, @"Report_Archive\");
        public string resourcesFile = Path.Combine(dir, @"Resources/TestData.json");
        public string screenshots = @"Report\\Screenshots\\";

        // Stored on the local machine
        public static string winAppDriver = @"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe";

        // Drivers
        public static readonly string chromeExe = Path.Combine(projectRootPath, @"Resources/Drivers/chromedriver.exe");
        public static readonly string chrome = Path.Combine(projectRootPath, @"Resources/Drivers");
        public static readonly string chromeBinary = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, @"Resources\Application\chrome.exe");
        public static readonly string firefoxExe = GetProjectBuildPath() + "\\Resources\\Drivers\\geckodriver.exe";
        public static readonly string edgeExe = GetProjectBuildPath() + "\\Resources\\Drivers\\";
        public static readonly string winiumExe = GetProjectBuildPath() + "\\Resources\\Drivers\\Winium.Desktop.Driver.exe";

        public static string appiumLogFile = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, @"Report\AppiumLog.log");

    }
}


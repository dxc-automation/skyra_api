using demo.Config;
using System.IO;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static demo.Data.FilePaths;
using demo.Data;

namespace demo.Utility
{
    public class Utils
    {

        public static void DeleteFiles(string folder)
        {
            Console.WriteLine("\n" + folder + "will be deleted");
            string[] filePaths = Directory.GetFiles(folder);
            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }
        }


        public static string CreateZip(string fileName)
        {
            FilePaths filePaths = new FilePaths();
            string folder = reportFolder;
            string zip = filePaths.reportZip + fileName + ".zip";

            try
            {
                ZipFile.CreateFromDirectory(folder, zip);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Archive created successfully !!! \n" + zip);
            return zip;
        }


        public static string GetDateTime()
        {
            DateTime dateTime = DateTime.Today;
            string date = dateTime.ToString("MMMM_dd");
            string time = dateTime.ToString("H:mm:ss").Replace(":", "-");
            return date + "_" + time;
        }

        public static void WriteFile(string fileName, string content)
        {
            string path = token + "/" + fileName;
            File.WriteAllText(path, content);
        }


        public static string GenerateRandomString(int charsNumber, bool numbers)
        {
            string? chars;

            switch (numbers)
            {
                case true:
                    chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    break;

                case false:
                    chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    break;
            }

            var stringChars = new char[charsNumber];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new string(stringChars);
            return finalString;
        }
    }
}

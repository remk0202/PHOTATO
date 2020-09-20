using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ImageSoter
{
    class Program
    {
        static void Main(string[] args)
        {
            bool hasConvert = true;
            string fileName, folderLocation = "";
            const string pattern = @"^A\d$|^B\d{2}$|^C\d{3}$|^D\d{4}$|^E\d{5}$|^F\d{6}$|^G\d{7}$";
            int fileCount = 1;

            Console.Write("input image folder address = ");
            folderLocation = Convert.ToString(Console.ReadLine());
            DirectoryInfo directoryInfo = new DirectoryInfo(folderLocation);

            foreach (FileInfo File in directoryInfo.GetFiles())
            {
                if ((File.Extension.ToLower().CompareTo(".png") == 0) || (File.Extension.ToLower().CompareTo(".jpg") == 0)
                    || (File.Extension.ToLower().CompareTo(".gif") == 0) || (File.Extension.ToLower().CompareTo(".jpeg") == 0))
                {
                    hasConvert = false;

                    foreach (Match match in Regex.Matches(System.IO.Path.GetFileNameWithoutExtension(File.Name), pattern))
                        hasConvert = true;

                    if (hasConvert == false)
                    {
                        fileName = SetAlphabet(folderLocation, fileCount) + fileCount + File.Extension;
                        System.IO.File.Move(File.FullName, fileName);
                        fileCount++;
                    }
                }
            }
        }

        private static string SetAlphabet(string folderLocation, int fileCount)
        {
            string fileName;
            if (fileCount < 10)
                fileName = folderLocation + "\\A";
            else if (fileCount >= 10 && fileCount < 100)
                fileName = folderLocation + "\\B";
            else if (fileCount >= 100 && fileCount < 1000)
                fileName = folderLocation + "\\C";
            else if (fileCount >= 1000 && fileCount < 10000)
                fileName = folderLocation + "\\D";
            else if (fileCount >= 10000 && fileCount < 100000)
                fileName = folderLocation + "\\E";
            else if (fileCount >= 100000 && fileCount < 1000000)
                fileName = folderLocation + "\\F";
            else
                fileName = folderLocation + "\\G";
            return fileName;
        }
    }
}
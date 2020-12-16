using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace image_sorter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void pinned_button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Topmost != true)
            {
                this.Topmost = true;
                pinned_button.Content = "\xE77A";
            }
            else
            {
                this.Topmost = false;
                pinned_button.Content = "\xE718";
            }
        }

        private void minimize_button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Window.GetWindow(this).Close();
        }

        private void FileViewer_DragEnter(Object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }

        private void FileViewer_DragDrop(Object sender, DragEventArgs e)
        {
            bool hasConvert;
            string fileName, Location, fileExtension;
            const string Pattern = @"^A\d$|^B\d{2}$|^C\d{3}$|^D\d{4}$|^E\d{5}$|^F\d{6}$|^G\d{7}$";
            int count = 1;
            var dragLocation = (string[])e.Data.GetData(DataFormats.FileDrop);
            Location = string.Join(string.Empty, dragLocation);

            DirectoryInfo directoryInfo = new DirectoryInfo(Location);
            // 파일 변환전 이미 변환된 파일이 있는지 확인하는 코드 !! 지우지 말것
            // TODO :: 로직이 마음에 안듬, 수정 요망
            foreach (FileInfo File in directoryInfo.GetFiles())
            {
                if ((File.Extension.ToLower().CompareTo(".png") == 0) || (File.Extension.ToLower().CompareTo(".jpg") == 0)
                    || (File.Extension.ToLower().CompareTo(".gif") == 0) || (File.Extension.ToLower().CompareTo(".jpeg") == 0) || (File.Extension.ToLower().CompareTo(".jfif") == 0))
                {
                    foreach (Match match in Regex.Matches(System.IO.Path.GetFileNameWithoutExtension(File.Name), Pattern))
                    {
                        fileName = SetAlphabet(Location, count) + count + File.Extension;
                        int ExtractedNumber = Convert.ToInt32(Regex.Replace(File.Name, @"\D", ""));
                        if (ExtractedNumber != count)
                            System.IO.File.Move(File.FullName, fileName);
                        count++;
                    }
                }
            }

            foreach (FileInfo File in directoryInfo.GetFiles())
            {
                if ((File.Extension.ToLower().CompareTo(".png") == 0) || (File.Extension.ToLower().CompareTo(".jpg") == 0)
                    || (File.Extension.ToLower().CompareTo(".gif") == 0) || (File.Extension.ToLower().CompareTo(".jpeg") == 0) || (File.Extension.ToLower().CompareTo(".jfif") == 0))
                {
                    hasConvert = false;

                    foreach (Match match in Regex.Matches(System.IO.Path.GetFileNameWithoutExtension(File.Name), Pattern))
                    {
                        hasConvert = true;
                    }

                    // jfif 파일 png 파일로 변환
                    if(File.Extension == ".jfif")
                    {
                        fileExtension = ".png";
                    }
                    else
                    {
                        fileExtension = File.Extension;
                    }
                    // 파일 변환
                    if (hasConvert == false)
                    {
                        fileName = SetAlphabet(Location, count) + count + fileExtension;
                        System.IO.File.Move(File.FullName, fileName);
                        count++;
                    }
                }
            }
            Window completionWindow = new CompletWindow();
            completionWindow.Owner = this;
            completionWindow.Show();
        }

        private static string SetAlphabet(string Location, int count)
        {
            string fileName;
            if (count < 10)
                fileName = Location + "\\A";
            else if (count >= 10 && count < 100)
                fileName = Location + "\\B";
            else if (count >= 100 && count < 1000)
                fileName = Location + "\\C";
            else if (count >= 1000 && count < 10000)
                fileName = Location + "\\D";
            else if (count >= 10000 && count < 100000)
                fileName = Location + "\\E";
            else if (count >= 100000 && count < 1000000)
                fileName = Location + "\\F";
            else
                fileName = Location + "\\G";
            return fileName;
        }
    }
}

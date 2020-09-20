using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Photato
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            byte Language = Properties.Settings.Default.Language;

            switch (Language)
            {
                case 0:
                    LanguageButton.Tag = "0";
                    LanguageButton.Content = "EN";
                    KumaComment.Content = "Drag & drop the Pictures folder";
                    KumaComment.FontFamily = new FontFamily("Segoe Print");
                    KumaComment.FontSize = 26;
                    break;
                case 1:
                    LanguageButton.Tag = "1";
                    LanguageButton.Content = "JP";
                    KumaComment.Content = "フォルダをDrag  &  Dropしてください。";
                    KumaComment.FontFamily = new FontFamily("UD Digi Kyokasho N-B");
                    KumaComment.FontSize = 24;
                    break;
                case 2:
                    LanguageButton.Tag = "2";
                    LanguageButton.Content = "KR";
                    KumaComment.Content = "사진 폴더를 드래그 & 드롭";
                    KumaComment.FontFamily = new FontFamily("Nanum Pen Script");
                    KumaComment.FontSize = 48;
                    break;
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Window.GetWindow(this).Close();
        }

        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void LanguageButton_Click(object sender, RoutedEventArgs e)
        {
            var Tag = ((Button)sender).Tag;

            switch (Tag)
            {
                case "0":
                    LanguageButton.Tag = "1";
                    Properties.Settings.Default.Language = 1;
                    LanguageButton.Content = "JP";
                    KumaComment.Content = "フォルダをDrag  &  Dropしてください。";
                    KumaComment.FontFamily = new FontFamily("UD Digi Kyokasho N-B");
                    KumaComment.FontSize = 24;
                    break;
                case "1":
                    LanguageButton.Tag = "2";
                    Properties.Settings.Default.Language = 2;
                    LanguageButton.Content = "KR";
                    KumaComment.Content = "사진 폴더를 드래그 & 드롭";
                    KumaComment.FontFamily = new FontFamily("Nanum Pen Script");
                    KumaComment.FontSize = 48;
                    break;
                case "2":
                    LanguageButton.Tag = "0";
                    Properties.Settings.Default.Language = 0;
                    LanguageButton.Content = "EN";
                    KumaComment.Content = "Drag & drop the Pictures folder";
                    KumaComment.FontFamily = new FontFamily("Segoe Print");
                    KumaComment.FontSize = 26;
                    break;
            }
            Properties.Settings.Default.Save();
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
            string fileName, Location;
            const string Pattern = @"^A\d$|^B\d{2}$|^C\d{3}$|^D\d{4}$|^E\d{5}$|^F\d{6}$|^G\d{7}$";
            int count = 1;
            var dragLocation = (string[])e.Data.GetData(DataFormats.FileDrop);
            Location = string.Join(string.Empty, dragLocation);

            DirectoryInfo directoryInfo = new DirectoryInfo(Location);
            foreach (FileInfo File in directoryInfo.GetFiles())
            {
                if ((File.Extension.ToLower().CompareTo(".png") == 0) || (File.Extension.ToLower().CompareTo(".jpg") == 0)
                    || (File.Extension.ToLower().CompareTo(".gif") == 0) || (File.Extension.ToLower().CompareTo(".jpeg") == 0))
                {
                    hasConvert = false;

                    foreach (Match match in Regex.Matches(System.IO.Path.GetFileNameWithoutExtension(File.Name), Pattern))
                        hasConvert = true;

                    if (hasConvert == false)
                    {
                        fileName = SetAlphabet(Location, count) + count + File.Extension;
                        System.IO.File.Move(File.FullName, fileName);
                        count++;
                    }
                }
            }
            Window completionWindow = new CompletionWindow();
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

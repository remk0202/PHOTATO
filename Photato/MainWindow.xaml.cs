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
                    KumaComment.Content = "イメージフォルダーをドラッグ&ドロップ";
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
                    KumaComment.Content = "イメージフォルダーをドラッグ&ドロップ";
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
            bool Converting;
            string newName, Location;
            const string Pattern = @"^A\d$|^B\d{2}$|^C\d{3}$|^D\d{4}$|^E\d{5}$|^F\d{6}$|^G\d{7}$";
            int Count = 1;
            var dragLocation = (string[])e.Data.GetData(DataFormats.FileDrop);
            Location = string.Join(string.Empty, dragLocation);

            DirectoryInfo directoryInfo = new DirectoryInfo(Location);

            foreach (FileInfo File in directoryInfo.GetFiles())
            {
                if ((File.Extension.ToLower().CompareTo(".png") == 0) || (File.Extension.ToLower().CompareTo(".jpg") == 0)
                    || (File.Extension.ToLower().CompareTo(".gif") == 0) || (File.Extension.ToLower().CompareTo(".jpeg") == 0)
                    || (File.Extension.ToLower().CompareTo(".bmp") == 0) || (File.Extension.ToLower().CompareTo(".raw") == 0))
                {
                    foreach (Match match in Regex.Matches(System.IO.Path.GetFileNameWithoutExtension(File.Name), Pattern))
                    {
                        newName = SetAlphabet(Location, Count) + Count + File.Extension;
                        int ExtractedNumber = Convert.ToInt32(Regex.Replace(File.Name, @"\D", ""));
                        if (ExtractedNumber != Count)
                            System.IO.File.Move(File.FullName, newName);
                        Count++;
                    }
                }
            }
            foreach (FileInfo File in directoryInfo.GetFiles())
            {
                if ((File.Extension.ToLower().CompareTo(".png") == 0) || (File.Extension.ToLower().CompareTo(".jpg") == 0)
                    || (File.Extension.ToLower().CompareTo(".gif") == 0) || (File.Extension.ToLower().CompareTo(".jpeg") == 0)
                    || (File.Extension.ToLower().CompareTo(".bmp") == 0) || (File.Extension.ToLower().CompareTo(".raw") == 0))
                {
                    Converting = false;

                    foreach (Match match in Regex.Matches(System.IO.Path.GetFileNameWithoutExtension(File.Name), Pattern))
                        Converting = true;

                    if (Converting == false)
                    {
                        newName = SetAlphabet(Location, Count) + Count + File.Extension;
                        System.IO.File.Move(File.FullName, newName);
                        Count++;
                    }
                }
            }
            Window completionWindow = new CompletionWindow();
            completionWindow.Owner = this;
            completionWindow.Show();
        }

        private static string SetAlphabet(string Location, int Count)
        {
            string newName;
            if (Count < 10)
                newName = Location + "\\A";
            else if (Count >= 10 && Count < 100)
                newName = Location + "\\B";
            else if (Count >= 100 && Count < 1000)
                newName = Location + "\\C";
            else if (Count >= 1000 && Count < 10000)
                newName = Location + "\\D";
            else if (Count >= 10000 && Count < 100000)
                newName = Location + "\\E";
            else if (Count >= 100000 && Count < 1000000)
                newName = Location + "\\F";
            else
                newName = Location + "\\G";
            return newName;
        }
    }
}

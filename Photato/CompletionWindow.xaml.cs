using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Photato
{
    public partial class CompletionWindow : Window
    {
        public CompletionWindow()
        {
            InitializeComponent();
            byte Language = Properties.Settings.Default.Language;

            switch (Language)
            {
                case 0:
                    commentLabel.Content = "Done!";
                    break;
                case 1:
                    commentLabel.Content = "整理が終わりました！";
                    break;
                case 2:
                    commentLabel.Content = "차곡 차곡 정리가 끝났습니다!";
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

        private void completeButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Window.GetWindow(this).Close();
        }
    }
}

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
                    commentLabel.Content = "Conversion completed successfully.";
                    break;
                case 1:
                    commentLabel.Content = "変換は正常に完了しました。";
                    break;
                case 2:
                    commentLabel.Content = "변환을 완료 했습니다.";
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

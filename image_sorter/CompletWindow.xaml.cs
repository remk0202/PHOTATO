using System.Windows;
using System.Windows.Input;

namespace image_sorter
{
    /// <summary>
    /// Interaction logic for CompletWindow.xaml
    /// </summary>
    public partial class CompletWindow : Window
    {
        public CompletWindow()
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Window.GetWindow(this).Close();
        }
    }
}

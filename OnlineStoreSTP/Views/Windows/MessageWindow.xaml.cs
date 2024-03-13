using System.Windows;

namespace OnlineStoreSTP.Views.Windows
{
    public partial class MessageWindow : Window
    {
        public MessageWindow(string text)
        {
            InitializeComponent();
            TextMessage.Text = text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

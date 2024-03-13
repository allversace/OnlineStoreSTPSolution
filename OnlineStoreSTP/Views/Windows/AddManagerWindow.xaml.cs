using OnlineStoreSTP.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;

namespace OnlineStoreSTP.Views.Windows
{
    public partial class AddManagerWindow : Window
    {
        public AddManagerWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[а-яА-Яa-zA-Z]$"))
                e.Handled = true;
        }
    }
}

using OnlineStoreSTP.Models.Data;
using OnlineStoreSTP.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;

namespace OnlineStoreSTP.Views.Windows
{
    public partial class EditClientWindow : Window
    {
        public EditClientWindow(Client client)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            MainWindowViewModel.SelectedClient = client;
            MainWindowViewModel.ClientName = client.Name;
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[а-яА-Яa-zA-Z]$"))
                e.Handled = true;
        }
    }
}

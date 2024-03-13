using OnlineStoreSTP.Models.Data;
using OnlineStoreSTP.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;

namespace OnlineStoreSTP.Views.Windows
{
    public partial class EditManagerWindow : Window
    {
        public EditManagerWindow(Manager manager)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            MainWindowViewModel.SelectedManager = manager;
            MainWindowViewModel.ManagerName = manager.Name;
        }

        private void NameTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[а-яА-Яa-zA-Z]$"))
                e.Handled = true;
        }
    }
}

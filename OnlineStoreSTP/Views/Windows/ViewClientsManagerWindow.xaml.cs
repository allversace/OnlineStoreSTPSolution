using OnlineStoreSTP.ViewModels;
using System.Windows;

namespace OnlineStoreSTP.Views.Windows
{
    public partial class ViewClientsManagerWindow : Window
    {
        public ViewClientsManagerWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}

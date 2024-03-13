using OnlineStoreSTP.ViewModels;
using System.Windows;

namespace OnlineStoreSTP.Views.Windows
{
    public partial class ViewProductClientWindow : Window
    {
        public ViewProductClientWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}

using OnlineStoreSTP.ViewModels;
using System.Windows;

namespace OnlineStoreSTP.Views.Windows
{
    public partial class ViewStatusClientWindow : Window
    {
        public ViewStatusClientWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}

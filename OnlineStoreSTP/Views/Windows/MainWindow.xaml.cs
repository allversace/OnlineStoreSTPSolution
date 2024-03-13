using OnlineStoreSTP.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace OnlineStoreSTP
{
    public partial class MainWindow : Window
    {
        public static ListView AllViewManagers;
        public static ListView AllViewClients;
        public static ListView AllViewProducts;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            AllViewManagers = AllManagersView;
            AllViewClients = AllClientsView;
            AllViewProducts = AllProductView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

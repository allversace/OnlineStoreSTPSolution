using OnlineStoreSTP.Models.Data;
using OnlineStoreSTP.ViewModels;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace OnlineStoreSTP.Views.Windows
{
    public partial class EditProductWindow : Window
    {
        public EditProductWindow(Product product)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            MainWindowViewModel.SelectedProduct = product;
            MainWindowViewModel.ProductName = product.Name;
            MainWindowViewModel.Price = product.Price;
            MainWindowViewModel.Type = product.Type;
            MainWindowViewModel.SubPeriod = product.SubscriptionPeriod;
        }

        private void TextBox_PreviewTextInputNumber(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[0-9]$"))
                e.Handled = true;
        }

        private void TextBox_PreviewTextInputABC(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[а-яА-Яa-zA-Z]$"))
                e.Handled = true;
        }

        private void TextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
    }
}

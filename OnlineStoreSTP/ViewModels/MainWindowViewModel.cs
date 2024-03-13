using OnlineStoreSTP.Classes;
using OnlineStoreSTP.Models;
using OnlineStoreSTP.Models.Data;
using OnlineStoreSTP.Views.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace OnlineStoreSTP.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private List<Manager> allManagers = DataWorker.GetAllManagers();
        public List<Manager> AllManagers
        {
            get { return allManagers; }
            set
            {
                allManagers = value;
                NotifyPropertyChanged("AllManagers");
            }
        }

        private List<Client> allClients = DataWorker.GetAllClients();
        public List<Client> AllClients
        {
            get { return allClients; }
            set
            {
                allClients = value;
                NotifyPropertyChanged();
            }
        }

        private List<Product> allProducts = DataWorker.GetAllProducts();
        public List<Product> AllProducts
        {
            get { return allProducts; }
            set
            {
                allProducts = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<ClientStatus> _allClientStatus;
        public ObservableCollection<ClientStatus> AllClientStatus
        {
            get
            {
                if (_allClientStatus == null)
                {
                    _allClientStatus = new ObservableCollection<ClientStatus>(SoftTradePlusEntities.GetContext().ClientStatus.ToList());
                }
                return _allClientStatus;
            }
        }

        //Commands for delete the window
        public static Client SelectedClient { get; set; }
        public static Manager SelectedManager { get; set; }
        public static Product SelectedProduct { get; set; }
        public static ClientStatus SelectedClientStatus { get; set; }
        public TabItem SelectedTabItem { get; set; }

        private RelayCommand deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj =>
                {
                    string resultStr = "Выберите элемент";
                    if (SelectedTabItem.Name == "ClientTab" && SelectedClient != null)
                    {
                        resultStr = DataWorker.DeleteClient(SelectedClient);
                        AllUpdateView();
                    }
                    if (SelectedTabItem.Name == "ManagerTab" && SelectedManager != null)
                    {
                        resultStr = DataWorker.DeleteManager(SelectedManager);
                        AllUpdateView();
                    }
                    if (SelectedTabItem.Name == "ProductTab" && SelectedProduct != null)
                    {
                        resultStr = DataWorker.DeleteProduct(SelectedProduct);
                        AllUpdateView();
                    }
                    SetNullToProperties();
                    ShowMessageToUser(resultStr);
                });
            }
        }

        //Commands for add the window
        public static string ManagerName { get; set; }
        private RelayCommand addNewManager;
        public RelayCommand AddNewManager
        {
            get
            {
                return addNewManager ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (ManagerName != null || ManagerName.Replace(" ", "").Length != 0)
                    {
                        resultStr = DataWorker.CreateManager(ManagerName);
                        AllUpdateView();
                        ShowMessageToUser(resultStr);
                        SetNullToProperties();
                        wnd.Close();
                    }
                });
            }
        }

        public static string ClientName { get; set; }
        public static Product ProductClient { get; set; }
        public static ClientStatus Status { get; set; }
        public static Manager SelectManager { get; set; }
        private RelayCommand addNewClient;
        public RelayCommand AddNewClient
        {
            get
            {
                return addNewClient ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    resultStr = DataWorker.CreateClient(ClientName, ProductClient.ProductId, Status.StatusId, SelectManager.ManagerId);
                    AllUpdateView();
                    ShowMessageToUser(resultStr);
                    SetNullToProperties();
                    wnd.Close();
                });
            }
        }

        public static string ProductName { get; set; }
        public static decimal Price { get; set; }
        public static string Type { get; set; }
        public static string SubPeriod { get; set; }
        private RelayCommand addNewProduct;
        public RelayCommand AddNewProduct
        {
            get
            {
                return addNewProduct ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    resultStr = DataWorker.CreateProduct(ProductName, Price, Type, SubPeriod);
                    AllUpdateView();
                    ShowMessageToUser(resultStr);
                    SetNullToProperties();
                    wnd.Close();
                });
            }
        }

        //Commands for open the window
        private RelayCommand openAddNewClient;
        public RelayCommand OpenAddNewClient
        {
            get
            {
                return openAddNewClient ?? new RelayCommand(obj =>
                {
                    OpenAddClientWindow();
                });
            }
        }
        private RelayCommand openAddNewManager;
        public RelayCommand OpenAddNewManager
        {
            get
            {
                return openAddNewManager ?? new RelayCommand(obj =>
                {
                    OpenAddManagerWindow();
                });
            }
        }
        private RelayCommand openAddNewProduct;
        public RelayCommand OpenAddNewProduct
        {
            get
            {
                return openAddNewProduct ?? new RelayCommand(obj =>
                {
                    OpenAddProductWindow();
                });
            }
        }

        private RelayCommand openEditItem;
        public RelayCommand OpenEditItem
        {
            get
            {
                return openEditItem ?? new RelayCommand(obj =>
                {
                    if (SelectedTabItem.Name == "ClientTab" && SelectedClient != null)
                    {
                        EditAddClientWindow(SelectedClient);
                    }
                    if (SelectedTabItem.Name == "ManagerTab" && SelectedManager != null)
                    {
                        EditAddManagerWindow(SelectedManager);
                    }
                    if (SelectedTabItem.Name == "ProductTab" && SelectedProduct != null)
                    {
                        EditAddProductWindow(SelectedProduct);
                    }
                });
            }
        }

        //Commands for searchInQuery
        private List<Client> specialClient;
        public List<Client> SpecialClient
        {
            get
            {
                if (specialClient == null && SelectedManager != null)
                {
                    specialClient = SoftTradePlusEntities.GetContext().Client.Where(x => x.ManagerId == SelectedManager.ManagerId).ToList();

                }
                return specialClient ?? (specialClient = new List<Client>());
            }
            set
            {
                specialClient = value;
            }
        }

        private List<Product> specialProduct;
        public List<Product> SpecialProduct
        {
            get
            {
                if (specialProduct == null && SelectedClient != null)
                {
                    specialProduct = SoftTradePlusEntities.GetContext().Product.Where(x => x.ProductId == SelectedClient.ProductId).ToList();

                }
                return specialProduct ?? (specialProduct = new List<Product>());
            }
            set
            {
                specialProduct = value;
            }
        }

        private List<Client> specialStatusClient;
        public List<Client> SpecialStatusClient
        {
            get
            {
                if (specialStatusClient == null && SelectedClientStatus != null)
                {
                    specialStatusClient = SoftTradePlusEntities.GetContext().Client.Where(x => x.StatusId == SelectedClientStatus.StatusId).ToList();

                }
                return specialStatusClient ?? (specialStatusClient = new List<Client>());
            }
            set
            {
                specialStatusClient = value;
            }
        }

        private RelayCommand openSearchItem;
        public RelayCommand OpenSearchItem
        {
            get
            {
                return openSearchItem ?? new RelayCommand(obj =>
                {
                    if (SelectedTabItem.Name == "ManagerTab" && SelectedManager != null)
                    {
                        OpenSearchClientManagerWindow();
                    }
                    if (SelectedTabItem.Name == "ClientTab" && SelectedClient != null)
                    {
                        OpenSearchProductClientWindow();
                    }
                    if (SelectedTabItem.Name == "StatusClientTab" && SelectedClientStatus != null)
                    {
                        OpenSearchStatusClientWindow();
                    }
                });
            }
        }

        //Commands for edit the window
        private RelayCommand editClient;
        public RelayCommand EditClient
        {
            get
            {
                return editClient ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран элемент";
                    if (SelectedClient != null && ClientName != null && Status != null && SelectManager != null)
                    {
                        resultStr = DataWorker.EditClient(SelectedClient, ClientName, ProductClient.ProductId, Status.StatusId, SelectedManager.ManagerId);
                        AllUpdateView();
                        SetNullToProperties();
                        ShowMessageToUser(resultStr);
                        window.Close();
                    }
                });
            }
        }

        private RelayCommand editManager;
        public RelayCommand EditManager
        {
            get
            {
                return editManager ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран элемент";
                    if (SelectedManager != null && ManagerName != null)
                    {
                        resultStr = DataWorker.EditManager(SelectedManager, ManagerName);
                        AllUpdateView();
                        SetNullToProperties();
                        ShowMessageToUser(resultStr);
                        window.Close();
                    }
                });
            }
        }

        private RelayCommand editProduct;
        public RelayCommand EditProduct
        {
            get
            {
                return editProduct ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран элемент";
                    if (SelectedProduct != null && ProductName != null && Price != 0 && Type != null && SubPeriod != null)
                    {
                        resultStr = DataWorker.EditProduct(SelectedProduct, ProductName, Price, Type, SubPeriod);
                        AllUpdateView();
                        SetNullToProperties();
                        ShowMessageToUser(resultStr);
                        window.Close();
                    }
                });
            }
        }

        //Methods for open the windows
        private void OpenAddClientWindow()
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            SetCenterPositionAndOpen(addClientWindow);
        }
        private void OpenAddManagerWindow()
        {
            AddManagerWindow addManagerWindow = new AddManagerWindow();
            SetCenterPositionAndOpen(addManagerWindow);
        }
        private void OpenAddProductWindow()
        {
            AddProductWindow addProductWindow = new AddProductWindow();
            SetCenterPositionAndOpen(addProductWindow);
        }

        //Methods for open search query the windows
        private void OpenSearchClientManagerWindow()
        {
            ViewClientsManagerWindow viewClientsManagerWindow = new ViewClientsManagerWindow();
            SetCenterPositionAndOpen(viewClientsManagerWindow);
        }
        private void OpenSearchProductClientWindow()
        {
            ViewProductClientWindow viewProductClientWindow = new ViewProductClientWindow();
            SetCenterPositionAndOpen(viewProductClientWindow);
        }
        private void OpenSearchStatusClientWindow()
        {
            ViewStatusClientWindow viewStatusClientWindow = new ViewStatusClientWindow();
            SetCenterPositionAndOpen(viewStatusClientWindow);
        }

        //Methods for edit the windows
        private void EditAddClientWindow(Client client)
        {
            EditClientWindow editClientWindow = new EditClientWindow(client);
            SetCenterPositionAndOpen(editClientWindow);
        }
        private void EditAddManagerWindow(Manager manager)
        {
            EditManagerWindow editManagerWindow = new EditManagerWindow(manager);
            SetCenterPositionAndOpen(editManagerWindow);
        }
        private void EditAddProductWindow(Product product)
        {
            EditProductWindow editProductWindow = new EditProductWindow(product);
            SetCenterPositionAndOpen(editProductWindow);
        }
        private void SetCenterPositionAndOpen(Window wnd)
        {
            wnd.Owner = Application.Current.MainWindow;
            wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wnd.ShowDialog();
        }
        private void ShowMessageToUser(string message)
        {
            MessageWindow messageWindow = new MessageWindow(message);
            SetCenterPositionAndOpen(messageWindow);
        }

        //Update Views
        private void SetNullToProperties()
        {
            ManagerName = null;
            ClientName = null;
            ProductClient = null;
            Status = null;
            SelectManager = null;
            ProductName = null;
            Price = 0;
            Type = null;
            SubPeriod = null;
        }
        private void AllUpdateView()
        {
            UpdateAllClientsView();
            UpdateAllManagersView();
            UpdateAllProductsView();
        }
        private void UpdateAllClientsView()
        {
            AllClients = DataWorker.GetAllClients();
            MainWindow.AllViewClients.ItemsSource = null;
            MainWindow.AllViewClients.Items.Clear();
            MainWindow.AllViewClients.ItemsSource = AllClients;
            MainWindow.AllViewClients.Items.Refresh();
        }
        private void UpdateAllManagersView()
        {
            AllManagers = DataWorker.GetAllManagers();
            MainWindow.AllViewManagers.ItemsSource = null;
            MainWindow.AllViewManagers.Items.Clear();
            MainWindow.AllViewManagers.ItemsSource = AllManagers;
            MainWindow.AllViewManagers.Items.Refresh();
        }
        private void UpdateAllProductsView()
        {
            AllProducts = DataWorker.GetAllProducts();
            MainWindow.AllViewProducts.ItemsSource = null;
            MainWindow.AllViewProducts.Items.Clear();
            MainWindow.AllViewProducts.ItemsSource = AllProducts;
            MainWindow.AllViewProducts.Items.Refresh();
        }
    }
}
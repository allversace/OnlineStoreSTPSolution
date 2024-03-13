using OnlineStoreSTP.Classes;
using OnlineStoreSTP.Models.Data;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStoreSTP.Models
{
    public static class DataWorker
    {
        private readonly static string answerWriting = "Ошибка, проверьте правильность написания!";
        private readonly static string answerRepeat = "Ошибка, такая запись уже существует!";
        private readonly static string answerNotFound = "Ошибка, такой записи не существует!";
        private readonly static string answerSuccessfully = "Успешно!";

        //Get all managers
        public static List<Manager> GetAllManagers()
        {
            var result = SoftTradePlusEntities.GetContext().Manager.ToList();
            return result;
        }

        //Get all clients
        public static List<Client> GetAllClients()
        {
            var result = SoftTradePlusEntities.GetContext().Client.ToList();
            return result;
        }

        //Get all products
        public static List<Product> GetAllProducts()
        {
            var result = SoftTradePlusEntities.GetContext().Product.ToList();
            return result;
        }

        //Create new manager, in the table Manager
        public static string CreateManager(string name)
        {
            try
            {
                bool verification = HelperClass.CheckNumber(name);
                if (verification)
                    return answerWriting;

                var checkIsExist = SoftTradePlusEntities.GetContext().Manager.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
                if (checkIsExist == null)
                {
                    Manager newManager = new Manager { Name = name };
                    SoftTradePlusEntities.GetContext().Manager.Add(newManager);
                    SoftTradePlusEntities.GetContext().SaveChanges();
                    return answerSuccessfully;
                }
                else
                    return answerRepeat;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        //Edit selected manager, in the table Manager
        public static string EditManager(Manager oldManager, string newName)
        {
            try
            {
                bool verification = HelperClass.CheckNumber(newName);
                if (verification)
                    return answerWriting;

                Manager manager = SoftTradePlusEntities.GetContext().Manager.FirstOrDefault(x => x.ManagerId == oldManager.ManagerId);
                if (manager != null)
                {
                    manager.Name = newName;
                    SoftTradePlusEntities.GetContext().SaveChanges();
                    return answerSuccessfully;
                }
                else
                    return answerRepeat;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        //Delete selected manager, in the table Manager
        public static string DeleteManager(Manager manager)
        {
            try
            {
                Manager deleteManager = SoftTradePlusEntities.GetContext().Manager.FirstOrDefault(x => x.ManagerId == manager.ManagerId);
                if (deleteManager != null)
                {
                    SoftTradePlusEntities.GetContext().Manager.Remove(deleteManager);
                    SoftTradePlusEntities.GetContext().SaveChanges();
                    return answerSuccessfully;
                }
                else
                    return answerNotFound;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        //Create new client, in the table Client
        public static string CreateClient(string name, int product, int status, int manager)
        {
            try
            {
                bool verification = HelperClass.CheckNumber(name) || HelperClass.CheckLetter(status.ToString()) || HelperClass.CheckLetter(manager.ToString()) || HelperClass.CheckLetter(product.ToString());
                if (verification)
                    return answerWriting;

                var checkIsExist = SoftTradePlusEntities.GetContext().Client.FirstOrDefault(x => x.Name.ToLower() == name.ToLower() && x.ProductId == product && x.StatusId == status && x.ManagerId == manager);
                if (checkIsExist == null)
                {
                    Client newClient = new Client { Name = name, ProductId = product, StatusId = status, ManagerId = manager };
                    SoftTradePlusEntities.GetContext().Client.Add(newClient);
                    SoftTradePlusEntities.GetContext().SaveChanges();
                    return answerSuccessfully;
                }
                else
                    return answerRepeat;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        //Edit selected client, in the table Client
        public static string EditClient(Client oldClient, string name, int product, int status, int manager)
        {
            try
            {
                bool verification = HelperClass.CheckNumber(name) || HelperClass.CheckLetter(product.ToString()) || HelperClass.CheckLetter(status.ToString()) || HelperClass.CheckLetter(manager.ToString());
                if (verification)
                    return answerWriting;

                Client editClient = SoftTradePlusEntities.GetContext().Client.FirstOrDefault(x => x.ClientId == oldClient.ClientId);
                if (editClient != null)
                {
                    editClient.Name = name;
                    editClient.ProductId = product;
                    editClient.StatusId = status;
                    editClient.ManagerId = manager;
                    SoftTradePlusEntities.GetContext().SaveChanges();
                    return answerSuccessfully;
                }
                else
                    return answerRepeat;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        //Delete selected client, in the table Client
        public static string DeleteClient(Client client)
        {
            try
            {
                Client deleteClient = SoftTradePlusEntities.GetContext().Client.FirstOrDefault(x => x.ClientId == client.ClientId);
                if (deleteClient != null)
                {
                    SoftTradePlusEntities.GetContext().Client.Remove(deleteClient);
                    SoftTradePlusEntities.GetContext().SaveChanges();
                    return answerSuccessfully;
                }
                else
                    return answerNotFound;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        //Create new product, in the table Product
        public static string CreateProduct(string name, decimal price, string type, string subPeriod)
        {
            try
            {
                bool verification = HelperClass.CheckNumber(name) || HelperClass.CheckLetter(price.ToString());
                if (verification)
                    return answerWriting;

                var checkIsExist = SoftTradePlusEntities.GetContext().Product.FirstOrDefault(x => x.Name.ToLower() == name.ToLower() && x.Price == price && x.Type.ToLower() == type.ToLower() && x.SubscriptionPeriod.ToLower() == subPeriod.ToLower());
                if (checkIsExist == null)
                {
                    Product newProduct = new Product { Name = name, Price = price, Type = type, SubscriptionPeriod = subPeriod };
                    SoftTradePlusEntities.GetContext().Product.Add(newProduct);
                    SoftTradePlusEntities.GetContext().SaveChanges();
                    return answerSuccessfully;
                }
                else
                    return answerRepeat;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        //Edit selected product, in the table Product
        public static string EditProduct(Product oldProduct, string name, decimal price, string type, string subPeriod)
        {
            try
            {
                bool verification = HelperClass.CheckNumber(name) || HelperClass.CheckLetter(price.ToString());
                if (verification)
                    return answerWriting;

                Product editProduct = SoftTradePlusEntities.GetContext().Product.FirstOrDefault(x => x.ProductId == oldProduct.ProductId);
                if (editProduct != null)
                {
                    editProduct.Name = name;
                    editProduct.Price = price;
                    editProduct.Type = type;
                    editProduct.SubscriptionPeriod = subPeriod;
                    SoftTradePlusEntities.GetContext().SaveChanges();
                    return answerSuccessfully;
                }
                else
                    return answerRepeat;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        //Delete selected product, in the table Product
        public static string DeleteProduct(Product product)
        {
            try
            {
                Product deleteProduct = SoftTradePlusEntities.GetContext().Product.FirstOrDefault(x => x.ProductId == product.ProductId);
                if (deleteProduct != null)
                {
                    SoftTradePlusEntities.GetContext().Product.Remove(deleteProduct);
                    SoftTradePlusEntities.GetContext().SaveChanges();
                    return answerSuccessfully;
                }
                else
                    return answerNotFound;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

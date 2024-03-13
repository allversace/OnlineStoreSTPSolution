using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineStoreSTP.Models.Data;
using OnlineStoreSTP.ViewModels;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class MainWindowViewModelTests
    {
        [TestMethod]
        public void AllClients_PropertyChanged_Fired()
        {
            var vm = new MainWindowViewModel();
            bool propertyChangedFired = false;
            vm.PropertyChanged += (sender, e) => {
                if (e.PropertyName == nameof(vm.AllClients))
                    propertyChangedFired = true;
            };

            vm.AllClients = new List<Client> { new Client() };

            Assert.IsTrue(propertyChangedFired);
        }
    }

}

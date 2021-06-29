using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    class CustomerManageViewModel : ViewModelBase
    {
        private ObservableCollection<CustomerInfo> listCustomerInfoes;
        private ObservableCollection<TypeCustomer> listCustomerTypes;

        public ObservableCollection<CustomerInfo> ListCustomerInfoes { get => listCustomerInfoes; set { listCustomerInfoes = value; RaisePropertiesChanged(); } }
        public ObservableCollection<TypeCustomer> ListCustomerTypes { get => listCustomerTypes; set {listCustomerTypes = value; RaisePropertiesChanged(); } }

        public CustomerManageViewModel()
        {
            ListCustomerTypes = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers);
            LoadListCustomerInfoes();
        }
        private void LoadListCustomerInfoes()
        {
            ListCustomerInfoes = new ObservableCollection<CustomerInfo>(ListCustomerTypes.Join(DataProvider.Instance.DB.Customers,
                                                                                               type => type.IdTypeCustomer,
                                                                                               customer => customer.IdTypeCustomer,
                                                                                               (type, customer) => new CustomerInfo
                                                                                               {
                                                                                                   Name = customer.FullName,
                                                                                                   IdCard = customer.IdCard,
                                                                                                   Id = customer.IdCustomer,
                                                                                                   Type = type.Displayname,
                                                                                                   Address = customer.Address,
                                                                                               }));
        }
    }
    class CustomerInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public string IdCard { get; set; }
        public int Id { get; set; }
    }
}

using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Views;
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
        private TypeCustomer selectedType;

        public ObservableCollection<CustomerInfo> ListCustomerInfoes { get => listCustomerInfoes; set { listCustomerInfoes = value; RaisePropertiesChanged(); } }
        public ObservableCollection<TypeCustomer> ListCustomerTypes { get => listCustomerTypes; set {listCustomerTypes = value; RaisePropertiesChanged(); } }
        public TypeCustomer SelectedType { get => selectedType; set => selectedType = value; }

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
                                                                                                   Type = type.DisplayName,
                                                                                                   Address = customer.Address,
                                                                                               }));
        }

        [Command]
        public void Filter(CustomerManageView p)
        {
            try
            {
                LoadListCustomerInfoes();

                if (!string.IsNullOrEmpty(p.cbboxFilterType.Text))
                {
                    ListCustomerInfoes = new ObservableCollection<CustomerInfo>(ListCustomerInfoes.Where(x => x.Type == p.cbboxFilterType.Text));
                }

                p.cbboxFilterType.Text = string.Empty;
            }
            catch { return; }
        }

        [Command]
        public void Search(CustomerManageView p)
        {
            try
            {
                LoadListCustomerInfoes();

                if (string.IsNullOrEmpty(p.tboxSearch.Text) || string.IsNullOrWhiteSpace(p.tboxSearch.Text))
                {
                    p.tboxSearch.Text = string.Empty;
                    return;
                }

                ListCustomerInfoes = new ObservableCollection<CustomerInfo>(ListCustomerInfoes.Where(x => x.Name.Contains(p.tboxSearch.Text) || x.IdCard.Contains(p.tboxSearch.Text)));

                p.tboxSearch.Text = string.Empty;
            }
            catch { return; }
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

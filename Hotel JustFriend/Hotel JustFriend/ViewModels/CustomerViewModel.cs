using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Hotel_JustFriend.Views;
using System.Diagnostics;
using System;
using Hotel_JustFriend.UserControls;


namespace Hotel_JustFriend.ViewModels
{
    class CustomerViewModel : ViewModelBase
    {
        private Customer _Customer;
        private ObservableCollection<TypeCustomer> _ListTypeCustomer;
        public ObservableCollection<TypeCustomer> ListTypeCustomer { get => _ListTypeCustomer; set { _ListTypeCustomer = value; RaisePropertyChanged(); } }

        public Customer Customer { get => _Customer; set => _Customer = value; }

        public CustomerViewModel(Customer customer)
        {
            ListTypeCustomer = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers.Where(p=>p.IsDelete==false));
            Customer = customer;
        }

        [Command]
        public void Save(EditCustomerView window)
        {
            try
            {
                if (string.IsNullOrEmpty(window.tbName.Text))
                {
                    MyMessageBox.Show("Nhập họ tên khách hàng", "Thông báo", MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrEmpty(window.cbType.Text))
                {
                    MyMessageBox.Show("Nhập loại khách hàng", "Thông báo", MessageBoxButton.OK);
                    return;
                }

                DataProvider.Instance.DB.SaveChanges();
                MyMessageBox.Show("Sửa thông tin khách hàng thành công", "Thông báo", MessageBoxButton.OK);
                window.Close();
            }
            catch { return; }
        }

        [Command]
        public void Close(Window p)
        {
            try
            {
                p.Close();
            }
            catch { return; }
        }
    }
}

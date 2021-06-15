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
    class CustomerViewModel :ViewModelBase
    {
        private string _fullname;
        private ObservableCollection<Customer> _ListCustomer;
        private string _idCard;
        private string _address;
        private int _idType;
        private int idcustomer;
        private ObservableCollection<TypeCustomer> _ListCustomerType;
        public string Fullname { get => _fullname; set => _fullname = value; }
        public string IdCard { get => _idCard; set => _idCard = value; }
        public string Address { get => _address; set => _address = value; }
        public int idType { get => _idType; set => _idType = value; }
        public ObservableCollection<TypeCustomer> ListCustomerType { get => _ListCustomerType; set { _ListCustomerType = value; RaisePropertyChanged(); } }

        public ObservableCollection<Customer> ListCustomer { get => _ListCustomer; set => _ListCustomer = value; }

        public CustomerViewModel(Customer a)
        {
            ListCustomerType = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers);
            Fullname = a.fullname;
            IdCard = a.idCard;
            Address = a.address;
            idType = a.idType;
            idcustomer = a.idCustomer;
        }
        [Command]
        public void Save(EditCustomerView p)
        {
            ListCustomer = new ObservableCollection<Customer>(DataProvider.Instance.DB.Customers);
            Customer a = ListCustomer.Where((c)=>c.idCustomer==idcustomer).FirstOrDefault();
            a.fullname = Fullname;
            a.idCard = IdCard;
            a.address = Address;
            a.idType = int.Parse(p.txtType.SelectedValue.ToString());
            DataProvider.Instance.DB.SaveChanges();
            MyMessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButton.OK);
            p.Close();
        }
        [Command]
        public void Close2(DetailCustomer p)
        {
            try
            {
                p.Close();
            }
            catch { return; }
        }
        [Command]
        public void Close1(EditCustomerView p)
        {
            try
            {
                p.Close();
            }
            catch { return; }
        }

    }
}

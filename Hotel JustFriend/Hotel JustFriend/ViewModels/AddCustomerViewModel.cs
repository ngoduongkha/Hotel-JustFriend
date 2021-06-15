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
using System.Windows;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    class AddCustomerViewModel : ViewModelBase
    {
        public ObservableCollection<TypeCustomer> ListCustomerType { get; set; }
        public string Fullname { get; set; }
        public string IdCard { get; set; }
        public Room SelectedRoom { get; set; }
        public AddCustomerViewModel(Room selectedRoom)
        {
            ListCustomerType = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers.ToList());
            SelectedRoom = selectedRoom;
        }
        [Command]
        public void MouseMoveWindow(Window p)
        {
            try
            {
                p.DragMove();
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
        
        [Command]
        public void Save(AddCustomerWindow p)
        {
            try
            {
                ObservableCollection<Customer> ListCustomer = new ObservableCollection<Customer>(DataProvider.Instance.DB.Customers);
                for (int i = 0; i < ListCustomer.Count; i++)
                {
                    if (ListCustomer[i].idCard == IdCard)
                    {
                        MyMessageBox.Show("Chứng minh nhân dân đã tồn tại", "Thông báo", MessageBoxButton.OK);
                        return;
                    }
                }
                Customer newCustomer = new Customer()
                {
                    fullname = Fullname,
                    idCard = IdCard,
                    idType = int.Parse(p.txtType.SelectedValue.ToString()),
                    address = p.txtNote.Text,
                };
                DataProvider.Instance.DB.Customers.Add(newCustomer);
                //-------
                int idCustomer = newCustomer.idCustomer;
                int idRoom = SelectedRoom.idRoom;
                RentInvoice ll = DataProvider.Instance.DB.RentInvoices.Where((o) => o.idRoom == SelectedRoom.idRoom).FirstOrDefault();
                RentInvoiceInfo info = new RentInvoiceInfo()
                {
                    idCustomer = newCustomer.idCustomer,
                    idRentInvoice = DataProvider.Instance.DB.RentInvoices.Where((o) => o.idRoom == SelectedRoom.idRoom).FirstOrDefault().idRentInvoice,
                };
                DataProvider.Instance.DB.RentInvoiceInfoes.Add(info);
                DataProvider.Instance.DB.SaveChanges();
                MyMessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK);
                Close(p);
            }
            catch
            {
                return;
            }
        }
    }
}

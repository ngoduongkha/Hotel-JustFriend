using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Views;
using System.Collections.ObjectModel;
using System.Linq;

namespace Hotel_JustFriend.ViewModels
{
    class AddCustomerViewModel : ViewModelBase
    {
        private Customer _CustomerRenting;

        public Room SelectedRoom { get; set; }
        public ObservableCollection<TypeCustomer> ListTypeCustomer { get; set; }
        public Customer CustomerRenting { get => _CustomerRenting; set { _CustomerRenting = value; RaisePropertyChanged(); } }

        public AddCustomerViewModel(Room selectedRoom)
        {
            SelectedRoom = selectedRoom;
            ListTypeCustomer = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers.Where(p => p.IsDelete == false));
            CustomerRenting = new Customer();
        }

        [Command]
        public void AutoFill(AddCustomerWindow window)
        {
            Customer customerInDB = DataProvider.Instance.DB.Customers.Where(x => x.IdCard == CustomerRenting.IdCard).SingleOrDefault();

            if (customerInDB != null)
            {
                CustomerRenting = customerInDB;
            }
            else
            {
                window.tbName.Text = string.Empty;
                window.tbAddress.Text = string.Empty;
                window.cbType.SelectedValue = 0;
            }
        }

        [Command]
        public void Save(AddCustomerWindow window)
        {
            try
            {
                if (string.IsNullOrEmpty(window.tbIdCard.Text))
                {
                    MyMessageBox.Show("Nhập CMND/CCCD", "Thông báo", System.Windows.MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrEmpty(window.tbName.Text))
                {
                    MyMessageBox.Show("Nhập họ tên khách hàng", "Thông báo", System.Windows.MessageBoxButton.OK);
                    return;
                }
                if (string.IsNullOrEmpty(window.cbType.Text))
                {
                    MyMessageBox.Show("Nhập loại khách hàng", "Thông báo", System.Windows.MessageBoxButton.OK);
                    return;
                }

                if (DataProvider.Instance.DB.Customers.Where(x => x.IdCard == CustomerRenting.IdCard).Count() == 0)
                {
                    DataProvider.Instance.DB.Customers.Add(CustomerRenting);
                    DataProvider.Instance.DB.SaveChanges();
                }

                int idRentInvoice = DataProvider.Instance.DB.RentInvoices.Where(x => x.Purchase == false).Where(x => x.IdRoom == SelectedRoom.IdRoom).First().IdRentInvoice;

                if (DataProvider.Instance.DB.RentInvoiceInfoes.Where(x => x.IdRentInvoice == idRentInvoice).Where(x => x.IdCustomer == CustomerRenting.IdCustomer).Count() != 0)
                {
                    MyMessageBox.Show("Khách hàng đã được thêm", "Thông báo", System.Windows.MessageBoxButton.OK);
                    return;
                }

                RentInvoiceInfo rentInvoiceInfo = new RentInvoiceInfo()
                {
                    IdCustomer = CustomerRenting.IdCustomer,
                    IdRentInvoice = DataProvider.Instance.DB.RentInvoices.Where(x => x.Purchase == false).Where(x => x.IdRoom == SelectedRoom.IdRoom).First().IdRentInvoice
                };

                DataProvider.Instance.DB.RentInvoiceInfoes.Add(rentInvoiceInfo);
                DataProvider.Instance.DB.SaveChanges();
                MyMessageBox.Show("Thêm thành công", "Thông báo", System.Windows.MessageBoxButton.OK);
                window.Close();
            }
            catch (System.Exception e)  { System.Diagnostics.Debug.WriteLine(e.InnerException); return; }
        }

        [Command]
        public void Close(AddCustomerWindow window)
        {
            try
            {
                window.Close();
            }
            catch { return; }
        }
    }
}

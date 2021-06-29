using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using Hotel_JustFriend.Models;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    class RoomRentalViewModel : ViewModelBase
    {
        private ObservableCollection<Room> ListRoom1;
        private ObservableCollection<RentInvoice> ListRentInvoice;
        private ObservableCollection<TypeCustomer> _ListCustomerType;
        private ObservableCollection<Customer> _ListCustomer;
        private string _fullname;
        private string _idCard;
        private string _address;
        private string _dateOfBirth;
        public string Fullname { get => _fullname; set => _fullname = value; }
        public string IdCard { get => _idCard; set => _idCard = value; }
        public string Address { get => _address; set => _address = value; }
        public string DateOfBirth { get => _dateOfBirth; set => _dateOfBirth = value; }
        public ObservableCollection<TypeCustomer> ListCustomerType { get => _ListCustomerType; set { _ListCustomerType = value; RaisePropertiesChanged(); } }
        public ObservableCollection<Customer> ListCustomer { get => _ListCustomer; set { _ListCustomer = value; RaisePropertiesChanged(); } }
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
        public void Rent(RoomRentalView p)
        {
            try
            {
                int k = int.Parse(p.idRoom.Text);
                int d = 0;
                ListRoom1 = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms
                       .Where(x => x.IsDelete == false)
                       );
    
                Room result = (from u in ListRoom1
                               where u.IdRoom == k
                               select u).Single();
                result.Status = "Sẵn sàng";
                //---
                d = 0;
                ListRentInvoice = new ObservableCollection<RentInvoice>(DataProvider.Instance.DB.RentInvoices);
                for (int i=0;i<ListRentInvoice.Count;i++)
                {
                    if (d < ListRentInvoice[i].IdRentInvoice) d = ListRentInvoice[i].IdRentInvoice;
                }
                d++;
                RentInvoice rentiv = new RentInvoice()
                {
                    IdRentInvoice = d,
                    IdRoom = int.Parse(p.idRoom.Text),
                    //DateStart = DateTime.Now,
                };
                p.idRentInvoice.Text = d.ToString();
                DataProvider.Instance.DB.SaveChanges();
            }
            catch { return; }
        }
        //[Command]
        //public void AddCustomer(RoomRentalView p)
        //{
        //    AddCustomerWindow window = new AddCustomerWindow(SelectedRoom);
        //    window.idroom = p.idRoom;
        //    window.idrentiv = p.idRentInvoice;
        //    window.ShowDialog();
        //}
        [Command]
        public void Save(AddCustomerWindow p)
        {
            try
            {
                int d = -1;
                ListCustomer = new ObservableCollection<Customer>(DataProvider.Instance.DB.Customers);
                for (int i = 0; i < ListRoom1.Count; i++)
                {
                    if (d < ListRoom1[i].IdRoom) d = ListRoom1[i].IdRoom;
                }
                d++;
                Customer newCustomer = new Customer()
                {
                    IdCustomer = d,
                    FullName = Fullname,
                    IdCard = IdCard,
                    IdTypeCustomer = int.Parse(p.txtType.SelectedValue.ToString()),
                };
                DataProvider.Instance.DB.Customers.Add(newCustomer);
                RentInvoiceInfo info = new RentInvoiceInfo()
                {
                    IdCustomer = d,
                    IdRentInvoice = int.Parse(p.idrentiv.Text),
                };
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

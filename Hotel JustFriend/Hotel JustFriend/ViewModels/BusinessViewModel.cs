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
    [POCOViewModel]
    public class BusinessViewModel : ViewModelBase
    {
        private ObservableCollection<Customer> _Trick;
        private ObservableCollection<RentInvoice> ListRentInvoice;
        private ObservableCollection<TypeCustomer> _ListCustomerType;
        private ObservableCollection<Customer> _ListCustomer;
        private ObservableCollection<RentInvoice> _ListRentInvoice;
        private ObservableCollection<RentInvoiceInfo> _ListRentInvoiceInfo;
        private string _fullname;
        private string _idCard;
        private string _address;
        private string _CustomerType;
        public string Fullname { get => _fullname; set => _fullname = value; }
        public string IdCard { get => _idCard; set => _idCard = value; }
        public string Address { get => _address; set => _address = value; }
        public ObservableCollection<TypeCustomer> ListCustomerType { get => _ListCustomerType; set { _ListCustomerType = value; RaisePropertiesChanged(); } }
        public ObservableCollection<Customer> ListCustomer { get => _ListCustomer; set { _ListCustomer = value; RaisePropertiesChanged(); } }
        private ObservableCollection<Room> _ListRoom;
        static private Room _SelectedRoom;
        private Customer _SelectedCustomer;
        public ObservableCollection<Room> ListRoom { get => _ListRoom; set { _ListRoom = value; RaisePropertyChanged(); } }
        public Room SelectedRoom { get => _SelectedRoom; set { _SelectedRoom = value; RaisePropertyChanged(); } }
        public Customer SelectedCustomer { get => _SelectedCustomer; set { _SelectedCustomer = value; RaisePropertyChanged(); } }

        public ObservableCollection<RentInvoiceInfo> ListRentInvoiceInfo { get => _ListRentInvoiceInfo; set { _ListRentInvoiceInfo = value; RaisePropertiesChanged(); } }

        public string CustomerType { get => _CustomerType; set => _CustomerType = value; }
        public ObservableCollection<Customer> Trick { get => _Trick; set { _Trick = value; RaisePropertyChanged(); } }

        public BusinessViewModel()
        {
            ListCustomerType = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers);
            LoadDB();
        }

        private void LoadDB()
        {
            try
            {
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms.Where(x => x.isDelete == false).OrderBy(x => x.floor).ThenBy(x => x.displayName));
            }
            catch { return; }
        }
        [Command]
        public void SelectRoom(Room selectedRoom)
        {
            try
            {
                Debug.WriteLine(selectedRoom.displayName);
                SelectedRoom = selectedRoom;
                //--------------------------//
                if (selectedRoom.status == true)
                {
                    int idrent = -1;
                    if (SelectedRoom != null)

                        idrent = DataProvider.Instance.DB.RentInvoices.Where((p) => p.idRoom == SelectedRoom.idRoom).FirstOrDefault().idRentInvoice;
                    ListRentInvoiceInfo = new ObservableCollection<RentInvoiceInfo>(DataProvider.Instance.DB.RentInvoiceInfoes
                                                                                    .Where((p) => p.idRentInvoice == idrent));
                }
                else ListRentInvoiceInfo = null;
                //--------------------------//
            }
            catch { return; }
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
        public void Close1(AddCustomerWindow p)
        {
            try
            {
                p.Close();
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
        public void Rent()
        {
            try
            {
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms
                       .Where(x => x.isDelete == false)
                       );
                Room result = ListRoom.Where((p) => p.idRoom == SelectedRoom.idRoom).FirstOrDefault();
                result.status = true;
                //---
                ListRentInvoice = new ObservableCollection<RentInvoice>(DataProvider.Instance.DB.RentInvoices);
                RentInvoice k = ListRentInvoice.Where((p) => p.idRoom == SelectedRoom.idRoom).FirstOrDefault();
                if (k==null)
                {
                    RentInvoice r = new RentInvoice()
                    {
                        idRoom = SelectedRoom.idRoom,
                        date= DateTime.Now,
                        purchase = true,
                    };
                    DataProvider.Instance.DB.RentInvoices.Add(r);
                }
                DataProvider.Instance.DB.SaveChanges();
            }
            catch { return; }
        }
        [Command]
        public void AddCustomer(BusinessView p)
        {
            AddCustomerWindow window = new AddCustomerWindow();
            window.ShowDialog();
        }
        [Command]
        public void DeleteCustomer()
        {
            ListCustomer = new ObservableCollection<Customer>(DataProvider.Instance.DB.Customers);
            Customer a = ListCustomer.Where((p) => p.idCustomer == SelectedCustomer.idCustomer).FirstOrDefault();
            RentInvoiceInfo b = ListRentInvoiceInfo.Where((p) => p.idCustomer == SelectedCustomer.idCustomer).FirstOrDefault();
            DataProvider.Instance.DB.RentInvoiceInfoes.Remove(b);
            DataProvider.Instance.DB.Customers.Remove(a);
            DataProvider.Instance.DB.SaveChanges();
        }
        [Command] void DetailCustomer()
        {
            Customer a = ListCustomer.Where((p) => p.idCustomer == SelectedCustomer.idCustomer).FirstOrDefault();
            
        }
        [Command] void EditCustomer()
        {
        }
        [Command]
        public void Save(AddCustomerWindow p)
        {
            try
            {
                ListCustomer = new ObservableCollection<Customer>(DataProvider.Instance.DB.Customers);
                for (int i=0;i<ListCustomer.Count;i++)
                {
                    if (ListCustomer[i].idCard==IdCard)
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
                int ii = newCustomer.idCustomer;
                int idd = SelectedRoom.idRoom;
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

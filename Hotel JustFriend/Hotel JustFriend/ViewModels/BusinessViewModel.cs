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
using Hotel_JustFriend.Template;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    public class BusinessViewModel : ViewModelBase
    {
        private ObservableCollection<Customer> _Trick = new ObservableCollection<Customer>();
        static private Customer _TrickCustomer;
        private ObservableCollection<RentInvoice> ListRentInvoice;
        private ObservableCollection<TypeCustomer> _ListCustomerType;
        private ObservableCollection<Customer> _ListCustomer;
        private ObservableCollection<RentInvoiceInfo> _ListRentInvoiceInfo;
        private ObservableCollection<TypeRoom> _ListRoomType;
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
        static private RentInvoice _SelectedRentInvoice;
        static private RentInvoiceInfo _SelectedCustomer;
        public ObservableCollection<Room> ListRoom { get => _ListRoom; set { _ListRoom = value; RaisePropertyChanged(); } }
        public Room SelectedRoom { get => _SelectedRoom; set { _SelectedRoom = value; RaisePropertyChanged(); } }

        public ObservableCollection<RentInvoiceInfo> ListRentInvoiceInfo { get => _ListRentInvoiceInfo; set { _ListRentInvoiceInfo = value; RaisePropertiesChanged(); } }

        public string CustomerType { get => _CustomerType; set => _CustomerType = value; }
        public ObservableCollection<Customer> Trick { get => _Trick; set { _Trick = value; RaisePropertyChanged(); } }
        public RentInvoiceInfo SelectedCustomer { get => _SelectedCustomer; set { _SelectedCustomer = value; RaisePropertyChanged(); } }

        public Customer TrickCustomer { get => _TrickCustomer; set { _TrickCustomer = value; RaisePropertiesChanged(); } }

        public RentInvoice SelectedRentInvoice { get => _SelectedRentInvoice; set { _SelectedRentInvoice = value; RaisePropertyChanged(); } }

        public ObservableCollection<TypeRoom> ListRoomType { get => _ListRoomType; set { _ListRoomType = value; RaisePropertiesChanged(); } }

        public BusinessViewModel()
        {
            ListCustomerType = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers.Where(p => p.isDelete == false));
            LoadDB();
        }
        private void loadcs()
        {
            if (SelectedRoom.status == true)
            {
                int idrent = -1;
                Trick.Clear();
                if (SelectedRoom != null)

                    idrent = DataProvider.Instance.DB.RentInvoices.Where((p) => p.idRoom == SelectedRoom.idRoom).FirstOrDefault().idRentInvoice;
                ListRentInvoiceInfo = new ObservableCollection<RentInvoiceInfo>(DataProvider.Instance.DB.RentInvoiceInfoes
                                                                                .Where((p) => p.idRentInvoice == idrent));
                ListCustomer = new ObservableCollection<Customer>(DataProvider.Instance.DB.Customers);
                for (int i = 0; i < ListCustomer.Count; i++)
                    for (int j = 0; j < ListRentInvoiceInfo.Count; j++)
                        if (ListCustomer[i].idCustomer == ListRentInvoiceInfo[j].idCustomer)
                        {
                            Customer a = new Customer();
                            a = ListCustomer[i];
                            Trick.Add(a);

                        }
            }
            else ListRentInvoiceInfo = null;
        }
        private void LoadDB()
        {
            try
            {
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms.Where(x => x.isDelete == false).OrderBy(x => x.floor).ThenBy(x => x.displayName));
                ListRoomType = (ObservableCollection<TypeRoom>)new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms.Where(c => c.isDelete == false));

            }
            catch { return; }
        }
        [Command]
        public void Pay()
        {
            if (SelectedRoom == null) return;
            DateTime da = new DateTime();
            da = DateTime.Now;
            DateTime a = (DateTime)SelectedRentInvoice.date;
            int d = da.Subtract(a).Days;
            d = d + 1;
            Bill bill = new Bill
            {
                date = SelectedRentInvoice.date,
                totalMoney = 0,
            };
            BillTemplate billtemp = new BillTemplate();
            billtemp.date.Text = d.ToString();

            billtemp.Roomname.Text = DataProvider.Instance.DB.Rooms.Where(c => c.idRoom == SelectedRoom.idRoom).FirstOrDefault().displayName;
            DataProvider.Instance.DB.Bills.Add(bill);
            decimal Price = (decimal)DataProvider.Instance.DB.TypeRooms.Where(c => c.idType == SelectedRoom.idType).FirstOrDefault().price;
            int idrent = DataProvider.Instance.DB.RentInvoices.Where((c) => c.idRoom == SelectedRoom.idRoom).FirstOrDefault().idRentInvoice;
            ListRentInvoiceInfo = new ObservableCollection<RentInvoiceInfo>(DataProvider.Instance.DB.RentInvoiceInfoes
                                                                                    .Where((c) => c.idRentInvoice == idrent));
            int lp = ListRentInvoiceInfo.Count;
            double heso = 0;
            BillInfo billinfo = new BillInfo
            {
                numberDay = d,
                price = Price,
                idBill = bill.idBill,
                idRoom = SelectedRoom.idRoom,
            };
            DataProvider.Instance.DB.BillInfoes.Add(billinfo);
            for (int i = 0; i < ListRentInvoiceInfo.Count; i++)
            {

                int idd = ListRentInvoiceInfo[i].idCustomer;
                Customer cus = DataProvider.Instance.DB.Customers.Where(c => c.idCustomer == idd).FirstOrDefault();
                double tg = (double)DataProvider.Instance.DB.TypeCustomers.Where(c => c.idType == cus.idType).FirstOrDefault().number;
                if (tg > heso) heso = tg;
                DataProvider.Instance.DB.BillInfoes.Add(billinfo);
                DataProvider.Instance.DB.RentInvoiceInfoes.Remove(ListRentInvoiceInfo[i]);
                BillDetailUC aa = new BillDetailUC();
                aa.STT.Text = (i + 1).ToString();
                aa.CustomerName.Text = cus.fullname.ToString();
                if (cus.address != null) aa.Address.Text = cus.address.ToString();
                else aa.Address.Text = "";
                aa.Type.Text = DataProvider.Instance.DB.TypeCustomers.Where(c => c.idType == cus.idType).FirstOrDefault().displayname;
                aa.number.Text = tg.ToString();
                billtemp.stp.Children.Add(aa);
            }
            int maxcustomer = (int)DataProvider.Instance.DB.Constants.Where(c => c.idConstant == 0).FirstOrDefault().maxCustomer;
            double percent = (double)DataProvider.Instance.DB.Constants.Where(c => c.idConstant == 0).FirstOrDefault().percent;
            int sokhach = 0;
            if (ListRentInvoiceInfo.Count > maxcustomer) sokhach = ListRentInvoiceInfo.Count - maxcustomer;
            bill.totalMoney = d * (Price + ((decimal)(sokhach * percent) * Price) + (Price * (decimal)(heso - 1)));
            billtemp.totalmoney.Text = string.Format("{0:C}", bill.totalMoney);
            billtemp.money.Text = string.Format("{0:C}", d * Price);
            billtemp.price.Text = string.Format("{0:C}", Price);
            billtemp.surcharge.Text = string.Format("{0:C}", ((decimal)(sokhach * percent) * Price) + (Price * (decimal)(heso - 1)) * d);
            ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms
                   .Where(x => x.isDelete == false)
                   );
            Room p = ListRoom.Where(c => c.idRoom == SelectedRoom.idRoom).FirstOrDefault();
            p.status = false;
            RentInvoice k = DataProvider.Instance.DB.RentInvoices.Where((c) => c.idRoom == p.idRoom).FirstOrDefault();

            if (k != null) DataProvider.Instance.DB.RentInvoices.Remove(k);
            DataProvider.Instance.DB.SaveChanges();

            SelectedRentInvoice = null;
            SelectRoom(p); 
            if (lp == 0)
            {
                MyMessageBox.Show("Phòng trống không thể thanh toán", "Nhắc nhở", MessageBoxButton.OK);
            }
            else billtemp.ShowDialog();
        }
        [Command]
        public void SelectRoom(Room selectedRoom)
        {
            try
            {
                Debug.WriteLine(selectedRoom.displayName);
                SelectedRoom = selectedRoom;
                SelectedRentInvoice = DataProvider.Instance.DB.RentInvoices.Where(p => p.idRoom == selectedRoom.idRoom).FirstOrDefault();
                //--------------------------//
                if (selectedRoom.status == true)
                {
                    if (Trick != null) Trick.Clear();
                    int idrent = -1;
                    if (SelectedRoom != null)
                        idrent = DataProvider.Instance.DB.RentInvoices.Where((p) => p.idRoom == SelectedRoom.idRoom).FirstOrDefault().idRentInvoice;
                    ListRentInvoiceInfo = new ObservableCollection<RentInvoiceInfo>(DataProvider.Instance.DB.RentInvoiceInfoes
                                                                                    .Where((p) => p.idRentInvoice == idrent));
                    ListCustomer = new ObservableCollection<Customer>(DataProvider.Instance.DB.Customers);
                    for (int i = 0; i < ListCustomer.Count; i++)
                        for (int j = 0; j < ListRentInvoiceInfo.Count; j++)
                            if (ListCustomer[i].idCustomer == ListRentInvoiceInfo[j].idCustomer)
                            {
                                Customer a = new Customer();
                                a = ListCustomer[i];
                                Trick.Add(a);
                            }
                }
                else
                {
                    Trick.Clear();
                    ListRentInvoiceInfo = null;
                }
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
            if (SelectedRoom == null) return;
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
                if (k == null)
                {
                    RentInvoice r = new RentInvoice()
                    {
                        idRoom = SelectedRoom.idRoom,
                        date = DateTime.Now,
                        purchase = true,
                    };
                    DataProvider.Instance.DB.RentInvoices.Add(r);
                }
                DataProvider.Instance.DB.SaveChanges();
                SelectRoom(result);
            }
            catch { return; }
        }
        [Command]
        public void AddCustomer(BusinessView p)
        {
            if (SelectedRoom == null) return;
            try
            {
                AddCustomerWindow window = new AddCustomerWindow();
                window.ShowDialog();
                loadcs();
            }
            catch
            { return; }
        }
        [Command]
        public void DeleteCustomer()
        {
            int k = TrickCustomer.idCustomer;
            ListCustomer = new ObservableCollection<Customer>(DataProvider.Instance.DB.Customers);
            Customer a = ListCustomer.Where((p) => p.idCustomer == k).FirstOrDefault();
            RentInvoiceInfo b = ListRentInvoiceInfo.Where((p) => p.idCustomer == k).FirstOrDefault();
            DataProvider.Instance.DB.RentInvoiceInfoes.Remove(b);
            DataProvider.Instance.DB.Customers.Remove(a);
            DataProvider.Instance.DB.SaveChanges();
            loadcs();
        }
        [Command]
        public void DetailCustomer()
        {
            ListCustomer = new ObservableCollection<Customer>(DataProvider.Instance.DB.Customers);
            int k = TrickCustomer.idCustomer;
            Customer a = ListCustomer.Where((p) => p.idCustomer == k).FirstOrDefault();
            DetailCustomer c = new DetailCustomer(a);
            c.ShowDialog();
            loadcs();
        }
        [Command]
        public void EditCustomer()
        {
            ListCustomer = new ObservableCollection<Customer>(DataProvider.Instance.DB.Customers);
            int k = TrickCustomer.idCustomer;
            Customer a = ListCustomer.Where((p) => p.idCustomer == k).FirstOrDefault();
            EditCustomerView c = new EditCustomerView(a);
            c.ShowDialog();
            loadcs();
        }
        [Command]
        public void Save(AddCustomerWindow p)
        {
            try
            {
                ListCustomer = new ObservableCollection<Customer>(DataProvider.Instance.DB.Customers);
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
        //---------------
        [Command]
        public void RoomFilter(BusinessView p)
        {
            // try
            //{
            ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms
                .Where(x => x.isDelete == false)
                .OrderBy(x => x.floor)
                .ThenBy(x => x.displayName));
            if (string.IsNullOrEmpty(p.txtFilterStatus.Text) && !string.IsNullOrEmpty(p.txtFilterType.Text))
            {
                int k = int.Parse(p.txtFilterType.SelectedValue.ToString());
                ListRoom = new ObservableCollection<Room>(ListRoom
                    .Where(x => x.idType == k)
                    .OrderBy(x => x.floor)
                    .ThenBy(x => x.displayName));
            }
            else if (!string.IsNullOrEmpty(p.txtFilterStatus.Text) && string.IsNullOrEmpty(p.txtFilterType.Text))
            {
                int id = 0;
                if (p.txtFilterStatus.Text == "Sẵn sàng") id = 0;
                else id = 1;
                if (id == 0)
                {
                    ListRoom = new ObservableCollection<Room>(ListRoom
                        .Where(x => x.status == false)
                        .OrderBy(x => x.floor)
                        .ThenBy(x => x.displayName));
                }
                else
                {
                    ListRoom = new ObservableCollection<Room>(ListRoom
                      .Where(x => x.status == true)
                      .OrderBy(x => x.floor)
                      .ThenBy(x => x.displayName));
                }
            }
            else if (!string.IsNullOrEmpty(p.txtFilterStatus.Text) && !string.IsNullOrEmpty(p.txtFilterType.Text))
            {
                int id = 0;
                if (p.txtFilterStatus.Text == "Sẵn sàng") id = 0;
                else id = 1;
                int k = int.Parse(p.txtFilterType.SelectedValue.ToString());
                if (id == 0)
                {
                    ListRoom = new ObservableCollection<Room>(ListRoom
                    .Where(x => x.idType == k && x.status == false)
                    .OrderBy(x => x.floor)
                    .ThenBy(x => x.displayName));
                }
                else
                {
                    ListRoom = new ObservableCollection<Room>(ListRoom
                    .Where(x => x.idType == k && x.status == true)
                    .OrderBy(x => x.floor)
                    .ThenBy(x => x.displayName));
                }

            }
            p.txtFilterStatus.Text = string.Empty;
            p.txtFilterType.Text = string.Empty;
            //    }
            //   catch { return; }
        }

        [Command]
        public void SearchRoom(BusinessView p)
        {
            try
            {
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms
                    .Where(x => x.isDelete == false)
                    .OrderBy(x => x.floor)
                    .ThenBy(x => x.displayName));

                if (string.IsNullOrEmpty(p.txtSearch.Text))
                    return;

                ListRoom = new ObservableCollection<Room>(ListRoom
                    .Where(x => x.displayName.Contains(p.txtSearch.Text))
                    .OrderBy(x => x.floor)
                    .ThenBy(x => x.displayName)
                    .ToList());
                p.txtSearch.Text = string.Empty;
            }
            catch { return; }
        }
    }
}

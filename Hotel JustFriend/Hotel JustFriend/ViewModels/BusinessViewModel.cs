using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Hotel_JustFriend.Views;
using System.Diagnostics;
using System;
using Hotel_JustFriend.UserControls;
using Hotel_JustFriend.Template;

namespace Hotel_JustFriend.ViewModels
{
    public class CustomerWithRentInvoice
    {
        public int IdCustomer { get; set; }
        public string FullName { get; set; }     
    }

    [POCOViewModel]
    public class BusinessViewModel : ViewModelBase
    {
        static private Customer _TrickCustomer;
        private ObservableCollection<TypeCustomer> _ListCustomerType;
        private ObservableCollection<Customer> _ListCustomer;
        private ObservableCollection<TypeRoom> _ListRoomType;
        private ObservableCollection<CustomerWithRentInvoice> _ListRentInvoiceInfo;
        private CustomerWithRentInvoice _SelectedCustomerRenting;

        private string _fullname;
        private string _idCard;
        private string _address;
        private string _CustomerType;
        public Visibility SwitchButton { get; set; }
        public bool CanAddUser { get; set; }
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

        public string CustomerType { get => _CustomerType; set => _CustomerType = value; }
        public RentInvoiceInfo SelectedCustomer { get => _SelectedCustomer; set { _SelectedCustomer = value; RaisePropertyChanged(); } }

        public Customer TrickCustomer { get => _TrickCustomer; set { _TrickCustomer = value; RaisePropertiesChanged(); } }

        public RentInvoice SelectedRentInvoice { get => _SelectedRentInvoice; set { _SelectedRentInvoice = value; RaisePropertyChanged(); } }

        public ObservableCollection<TypeRoom> ListRoomType { get => _ListRoomType; set { _ListRoomType = value; RaisePropertiesChanged(); } }

        public ObservableCollection<CustomerWithRentInvoice> ListRentInvoiceInfo { get => _ListRentInvoiceInfo; set { _ListRentInvoiceInfo = value; RaisePropertyChanged(); } }

        public CustomerWithRentInvoice SelectedCustomerRenting { get => _SelectedCustomerRenting; set => _SelectedCustomerRenting = value; }

        public BusinessViewModel()
        {
            ListCustomerType = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers.Where(p => p.IsDelete == false));
            ListRoomType = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms.Where(c => c.IsDelete == false));
            CanAddUser = false;

            LoadDB();
        }

        private void LoadDB()
        {
            try
            {
                ListRoom = new ObservableCollection<Room>(
                    DataProvider.Instance.DB.Rooms
                    .Where(x => x.IsDelete == false)
                    .OrderBy(x => x.Floor)
                    .ThenBy(x => x.Number));
            }
            catch { return; }
        }

        //[Command]
        //public void Pay()
        //{
        //    if (SelectedRoom == null) return;
        //    DateTime da = new DateTime();
        //    da = DateTime.Now;
        //    DateTime a = (DateTime)SelectedRentInvoice.Date;
        //    int d = da.Subtract(a).Days;
        //    d = d + 1;
        //    Bill bill = new Bill
        //    {
        //        Date = SelectedRentInvoice.Date,
        //        TotalMoney = 0,
        //    };
        //    BillTemplate billtemp = new BillTemplate();
        //    billtemp.date.Text = d.ToString();

        //    billtemp.Roomname.Text = DataProvider.Instance.DB.Rooms.Where(c => c.IdRoom == SelectedRoom.IdRoom).FirstOrDefault().DisplayName;
        //    DataProvider.Instance.DB.Bills.Add(bill);
        //    decimal Price = (decimal)DataProvider.Instance.DB.TypeRooms.Where(c => c.IdTypeRoom == SelectedRoom.IdTypeRoom).FirstOrDefault().Price;
        //    int idrent = DataProvider.Instance.DB.RentInvoices.Where((c) => c.IdRoom == SelectedRoom.IdRoom).FirstOrDefault().IdRentInvoice;
        //    ListRentInvoiceInfo = new ObservableCollection<RentInvoiceInfo>(DataProvider.Instance.DB.RentInvoiceInfoes
        //                                                                            .Where((c) => c.IdRentInvoice == idrent));
        //    int lp = ListRentInvoiceInfo.Count;
        //    double heso = 0;
        //    BillInfo billinfo = new BillInfo
        //    {
        //        NumberDay = d,
        //        Price = Price,
        //        IdBill = bill.IdBill,
        //        IdRoom = SelectedRoom.IdRoom,
        //    };
        //    DataProvider.Instance.DB.BillInfoes.Add(billinfo);
        //    for (int i = 0; i < ListRentInvoiceInfo.Count; i++)
        //    {

        //        int idd = ListRentInvoiceInfo[i].IdCustomer;
        //        Customer cus = DataProvider.Instance.DB.Customers.Where(c => c.IdCustomer == idd).FirstOrDefault();
        //        double tg = (double)DataProvider.Instance.DB.TypeCustomers.Where(c => c.IdTypeCustomer == cus.IdTypeCustomer).FirstOrDefault().CoefficientsObtained;
        //        if (tg > heso) heso = tg;
        //        DataProvider.Instance.DB.BillInfoes.Add(billinfo);
        //        DataProvider.Instance.DB.RentInvoiceInfoes.Remove(ListRentInvoiceInfo[i]);
        //        BillDetailUC aa = new BillDetailUC();
        //        aa.STT.Text = (i + 1).ToString();
        //        aa.CustomerName.Text = cus.FullName.ToString();
        //        aa.Type.Text = DataProvider.Instance.DB.TypeCustomers.Where(c => c.IdTypeCustomer == cus.IdTypeCustomer).FirstOrDefault().Displayname;
        //        aa.number.Text = tg.ToString();
        //        billtemp.stp.Children.Add(aa);
        //    }
        //    int maxcustomer = (int)DataProvider.Instance.DB.Constants.Where(c => c.IdConstant == 0).FirstOrDefault().MaxCustomer;
        //    double percent = (double)DataProvider.Instance.DB.Constants.Where(c => c.IdConstant == 0).FirstOrDefault().Percent;
        //    int sokhach = 0;
        //    if (ListRentInvoiceInfo.Count > maxcustomer) sokhach = ListRentInvoiceInfo.Count - maxcustomer;
        //    bill.TotalMoney = d * (Price + ((decimal)(sokhach * percent) * Price) + (Price * (decimal)(heso - 1)));
        //    billtemp.totalmoney.Text = string.Format("{0:C}", bill.TotalMoney);
        //    billtemp.money.Text = string.Format("{0:C}", d * Price);
        //    billtemp.price.Text = string.Format("{0:C}", Price);
        //    billtemp.surcharge.Text = string.Format("{0:C}", ((decimal)(sokhach * percent) * Price) + (Price * (decimal)(heso - 1)) * d);
        //    ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms
        //           .Where(x => x.IsDelete == false)
        //           );
        //    Room p = ListRoom.Where(c => c.IdRoom == SelectedRoom.IdRoom).FirstOrDefault();
        //    p.Status = "Sẵn sàng";
        //    RentInvoice k = DataProvider.Instance.DB.RentInvoices.Where((c) => c.IdRoom == p.IdRoom).FirstOrDefault();

        //    if (k != null) DataProvider.Instance.DB.RentInvoices.Remove(k);
        //    DataProvider.Instance.DB.SaveChanges();

        //    SelectedRentInvoice = null;
        //    SelectRoom(p); 
        //    if (lp == 0)
        //    {
        //        MyMessageBox.Show("Phòng trống không thể thanh toán", "Nhắc nhở", MessageBoxButton.OK);
        //    }
        //    else billtemp.ShowDialog();
        //}

        [Command]
        public void SelectRoom(Room selectedRoom)
        {
            try
            {
                SelectedRoom = selectedRoom;

                if (selectedRoom.Status == "Đang thuê")
                {
                    SwitchButton = Visibility.Collapsed;
                    CanAddUser = true;
                    SelectedRentInvoice = DataProvider.Instance.DB.RentInvoices.Where(p => p.IdRoom == selectedRoom.IdRoom).SingleOrDefault();
                    ListRentInvoiceInfo = new ObservableCollection<CustomerWithRentInvoice>(DataProvider.Instance.DB.RentInvoiceInfoes
                        .Where(x => x.IdRentInvoice == SelectedRentInvoice.IdRentInvoice)
                        .Join(DataProvider.Instance.DB.Customers, (InvoiceInfo) => InvoiceInfo.IdCustomer, (Customer) => Customer.IdCustomer,
                            (InvoiceInfo, Customer) => new CustomerWithRentInvoice()
                            {
                                IdCustomer = Customer.IdCustomer,
                                FullName = Customer.FullName,
                            })
                        );
                }
                else
                {
                    CanAddUser = false;
                    SwitchButton = Visibility.Visible;
                    SelectedRentInvoice = null;
                    ListRentInvoiceInfo = null;
                }
            }
            catch { return; }
        }

        [Command]
        public void Rent()
        {
            try
            {
                if (SelectedRoom == null)
                {
                    return;
                }

                SelectedRoom.Status = "Đang thuê";

                RentInvoice rentInvoice = new RentInvoice()
                {
                    Date = DateTime.Now,
                    IdRoom = SelectedRoom.IdRoom,
                    Purchase = false
                };

                DataProvider.Instance.DB.RentInvoices.Add(rentInvoice);
                DataProvider.Instance.DB.SaveChanges();
                SwitchButton = Visibility.Collapsed;
                CanAddUser = true;

                LoadDB();
            }
            catch { return; }
        }

        [Command]
        public void AddCustomer(BusinessView p)
        {
            try
            {
                if (SelectedRoom == null) return;
                if (ListRentInvoiceInfo.Count() == DataProvider.Instance.DB.Constants.FirstOrDefault().MaxCustomer)
                {
                    MyMessageBox.Show("Đã đủ số lượng khách", "Thông báo", MessageBoxButton.OK);
                    return;
                }

                AddCustomerWindow window = new AddCustomerWindow(SelectedRoom);
                window.ShowDialog();

                ListRentInvoiceInfo = new ObservableCollection<CustomerWithRentInvoice>(DataProvider.Instance.DB.RentInvoiceInfoes
                        .Where(x => x.IdRentInvoice == SelectedRentInvoice.IdRentInvoice)
                        .Join(DataProvider.Instance.DB.Customers, (InvoiceInfo) => InvoiceInfo.IdCustomer, (Customer) => Customer.IdCustomer,
                            (InvoiceInfo, Customer) => new CustomerWithRentInvoice()
                            {
                                IdCustomer = Customer.IdCustomer,
                                FullName = Customer.FullName,
                            })
                        );
            }
            catch { return; }
        }

        [Command]
        public void DeleteCustomer()
        {
            try
            {
                if (SelectedCustomerRenting != null)
                {
                    var beenDeleted = DataProvider.Instance.DB.RentInvoiceInfoes.Where(x => x.IdRentInvoice == SelectedRentInvoice.IdRentInvoice).Where(x => x.IdCustomer == SelectedCustomerRenting.IdCustomer).SingleOrDefault();
                    DataProvider.Instance.DB.RentInvoiceInfoes.Remove(beenDeleted);
                    DataProvider.Instance.DB.SaveChanges();

                    ListRentInvoiceInfo = new ObservableCollection<CustomerWithRentInvoice>(DataProvider.Instance.DB.RentInvoiceInfoes
                        .Where(x => x.IdRentInvoice == SelectedRentInvoice.IdRentInvoice)
                        .Join(DataProvider.Instance.DB.Customers, (InvoiceInfo) => InvoiceInfo.IdCustomer, (Customer) => Customer.IdCustomer,
                            (InvoiceInfo, Customer) => new CustomerWithRentInvoice()
                            {
                                IdCustomer = Customer.IdCustomer,
                                FullName = Customer.FullName,
                            })
                        );
                    SelectedCustomerRenting = null;
                    MyMessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButton.OK);
                }
            }
            catch { return; }
        }

        [Command]
        public void DetailCustomer()
        {
            try
            {
                if (SelectedCustomerRenting != null)
                {
                    Customer customer = DataProvider.Instance.DB.Customers.Where(x => x.IdCustomer == SelectedCustomerRenting.IdCustomer).FirstOrDefault();
                    DetailCustomer detail = new DetailCustomer(customer);
                    detail.ShowDialog();
                }
            }
            catch { return; }
        }

        [Command]
        public void EditCustomer()
        {
            try
            {
                if (SelectedCustomerRenting != null)
                {
                    Customer customer = DataProvider.Instance.DB.Customers.Where(x => x.IdCustomer == SelectedCustomerRenting.IdCustomer).FirstOrDefault();
                    EditCustomerView edit = new EditCustomerView(customer);
                    edit.ShowDialog();

                   
                }
            }
            catch { return; }
        }

        [Command]
        public void RoomFilter(BusinessView p)
        {
            try
            {
                LoadDB();

                if (string.IsNullOrEmpty(p.cbFilterStatus.Text) && !string.IsNullOrEmpty(p.cbFilterType.Text))
                {
                    int idType = DataProvider.Instance.DB.TypeRooms.Where(x => x.DisplayName == p.cbFilterType.Text).SingleOrDefault().IdTypeRoom;
                    ListRoom = new ObservableCollection<Room>(ListRoom
                        .Where(x => x.IdTypeRoom == idType)
                        .OrderBy(x => x.Floor)
                        .ThenBy(x => x.Number));
                }
                else if (!string.IsNullOrEmpty(p.cbFilterStatus.Text) && string.IsNullOrEmpty(p.cbFilterType.Text))
                {
                    ListRoom = new ObservableCollection<Room>(ListRoom
                        .Where(x => x.Status == p.cbFilterStatus.Text)
                        .OrderBy(x => x.Floor)
                        .ThenBy(x => x.Number));
                }
                else 
                {
                    int idType = DataProvider.Instance.DB.TypeRooms.Where(x => x.DisplayName == p.cbFilterType.Text).SingleOrDefault().IdTypeRoom;
                    ListRoom = new ObservableCollection<Room>(ListRoom
                        .Where(x => x.IdTypeRoom == idType)
                        .Where(x => x.Status == p.cbFilterStatus.Text)
                        .OrderBy(x => x.Floor)
                        .ThenBy(x => x.Number));
                }

                p.cbFilterStatus.Text = string.Empty;
                p.cbFilterType.Text = string.Empty;
            }
            catch { return; }
        }

        [Command]
        public void SearchRoom(BusinessView p)
        {
            try
            {
                LoadDB();

                if (string.IsNullOrEmpty(p.tbSearch.Text) || string.IsNullOrWhiteSpace(p.tbSearch.Text))
                    return;

                ListRoom = new ObservableCollection<Room>(ListRoom
                    .Where(x => x.DisplayName.Contains(p.tbSearch.Text))
                    .OrderBy(x => x.Floor)
                    .ThenBy(x => x.Number));

                p.tbSearch.Text = string.Empty;
            }
            catch { return; }
        }
    }
}

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

        [Command]
        public void Pay()
        {
            try
            {
                if (SelectedRoom == null)
                    return;

                BilltemplateViewModel billtemplateViewModel = new BilltemplateViewModel(SelectedRentInvoice);
                BillTemplate billTemplate = new BillTemplate();
                billTemplate.DataContext = billtemplateViewModel;
                billTemplate.ShowDialog();
            }
            catch { return; }
        }

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

                if (SelectedRoom.Status == "Hư hỏng")
                {
                    MyMessageBox.Show("Phòng đang hư", "Thông báo", MessageBoxButton.OK);
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

                SelectedRentInvoice = rentInvoice;
                SelectedRoom = DataProvider.Instance.DB.Rooms.Where(x => x.IsDelete == false).Where(x => x.IdRoom == SelectedRentInvoice.IdRoom).SingleOrDefault();

                LoadDB();
            }
            catch { return; }
        }

        [Command]
        public void AddCustomer(BusinessView p)
        {
            try
            {
                if (SelectedRoom == null) { MyMessageBox.Show("Chọn phòng trước khi thao tác", "Nhắc nhở", MessageBoxButton.OK); return; }

                if (ListRentInvoiceInfo != null)
                {
                    if (ListRentInvoiceInfo.Count() == DataProvider.Instance.DB.Constants.FirstOrDefault().MaxCustomer)
                    {
                        MyMessageBox.Show("Đã đủ số lượng khách", "Thông báo", MessageBoxButton.OK);
                        return;
                    }
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
        public void RoomProblemsReport()
        {
            try
            {
                if (SelectedRoom != null)
                {
                    if (MyMessageBox.Show("Báo hỏng " + SelectedRoom.DisplayName + " ?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        if (SelectedRoom.Status == "Hư hỏng")
                        {
                            MyMessageBox.Show("Phòng đã báo hỏng rồi", "Nhắc nhở", MessageBoxButton.OK);
                        }
                        else if (SelectedRoom.Status == "Đang thuê")
                        {
                            MyMessageBox.Show("Phòng đang cho thuê, không thể báo hỏng", "Nhắc nhở", MessageBoxButton.OK);
                        }
                        else
                        {
                            SelectedRoom.Status = "Hư hỏng";
                            DataProvider.Instance.DB.SaveChanges();
                            MyMessageBox.Show("Báo hỏng thành công, phòng sẽ được sửa chữa", "Thông báo", MessageBoxButton.OK);
                        }
                        LoadDB();
                    }
                }
                else
                {
                    MyMessageBox.Show("Chọn phòng trước khi thao tác", "Nhắc nhở", MessageBoxButton.OK);
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

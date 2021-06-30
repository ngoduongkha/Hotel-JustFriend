﻿using DevExpress.Mvvm;
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
        private ObservableCollection<Room> _ListRoom;
        private Room _SelectedRoom;
        public ObservableCollection<TypeRoom> ListRoomType { get; } = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms.Where(x => x.IsDelete == false));
        private ObservableCollection<CustomerWithRentInvoice> _ListRentInvoiceInfo;
        private CustomerWithRentInvoice _SelectedCustomerRenting;

        public Visibility SwitchButton { get; set; } = Visibility.Visible;
        public bool CanAddUser { get; set; } = false;
        public Room SelectedRoom { get => _SelectedRoom; set { _SelectedRoom = value; RaisePropertyChanged(); } }



        private RentInvoice _SelectedRentInvoice;
        public ObservableCollection<Room> ListRoom { get => _ListRoom; set { _ListRoom = value; RaisePropertyChanged(); } }
       
        public RentInvoice SelectedRentInvoice { get => _SelectedRentInvoice; set { _SelectedRentInvoice = value; RaisePropertyChanged(); } }
        public ObservableCollection<CustomerWithRentInvoice> ListRentInvoiceInfo { get => _ListRentInvoiceInfo; set { _ListRentInvoiceInfo = value; RaisePropertyChanged(); } }
        public CustomerWithRentInvoice SelectedCustomerRenting { get => _SelectedCustomerRenting; set => _SelectedCustomerRenting = value; }

        public BusinessViewModel()
        {
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
                if (ListRentInvoiceInfo.Count == 0)
                {
                    MyMessageBox.Show("Phòng không có khách hàng", "Thông báo", MessageBoxButton.OK);
                    SelectedRoom.Status = "Sẵn sàng";
                    DataProvider.Instance.DB.SaveChanges();

                    SelectedRentInvoice.Purchase = true;
                    SelectedRoom.Status = "Sẵn sàng";
                    ListRentInvoiceInfo = null;
                    SelectedRentInvoice = null;

                    CanAddUser = false;
                    SwitchButton = Visibility.Visible;

                    LoadDB();
                    return;
                }
                BilltemplateViewModel billtemplateViewModel = new BilltemplateViewModel(SelectedRentInvoice);
                BillTemplate billTemplate = new BillTemplate();
                billTemplate.DataContext = billtemplateViewModel;
                billTemplate.ShowDialog();

                SelectedRentInvoice.Purchase = true;
                SelectedRoom.Status = "Sẵn sàng";
                ListRentInvoiceInfo = null;
                SelectedRentInvoice = null;

                CanAddUser = false;
                SwitchButton = Visibility.Visible;
                
                DataProvider.Instance.DB.SaveChanges();

                LoadDB();
                SelectedRoom = DataProvider.Instance.DB.Rooms.Where(x => x.IsDelete == false).Where(x => x.IdRoom == SelectedRoom.IdRoom).SingleOrDefault();
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

                    SelectedRentInvoice = DataProvider.Instance.DB.RentInvoices.Where(x => x.Purchase == false).Where(p => p.IdRoom == selectedRoom.IdRoom).SingleOrDefault();

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

                ListRentInvoiceInfo = new ObservableCollection<CustomerWithRentInvoice>();
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
                
                if (string.IsNullOrEmpty(p.cbFilterStatus.Text) && string.IsNullOrEmpty(p.cbFilterType.Text))
                {
                    return;
                }
                else if (string.IsNullOrEmpty(p.cbFilterStatus.Text) && !string.IsNullOrEmpty(p.cbFilterType.Text))
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

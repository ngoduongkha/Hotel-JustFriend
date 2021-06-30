using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    class CustomizeParametersViewModel : ViewModelBase
    {
        private ObservableCollection<TypeRoom> _listRoomType;
        private ObservableCollection<TypeCustomer> _listCustomerType;
        private string _typeName;
        private string _maxCustomer;
        private string _percent;

        public string TypeName { get => _typeName; set { _typeName = value; RaisePropertiesChanged(); } }
        public ObservableCollection<TypeRoom> ListRoomType { get => _listRoomType; set { _listRoomType = value; RaisePropertiesChanged(); } }
        public ObservableCollection<TypeCustomer> ListCustomerType { get => _listCustomerType; set { _listCustomerType = value; RaisePropertiesChanged(); } }
        public string MaxCustomer { get => _maxCustomer; set { _maxCustomer = value; RaisePropertiesChanged(); } }
        public string Percent { get => _percent;  set { _percent = value; RaisePropertiesChanged(); } }

        public CustomizeParametersViewModel()
        {
            ListRoomType = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms.Where((p) => p.IsDelete == false));
            ListCustomerType = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers.Where((p) => p.IsDelete == false));
            MaxCustomer = (DataProvider.Instance.DB.Constants.Find(0) as Constant).MaxCustomer.ToString();
            Percent = (DataProvider.Instance.DB.Constants.Find(0) as Constant).Percent.ToString();
        }

        [Command]
        public void CallAddCustomerType(ComboBox cbbox)
        {
            AddTypeWindow window = new AddTypeWindow(true);
            window.ShowDialog();
            ListCustomerType = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers.Where((p) => p.IsDelete == false));
            cbbox.SelectedIndex = cbbox.Items.Count - 1;
        }

        [Command]
        public void CallAddRoomType(ComboBox cbbox)
        {
            AddTypeWindow window = new AddTypeWindow(false);
            window.ShowDialog();
            ListRoomType = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms.Where((p) => p.IsDelete == false));
            cbbox.SelectedIndex = cbbox.Items.Count - 1;
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
        public void CloseWindow(Window p)
        {
            try
            {
                p.Close();
            }
            catch { return; }
        }

        [Command]
        public void AddType(AddTypeWindow p)
        {
            try
            {
                if (p.IsAddCustomerType)
                {
                    TypeCustomer newType = new TypeCustomer()
                    {
                        CoefficientsObtained = 1,
                        DisplayName = TypeName,
                        IsDelete = false,
                    };
                    DataProvider.Instance.DB.TypeCustomers.Add(newType);
                    DataProvider.Instance.DB.SaveChanges();
                    p.Close();
                }
                else
                {
                    TypeRoom newType = new TypeRoom()
                    {
                        DisplayName = TypeName,
                        Price = 0,
                        IsDelete = false,
                    };
                    DataProvider.Instance.DB.TypeRooms.Add(newType);
                    DataProvider.Instance.DB.SaveChanges();
                    p.Close();
                }
            }
            catch
            {
                MyMessageBox.Show("Có lỗi xảy ra", "Thông báo", MessageBoxButton.OK);
                return;
            }
        }

        [Command]
        public void DeleteType(ComboBox cbbox)
        {
            try
            {
                if (cbbox.SelectedItem != null)
                {
                    if (cbbox.Name == "cbboxRoomType")
                    {
                        TypeRoom deleteRoomType = cbbox.SelectedItem as TypeRoom;
                        ObservableCollection<Room> listRoomsOfType = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms.Where(p => p.IdTypeRoom == deleteRoomType.IdTypeRoom));
                        if (listRoomsOfType.Count > 0)
                        {
                            MyMessageBox.Show("Tồn tại phòng thuộc loại phòng này, không thể xóa loại phòng", "Nhắc nhở", MessageBoxButton.OK);
                        }
                        else
                        {
                            deleteRoomType.IsDelete = true;
                            DataProvider.Instance.DB.SaveChanges();
                            ListRoomType = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms.Where((p) => p.IsDelete == false));
                            MyMessageBox.Show("Xóa loại phòng thành công", "Thông báo", MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        TypeCustomer deleteCustomerType = cbbox.SelectedItem as TypeCustomer;
                        if (deleteCustomerType.DisplayName == "Nội địa")
                        {
                            MyMessageBox.Show("Không thể xóa loại khách nội địa vì đây là loại khách mặc định", "Thông báo", MessageBoxButton.OK);
                            return;
                        }
                        ObservableCollection<Customer> listCustomerOfType = new ObservableCollection<Customer>(DataProvider.Instance.DB.Customers.Where(p => p.IdTypeCustomer == deleteCustomerType.IdTypeCustomer));
                        if (listCustomerOfType.Count > 0)
                        {
                            if (MyMessageBox.Show("Tồn tại khách thuộc loại này" +
                                ", xóa loại khách thì khách thuộc loại này chuyển thành khách nội địa?", 
                                "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                deleteCustomerType.IsDelete = true;
                                foreach(Customer customer in listCustomerOfType)
                                {
                                    customer.IdTypeCustomer = 1;
                                }
                                DataProvider.Instance.DB.SaveChanges();
                                ListCustomerType = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers.Where((p) => p.IsDelete == false));
                                MyMessageBox.Show("Xóa loại khách thành công, khách loại này chuyển thành khách nội địa", "Thông báo", MessageBoxButton.OK);
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            deleteCustomerType.IsDelete = true;
                            DataProvider.Instance.DB.SaveChanges();
                            ListCustomerType = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers.Where((p) => p.IsDelete == false));
                            MyMessageBox.Show("Xóa loại khách thành công", "Thông báo", MessageBoxButton.OK);
                        }
                    }
                }
            }
            catch {
                MyMessageBox.Show("Có lỗi xảy ra", "Thông báo", MessageBoxButton.OK);
                return;
            }
        }
        [Command]
        public void SaveEdited(object p)
        {
            try
            {
                var parameters = (object[])p;
                TextBox tbox = parameters[0] as TextBox;
                if (tbox.Name == "tboxPrice")
                {
                    ComboBox cbbox = parameters[1] as ComboBox;
                    if (cbbox.SelectedItem != null && tbox.Text != null)
                    {
                        TypeRoom typeRoom = DataProvider.Instance.DB.TypeRooms.Find((cbbox.SelectedItem as TypeRoom).IdTypeRoom);
                        typeRoom.Price = int.Parse(tbox.Text);
                        DataProvider.Instance.DB.SaveChanges();
                    }
                    return;
                }
                if (tbox.Name == "tboxNumber")
                {
                    ComboBox cbbox = parameters[1] as ComboBox;
                    if (cbbox.SelectedItem != null && tbox.Text != null)
                    {
                        TypeCustomer typeCustomer = DataProvider.Instance.DB.TypeCustomers.Find((cbbox.SelectedItem as TypeCustomer).IdTypeCustomer);
                        typeCustomer.CoefficientsObtained = double.Parse(tbox.Text);
                        DataProvider.Instance.DB.SaveChanges();
                    }
                    return;
                }
                if (tbox.Name == "tboxMaxCustomer")
                {
                    if (tbox.Text != null)
                    {
                        if (int.Parse(MaxCustomer) > 2)
                        {
                            (DataProvider.Instance.DB.Constants.Find(0) as Constant).MaxCustomer = int.Parse(MaxCustomer);
                            MaxCustomer = (DataProvider.Instance.DB.Constants.Find(0) as Constant).MaxCustomer.ToString();
                            DataProvider.Instance.DB.SaveChanges();
                            MyMessageBox.Show("Sửa số khách hàng tối đa thành công", "Thông báo", MessageBoxButton.OK);
                        }
                        else
                        {
                            MyMessageBox.Show("Số khách hàng tối đa phải lớn hơn 2", "Nhắc nhở", MessageBoxButton.OK);
                            return;
                        }
                    }
                    return;
                }
                if (tbox.Name == "tboxPercent")
                {
                    if (tbox.Text != null)
                    {
                        if (double.Parse(Percent) >= 0)
                        {
                            (DataProvider.Instance.DB.Constants.Find(0) as Constant).Percent = double.Parse(Percent);
                            Percent = (DataProvider.Instance.DB.Constants.Find(0) as Constant).Percent.ToString();
                            DataProvider.Instance.DB.SaveChanges();
                            MyMessageBox.Show("Sửa phụ thu thành công", "Thông báo", MessageBoxButton.OK);
                        }
                        else
                        {
                            MyMessageBox.Show("Phụ thu phải là số dương", "Nhắc nhở", MessageBoxButton.OK);
                        }
                    }
                    return;
                }
            }
            catch 
            {
                MyMessageBox.Show("Có lỗi xảy ra", "Thông báo", MessageBoxButton.OK);
                return;
            }
        }
    }
}

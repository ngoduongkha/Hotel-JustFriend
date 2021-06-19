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
            ListRoomType = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms.Where((p) => p.isDelete == false));
            ListCustomerType = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers.Where((p) => p.isDelete == false));
            MaxCustomer = (DataProvider.Instance.DB.Constants.Find(0) as Constant).maxCustomer.ToString();
            Percent = (DataProvider.Instance.DB.Constants.Find(0) as Constant).percent.ToString();
        }

        [Command]
        public void CallAddCustomerType()
        {
            AddTypeWindow window = new AddTypeWindow(true);
            window.ShowDialog();
            ListCustomerType = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers.Where((p) => p.isDelete == false));
        }

        [Command]
        public void CallAddRoomType()
        {
            AddTypeWindow window = new AddTypeWindow(false);
            window.ShowDialog();
            ListRoomType = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms.Where((p) => p.isDelete == false));
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
                        displayname = TypeName,
                        isDelete = false,
                    };
                    DataProvider.Instance.DB.TypeCustomers.Add(newType);
                    DataProvider.Instance.DB.SaveChanges();
                    p.Close();
                }
                else
                {
                    TypeRoom newType = new TypeRoom()
                    {
                        fullname = TypeName,
                        price = 0,
                        isDelete = false,
                    };
                    DataProvider.Instance.DB.TypeRooms.Add(newType);
                    DataProvider.Instance.DB.SaveChanges();
                    p.Close();
                }
            }
            catch {
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
                        deleteRoomType.isDelete = true;
                        DataProvider.Instance.DB.SaveChanges();
                        ListRoomType = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms.Where((p) => p.isDelete == false));
                    }
                    else
                    {
                        TypeCustomer deleteCustomerType = cbbox.SelectedItem as TypeCustomer;
                        deleteCustomerType.isDelete = true;
                        DataProvider.Instance.DB.SaveChanges();
                        ListCustomerType = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers.Where((p) => p.isDelete == false));
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
                        TypeRoom typeRoom = DataProvider.Instance.DB.TypeRooms.Find((cbbox.SelectedItem as TypeRoom).idType);
                        typeRoom.price = int.Parse(tbox.Text);
                        DataProvider.Instance.DB.SaveChanges();
                    }
                    return;
                }
                if (tbox.Name == "tboxNumber")
                {
                    ComboBox cbbox = parameters[1] as ComboBox;
                    if (cbbox.SelectedItem != null && tbox.Text != null)
                    {
                        TypeCustomer typeCustomer = DataProvider.Instance.DB.TypeCustomers.Find((cbbox.SelectedItem as TypeCustomer).idType);
                        typeCustomer.number = double.Parse(tbox.Text);
                        DataProvider.Instance.DB.SaveChanges();
                    }
                    return;
                }
                if (tbox.Name == "tboxMaxCustomer")
                {
                    if (tbox.Text != null)
                    {
                        (DataProvider.Instance.DB.Constants.Find(0) as Constant).maxCustomer = int.Parse(MaxCustomer);
                        MaxCustomer = (DataProvider.Instance.DB.Constants.Find(0) as Constant).maxCustomer.ToString();
                        DataProvider.Instance.DB.SaveChanges();
                    }
                    return;
                }
                if (tbox.Name == "tboxPercent")
                {
                    if (tbox.Text != null)
                    {
                        (DataProvider.Instance.DB.Constants.Find(0) as Constant).percent = double.Parse(Percent);
                        Percent = (DataProvider.Instance.DB.Constants.Find(0) as Constant).percent.ToString();
                        DataProvider.Instance.DB.SaveChanges();
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

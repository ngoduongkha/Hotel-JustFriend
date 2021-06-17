using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    class CustomizeParametersViewModel : ViewModelBase
    {
        private ObservableCollection<TypeRoom> _listRoomType;
        private ObservableCollection<TypeCustomer> _listCustomerType;
        private string _typeName;

        public string TypeName { get => _typeName; set { _typeName = value; RaisePropertiesChanged(); } }
        public ObservableCollection<TypeRoom> ListRoomType { get => _listRoomType; set { _listRoomType = value; RaisePropertiesChanged(); } }
        public ObservableCollection<TypeCustomer> ListCustomerType { get => _listCustomerType; set { _listCustomerType = value; RaisePropertiesChanged(); } }

        public CustomizeParametersViewModel()
        {
            ListRoomType = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms);
            ListCustomerType = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers);
        }

        [Command]
        public void CallAddCustomerType()
        {
            AddTypeWindow window = new AddTypeWindow(true);
            window.ShowDialog();
            ListCustomerType = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers);
        }

        [Command]
        public void CallAddRoomType()
        {
            AddTypeWindow window = new AddTypeWindow(false);
            window.ShowDialog();
            ListRoomType = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms);
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
        public void Add(AddTypeWindow p)
        {
            try
            {
                if (p.IsAddCustomerType)
                {
                    TypeCustomer newType = new TypeCustomer()
                    {
                        displayname = TypeName,
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
                    };
                    DataProvider.Instance.DB.TypeRooms.Add(newType);
                    DataProvider.Instance.DB.SaveChanges();
                    p.Close();
                }
            }
            catch { return; }
        }

        [Command]
        public void DeleteRoomType(ComboBox cbbox)
        {
            try
            {
                if (cbbox.SelectedItem != null)
                {
                    TypeRoom deleteRoom = cbbox.SelectedItem as TypeRoom;
                    DataProvider.Instance.DB.TypeRooms.Remove(deleteRoom);
                    DataProvider.Instance.DB.SaveChanges();
                    ListRoomType = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms);
                }
            }
            catch { return; }
        }
        [Command]
        public void DeleteCustmerType(ComboBox cbbox)
        {
            try
            {
                if (cbbox.SelectedItem != null)
                {
                    TypeCustomer deleteCustomer = cbbox.SelectedItem as TypeCustomer;
                    DataProvider.Instance.DB.TypeCustomers.Remove(deleteCustomer);
                    DataProvider.Instance.DB.SaveChanges();
                    ListCustomerType = new ObservableCollection<TypeCustomer>(DataProvider.Instance.DB.TypeCustomers);
                }
            }
            catch { return; }
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
                }
            }
            catch { return; }
        }
    }
}

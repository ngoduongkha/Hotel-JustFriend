using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    class CustomizeParametersViewModel : ViewModelBase
    {
        private ObservableCollection<TypeRoom> _listRoomType;
        private string _type;

        public string Type { get => _type; set { _type = value; RaisePropertiesChanged(); } }
        public ObservableCollection<TypeRoom> ListRoomType { get => _listRoomType; set { _listRoomType = value; RaisePropertiesChanged(); } }

        public CustomizeParametersViewModel()
        {
            ListRoomType = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms);
        }

        [Command]
        public void CallAddCustomerType()
        {
            AddCustomerTypeWindow window = new AddCustomerTypeWindow();
            window.ShowDialog();
        }
        [Command]
        public void CallAddRoomType()
        {
            AddCustomerTypeWindow window = new AddCustomerTypeWindow();
            window.ShowDialog();
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
        public void Add()
        {
            TypeRoom newType = new TypeRoom()
            {
                fullname = Type,
                price = 0,
            };
            DataProvider.Instance.DB.TypeRooms.Add(newType);
        }
    }
}

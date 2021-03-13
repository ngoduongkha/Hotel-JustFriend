using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    public class AddRoomViewModel : ViewModelBase
    {
        private ObservableCollection<string> _ListRoomType;
        private int _RoomID;
        private string _DisplayName;
        private string _RoomType;
        private decimal _RoomPrice;
        private string _RoomNote;

        public ObservableCollection<string> ListRoomType { get => _ListRoomType; set => _ListRoomType = value; }

        public int RoomID { get => _RoomID; set { _RoomID = value; RaisePropertyChanged(); } }

        public string DisplayName { get => _DisplayName; set { _DisplayName = value; RaisePropertyChanged(); } }

        public string RoomType { get => _RoomType; set { _RoomType = value; RaisePropertyChanged(); } }

        public decimal RoomPrice { get => _RoomPrice; set { _RoomPrice = value; RaisePropertyChanged(); } }

        public string RoomNote { get => _RoomNote; set { _RoomNote = value; RaisePropertyChanged(); } }

        

        public AddRoomViewModel()
        {
            DbSet<Room> data = DataProvider.Instance.DB.Rooms;
            ListRoomType = new ObservableCollection<string>(data.Select(x => x.type).Distinct());
            RoomID = data.Select(x => x.isDelete == false).Count() + 1;
        }

        #region Command
        [Command]
        public void Add(Window p)
        {
            Room newRoom = new Room() { idRoom = RoomID, displayName = DisplayName, type = RoomType, price = RoomPrice, note = RoomNote };
            DataProvider.Instance.DB.Rooms.Add(newRoom);
            DataProvider.Instance.DB.SaveChanges();
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
        public void Close(Window p)
        {
            try
            {
                p.Close();
            }
            catch { return; }
        }
        #endregion
    }
}
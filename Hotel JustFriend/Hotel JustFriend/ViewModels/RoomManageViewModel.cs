using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Utility;
using Hotel_JustFriend.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Hotel_JustFriend.ViewModels
{
    public class RoomWithType
    {
        public int idRoom { get; set; }
        public int floor { get; set; }
        public int number { get; set; }
        public string displayName { get; set; }
        public string typeRoomName { get; set; }
        public string status { get; set; }
        public string note { get; set; }
        public Nullable<decimal> price { get; set; }
    }


    [POCOViewModel]
    public class RoomManageViewModel : ViewModelBase
    {
        private ObservableCollection<Room> _ListRoom;
        private ObservableCollection<RoomWithType> _ListRoomWithType;
        private ObservableCollection<TypeRoom> _ListRoomType;
        private RoomWithType _SelectedRoom;

        public ObservableCollection<RoomWithType> ListRoomWithType { get => _ListRoomWithType; set { _ListRoomWithType = value; RaisePropertyChanged(); } }
        public ObservableCollection<TypeRoom> ListRoomType { get => _ListRoomType; set => _ListRoomType = value; }
        public RoomWithType SelectedRoom { get => _SelectedRoom; set { _SelectedRoom = value; RaisePropertyChanged(); } }
        public ObservableCollection<Room> ListRoom { get => _ListRoom; set => _ListRoom = value; }

        public RoomManageViewModel()
        {
            ListRoomType = new ObservableCollection<TypeRoom>(DataProvider.Instance.DB.TypeRooms);

            LoadDB();
        }

        private void LoadDB()
        {
            try
            {
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms.Where(x => x.isDelete == false).OrderBy(x => x.floor).ThenBy(x => x.number));
                ListRoomWithType = new ObservableCollection<RoomWithType>(
                    ListRoom.Join(ListRoomType, (Room) => Room.idType, (TypeRoom) => TypeRoom.idType,
                    (Room, TypeRoom) =>
                    new RoomWithType
                    {
                        idRoom = Room.idRoom,
                        floor = Room.floor,
                        number = Room.number,
                        displayName = Room.displayName,
                        typeRoomName = TypeRoom.fullname,
                        status = Room.status,
                        note = Room.note,
                        price = TypeRoom.price
                    }));
            }
            catch { return; }
        }

        [Command]
        public void AddRoom()
        {
            try
            {
                RoomDetailView addRoom = new RoomDetailView();
                addRoom.ShowDialog();

                LoadDB();
            }
            catch { return; }
        }

        [Command]
        public void DeleteRoom()
        {
            try
            {
                if (SelectedRoom == null)
                {
                    MyMessageBox.Show("Không có gì để xóa!", "Thông báo", System.Windows.MessageBoxButton.OK);
                    return;
                }
                if (SelectedRoom.status == "Đang thuê")
                {
                    MyMessageBox.Show("Phòng đang được thuê!", "Thông báo", System.Windows.MessageBoxButton.OK);
                    return;
                }

                DataProvider.Instance.DB.Rooms.Where(x => x.idRoom == SelectedRoom.idRoom).SingleOrDefault().isDelete = true;
                DataProvider.Instance.DB.SaveChanges();
                MyMessageBox.Show("Xóa thành công!", "Thông báo", System.Windows.MessageBoxButton.OK);

                LoadDB();
            }
            catch { return; }
        }

        [Command]
        public void FixRoom()
        {
            try
            {
                if (SelectedRoom == null)
                {
                    MyMessageBox.Show("Không có gì để sửa chữa!", "Thông báo", System.Windows.MessageBoxButton.OK);
                    return;
                }
                if (SelectedRoom.status != "Hư hỏng")
                {
                    MyMessageBox.Show("Phòng không hỏng!", "Thông báo", System.Windows.MessageBoxButton.OK);
                    return;
                }

                DataProvider.Instance.DB.Rooms.Where(x => x.idRoom == SelectedRoom.idRoom).SingleOrDefault().status = "Sẵn sàng";
                DataProvider.Instance.DB.SaveChanges();
                MyMessageBox.Show("Sửa chữa thành công!", "Thông báo", System.Windows.MessageBoxButton.OK);

                LoadDB();
            }
            catch { return; }
        }

        [Command]
        public void RoomFilter(RoomManageView p)
        {
            try
            {
                LoadDB();

                if (string.IsNullOrEmpty(p.txtFilterStatus.Text) && !string.IsNullOrEmpty(p.txtFilterType.Text))
                {
                    ListRoomWithType = new ObservableCollection<RoomWithType>(ListRoomWithType.Where(x => x.typeRoomName == p.txtFilterType.Text));
                }
                else if (!string.IsNullOrEmpty(p.txtFilterStatus.Text) && string.IsNullOrEmpty(p.txtFilterType.Text))
                {
                    ListRoomWithType = new ObservableCollection<RoomWithType>(ListRoomWithType.Where(x => x.status == p.txtFilterStatus.Text));
                }
                else if (!string.IsNullOrEmpty(p.txtFilterStatus.Text) && !string.IsNullOrEmpty(p.txtFilterType.Text))
                {
                    ListRoomWithType = new ObservableCollection<RoomWithType>(ListRoomWithType.Where(x => x.typeRoomName == p.txtFilterType.Text && x.status == p.txtFilterStatus.Text));
                }

                p.txtFilterStatus.Text = string.Empty;
                p.txtFilterType.Text = string.Empty;
            }
            catch { return; }
        }
    }
}
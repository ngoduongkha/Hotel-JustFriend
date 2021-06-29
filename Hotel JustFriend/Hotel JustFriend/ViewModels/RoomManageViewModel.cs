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
        public int IdRoom { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }
        public string DisplayName { get; set; }
        public string TypeRoomName { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public decimal Price { get; set; }
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
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms.Where(x => x.IsDelete == false).OrderBy(x => x.Floor).ThenBy(x => x.Number));
                ListRoomWithType = new ObservableCollection<RoomWithType>(
                    ListRoom.Join(ListRoomType, (Room) => Room.IdTypeRoom, (TypeRoom) => TypeRoom.IdTypeRoom,
                    (Room, TypeRoom) =>
                    new RoomWithType
                    {
                        IdRoom = Room.IdRoom,
                        Floor = Room.Floor,
                        Number = Room.Number,
                        DisplayName = Room.DisplayName,
                        TypeRoomName = TypeRoom.DisplayName,
                        Status = Room.Status,
                        Note = Room.Note,
                        Price = TypeRoom.Price
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
                if (SelectedRoom.Status == "Đang thuê")
                {
                    MyMessageBox.Show("Phòng đang được thuê!", "Thông báo", System.Windows.MessageBoxButton.OK);
                    return;
                }

                DataProvider.Instance.DB.Rooms.Where(x => x.IdRoom == SelectedRoom.IdRoom).SingleOrDefault().IsDelete = true;
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
                if (SelectedRoom.Status != "Hư hỏng")
                {
                    MyMessageBox.Show("Phòng không hỏng!", "Thông báo", System.Windows.MessageBoxButton.OK);
                    return;
                }

                DataProvider.Instance.DB.Rooms.Where(x => x.IdRoom == SelectedRoom.IdRoom).SingleOrDefault().Status = "Sẵn sàng";
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

                if (string.IsNullOrEmpty(p.cbFilterStatus.Text) && !string.IsNullOrEmpty(p.cbFilterType.Text))
                {
                    ListRoomWithType = new ObservableCollection<RoomWithType>(ListRoomWithType.Where(x => x.TypeRoomName == p.cbFilterType.Text));
                }
                else if (!string.IsNullOrEmpty(p.cbFilterStatus.Text) && string.IsNullOrEmpty(p.cbFilterType.Text))
                {
                    ListRoomWithType = new ObservableCollection<RoomWithType>(ListRoomWithType.Where(x => x.Status == p.cbFilterStatus.Text));
                }
                else if (!string.IsNullOrEmpty(p.cbFilterStatus.Text) && !string.IsNullOrEmpty(p.cbFilterType.Text))
                {
                    ListRoomWithType = new ObservableCollection<RoomWithType>(ListRoomWithType.Where(x => x.TypeRoomName == p.cbFilterType.Text && x.Status == p.cbFilterStatus.Text));
                }

                p.cbFilterStatus.Text = string.Empty;
                p.cbFilterType.Text = string.Empty;
            }
            catch { return; }
        }

        [Command]
        public void SearchRoom(RoomManageView p)
        {
            try
            {
                LoadDB();

                if (string.IsNullOrEmpty(p.tbSearch.Text) || string.IsNullOrWhiteSpace(p.tbSearch.Text))
                {
                    p.tbSearch.Text = string.Empty;
                    return;
                }

                ListRoomWithType = new ObservableCollection<RoomWithType>(ListRoomWithType.Where(x => x.DisplayName.Contains(p.tbSearch.Text)));

                p.tbSearch.Text = string.Empty;
            }
            catch { return; }
        }
    }
}
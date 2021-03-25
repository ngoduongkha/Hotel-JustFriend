using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Utility;
using Hotel_JustFriend.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    public class RoomManageViewModel : ViewModelBase
    {
        private ObservableCollection<Room> _ListRoom;
        private ObservableCollection<string> _ListRoomType;
        private ObservableCollection<string> _ListRoomStatus;
        private Room _SelectedRoom;

        public ObservableCollection<Room> ListRoom { get => _ListRoom; set { _ListRoom = value; RaisePropertyChanged(); } }
        public ObservableCollection<string> ListRoomType { get => _ListRoomType; set { _ListRoomType = value; RaisePropertyChanged(); } }
        public ObservableCollection<string> ListRoomStatus { get => _ListRoomStatus; set { _ListRoomStatus = value; RaisePropertyChanged(); } }
        public Room SelectedRoom { get => _SelectedRoom; set { _SelectedRoom = value; RaisePropertyChanged(); } }

        public RoomManageViewModel()
        {
            LoadDB();
        }

        private void LoadDB()
        {
            try
            {
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms.Where(x => x.isDelete == false).OrderBy(x => x.floor).ThenBy(x => x.number));
                ListRoomStatus = new ObservableCollection<string>(ListRoom.Select(x => x.status).Distinct());
                ListRoomType = new ObservableCollection<string>(ListRoom.Select(x => x.type).Distinct());
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
                if (SelectedRoom.status != "Hỏng")
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
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms.Where(x => x.isDelete == false));

                if (string.IsNullOrEmpty(p.txtFilterStatus.Text) && !string.IsNullOrEmpty(p.txtFilterType.Text))
                {
                    ListRoom = new ObservableCollection<Room>(ListRoom
                        .Where(x => x.type == p.txtFilterType.Text)
                        .OrderBy(x => x.floor)
                        .ThenBy(x => x.number));
                }
                else if (!string.IsNullOrEmpty(p.txtFilterStatus.Text) && string.IsNullOrEmpty(p.txtFilterType.Text))
                {
                    ListRoom = new ObservableCollection<Room>(ListRoom
                        .Where(x => x.status == p.txtFilterStatus.Text)
                        .OrderBy(x => x.floor)
                        .ThenBy(x => x.number));
                }
                else if (!string.IsNullOrEmpty(p.txtFilterStatus.Text) && !string.IsNullOrEmpty(p.txtFilterType.Text))
                {
                    ListRoom = new ObservableCollection<Room>(ListRoom
                        .Where(x => x.type == p.txtFilterType.Text && x.status == p.txtFilterStatus.Text)
                        .OrderBy(x => x.floor)
                        .ThenBy(x => x.number));
                }

                p.txtFilterStatus.Text = string.Empty;
                p.txtFilterType.Text = string.Empty;
            }
            catch { return; }
        }

        [Command]
        public void SearchRoom(RoomManageView p)
        {
            try
            {
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms.Where(x => x.isDelete == false));

                if (string.IsNullOrEmpty(p.txtSearch.Text))
                    return;

                ListRoom = new ObservableCollection<Room>(ListRoom
                    .Where(x => x.displayName.Contains(p.txtSearch.Text))
                    .OrderBy(x => x.floor)
                    .ThenBy(x => x.number)
                    .ToList());

                p.txtSearch.Text = string.Empty;
            }
            catch { return; }
        }
    }
}

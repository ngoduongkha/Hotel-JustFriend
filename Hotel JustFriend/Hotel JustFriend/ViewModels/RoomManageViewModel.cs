using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
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
        private bool _IsEditable;
        private Room _SelectedRoom;

        public ObservableCollection<Room> ListRoom { get => _ListRoom; set { _ListRoom = value; RaisePropertyChanged(); } }
        public ObservableCollection<string> ListRoomType { get => _ListRoomType; set { _ListRoomType = value; RaisePropertyChanged(); } }
        public ObservableCollection<string> ListRoomStatus { get => _ListRoomStatus; set { _ListRoomStatus = value; RaisePropertyChanged(); } }
        public bool IsEditable { get => _IsEditable; set { _IsEditable = value; RaisePropertyChanged(); } }
        public Room SelectedRoom { get => _SelectedRoom; set { _SelectedRoom = value; RaisePropertyChanged(); } }

        public RoomManageViewModel()
        {
            ResetView();
        }

        public void ResetView()
        {
            var RoomVisible = DataProvider.Instance.DB.Rooms.Where(x => x.isDelete == false);
            ListRoom = new ObservableCollection<Room>(RoomVisible);
            ListRoomType = new ObservableCollection<string>(RoomVisible.Select(x => x.type).Distinct());
            ListRoomStatus = new ObservableCollection<string>(RoomVisible.Select(x => x.status).Distinct());

            SortRoom();
        }

        public void SortRoom()
        {
            try
            {
                if (ListRoom == null)
                    return;
                ListRoom = new ObservableCollection<Room>(ListRoom.OrderBy(x => x.floor).OrderBy(x => x.number));
                IsEditable = false;
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
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms.Where(x => x.isDelete == false));
                ResetView();
            }
            catch { return; }
        }

        [Command]
        public void DeleteRoom()
        {
            try
            {
                var beenDeleted = DataProvider.Instance.DB.Rooms.Where(x => x.idRoom == SelectedRoom.idRoom).SingleOrDefault();
                beenDeleted.isDelete = true;
                ListRoom.Remove(SelectedRoom);
                DataProvider.Instance.DB.SaveChanges();
                ResetView();
            }
            catch { return; }
        }

        [Command]
        public void RoomFilter(RoomManageView p)
        {
            try
            {
                if (string.IsNullOrEmpty(p.txtFilterStatus.Text) && string.IsNullOrEmpty(p.txtFilterType.Text))
                {
                    ResetView();
                }
                else if (string.IsNullOrEmpty(p.txtFilterStatus.Text) && !string.IsNullOrEmpty(p.txtFilterType.Text))
                {
                    ListRoom = new ObservableCollection<Room>(ListRoom.Where(x => x.type == p.txtFilterType.Text));
                    SortRoom();
                }
                else if (!string.IsNullOrEmpty(p.txtFilterStatus.Text) && string.IsNullOrEmpty(p.txtFilterType.Text))
                {
                    ListRoom = new ObservableCollection<Room>(ListRoom.Where(x => x.type == p.txtFilterStatus.Text));
                    SortRoom();
                }
                else
                {
                    ListRoom = new ObservableCollection<Room>(ListRoom.Where(x => x.type == p.txtFilterType.Text && x.status == p.txtFilterStatus.Text));
                    SortRoom();
                }
            }
            catch { return; }
        }

        [Command]
        public void SelectedChanged(ListView lv)
        {
            if (lv.SelectedItem != null)
            {
                IsEditable = true;
            }
            else
            {
                IsEditable = false;
            }
        }
    }
}

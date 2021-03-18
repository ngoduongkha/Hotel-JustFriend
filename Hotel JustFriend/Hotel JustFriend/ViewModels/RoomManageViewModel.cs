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
        private bool _IsEditable;
        private Room _SelectedRoom;

        public ObservableCollection<Room> ListRoom { get => _ListRoom; set { _ListRoom = value; RaisePropertyChanged(); } }
        public bool IsEditable { get => _IsEditable; set { _IsEditable = value; RaisePropertyChanged(); } }

        public Room SelectedRoom { get => _SelectedRoom; set { _SelectedRoom = value; RaisePropertyChanged(); } }

        public RoomManageViewModel()
        {
            IsEditable = false;
            ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms.Where(x => x.isDelete == false));
        }

        [Command]
        public void AddRoom()
        {
            try
            {
                RoomDetailView addRoom = new RoomDetailView();
                addRoom.ShowDialog();
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms.Where(x => x.isDelete == false));
            }
            catch { return; }
        }

        [Command]
        public void EditRoom()
        {
            try
            {
                RoomDetailView editRoom = new RoomDetailView();
                editRoom.Title = "Cập nhật phòng";
                editRoom.txtFloor.Text = SelectedRoom.floor.ToString();
                editRoom.txtNumber.Text = SelectedRoom.number.ToString();
                editRoom.txtType.Text = SelectedRoom.type.ToString();
                editRoom.txtPrice.Text = SelectedRoom.price.ToString();
                editRoom.txtNote.Text = SelectedRoom.note.ToString() ?? "";
                editRoom.ShowDialog();
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

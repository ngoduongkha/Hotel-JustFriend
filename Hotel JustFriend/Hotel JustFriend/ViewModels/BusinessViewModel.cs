using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Hotel_JustFriend.Views;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    public class BusinessViewModel : ViewModelBase
    {

        private ObservableCollection<Room> _ListRoom;
        //private Room _SelectedRoom;

        public ObservableCollection<Room> ListRoom { get => _ListRoom; set => _ListRoom = value; }
        //public Room SelectedRoom { get => _SelectedRoom; set { _SelectedRoom = value; RaisePropertyChanged(); } }

        public BusinessViewModel()
        {
            LoadDB();
        }

        private void LoadDB()
        {
            try
            {
                ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms.Where(x => x.isDelete == false).OrderBy(x => x.floor).ThenBy(x => x.number));
            }
            catch { return; }
        }
        [Command]
        public void ClickRoom()
        {
            BookingWindow a = new BookingWindow();
            a.ShowDialog();
        }

        [Command]
        public void SelectRoom(Room x)
        {
            try
            {
                //SelectedRoom = x;
                MessageBox.Show("a");
            }
            catch { return; }
        }
    }
}
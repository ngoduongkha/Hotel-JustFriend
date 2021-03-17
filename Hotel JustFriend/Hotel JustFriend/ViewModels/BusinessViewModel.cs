using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    public class BusinessViewModel : ViewModelBase
    {
        private WrapPanel panel;

        private ObservableCollection<Room> _ListRoom;

        public ObservableCollection<Room> ListRoom { get => _ListRoom; set => _ListRoom = value; }

        public BusinessViewModel(WrapPanel x)
        {
            panel = x;
        }

        [Command]
        public void Load()
        {
            ListRoom = new ObservableCollection<Room>(DataProvider.Instance.DB.Rooms.Where(x => x.isDelete == false));
            Button button = new Button();
            panel.Children.Add(button);
        }
    }
}
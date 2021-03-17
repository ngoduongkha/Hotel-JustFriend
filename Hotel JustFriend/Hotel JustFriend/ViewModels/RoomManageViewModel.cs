using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Views;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    public class RoomManageViewModel : ViewModelBase
    {
        [Command]
        public void AddRoom()
        {
            try
            {
                AddRoomWindow addRoom = new AddRoomWindow();
                addRoom.ShowDialog();
            }
            catch { return; }
        }
    }
}

using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

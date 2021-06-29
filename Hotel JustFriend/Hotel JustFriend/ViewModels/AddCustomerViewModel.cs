using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Models;
using Hotel_JustFriend.Views;

namespace Hotel_JustFriend.ViewModels
{
    class AddCustomerViewModel : ViewModelBase
    {
        public Room SelectedRoom { get; set; }

        public AddCustomerViewModel(Room selectedRoom)
        {
            SelectedRoom = selectedRoom;
        }

        [Command]
        public void Close(AddCustomerWindow window)
        {
            try
            {
                window.Close();
            }
            catch { return; }
        }
    }
}

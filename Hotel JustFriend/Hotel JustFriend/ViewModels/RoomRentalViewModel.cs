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
    class RoomRentalViewModel : ViewModelBase
    {
        [Command]
        public void AddCustomer()
        {
            AddCustomerWindow window = new AddCustomerWindow();
            window.ShowDialog();
        }
    }
}

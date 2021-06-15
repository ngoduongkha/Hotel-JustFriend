using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    class CustomizeParametersViewModel : ViewModelBase
    {
        [Command]
        public void CallAddCustomerType()
        {
            AddCustomerTypeWindow window = new AddCustomerTypeWindow();
            window.ShowDialog();
        }
        [Command]
        public void CallAddRoomType()
        {
            AddCustomerTypeWindow window = new AddCustomerTypeWindow();
            window.ShowDialog();
        }
    }
}

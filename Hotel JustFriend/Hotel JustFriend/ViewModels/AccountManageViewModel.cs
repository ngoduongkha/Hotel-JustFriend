using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Hotel_JustFriend.ViewModels
{
    [POCOViewModel]
    class AccountManageViewModel : ViewModelBase
    {
        [Command]
        public void AddAccount(AccountManageView p)
        {
            try
            {
                p.gridMain.IsEnabled = false;
                p.gridDetail.IsEnabled = true;
            }
            catch { return; }
        }

        [Command]
        public void EditAccount(AccountManageView p)
        {
            try
            {
                p.gridMain.IsEnabled = false;
                p.gridDetail.IsEnabled = true;
            }
            catch { return; }
        }
    }
}

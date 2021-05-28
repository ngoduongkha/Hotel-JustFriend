using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Hotel_JustFriend.Views;

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

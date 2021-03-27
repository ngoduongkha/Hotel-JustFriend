using Hotel_JustFriend.ViewModels;
using System.Windows.Controls;

namespace Hotel_JustFriend.Views
{
    /// <summary>
    /// Interaction logic for AccountManageView.xaml
    /// </summary>
    public partial class AccountManageView : UserControl
    {
        public AccountManageView()
        {
            InitializeComponent();
            this.DataContext = new AccountManageViewModel();
        }
    }
}

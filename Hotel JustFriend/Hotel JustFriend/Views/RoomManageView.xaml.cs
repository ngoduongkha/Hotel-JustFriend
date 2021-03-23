using Hotel_JustFriend.ViewModels;
using System.Windows.Controls;

namespace Hotel_JustFriend.Views
{
    /// <summary>
    /// Interaction logic for UserControlRoomManage.xaml
    /// </summary>
    public partial class RoomManageView : UserControl
    {
        public RoomManageView()
        {
            InitializeComponent();
            this.DataContext = new RoomManageViewModel();
        }
    }
}

using Hotel_JustFriend.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Hotel_JustFriend.Views
{
    /// <summary>
    /// Interaction logic for AddRoomWindow.xaml
    /// </summary>
    public partial class RoomDetailView : Window
    {
        public RoomDetailView()
        {
            InitializeComponent();
            this.DataContext = new RoomDetailViewModel();
        }
    }
}

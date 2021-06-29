using Hotel_JustFriend.Models;
using Hotel_JustFriend.ViewModels;
using System.Windows;

namespace Hotel_JustFriend.Views
{
    /// <summary>
    /// Interaction logic for AddCustomerWindow.xaml
    /// </summary>
    public partial class AddCustomerWindow : Window
    {
        public AddCustomerWindow(Room selectedRoom)
        {
            InitializeComponent();
            DataContext = new AddCustomerViewModel(selectedRoom);
        }
    }
}

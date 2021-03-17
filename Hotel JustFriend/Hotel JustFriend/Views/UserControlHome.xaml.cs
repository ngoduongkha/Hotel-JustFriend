using Hotel_JustFriend.ViewModels;
using System.Windows.Controls;

namespace Hotel_JustFriend.Views
{
    /// <summary>
    /// Interaction logic for UserControlHome.xaml
    /// </summary>
    public partial class UserControlHome : UserControl
    {
        public UserControlHome()
        {
            InitializeComponent();
            this.DataContext = new BusinessViewModel(panel);
        }
    }
}

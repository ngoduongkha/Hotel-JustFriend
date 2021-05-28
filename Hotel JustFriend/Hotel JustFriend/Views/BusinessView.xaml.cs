using Hotel_JustFriend.Models;
using Hotel_JustFriend.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Hotel_JustFriend.Views
{
    /// <summary>
    /// Interaction logic for UserControlHome.xaml
    /// </summary>
    public partial class BusinessView : UserControl
    {
        public BusinessView()
        {
            InitializeComponent();
            this.DataContext = new BusinessViewModel();
        }

    }
}
